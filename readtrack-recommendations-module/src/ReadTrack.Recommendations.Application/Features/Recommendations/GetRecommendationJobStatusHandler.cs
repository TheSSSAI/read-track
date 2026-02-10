using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ReadTrack.Recommendations.Application.DTOs;
using ReadTrack.Recommendations.Application.Interfaces;
using ReadTrack.Shared.Results; // Assuming Shared Kernel

namespace ReadTrack.Recommendations.Application.Features.Recommendations
{
    /// <summary>
    /// Handles the query to retrieve the status and results of a recommendation job.
    /// This supports the client-side polling mechanism.
    /// </summary>
    public class GetRecommendationJobStatusHandler : IRequestHandler<GetRecommendationJobStatusQuery, Result<RecommendationJobStatusDto>>
    {
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly ILogger<GetRecommendationJobStatusHandler> _logger;

        public GetRecommendationJobStatusHandler(
            IRecommendationRepository recommendationRepository,
            ILogger<GetRecommendationJobStatusHandler> logger)
        {
            _recommendationRepository = recommendationRepository ?? throw new ArgumentNullException(nameof(recommendationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<RecommendationJobStatusDto>> Handle(GetRecommendationJobStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Fetch the Job
                var job = await _recommendationRepository.GetJobByIdAsync(request.JobId, cancellationToken);

                if (job == null)
                {
                    _logger.LogWarning("RecommendationJob {JobId} not found.", request.JobId);
                    return Result<RecommendationJobStatusDto>.NotFound($"Recommendation job with ID {request.JobId} was not found.");
                }

                // 2. Security Check (Optional but recommended)
                // Ensure the requesting user owns the job. 
                // Note: The Query usually contains the UserId from the claims for verification.
                // If the Query does not carry UserId, this check might be skipped or handled by middleware.
                // Assuming request.UserId exists for security validation.
                if (request.UserId != Guid.Empty && job.UserId != request.UserId)
                {
                    _logger.LogWarning("User {UserId} attempted to access Job {JobId} belonging to User {OwnerId}", request.UserId, job.Id, job.UserId);
                    return Result<RecommendationJobStatusDto>.Forbidden("You are not authorized to view this recommendation job.");
                }

                // 3. Map to DTO
                var dto = new RecommendationJobStatusDto
                {
                    JobId = job.Id,
                    Status = job.Status.ToString(),
                    FailureReason = job.FailureReason,
                    CreatedAt = job.CreatedAt,
                    UpdatedAt = job.UpdatedAt,
                    // Only populate results if completed successfully
                    Result = job.Status == Domain.Enums.RecommendationJobStatus.Completed && job.GeneratedRecommendations != null
                        ? job.GeneratedRecommendations.Select(r => new RecommendationDto
                        {
                            Id = r.Id,
                            Title = r.BookTitle,
                            Author = r.Author,
                            Description = r.Description,
                            Reasoning = r.Reasoning,
                            ConfidenceScore = r.ConfidenceScore,
                            ThumbnailUrl = r.ThumbnailUrl,
                            Isbn = r.Isbn
                        }).ToList()
                        : null
                };

                return Result<RecommendationJobStatusDto>.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving status for Job {JobId}", request.JobId);
                return Result<RecommendationJobStatusDto>.Failure("InternalError", "An error occurred while retrieving the job status.");
            }
        }
    }
}
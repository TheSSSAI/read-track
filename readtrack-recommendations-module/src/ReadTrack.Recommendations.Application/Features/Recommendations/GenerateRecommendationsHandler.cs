using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ReadTrack.Recommendations.Application.Interfaces;
using ReadTrack.Recommendations.Application.Services;
using ReadTrack.Recommendations.Domain.Entities;
using ReadTrack.Shared.Abstractions; // Assuming Shared Kernel
using ReadTrack.Shared.Results; // Assuming Shared Kernel

namespace ReadTrack.Recommendations.Application.Features.Recommendations
{
    /// <summary>
    /// Handles the request to generate book recommendations for a user.
    /// This handler initiates an asynchronous background job to process the recommendation logic.
    /// </summary>
    public class GenerateRecommendationsHandler : IRequestHandler<GenerateRecommendationsCommand, Result<Guid>>
    {
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IBackgroundJobService _backgroundJobService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GenerateRecommendationsHandler> _logger;

        public GenerateRecommendationsHandler(
            IRecommendationRepository recommendationRepository,
            IUserSubscriptionService userSubscriptionService,
            IBackgroundJobService backgroundJobService,
            IUnitOfWork unitOfWork,
            ILogger<GenerateRecommendationsHandler> logger)
        {
            _recommendationRepository = recommendationRepository ?? throw new ArgumentNullException(nameof(recommendationRepository));
            _userSubscriptionService = userSubscriptionService ?? throw new ArgumentNullException(nameof(userSubscriptionService));
            _backgroundJobService = backgroundJobService ?? throw new ArgumentNullException(nameof(backgroundJobService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<Guid>> Handle(GenerateRecommendationsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GenerateRecommendationsCommand for User {UserId}", request.UserId);

            try
            {
                // 1. Validation & Subscription Check (Rate Limiting)
                // We check the subscription status to enforce the 5 suggestions/month limit for Free users.
                var subscriptionStatus = await _userSubscriptionService.GetUserSubscriptionStatusAsync(request.UserId, cancellationToken);

                if (subscriptionStatus == null)
                {
                    _logger.LogWarning("Subscription status not found for User {UserId}", request.UserId);
                    return Result<Guid>.Failure("SubscriptionNotFound", "User subscription information could not be retrieved.");
                }

                // Assuming SubscriptionStatusDto has properties: Tier (Enum or String) and MonthlyAiSuggestionUsage
                if (IsLimitExceeded(subscriptionStatus))
                {
                    _logger.LogInformation("User {UserId} has exceeded their monthly AI suggestion limit.", request.UserId);
                    return Result<Guid>.Failure("LimitExceeded", "You have reached your monthly limit for AI suggestions. Please upgrade to Premium for unlimited access.");
                }

                // 2. Create the Job Entity
                // The job starts in the 'Pending' state.
                var recommendationJob = new RecommendationJob(request.UserId);

                // 3. Persist the Job
                await _recommendationRepository.AddJobAsync(recommendationJob, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Created RecommendationJob {JobId} for User {UserId}", recommendationJob.Id, request.UserId);

                // 4. Enqueue Background Processing
                // We use the background job service (e.g., Hangfire wrapper) to offload the heavy RAG pipeline.
                _backgroundJobService.Enqueue<IRecommendationProcessService>(
                    service => service.ProcessJobAsync(recommendationJob.Id, CancellationToken.None));

                _logger.LogInformation("Enqueued RecommendationJob {JobId} for processing", recommendationJob.Id);

                return Result<Guid>.Success(recommendationJob.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while handling GenerateRecommendationsCommand for User {UserId}", request.UserId);
                return Result<Guid>.Failure("InternalError", "An error occurred while initiating the recommendation process.");
            }
        }

        private bool IsLimitExceeded(SubscriptionStatusDto subscriptionStatus)
        {
            // Business Rule: Free Users are limited to 5 AI suggestions per month.
            // Premium Users have unlimited access.
            const int FreeTierLimit = 5;
            
            // Assuming "Free" is the identifier for the free tier. This should ideally be an Enum.
            bool isFreeTier = string.Equals(subscriptionStatus.Tier, "Free", StringComparison.OrdinalIgnoreCase);
            
            if (isFreeTier && subscriptionStatus.AiSuggestionCount >= FreeTierLimit)
            {
                return true;
            }

            return false;
        }
    }
}
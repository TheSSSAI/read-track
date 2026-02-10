using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReadTrack.Recommendations.Application.Interfaces;
using ReadTrack.Recommendations.Domain.Entities;
using ReadTrack.Recommendations.Domain.Enums;

namespace ReadTrack.Recommendations.Infrastructure.Persistence
{
    /// <summary>
    /// Entity Framework Core implementation of the recommendation repository.
    /// Handles persistence for RecommendationJobs and Recommendation entities.
    /// </summary>
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly RecommendationsDbContext _context;
        private readonly ILogger<RecommendationRepository> _logger;

        public RecommendationRepository(
            RecommendationsDbContext context,
            ILogger<RecommendationRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task<Guid> AddJobAsync(RecommendationJob job, CancellationToken cancellationToken)
        {
            if (job == null) throw new ArgumentNullException(nameof(job));

            try
            {
                await _context.RecommendationJobs.AddAsync(job, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Successfully added RecommendationJob {JobId}", job.Id);
                return job.Id;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to add RecommendationJob {JobId}", job.Id);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<RecommendationJob?> GetJobAsync(Guid jobId, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.RecommendationJobs
                    .Include(j => j.GeneratedRecommendations)
                    .FirstOrDefaultAsync(j => j.Id == jobId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving RecommendationJob {JobId}", jobId);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task UpdateJobAsync(RecommendationJob job, CancellationToken cancellationToken)
        {
            if (job == null) throw new ArgumentNullException(nameof(job));

            try
            {
                _context.RecommendationJobs.Update(job);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogDebug("Updated RecommendationJob {JobId} status to {Status}", job.Id, job.Status);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error updating RecommendationJob {JobId}", job.Id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating RecommendationJob {JobId}", job.Id);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task AddRecommendationsAsync(IEnumerable<Recommendation> recommendations, CancellationToken cancellationToken)
        {
            if (recommendations == null) throw new ArgumentNullException(nameof(recommendations));

            var recommendationList = recommendations.ToList();
            if (!recommendationList.Any()) return;

            try
            {
                await _context.Recommendations.AddRangeAsync(recommendationList, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Added {Count} recommendations", recommendationList.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error persisting batch of {Count} recommendations", recommendationList.Count);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<List<Recommendation>> GetRecommendationsByUserIdAsync(Guid userId, int limit, CancellationToken cancellationToken)
        {
            try
            {
                // Assuming retrieval of most recent completed recommendations
                return await _context.Recommendations
                    .AsNoTracking()
                    .Where(r => r.UserId == userId)
                    .OrderByDescending(r => r.GeneratedAt)
                    .Take(limit)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving recommendations for User {UserId}", userId);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<RecommendationJobStatus> GetJobStatusAsync(Guid jobId, CancellationToken cancellationToken)
        {
            try
            {
                var job = await _context.RecommendationJobs
                    .AsNoTracking()
                    .Select(j => new { j.Status })
                    .FirstOrDefaultAsync(j => j.Id == jobId, cancellationToken);

                if (job == null)
                {
                    _logger.LogWarning("Job status requested for non-existent Job {JobId}", jobId);
                    throw new KeyNotFoundException($"RecommendationJob with ID {jobId} not found.");
                }

                return job.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving status for Job {JobId}", jobId);
                throw;
            }
        }
    }
}
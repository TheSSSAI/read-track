using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReadTrack.Monetization.Domain.Entities;
using ReadTrack.Monetization.Domain.Interfaces;
using ReadTrack.Monetization.Domain.ValueObjects;

namespace ReadTrack.Monetization.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for managing Subscription aggregates.
/// Implements standard CRUD and domain-specific query operations using Entity Framework Core.
/// </summary>
public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly MonetizationDbContext _context;
    private readonly ILogger<SubscriptionRepository> _logger;

    public SubscriptionRepository(MonetizationDbContext context, ILogger<SubscriptionRepository> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<Subscription?> GetByIdAsync(Guid subscriptionId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Subscriptions
                .Include(s => s.Transactions)
                .FirstOrDefaultAsync(s => s.Id == subscriptionId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving subscription by ID: {SubscriptionId}", subscriptionId);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<Subscription?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        try
        {
            // We typically want the most relevant (active or latest) subscription for the user
            return await _context.Subscriptions
                .Include(s => s.Transactions)
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.ValidUntil)
                .FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving subscription by UserID: {UserId}", userId);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<Subscription?> GetByExternalIdAsync(string externalSubscriptionId, Provider provider, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.Subscriptions
                .Include(s => s.Transactions)
                .FirstOrDefaultAsync(s => s.ExternalSubscriptionId == externalSubscriptionId && s.Provider == provider, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving subscription by ExternalID: {ExternalId} Provider: {Provider}", externalSubscriptionId, provider);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task AddAsync(Subscription subscription, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Subscriptions.AddAsync(subscription, cancellationToken);
            _logger.LogInformation("Added new subscription for User: {UserId} with ExternalID: {ExternalId}", subscription.UserId, subscription.ExternalSubscriptionId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding subscription for User: {UserId}", subscription.UserId);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Subscriptions.Update(subscription);
            
            // Explicitly handling Entity State for tracking if detached
            if (_context.Entry(subscription).State == EntityState.Detached)
            {
                _context.Attach(subscription);
                _context.Entry(subscription).State = EntityState.Modified;
            }

            _logger.LogInformation("Updated subscription {SubscriptionId} for User: {UserId}", subscription.Id, subscription.UserId);
            await Task.CompletedTask; // EF Core Add/Update are synchronous until SaveChanges is called on UoW
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating subscription {SubscriptionId}", subscription.Id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<List<Subscription>> GetExpiredActiveSubscriptionsAsync(DateTimeOffset thresholdDate, int batchSize = 100, CancellationToken cancellationToken = default)
    {
        try
        {
            // Find subscriptions that are marked Active but have passed their ValidUntil date
            return await _context.Subscriptions
                .Where(s => s.Status == SubscriptionStatus.Active && s.ValidUntil <= thresholdDate)
                .OrderBy(s => s.ValidUntil)
                .Take(batchSize)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving expired subscriptions. Threshold: {Threshold}", thresholdDate);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByExternalTransactionIdAsync(string externalTransactionId, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if any transaction record exists with this ID to support idempotency
            return await _context.PaymentTransactions
                .AnyAsync(t => t.ExternalTransactionId == externalTransactionId, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking existence of transaction ID: {TransactionId}", externalTransactionId);
            throw;
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReadTrack.Users.Application.Common.Exceptions;
using ReadTrack.Users.Application.Interfaces;
using ReadTrack.Users.Domain.Events;

namespace ReadTrack.Users.Application.Features.Users.Commands
{
    /// <summary>
    /// Handles the initiation of the account deletion process in compliance with GDPR Right to Erasure.
    /// This handler marks the user for deletion and queues the asynchronous cleanup job.
    /// </summary>
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, bool>
    {
        private readonly IUsersDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IBackgroundJobService _backgroundJobService;
        private readonly IPublisher _publisher;
        private readonly ILogger<DeleteAccountCommandHandler> _logger;

        public DeleteAccountCommandHandler(
            IUsersDbContext dbContext,
            ICurrentUserService currentUserService,
            IBackgroundJobService backgroundJobService,
            IPublisher publisher,
            ILogger<DeleteAccountCommandHandler> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _backgroundJobService = backgroundJobService ?? throw new ArgumentNullException(nameof(backgroundJobService));
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var requestingUserId = _currentUserService.UserId;

            // 1. Security Check: Ensure the user is deleting their own account
            if (request.UserId != requestingUserId && !_currentUserService.IsAdmin)
            {
                _logger.LogWarning("Unauthorized deletion attempt. Requesting User: {RequestingUser}, Target User: {TargetUser}", requestingUserId, request.UserId);
                throw new ForbiddenAccessException("You are not authorized to delete this account.");
            }

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning("Delete account failed: User {UserId} not found", request.UserId);
                throw new NotFoundException(nameof(Domain.Entities.User), request.UserId);
            }

            _logger.LogInformation("Initiating account deletion for User: {UserId}", user.Id);

            // 2. Domain Logic: Mark user as pending deletion or logically deleted
            // This prevents further logins immediately while the heavy cleanup happens asynchronously.
            user.InitiateDeletion();
            
            // 3. Publish Domain Event (In-Process)
            // This notifies other modules (Reading, Engagement) to begin their own cleanup routines
            // or marks the user as inactive in their local caches.
            await _publisher.Publish(new UserAccountDeletedEvent(user.Id), cancellationToken);

            // 4. Save Changes (User state update)
            await _dbContext.SaveChangesAsync(cancellationToken);

            // 5. Enqueue Asynchronous Hard Delete Job
            // This job will handle PII scrubbing, external auth provider removal, and log anonymization.
            // We use an expression to type-safe queue the job.
            _backgroundJobService.Enqueue<IAccountCleanupJob>(job => job.ProcessHardDeleteAsync(user.Id));

            _logger.LogInformation("Account deletion queued successfully for User: {UserId}", user.Id);

            return true;
        }
    }
}
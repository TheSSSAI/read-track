using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadTrack.Users.Domain.Entities
{
    /// <summary>
    /// Represents the User subscription tier roles.
    /// Defined here to ensure strong typing within the Domain boundary.
    /// </summary>
    public enum UserRole
    {
        Free = 0,
        Premium = 1,
        Administrator = 99
    }

    /// <summary>
    /// Represents a registered user within the system.
    /// Acts as the Aggregate Root for the User Identity context.
    /// Responsible for encapsulating PII and enforcing identity-related business rules.
    /// </summary>
    public class User
    {
        // Core Identity Properties
        public Guid Id { get; private set; }
        
        [Required]
        [MaxLength(100)]
        public string Auth0Id { get; private set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; private set; }

        // Profile Properties
        [Required]
        [MaxLength(100)]
        public string DisplayName { get; private set; }

        [MaxLength(500)]
        public string? PhotoUrl { get; private set; }

        // State & Authorization
        public UserRole Role { get; private set; }
        public bool IsActive { get; private set; }

        // Audit Trails
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Domain Events collection to be dispatched by infrastructure
        private readonly List<object> _domainEvents = new();
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        // Navigation properties (for EF Core configuration)
        private readonly List<DataExportJob> _dataExportJobs = new();
        public virtual IReadOnlyCollection<DataExportJob> DataExportJobs => _dataExportJobs.AsReadOnly();

        /// <summary>
        /// EF Core constructor
        /// </summary>
        protected User() { }

        /// <summary>
        /// Private constructor to enforce factory method usage.
        /// </summary>
        private User(string auth0Id, string email, string displayName, string? photoUrl)
        {
            Id = Guid.NewGuid();
            Auth0Id = auth0Id ?? throw new ArgumentNullException(nameof(auth0Id));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
            PhotoUrl = photoUrl;
            Role = UserRole.Free; // Default to Free tier as per REQ-REG-001
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Factory method to create a new user during registration.
        /// </summary>
        /// <param name="auth0Id">The unique identifier from the identity provider.</param>
        /// <param name="email">The user's email address.</param>
        /// <param name="displayName">The user's display name.</param>
        /// <param name="photoUrl">Optional URL to the user's profile picture.</param>
        /// <returns>A new valid User instance.</returns>
        public static User Create(string auth0Id, string email, string displayName, string? photoUrl)
        {
            if (string.IsNullOrWhiteSpace(auth0Id)) throw new ArgumentException("Auth0Id cannot be empty.", nameof(auth0Id));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty.", nameof(email));
            if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("DisplayName cannot be empty.", nameof(displayName));

            var user = new User(auth0Id, email, displayName, photoUrl);
            
            // In a full implementation, we would add a UserCreatedEvent here
            // user.AddDomainEvent(new UserCreatedEvent(user.Id));
            
            return user;
        }

        /// <summary>
        /// Updates the user's profile information.
        /// </summary>
        public void UpdateProfile(string displayName, string? photoUrl)
        {
            if (string.IsNullOrWhiteSpace(displayName)) 
                throw new ArgumentException("DisplayName cannot be empty.", nameof(displayName));

            DisplayName = displayName;
            PhotoUrl = photoUrl;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Records a successful login event.
        /// </summary>
        public void RecordLogin()
        {
            LastLoginAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates the user's subscription role.
        /// </summary>
        /// <param name="newRole">The new role to assign.</param>
        public void UpdateRole(UserRole newRole)
        {
            if (Role == newRole) return;

            Role = newRole;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Deactivates the user account (Soft Delete logic).
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Adds a domain event to the entity.
        /// </summary>
        protected void AddDomainEvent(object domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Clears domain events after they have been dispatched.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
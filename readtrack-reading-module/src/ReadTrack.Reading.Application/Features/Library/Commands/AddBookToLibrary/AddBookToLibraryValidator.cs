using FluentValidation;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Application.Features.Library.Commands.AddBookToLibrary
{
    /// <summary>
    /// Validates the AddBookToLibraryCommand to ensure all required data is present and valid before processing.
    /// </summary>
    public class AddBookToLibraryValidator : AbstractValidator<AddBookToLibraryCommand>
    {
        public AddBookToLibraryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User ID is required to add a book to the library.");

            RuleFor(x => x.GoogleBookId)
                .NotEmpty()
                .WithMessage("Google Book ID is required to identify the book.")
                .MaximumLength(50)
                .WithMessage("Google Book ID cannot exceed 50 characters.");

            RuleFor(x => x.InitialShelf)
                .IsInEnum()
                .WithMessage("A valid initial shelf status must be selected.");
        }
    }
}
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ReadTrack.Users.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for cloud file storage operations (e.g., AWS S3).
    /// Used primarily for storing and retrieving GDPR data export files.
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// Uploads a file stream to the storage provider.
        /// </summary>
        /// <param name="fileStream">The stream containing the file data.</param>
        /// <param name="fileName">The unique name/key for the file.</param>
        /// <param name="contentType">The MIME type of the file (e.g., "application/json").</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The unique key or identifier of the uploaded file.</returns>
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken);

        /// <summary>
        /// Generates a secure, temporary URL to access a stored file.
        /// </summary>
        /// <param name="key">The unique key of the file.</param>
        /// <param name="expiration">The duration for which the URL should remain valid.</param>
        /// <returns>A signed URL string.</returns>
        string GetPresignedUrl(string key, TimeSpan expiration);

        /// <summary>
        /// Deletes a file from storage.
        /// </summary>
        /// <param name="key">The unique key of the file to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task DeleteFileAsync(string key, CancellationToken cancellationToken);
    }
}
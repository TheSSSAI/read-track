using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReadTrack.Users.Application.Interfaces;

namespace ReadTrack.Users.Infrastructure.Services
{
    /// <summary>
    /// Implementation of IFileStorageService using Amazon S3.
    /// </summary>
    public class S3FileStorageService : IFileStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly ILogger<S3FileStorageService> _logger;
        private readonly string _bucketName;

        public S3FileStorageService(
            IAmazonS3 s3Client, 
            IConfiguration configuration,
            ILogger<S3FileStorageService> logger)
        {
            _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            _bucketName = configuration["AWS:S3:BucketName"] 
                ?? throw new InvalidOperationException("AWS S3 BucketName configuration is missing.");
        }

        /// <inheritdoc />
        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken)
        {
            if (fileStream == null || !fileStream.CanRead)
            {
                throw new ArgumentException("File stream must be readable.", nameof(fileStream));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("File name cannot be empty.", nameof(fileName));
            }

            var objectKey = $"exports/{Guid.NewGuid()}/{fileName}";

            try
            {
                var transferUtility = new TransferUtility(_s3Client);
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = fileStream,
                    Key = objectKey,
                    BucketName = _bucketName,
                    CannedACL = S3CannedACL.Private, // Ensure file is private by default
                    AutoCloseStream = false
                };

                _logger.LogInformation("Starting file upload to S3. Bucket: {Bucket}, Key: {Key}", _bucketName, objectKey);

                await transferUtility.UploadAsync(uploadRequest, cancellationToken);

                _logger.LogInformation("File upload completed successfully. Key: {Key}", objectKey);

                return objectKey;
            }
            catch (AmazonS3Exception ex)
            {
                _logger.LogError(ex, "AWS S3 error during file upload. Bucket: {Bucket}, Key: {Key}", _bucketName, objectKey);
                throw new InvalidOperationException($"Failed to upload file to S3: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during file upload to S3.");
                throw;
            }
        }

        /// <summary>
        /// Generates a presigned URL for downloading a file securely.
        /// </summary>
        public string GetPresignedUrl(string objectKey, TimeSpan expiry)
        {
            if (string.IsNullOrWhiteSpace(objectKey))
            {
                throw new ArgumentException("Object key cannot be empty.", nameof(objectKey));
            }

            try
            {
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = _bucketName,
                    Key = objectKey,
                    Expires = DateTime.UtcNow.Add(expiry),
                    Protocol = Protocol.HTTPS
                };

                string url = _s3Client.GetPreSignedURL(request);
                return url;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating presigned URL for key: {Key}", objectKey);
                throw new InvalidOperationException("Failed to generate download link.", ex);
            }
        }
    }
}
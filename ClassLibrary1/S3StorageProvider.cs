using Amazon.S3;
using Amazon.S3.Util;
using System;
using System.Threading.Tasks;

namespace S3Storage
{
    public class S3StorageProvider
    {
        private readonly IAmazonS3 _s3Client;

        public S3StorageProvider(IAmazonS3 s3Client)
        {
            this._s3Client = s3Client;
        }
        

        public async Task<bool> CreateBucket(string bucketName)
        {
            bool doesBucktExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (doesBucktExists)
                return true;

            var resut = await _s3Client.PutBucketAsync(bucketName);

            return true;
        }

        public async Task DeleteBucketAsync(string bucketName)
        {
            await DeleteAllBucketObject(bucketName);
            await _s3Client.DeleteBucketAsync(bucketName);
        }

        public async Task DeleteAllBucketObject(string bucketName)
        {
            var keys = await _s3Client.GetAllObjectKeysAsync(bucketName, string.Empty, null);

            foreach (var key in keys)
            {
                await _s3Client.DeleteObjectAsync(bucketName, key);
            }

        }


    }
}

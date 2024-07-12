using Application.Common.Interface;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Application.DTOs.Internal;
using Domain.Shared;
using System.Net;
using Error = Domain.Shared.Error;
using Domain.Constants;

namespace Infrastructure.Services.CloudinaryUpload
{
    public class Media : IMedia
    {
        private record CloudinarySettings(string CloudName, string ApiKey, string ApiSecret);
        private readonly CloudinarySettings _settings;
        public IConfiguration Configuration;
        private readonly Cloudinary _cloudinary;
        public Media(IConfiguration configuration)
        {
            Configuration = configuration;
            _settings = Configuration.GetSection("CloudDinarySettings").Get<CloudinarySettings>() ?? throw new ArgumentNullException("Not Found Config Cloudinary");
            Account account = new Account(_settings.CloudName, _settings.ApiKey, _settings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }
        public async Task<Result<bool>> DeleteImageAsync(string id, CancellationToken cancellationToken = default)
        {
            var param = new DeletionParams(id);
            var result = await _cloudinary.DestroyAsync(param);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return Result<bool>.ResultSuccess(true);
            }
            return Result<bool>.ResultFailures(new Error("ImageDelete", result.Error.Message));
        }
        public async Task<Result<ImageUpload>> UploadLoadImageAsync(IFormFile file, string folder, CancellationToken cancellationToken = default)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        Folder = folder,
                        File = new FileDescription(Guid.NewGuid().ToString(), stream)
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            if (uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return Result<ImageUpload>.ResultSuccess(new ImageUpload(uploadResult.PublicId, uploadResult.SecureUrl.ToString()));
            }
            return Result<ImageUpload>.ResultFailures(ErrorConstants.UploadImageOccursErrorWithFileName(file.FileName));
        }

        public Task<Result<IEnumerable<ImageUpload>>> UploadLoadImagesAsync(IFormFileCollection file, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

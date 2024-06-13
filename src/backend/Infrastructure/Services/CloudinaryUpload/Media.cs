using Application.Common.Interface;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Internal;

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
        public Task<bool> DeleteImageAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<ImageUpload> UploadLoadImageAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(Guid.NewGuid().ToString(), stream)
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            return await Task.FromResult(new ImageUpload(uploadResult.PublicId, uploadResult.Url.ToString()));
        }

        public Task<IEnumerable<ImageUpload>> UploadLoadImagesAsync(IFormFileCollection file, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

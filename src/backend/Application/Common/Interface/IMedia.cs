using Application.DTOs.Internal;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interface
{
    public interface IMedia
    {
        Task<ImageUpload> UploadLoadImageAsync(IFormFile file, CancellationToken cancellationToken = default);
        Task<IEnumerable<ImageUpload>> UploadLoadImagesAsync(IFormFileCollection file, CancellationToken cancellationToken = default);
        Task<bool> DeleteImageAsync(string id, CancellationToken cancellationToken = default);
    }
}

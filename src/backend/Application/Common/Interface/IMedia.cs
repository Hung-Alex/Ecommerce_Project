using Application.DTOs.Internal;
using Domain.Shared;
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
        Task<Result<ImageUpload>> UploadLoadImageAsync(IFormFile file, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<ImageUpload>>> UploadLoadImagesAsync(IFormFileCollection file, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteImageAsync(string id, CancellationToken cancellationToken = default);
    }
}

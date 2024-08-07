﻿using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Posts.Commands.UpdatePost
{
    public record UpdatePostCommand(Guid Id, string Title, string ShortDescription, string Description, string UrlSlug, bool Published, IFormFile? Image) : IRequest<Result<bool>>, IValidatableRequest;
}

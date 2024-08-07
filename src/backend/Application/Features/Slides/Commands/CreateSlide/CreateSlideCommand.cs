﻿using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Slides.Commands.CreateSlide
{
    public record CreateSlideCommand(string Title, string Description, bool IsActive,IFormFile Image) : IRequest<Result<bool>>, IValidatableRequest;
}

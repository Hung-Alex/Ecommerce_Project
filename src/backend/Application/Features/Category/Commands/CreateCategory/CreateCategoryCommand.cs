﻿using Domain.Behavior;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Category.Commands.CreateCategory
{
    public record CreateCategoryCommand(string Name, string Description, string UrlSlug, IFormFile FormFile) : IRequest, IValidatableRequest;
}
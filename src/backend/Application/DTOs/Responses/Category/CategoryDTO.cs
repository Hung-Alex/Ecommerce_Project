﻿namespace Application.DTOs.Responses.Category
{
    public record CategoryDTO(string Name, string Description, string UrlSlug, string Image) : BaseDTO();
}
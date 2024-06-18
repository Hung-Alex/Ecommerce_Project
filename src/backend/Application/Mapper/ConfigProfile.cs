using Application.DTOs.Responses.Brand;
using AutoMapper;
using Domain.Entities.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class ConfigProfile:Profile
    {
        public ConfigProfile() 
        {
            CreateMap<Brand,BrandDTOs>().ReverseMap();
        }  
    }
}

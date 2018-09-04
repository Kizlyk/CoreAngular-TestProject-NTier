using AutoMapper;
using ProductDAL.Models;
using ProductServices.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductServices.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ReverseMap()
                .ForMember(p => p.Timestamp, p => p.Ignore());
        }
    }
}

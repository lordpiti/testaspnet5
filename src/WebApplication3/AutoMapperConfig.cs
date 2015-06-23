using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<Book, GenericBook>()
                .ForMember(dest => dest.Id,
                           opts => opts.MapFrom(src => src.Id.ToString()));

            AutoMapper.Mapper.CreateMap<GenericBook, Book>()
                .ForMember(dest => dest.Id,
                           opts => opts.MapFrom(src => src.Id));

            AutoMapper.Mapper.CreateMap<Libro, GenericBook>()
                .ForMember(dest => dest.Id,
                           opts => opts.MapFrom(src => src.Id.ToString()));
        }
    }
}

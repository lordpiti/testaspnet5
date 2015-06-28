using DAL.Models;
using MongoDB.Bson;
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
                           opts => opts.MapFrom(src => src.Id!=null?new ObjectId(src.Id):new ObjectId()));

            AutoMapper.Mapper.CreateMap<Libro, GenericBook>()
                .ForMember(dest => dest.Id,
                           opts => opts.MapFrom(src => src.Id.ToString()));
            AutoMapper.Mapper.CreateMap<GenericBook, Libro>()
                .ForMember(dest => dest.Id,
                           opts => opts.MapFrom(src => Convert.ToInt32(src.Id)));
        }
    }
}

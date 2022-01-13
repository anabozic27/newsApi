using AutoMapper;
using News.BL.Interfaces.Mapping;
using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BL.Models.Response
{
    public class CategoryViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
        public bool Active { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName,
                opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.ParentCategoryId,
                opts => opts.MapFrom(src => src.ParentCategoryId))
                .ForMember(dest => dest.ParentCategoryName,
                opts => opts.MapFrom(src => src.ParentCategory.Name))
                .ForMember(dest => dest.Active,
                opts => opts.MapFrom(src => src.Active));
        }
    }
}

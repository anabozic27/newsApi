using AutoMapper;
using News.BL.Interfaces.Mapping;
using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BL.Models.Response
{
    public class ArticleViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CreateByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedByUserId { get; set; }
        public string ModifiedByUserName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Article, ArticleViewModel>()
                .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title,
                opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.Text,
                opts => opts.MapFrom(src => src.Text))
                .ForMember(dest => dest.CategoryId,
                opts => opts.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CategoryName,
                opts => opts.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.CreateByUserId,
                opts => opts.MapFrom(src => src.CreateByUserId))
                .ForMember(dest => dest.CreatedByUserName,
                opts => opts.MapFrom(src => src.CreateByUser.FirstName + " " + src.CreateByUser.LastName))
                .ForMember(dest => dest.CreatedDate,
                opts => opts.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.ModifiedByUserId,
                opts => opts.MapFrom(src => src.ModifiedByUserId))
                .ForMember(dest => dest.ModifiedByUserName,
                opts => opts.MapFrom(src => src.ModifiedByUser.FirstName + " " + src.ModifiedByUser.LastName))
                .ForMember(dest => dest.ModifiedDate,
                opts => opts.MapFrom(src => src.ModifiedDate))
                .ForMember(dest => dest.Active,
                opts => opts.MapFrom(src => src.Active));
        }
    }
}

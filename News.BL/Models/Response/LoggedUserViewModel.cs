using AutoMapper;
using News.BL.Interfaces.Mapping;
using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BL.Models.Response
{
    public class LoggedUserViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ArticleCategoryId { get; set; }
        public string ArticleCategoryName { get; set; }
        public string Token { get; set; }
        public bool Active { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<User, LoggedUserViewModel>()
                .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username,
                opts => opts.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password,
                opts => opts.MapFrom(src => src.Password))
                .ForMember(dest => dest.FirstName,
                opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,
                opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.ArticleCategoryId,
                opts => opts.MapFrom(src => src.ArticleCategoryId))
                .ForMember(dest => dest.ArticleCategoryName,
                opts => opts.MapFrom(src => src.ArticleCategory.Name))
                .ForMember(dest => dest.Token,
                opts => opts.MapFrom(src => string.Empty))
                .ForMember(dest => dest.Active,
                opts => opts.MapFrom(src => src.Active));
        }
    }
}

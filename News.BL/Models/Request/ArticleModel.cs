using AutoMapper;
using FluentValidation;
using News.BL.Interfaces.Mapping;
using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BL.Models.Request
{
    public class ArticleModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public int CreateByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ArticleModel, Article>()
                .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title,
                opts => opts.MapFrom(src => src.Title))
                .ForMember(dest => dest.Text,
                opts => opts.MapFrom(src => src.Text))
                .ForMember(dest => dest.CategoryId,
                opts => opts.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.CreateByUserId,
                opts => opts.MapFrom(src => src.CreateByUserId))
                .ForMember(dest => dest.CreatedDate,
                opts => opts.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.ModifiedByUserId,
                opts => opts.MapFrom(src => src.ModifiedByUserId))
                .ForMember(dest => dest.ModifiedDate,
                opts => opts.MapFrom(src => src.ModifiedDate))
                .ForMember(dest => dest.Active,
                opts => opts.MapFrom(src => src.Active));
        }
    }

    public class ArticleModelValidator : AbstractValidator<ArticleModel>
    {
        public ArticleModelValidator()
        {
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
        }
    }
}

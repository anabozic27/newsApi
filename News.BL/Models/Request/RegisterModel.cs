using AutoMapper;
using FluentValidation;
using News.BL.Interfaces.Mapping;
using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BL.Models.Request
{
    public class RegisterModel : IHaveCustomMapping
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int? ArticleCategoryId { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<RegisterModel, User>()
                .ForMember(dest => dest.Username,
                opts => opts.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password,
                opts => opts.MapFrom(src => src.Password))
                .ForMember(dest => dest.FirstName,
                opts => opts.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,
                opts => opts.MapFrom(src => src.LastName))
                .ForMember(dest => dest.ArticleCategoryId,
                opts => opts.MapFrom(src => src.ArticleCategoryId));
        }
    }

    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MinimumLength(4).MaximumLength(15);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}

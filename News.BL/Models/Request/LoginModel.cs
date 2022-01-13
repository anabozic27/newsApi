using AutoMapper;
using FluentValidation;
using News.BL.Interfaces.Mapping;
using News.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace News.BL.Models.Request
{
    public class LoginModel : IHaveCustomMapping
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<LoginModel, User>()
                .ForMember(dest => dest.Username,
                opts => opts.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password,
                opts => opts.MapFrom(src => src.Password));
        }
    }

    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

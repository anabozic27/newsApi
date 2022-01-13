using AutoMapper;
using FluentValidation;
using News.BL.Interfaces.Mapping;
using News.Models.Entities;

namespace News.BL.Models.Request
{
    public class CategoryModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryId { get; set; }
        public bool Active { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CategoryModel, Category>()
                .ForMember(dest => dest.Id,
                opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                opts => opts.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.ParentCategoryId,
                opts => opts.MapFrom(src => src.ParentCategoryId))
                .ForMember(dest => dest.Active,
                opts => opts.MapFrom(src => src.Active));
        }
    }

    public class CategoryModelValidator : AbstractValidator<CategoryModel>
    {
        public CategoryModelValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty();
        }
    }
}

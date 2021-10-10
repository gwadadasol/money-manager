
using AutoMapper;
using CategoryService.Domains.Dtos;
using CategoryService.Domains.Model;

namespace CategoryService.Domains.Profiles
{

    public class CategoryServiceProfile: Profile
    {
        public CategoryServiceProfile()
        {
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryDto, CategoryEntity>();

        }


    }
}
using AutoMapper;
using Yummi.Core.DTOs;

namespace Yummi.Application.Mappers
{
    public class RecipeMap:Profile
    {
        public RecipeMap()
        {
            CreateMap<Core.Entities.Recipe, RecipeDto>();
            CreateMap<RecipeDto, Core.Entities.Recipe>();
        }
    }
}

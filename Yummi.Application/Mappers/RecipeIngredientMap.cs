using AutoMapper;
using Yummi.Application.DTOs;

namespace Yummi.Application.Mappers
{
    public class RecipeIngredientMap:Profile
    {
        public RecipeIngredientMap()
        {
            CreateMap<Core.Entities.RecipeIngredient, RecipeIngredientDto>();
            CreateMap<RecipeIngredientDto, Core.Entities.RecipeIngredient>();
        }
    }
}

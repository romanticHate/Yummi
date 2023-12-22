using AutoMapper;
using Yummi.Core.DTOs;

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

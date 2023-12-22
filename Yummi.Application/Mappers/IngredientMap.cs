using AutoMapper;
using Yummi.Core.DTOs;

namespace Yummi.Application.Mappers
{
    public class IngredientMap:Profile
    {
        public IngredientMap()
        {
            CreateMap<Core.Entities.Ingredient, IngredientDto>();
            CreateMap<IngredientDto, Core.Entities.Ingredient>();
        }
    }
}

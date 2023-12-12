using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

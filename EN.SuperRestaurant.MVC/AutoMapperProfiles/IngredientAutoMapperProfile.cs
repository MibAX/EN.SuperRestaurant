using AutoMapper;
using EN.SuperRestaurant.Entities.Ingredients;
using EN.SuperRestaurant.MVC.Models.Ingredients;

namespace EN.SuperRestaurant.MVC.AutoMapperProfiles
{
    public class IngredientAutoMapperProfile : Profile
    {
        public IngredientAutoMapperProfile()
        {
            CreateMap<Ingredient, IngredientViewModel>();
            CreateMap<Ingredient, IngredientDetailsViewModel>();
            CreateMap<CreateUpdateIngredientViewModel, Ingredient>().ReverseMap();
        }
    }
}

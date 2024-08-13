using AutoMapper;
using EN.SuperRestaurant.Entities.Meals;
using EN.SuperRestaurant.MVC.Models.Meals;

namespace EN.SuperRestaurant.MVC.AutoMapperProfiles
{
    public class MealAutoMapperProfile : Profile
    {
        public MealAutoMapperProfile()
        {
            CreateMap<Meal, MealViewModel>();
            CreateMap<Meal, MealDetailsViewModel>();
            
            CreateMap<CreateUpdateMealViewModel, Meal>();

            CreateMap<Meal, CreateUpdateMealViewModel>()
                .ForMember(createUpdateMealViewModel => createUpdateMealViewModel.IngredientIds,
                    opts =>
                        opts.MapFrom(meal => meal.Ingredients.Select(ingredient => ingredient.Id))
                );
                
        }
    }
}

using AutoMapper;
using EN.SuperRestaurant.Entities.Customers;
using EN.SuperRestaurant.MVC.Models.Customers;

namespace EN.SuperRestaurant.MVC.AutoMapperProfiles
{
    public class CustomerAutoMapperProfile : Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Customer, CustomerDetailsViewModel>();

            CreateMap<CreateUpdateCustomerViewModel, Customer>().ReverseMap();

        }
    }
}

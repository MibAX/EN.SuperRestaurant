﻿using AutoMapper;
using EN.SuperRestaurant.Entities.Orders;
using EN.SuperRestaurant.MVC.Models.Orders;

namespace EN.SuperRestaurant.MVC.AutoMapperProfiles
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {
            CreateMap<Order, OrderViewModel>();
            CreateMap<Order, OrderDetailsViewModel>();

            CreateMap<CreateUpdateOrderViewModel, Order>();

            CreateMap<Order, CreateUpdateOrderViewModel>();
        }
    }
}

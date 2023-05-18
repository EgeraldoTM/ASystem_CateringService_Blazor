using AutoMapper;
using CateringApi.DAL.Models;
using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;

namespace CateringApi.Helpers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// Domain to Dto
			CreateMap<FoodItem, FoodItemDto>();
			CreateMap<Category, CategoryDto>();
			CreateMap<Menu, MenuDto>();
			CreateMap<Order, OrderDto>();
			CreateMap<OrderDetail, OrderDetailDto>();

			//Dto to Domain
			CreateMap<FoodItemDto, FoodItem>()
				.ForMember(f => f.Id, opt => opt.Ignore())
				.ForMember(f => f.Category, opt => opt.Ignore());

			CreateMap<MenuDto, Menu>();
			CreateMap<MenuRequest, Menu>()
				.ForMember(m => m.FoodItems, opt => opt.Ignore());

			CreateMap<CreateOrderRequest, Order>();
			CreateMap<UpdateOrderRequest, Order>()
				.ForMember(o => o.Details, opt => opt.Ignore())
				.AfterMap((dto, o) =>
				{
					var foodItemIds = o.Details.Select(d => d.FoodItemId);
					//var foodItemIdsFromDto = dto.Details.Select(d => d.FoodItemId).ToList();

					//var removedDetails = o.Details.Where(d => !foodItemIdsFromDto.Contains(d.FoodItemId)).ToList();
					//foreach (var item in removedDetails)
					//	o.Details.Remove(item);

					var addedDetails = dto.Details.Where(d => !foodItemIds.Contains(d.FoodItemId)).ToList();
					foreach(var item in addedDetails)
						o.Details.Add(new OrderDetail { FoodItemId = item.FoodItemId, Quantity = item.Quantity, Price = item.Price });

				});

			CreateMap<OrderDetailRequest, OrderDetail>();

			CreateMap<NewUserDto, User>()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
		}
	}
}

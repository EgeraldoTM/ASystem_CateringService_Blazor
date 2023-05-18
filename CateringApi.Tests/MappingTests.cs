using AutoMapper;
using CateringApi.DAL.Models;
using CateringApi.Helpers;
using CateringApi.Helpers.Common.Requests;

namespace CateringApi.Tests
{
	public class MappingTests
	{
		private IMapper _mapper;
		private UpdateOrderRequest _updateOrderDto;
		private Order _order;

		[SetUp]
		public void Setup()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(typeof(MappingProfile));
			});
			_mapper = config.CreateMapper();

			_updateOrderDto = new UpdateOrderRequest
			{
				Details = new List<OrderDetailRequest>
				{
					new OrderDetailRequest{ FoodItemId = 1, Quantity = 1, Price = 1},
					new OrderDetailRequest{ FoodItemId = 2, Quantity = 1, Price = 1},
					new OrderDetailRequest{ FoodItemId = 3, Quantity = 1, Price = 1}
				}
			};

			_order = new Order
			{
				Details = new List<OrderDetail>
				{
					new OrderDetail{ FoodItemId = 2, Quantity = 1, Price = 1},
					new OrderDetail{ FoodItemId = 4, Quantity = 1, Price = 1}
				}
			};
		}

		[Test]
		public void UpdateOrderDto_ShouldAddOrRemoveCorrespondingDetails()
		{
			var order = _mapper.Map(_updateOrderDto, _order);

			var destinationDetails = order.Details.ToList();

			Assert.That(destinationDetails, Has.Count.EqualTo(4));
		}
	}
}

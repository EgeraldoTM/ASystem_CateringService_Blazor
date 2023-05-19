using AutoMapper;
using CateringApi.BLL.Repositories.Interfaces;
using CateringApi.BLL.Services.Interfaces;
using CateringApi.DAL.Models;
using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;
using CateringApi.Helpers.Exceptions;

namespace CateringApi.BLL.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public OrderService(IOrderRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<OrderDto?> Get(string employeeId, DateTime? date = null)
		{
			var order = await _repository.GetFull(employeeId, date);

			if (order is not null)
			{
				var orderDto = _mapper.Map<OrderDto>(order);
				return orderDto;
			}

			return null;
		}

		public async Task Create(CreateOrderRequest request)
		{
			var newOrder = _mapper.Map<CreateOrderRequest, Order>(request);

			_repository.Add(newOrder);

			await _unitOfWork.CompleteAsync();
		}

		public async Task Update(int id, UpdateOrderRequest request)
		{
			var order = await _repository.GetWithDetails(id) ?? throw new NotFoundException("Invalid Order Id.");

			_mapper.Map(request, order);

			await _unitOfWork.CompleteAsync();
		}

		public async Task<bool> AddQuantity(int id, int detailId)
		{
			var order = await _repository.GetWithDetails(id) ?? throw new NotFoundException("Invalid Order Id");

			var detail = order.Details.FirstOrDefault(d => d.Id == detailId);

			if (detail is not null)
			{
				detail.Quantity++;
				await _unitOfWork.CompleteAsync();
				return true;
			}
			return false;
		}

		public async Task<bool> SubtractQuantity(int id, int detailId)
		{
			var order = await _repository.GetWithDetails(id) ?? throw new NotFoundException("Invalid Order Id");

			var detail = order.Details.FirstOrDefault(d => d.Id == detailId);

			if (detail is not null && detail.Quantity > 1)
			{
				detail.Quantity--;
				await _unitOfWork.CompleteAsync();
				return true;
			}

			return false;
		}

		public async Task<bool> RemoveDetail(int id, int detailId)
		{
			var order = await _repository.GetWithDetails(id) ?? throw new NotFoundException("Invalid Order Id");

			var detail = order.Details.FirstOrDefault(d => d.Id == detailId);

			if (detail is not null)
			{
				order.Details.Remove(detail);
				await _unitOfWork.CompleteAsync();
				return true;
			}

			return false;
		}

		public async Task Delete(int id)
		{
			var order = await _repository.GetAsync(id) ?? throw new NotFoundException("Invalid Order Id");

			order.IsDeleted = true;
			await _unitOfWork.CompleteAsync();
		}
	}
}

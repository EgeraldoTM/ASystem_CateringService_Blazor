using AutoMapper;
using CateringApi.BLL.Repositories.Interfaces;
using CateringApi.BLL.Services.Interfaces;
using CateringApi.DAL.Models;
using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Exceptions;

namespace CateringApi.BLL.Services
{
	public class FoodItemService : IFoodItemService
	{
		private readonly IFoodItemRepository _repository;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public FoodItemService(IFoodItemRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<FoodItemDto>> GetAll(string? query)
		{
			var foodItems = await _repository.GetAllWithCategoryAsync(query);
			var foodItemDtos = _mapper.Map<IEnumerable<FoodItemDto>>(foodItems);

			return foodItemDtos;
		}

		public async Task<FoodItemDto> Get(int id)
		{
			var foodItem = await _repository.GetWithCategoryAsync(id) ?? throw new NotFoundException("Invalid Food Item Id");
			var foodItemDto = _mapper.Map<FoodItemDto>(foodItem);

			return foodItemDto;
		}

		public async Task<FoodItemDto> Create(FoodItemDto foodItem)
		{
			var newFoodItem = _mapper.Map<FoodItem>(foodItem);

			_repository.Add(newFoodItem);

			await _unitOfWork.CompleteAsync();

			var response = _mapper.Map<FoodItemDto>(newFoodItem);

			return response;
		}

		public async Task Update(int id, FoodItemDto foodItem)
		{
			var foodItemInDb = await _repository.GetAsync(id) ?? throw new NotFoundException("Invalid Food Item Id");
			_mapper.Map(foodItem, foodItemInDb);

			await _unitOfWork.CompleteAsync();
		}

		public async Task Delete(int id)
		{
			var foodItem = await _repository.GetAsync(id) ?? throw new NotFoundException("Invalid Food Item Id");
			foodItem.IsDeleted = true;

			await _unitOfWork.CompleteAsync();
		}
	}
}

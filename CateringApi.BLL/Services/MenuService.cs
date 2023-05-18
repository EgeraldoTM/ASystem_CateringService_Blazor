using AutoMapper;
using CateringApi.BLL.Repositories.Interfaces;
using CateringApi.BLL.Services.Interfaces;
using CateringApi.DAL.Models;
using CateringApi.Helpers.Common.DTOs;
using CateringApi.Helpers.Common.Requests;
using CateringApi.Helpers.Exceptions;

namespace CateringApi.BLL.Services
{
	public class MenuService : IMenuService
	{
		private readonly IMenuRepository _menuRepository;
		private readonly IFoodItemRepository _foodItemRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public MenuService(IMenuRepository menuRepository, IFoodItemRepository foodItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_menuRepository = menuRepository;
			_foodItemRepository = foodItemRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<MenuDto?> GetForSpecificDay(DateTime? date = null)
		{
			var menu = await _menuRepository.GetForSpecificDay(date);

			if (menu is not null)
			{
				var menuDto = _mapper.Map<MenuDto>(menu);
				return menuDto;
			}

			return new MenuDto();
		}

		public async Task<MenuDto?> Get(int id)
		{
			var menu = await _menuRepository.GetWithFoodItems(id) ?? throw new NotFoundException("Invalid Menu Id.");

			var menuDto = _mapper.Map<MenuDto>(menu);

			return menuDto;
		}

		public async Task<bool> Create(MenuRequest request)
		{
			var foodItems = await _foodItemRepository.FindAsync(f => request.FoodIds.Contains(f.Id));
			var newMenu = _mapper.Map<MenuRequest, Menu>(request);
			newMenu.FoodItems = foodItems.ToList();

			_menuRepository.Add(newMenu);

			await _unitOfWork.CompleteAsync();

			var menuDto = _mapper.Map<MenuDto>(newMenu);

			return true;
		}

		public async Task<bool> Update(int id, MenuRequest request)
		{
			var menu = await _menuRepository.GetWithFoodItems(id) ?? throw new NotFoundException("Invalid Menu Id");

			menu.Date = request.Date;

			if (request.FoodIds != null && request.FoodIds.Any())
			{
				var foodItems = await _foodItemRepository.FindAsync(f => request.FoodIds.Contains(f.Id));

				foreach (var item in foodItems)
				{
					if (!menu.FoodItems.Contains(item))
						menu.FoodItems.Add(item);
				}
			}

			await _unitOfWork.CompleteAsync();

			return true;
		}

		//public async Task<ResponseResult<MenuDto>> UpdateDateAsync(int id, DateTime date)
		//{
		//	if (date.Date < DateTime.Now.Date)
		//		return ResponseResult<MenuDto>.BadRequest("Cannot create menu for a day in the past.");

		//	var menu = await _menuRepository.GetWithFoodItemsAsync(id);

		//	if (menu == null)
		//		return ResponseResult<MenuDto>.NotFound("Invalid Menu Id.");

		//	menu.Date = date;

		//	await _unitOfWork.CompleteAsync();

		//	return ResponseResult<MenuDto>.Ok(null);
		//}

		//public async Task<ResponseResult<MenuDto>> AddFoodItemsAsync(int id, List<int> foodIds)
		//{
		//	var menu = await _menuRepository.GetWithFoodItemsAsync(id);

		//	if (menu == null)
		//		return ResponseResult<MenuDto>.NotFound("Invalid Menu Id");

		//	var foodItems = await _foodItemRepository.FindAsync(f => foodIds.Contains(f.Id));

		//	foreach (var foodItem in foodItems)
		//		menu.FoodItems.Add(foodItem);

		//	await _unitOfWork.CompleteAsync();

		//	return ResponseResult<MenuDto>.Ok(null);
		//}

		public async Task<bool> RemoveFoodItem(int id, int foodItemId)
		{
			var menu = await _menuRepository.GetWithFoodItems(id);

			var foodItem = await _foodItemRepository.GetAsync(foodItemId);

			if (menu is not null && foodItem is not null)
			{
				menu.FoodItems.Remove(foodItem);
				await _unitOfWork.CompleteAsync();

				return true;
			}

			return false;
		}

		public async Task<bool> Delete(int id)
		{
			var menu = await _menuRepository.GetAsync(id) ?? throw new NotFoundException("Invalid Menu Id");

			menu.IsDeleted = true;
			await _unitOfWork.CompleteAsync();

			return true;
		}

		//private async Menu MapFoodIds(IEnumerable<int> source, )
		//{

		//	var foodItems = await _foodItemRepository.FindAsync(f => request.FoodIds.Contains(f.Id));

		//	foreach (var item in foodItems)
		//	{
		//		if (!menu.FoodItems.Contains(item))
		//			menu.FoodItems.Add(item);
		//	}

		//	return menu;
		//}
	}
}

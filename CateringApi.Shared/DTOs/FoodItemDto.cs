﻿namespace CateringApi.Shared.DTOs
{
	public class FoodItemDto
	{
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto? Category { get; set; }
    }
}
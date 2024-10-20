﻿namespace CoffeeManagementSystem.Entities.Models
{
	public class Cart
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public List<CartItem> CartItems { get; set; } = new List<CartItem>();
		public decimal TotalPrice { get; set; }
	}


}

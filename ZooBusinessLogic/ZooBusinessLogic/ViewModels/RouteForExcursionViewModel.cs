using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZooBusinessLogic.ViewModels
{
	public class RouteForExcursionViewModel
	{
		public int Id { get; set; }
		public int? ExcursionId { get; set; }
		public int RouteId { get; set; }
		public string RouteName { get; set; }
		public DateTime StartRoute { get; set; }
		[DisplayName("Количество")]
		public int Count { get; set; }
		[DisplayName("Цена")]
		public int Cost { get; set; }
	}
}

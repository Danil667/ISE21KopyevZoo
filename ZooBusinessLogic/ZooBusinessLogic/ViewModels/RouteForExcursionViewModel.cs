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
		[DisplayName("Название")]
		public string RouteName { get; set; }
		public DateTime StartRoute { get; set; }
		[DisplayName("Цена")]
		public int Cost { get; set; }
	}
}

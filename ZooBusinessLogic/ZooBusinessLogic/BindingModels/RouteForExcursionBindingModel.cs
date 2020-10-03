using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBusinessLogic.BindingModels
{
	public class RouteForExcursionBindingModel
	{
		public int Id { get; set; }
		public int? ExcursionId { get; set; }
		public int RouteId { get; set; }
		public string RouteName { get; set; }
		public DateTime StartRoute { get; set; }
		public int Cost { get; set; }
		public int Count { get; set; }
	}
}

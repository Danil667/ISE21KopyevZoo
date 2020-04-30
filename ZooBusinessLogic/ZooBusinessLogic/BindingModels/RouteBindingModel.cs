using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBusinessLogic.BindingModels
{
	public class RouteBindingModel
	{
		public int? Id { set; get; }
		public string RouteName { set; get; }
		public DateTime StartRoute { set; get; }
		public int Cost { set; get; }
	}
}

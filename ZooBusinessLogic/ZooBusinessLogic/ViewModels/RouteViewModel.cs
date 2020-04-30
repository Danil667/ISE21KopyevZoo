using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZooBusinessLogic.ViewModels
{
	public class RouteViewModel
	{
		public int Id { set; get; }
		[DisplayName("Название маршрута")]
		public string RouteName { set; get; }
		[DisplayName("Дата начала")]
		public DateTime StartRoute { set; get; }
		[DisplayName("Цена")]
		public int Cost { set; get; }
	}
}

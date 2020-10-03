using System;
using System.Collections.Generic;
using System.Text;
using ZooBusinessLogic.Enums;

namespace ZooBusinessLogic.BindingModels
{
	public class ExcursionBindingModel
	{
		public int? Id { set; get; }
		public int? ClientId { get; set; }
		public DateTime ExcursionCreate { get; set; }
		public ExcursionStatus Status { get; set; }
		public string Name_Excursion { set; get; }
		public decimal PaidSum { get; set; }
		public decimal Remain { get; set; }
		public DateTime? DateFrom { get; set; }
		public DateTime? DateTo { get; set; }
		public List<RouteForExcursionBindingModel> RouteForExcursions { get; set; }
		public decimal Final_Cost { get; set; }
	}
}

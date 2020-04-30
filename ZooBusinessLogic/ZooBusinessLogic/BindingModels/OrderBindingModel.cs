using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBusinessLogic.BindingModels
{
	public class OrderBindingModel
	{
		public int? Id { get; set; }
		public int? ClientId { get; set; }
		public int ExcursionId { get; set; }
		public decimal Sum { get; set; }
		public DateTime DateCreate { get; set; }
	}
}

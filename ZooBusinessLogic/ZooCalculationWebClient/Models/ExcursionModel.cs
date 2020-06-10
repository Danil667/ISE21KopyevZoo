using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooBusinessLogic.Enums;

namespace ZooCalculationWebClient.Models
{
	public class ExcursionModel
	{
		public int Id { set; get; }
		public int ClientId { get; set; }
		public DateTime ExcursionCreate { get; set; }
		public ExcursionStatus Status { get; set; }
		public string Name_Excursion { set; get; }
		public int PaidSum { get; set; }
		public decimal Final_Cost { set; get; }
		public List<RouteModel> RouteForExcursions { get; set; }
	}
}

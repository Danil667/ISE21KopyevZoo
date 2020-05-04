using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ZooBusinessLogic.Enums;

namespace ZooBusinessLogic.ViewModels
{
	public class ExcursionViewModel
	{
		public int Id { set; get; }
		public int? ClientId { get; set; }
		[DisplayName("Клиент")]
		public string ClientFIO { get; set; }
		[DisplayName("Дата создания")]
		public DateTime ExcursionCreate { get; set; }
		[DisplayName("Статус")]
		public ExcursionStatus Status { get; set; }
		[DisplayName("Назване экскурсии")]
		public string Name_Excursion { set; get; }
		[DisplayName("сумма")]
		public decimal Cost { get; set; }
		[DisplayName("Оплаченная сумма")]
		public decimal PaidSum { get; set; }
		[DisplayName("Оcтаток")]
		public decimal Remain { get; set; }
		public List<RouteForExcursionViewModel> RouteForExcursions { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZooBusinessLogic.ViewModels
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public int? ClientId { get; set; }
		public int ExcursionId { get; set; }
		[DisplayName("Сумма оплаты")]
		public decimal Sum { get; set; }
		[DisplayName("Дата оплаты")]
		public DateTime DateCreate { get; set; }
	}
}

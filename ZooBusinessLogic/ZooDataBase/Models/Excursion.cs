using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZooBusinessLogic.Enums;

namespace ZooDataBase.Models
{
	public class Excursion
	{
		public int Id { set; get; }
		public int? ClientId { get; set; }
		[Required]
		public DateTime ExcursionCreate { get; set; }
		public ExcursionStatus Status { get; set; }
		public string Name_Excursion { set; get; }
		[Required]
		public int PaidSum { get; set; }
		[Required]
		public decimal Final_Cost { set; get; }
		[ForeignKey("ExcursionId")]
		public virtual List<RouteForExcursion> RouteForExcursions { get; set; }
		[Required]
		[ForeignKey("ExcursionId")]
		public virtual List<Order> Orders { get; set; }
		public Client Client { get; set; }
	}
}

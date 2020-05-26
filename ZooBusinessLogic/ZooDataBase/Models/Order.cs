using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZooDataBase.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int? ClientId { get; set; }
		[Required]
		public int ExcursionId { get; set; }
		[Required]
		public decimal Sum { get; set; }
		[Required]
		public DateTime DateCreate { get; set; }
		public virtual Client Clients { get; set; }
		public virtual Excursion Excursions { get; set; }
	}
}

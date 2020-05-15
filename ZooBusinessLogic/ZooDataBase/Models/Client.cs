using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZooDataBase.Models
{
	public class Client
	{
		public int Id { get; set; }
		[Required]
		public string ClientFIO { get; set; }
		[Required]
		public string Login { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public bool BlockStatus { get; set; }
		[ForeignKey("ClientId")]
		public virtual List<Order> Orders { get; set; }
		[ForeignKey("ClientId")]
		public virtual List<Excursion> Excursions { get; set; }
	}
}

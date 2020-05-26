using System;
using System.Collections.Generic;
using System.Text;

namespace ZooDataBase.Models
{
	public class RouteForExcursion
	{
		public int Id { get; set; }
		public int? ExcursionId { get; set; }
		public int RouteId { get; set; }
		public virtual Excursion Excursions { get; set; }
		public virtual Route Routes { get; set; }
	}
}

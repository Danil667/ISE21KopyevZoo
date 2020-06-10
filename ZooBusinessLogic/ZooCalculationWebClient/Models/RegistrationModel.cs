using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZooCalculationWebClient.Models
{
	public class RegistrationModel
	{
		[Required]
		public string Login { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		public string ClientFIO { get; set; }
	}
}

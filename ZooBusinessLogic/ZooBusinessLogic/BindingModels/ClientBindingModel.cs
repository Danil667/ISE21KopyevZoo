using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBusinessLogic.BindingModels
{
	public class ClientBindingModel
	{
		public int? Id { get; set; }
		public string ClientFIO { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public bool BlockStatus { get; set; }
	}
}

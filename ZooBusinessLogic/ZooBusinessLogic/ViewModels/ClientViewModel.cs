using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZooBusinessLogic.ViewModels
{
	public class ClientViewModel
	{
		public int? Id { get; set; }
		[DisplayName("ФИО")]
		public string ClientFIO { get; set; }
		[DisplayName("Email")]
		public string Login { get; set; }
		[DisplayName("Пароль")]
		public string Password { get; set; }
		[DisplayName("Блокировка")]
		public bool BlockStatus { get; set; }
	}
}

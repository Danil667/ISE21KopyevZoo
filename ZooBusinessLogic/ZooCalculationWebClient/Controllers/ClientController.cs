using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZooBusinessLogic.BindingModels;
using ZooBusinessLogic.Interfaces;
using ZooCalculationWebClient.Models;

namespace ZooCalculationWebClient.Controllers
{
	public class ClientController : Controller
	{
		private readonly IClientLogic client;
		private readonly int passwordMinLength = 6;
		private readonly int passwordMaxLength = 20;
		private readonly int loginMinLength = 1;
		private readonly int loginMaxLength = 50;
		public ClientController(IClientLogic client)
		{
			this.client = client;
		}
		public ActionResult Profile()
		{
			ViewBag.User = Program.Client;
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(SignIn user)
		{
			var clientView = client.Read(new ClientBindingModel
			{
				Login = user.Login,
				Password = user.Password
			}).FirstOrDefault();
			if (clientView == null)
			{
				ModelState.AddModelError("", "Вы ввели неверный пароль, либо пользователь не найден");
				return View(user);
			}
			if (clientView.BlockStatus == true)
			{
				ModelState.AddModelError("", "Пользователь заблокирован");
				return View(user);
			}
			Program.Client = clientView;
			return RedirectToAction("Index", "Home");
		}
		public IActionResult Logout()
		{
			Program.Client = null;
			return RedirectToAction("Index", "Home");
		}
		public IActionResult Registration()
		{
			return View();
		}
		[HttpPost]
		public ViewResult Registration(RegistrationModel user)
		{
			if (String.IsNullOrEmpty(user.Login))
			{
				ModelState.AddModelError("", "Введите логин");
				return View(user);
			}
			if (user.Login.Length > loginMaxLength ||
		   user.Login.Length < loginMinLength)
			{
				ModelState.AddModelError("", $"Длина логина должна быть от {loginMinLength} до {loginMaxLength} символов");
				return View(user);
			}
			var existClient = client.Read(new ClientBindingModel
			{
				Login = user.Login
			}).FirstOrDefault();
			if (existClient != null)
			{
				ModelState.AddModelError("", "Уже есть клиент с таким логином");
				return View(user);
			}
			if (!Regex.IsMatch(user.Login, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
			{
				ModelState.AddModelError("", "логин введен некорректно");
				return View(user);
			}
			if (user.Password.Length > passwordMaxLength ||
			user.Password.Length < passwordMinLength)
			{
				ModelState.AddModelError("", $"Длина пароля должна быть от {passwordMinLength} до {passwordMaxLength} символов");
				return View(user);
			}
			if (String.IsNullOrEmpty(user.ClientFIO))
			{
				ModelState.AddModelError("", "Введите ФИО");
				return View(user);
			}
			if (String.IsNullOrEmpty(user.Password))
			{
				ModelState.AddModelError("", "Введите пароль");
				return View(user);
			}
			client.CreateOrUpdate(new ClientBindingModel
			{
				ClientFIO = user.ClientFIO,
				Login = user.Login,
				Password = user.Password,
				BlockStatus = false
			});
			ModelState.AddModelError("", "Вы успешно зарегистрированы");
			return View("Registration", user);
		}
	}
}

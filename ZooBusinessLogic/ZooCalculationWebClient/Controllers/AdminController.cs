using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZooBusinessLogic.BindingModels;
using ZooBusinessLogic.Interfaces;
using ZooCalculationWebClient.Models;

namespace ZooCalculationWebClient.Controllers
{
    public class AdminController : Controller
    {
        private string password = "Admin";
        private readonly IClientLogic _client;

        public AdminController(IClientLogic client)
        {
            _client = client;
        }
        public IActionResult Index(AdminModel model)
        {
            if (model.Password == password)
            {
                Program.AdminMode = !Program.AdminMode;
                return RedirectToAction("Blocking");
            }
            if (String.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", "Введите пароль");
                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Вы ввели неверный пароль");
                return View(model);
            }
        }
        public IActionResult Blocking()
        {
            ViewBag.Clients = _client.Read(null);
            return View();
        }
        public ActionResult BlockStatus(int id)
        {
            if (ModelState.IsValid)
            {
                var existClient = _client.Read(new ClientBindingModel
                {
                    Id = id
                }).FirstOrDefault();
                _client.CreateOrUpdate(new ClientBindingModel
                {
                    Id = id,
                    ClientFIO = existClient.ClientFIO,
                    Login = existClient.Login,
                    Password = existClient.Password,
                    BlockStatus = !existClient.BlockStatus
                });
                return RedirectToAction("Blocking");
            }
            return RedirectToAction("Blocking");
        }
        public IActionResult Logout()
        {
            Program.AdminMode = false;
            return RedirectToAction("Index", "Home");
        }
    }
}

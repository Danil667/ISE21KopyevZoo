using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZooBusinessLogic.Interfaces;

namespace ZooCalculationWebClient.Controllers
{
    public class RouteController : Controller
    {
        private readonly IRouteLogic routes;
        public RouteController(IRouteLogic routes)
        {
            this.routes = routes;
        }
        public IActionResult Route()
        {
            ViewBag.Routes = routes.Read(null);
            return View();
        }
    }
}
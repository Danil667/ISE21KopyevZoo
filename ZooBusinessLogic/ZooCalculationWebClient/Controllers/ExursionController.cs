using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZooBusinessLogic.BindingModels;
using ZooBusinessLogic.BusinessLogic;
using ZooBusinessLogic.Enums;
using ZooBusinessLogic.Interfaces;
using ZooBusinessLogic.ViewModels;
using ZooCalculationWebClient.Models;

namespace ZooCalculationWebClient.Controllers
{
    public class ExcursionController : Controller
    {
        private readonly IExcursionLogic edLogic;
        private readonly IRouteLogic routeLogic;
        private readonly IOrderLogic payLogic;
        private readonly ReportLogic reportLogic;

        public ExcursionController(IExcursionLogic edLogic, IRouteLogic courseLogic, IOrderLogic payLogic, ReportLogic reportLogic)
        {
            this.edLogic = edLogic;
            this.routeLogic = courseLogic;
            this.payLogic = payLogic;
            this.reportLogic = reportLogic;
        }
        public IActionResult Excursion()
        {
            ViewBag.Excursions = edLogic.Read(new ExcursionBindingModel
            {
                ClientId = Program.Client.Id
            });
            return View();
        }
        [HttpPost]
        public IActionResult Excursion(ReportModel model)
        {
            var payList = new List<OrderViewModel>();
            var excursions = new List<ExcursionViewModel>();
            excursions = edLogic.Read(new ExcursionBindingModel
            {
                ClientId = Program.Client.Id,
                DateFrom = model.From,
                DateTo = model.To
            });
            var pays = payLogic.Read(null);
            foreach (var ed in excursions)
            {
                foreach (var pay in pays)
                {
                    if (pay.ClientId == Program.Client.Id && pay.ExcursionId == ed.Id)
                        payList.Add(pay);
                }
            }
            ViewBag.Order = payList;
            ViewBag.Excursion = excursions;
            string fileName = "D:\\temp\\PdfReport.pdf";
            if (model.SendMail)
            {
                reportLogic.SaveExcursionOrdersToPdfFile(fileName, new ExcursionBindingModel
                {
                    ClientId = Program.Client.Id,
                    DateFrom = model.From,
                    DateTo = model.To
                }, Program.Client.Login);
            }
            return RedirectToAction("Excursion");
        }
        public IActionResult CreateExcursion()
        {
            ViewBag.RouteForExcursions = routeLogic.Read(null);
            return View();
        }
        [HttpPost]
        public ActionResult CreateExcursion(CreateExcursion model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RouteForExcursions = routeLogic.Read(null);
                return View(model);
            }

            var edRoutes = new List<RouteForExcursionBindingModel>();

            foreach (var course in model.RouteForExcursions)
            {
                if (course.Value > 0)
                {
                    edRoutes.Add(new RouteForExcursionBindingModel
                    {
                        RouteId = course.Key,
                        Count = course.Value
                    });
                }
            }
            if (edRoutes.Count == 0)
            {
                ViewBag.Products = routeLogic.Read(null);
                ModelState.AddModelError("", "Ни один маршрут не выбран");
                return View(model);
            }
            edLogic.CreateOrUpdate(new ExcursionBindingModel
            {
                ClientId = Program.Client.Id,
                ExcursionCreate = DateTime.Now,
                Status = ExcursionStatus.Принят,
                Final_Cost = CalculateSum(edRoutes),
                RouteForExcursions = edRoutes
            });
            return RedirectToAction("Excursion");
        }
        private decimal CalculateSum(List<RouteForExcursionBindingModel> edRoutes)
        {
            decimal sum = 0;

            foreach (var course in edRoutes)
            {
                var courseData = routeLogic.Read(new RouteBindingModel { Id = course.RouteId }).FirstOrDefault();

                if (courseData != null)
                {
                    sum += courseData.Cost;
                }
            }
            return sum * edRoutes.Count;
        }

        public IActionResult Order(int id)
        {
            var excursion = edLogic.Read(new ExcursionBindingModel
            {
                Id = id
            }).FirstOrDefault();
            ViewBag.Excursion = excursion;
            ViewBag.Remain = CalculateRemain(excursion);
            return View();
        }
        [HttpPost]
        public ActionResult Order(OrderModel model)
        {
            ExcursionViewModel excursion = edLogic.Read(new ExcursionBindingModel
            {
                Id = model.ExcursionId
            }).FirstOrDefault();
            decimal Remain = CalculateRemain(excursion);
            if (!ModelState.IsValid)
            {
                ViewBag.Excursion = excursion;
                ViewBag.Remain = Remain;
                return View(model);
            }
            if (Remain < model.OrderSum)
            {
                ViewBag.Excursion = excursion;
                ViewBag.Remain = Remain;
                return View(model);
            }
            payLogic.CreateOrUpdate(new OrderBindingModel
            {
                ExcursionId = excursion.Id,
                ClientId = Program.Client.Id,
                DateCreate = DateTime.Now,
                Sum = model.OrderSum
            });
            Remain -= model.OrderSum;
            edLogic.CreateOrUpdate(new ExcursionBindingModel
            {
                Id = excursion.Id,
                ClientId = excursion.ClientId,
                ExcursionCreate = excursion.ExcursionCreate,
                Status = Remain > 0 ? ExcursionStatus.В_Рассрочке : ExcursionStatus.Заказ_оплачен,
                Final_Cost = excursion.Cost,
                RouteForExcursions = excursion.RouteForExcursions.Select(rec => new RouteForExcursionBindingModel
                {
                    Id = rec.Id,
                    ExcursionId = rec.ExcursionId,
                    RouteId = rec.RouteId,
                    Cost = rec.Cost
                }).ToList()
            });
            return RedirectToAction("Excursion");
        }

        private decimal CalculateRemain(ExcursionViewModel excursion)
        {
            decimal sum = excursion.Cost;
            decimal paidSum = payLogic.Read(new OrderBindingModel
            {
                ExcursionId = excursion.Id
            }).Select(rec => rec.Sum).Sum();

            return sum - paidSum;
        }
        public IActionResult SendWordReport(int id)
        {
            var excursion = edLogic.Read(new ExcursionBindingModel { Id = id }).FirstOrDefault();
            string fileName = "D:\\temp\\" + excursion.Id + ".docx";
            reportLogic.SaveRouteForExcursionsToWordFile(fileName, excursion, Program.Client.Login);
            return RedirectToAction("Excursion");
        }
        public IActionResult SendExcelReport(int id)
        {
            var excursion = edLogic.Read(new ExcursionBindingModel { Id = id }).FirstOrDefault();
            string fileName = "D:\\temp\\" + excursion.Id + ".xlsx";
            reportLogic.SaveRouteForExcursionsToExcelFile(fileName, excursion, Program.Client.Login);
            return RedirectToAction("Excursion");
        }
    }
}

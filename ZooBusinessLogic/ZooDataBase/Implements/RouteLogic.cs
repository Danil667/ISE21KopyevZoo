using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ZooBusinessLogic.BindingModels;
using ZooBusinessLogic.Interfaces;
using ZooBusinessLogic.ViewModels;
using ZooDataBase.Models;

namespace ZooDataBase.Implements
{
    public class RouteLogic : IRouteLogic
    {
        private readonly string RouteFileName = "D://data//Routes.xml";
        public List<Route> Routes { get; set; }
        public RouteLogic()
        {
            Routes = LoadRoutes();
        }
        private List<Route> LoadRoutes()
        {
            var list = new List<Route>();
            if (File.Exists(RouteFileName))
            {
                XDocument xDocument = XDocument.Load(RouteFileName);
                var xElements = xDocument.Root.Elements("Route").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Route
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        RouteName = elem.Element("RouteName").Value,
                        StartRoute = Convert.ToDateTime(elem.Element("StartRoute").Value),
                        Cost = Convert.ToInt32(elem.Element("Cost").Value),
                    });
                }
            }
            return list;
        }
        public void SaveToDatabase()
        {
            var routes = LoadRoutes();
            using (var context = new ZooDatabase())
            {
                foreach (var route in routes)
                {
                    Route element = context.Routes.FirstOrDefault(rec => rec.Id == route.Id);
                    if (element != null)
                    {
                        break;
                    }
                    else
                    {
                        element = new Route();
                        context.Routes.Add(element);
                    }
                    element.RouteName = route.RouteName;
                    element.StartRoute = route.StartRoute;
                    element.Cost = route.Cost;
                    context.SaveChanges();
                }
            }
        }
        public List<RouteViewModel> Read(RouteBindingModel model)
        {
            SaveToDatabase();
            return Routes
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new RouteViewModel
            {
                Id = rec.Id,
                RouteName = rec.RouteName,
                StartRoute = rec.StartRoute,
                Cost = rec.Cost,
            })
            .ToList();
        }
    }
}

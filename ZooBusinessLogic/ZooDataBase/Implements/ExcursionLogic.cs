using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZooBusinessLogic.BindingModels;
using ZooBusinessLogic.Interfaces;
using ZooBusinessLogic.ViewModels;
using ZooDataBase.Models;

namespace ZooDataBase.Implements
{
    public class ExcursionLogic : IExcursionLogic
    {
        public void CreateOrUpdate(ExcursionBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Excursion elem = model.Id.HasValue ? null : new Excursion();
                        if (model.Id.HasValue)
                        {
                            elem = context.Excursions.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (elem == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                            elem.ClientId = model.ClientId;
                            elem.ExcursionCreate = model.ExcursionCreate;
                            elem.Final_Cost = model.Final_Cost;
                            elem.Status = model.Status;
                            context.SaveChanges();
                        }
                        else
                        {
                            elem.ClientId = model.ClientId;
                            elem.ExcursionCreate = model.ExcursionCreate;
                            elem.Final_Cost = model.Final_Cost;
                            elem.Status = model.Status;
                            context.Excursions.Add(elem);
                            context.SaveChanges();
                            var routes = model.RouteForExcursions
                               .GroupBy(rec => rec.RouteId)
                               .Select(rec => new
                               {
                                   RouteId = rec.Key,
                               });

                            foreach (var route in routes)
                            {
                                context.RouteForExcursions.Add(new RouteForExcursion
                                {
                                    ExcursionId = elem.Id,
                                    RouteId = route.RouteId,
                                });
                                context.SaveChanges();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(ExcursionBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                Excursion elem = context.Excursions.FirstOrDefault(rec => rec.Id == model.Id.Value);

                if (elem != null)
                {
                    context.Excursions.Remove(elem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<ExcursionViewModel> Read(ExcursionBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                return context.Excursions.Where(rec => rec.Id == model.Id || (rec.ClientId == model.ClientId))
                .Select(rec => new ExcursionViewModel
                {
                    Id = rec.Id,
                    ClientId = rec.ClientId,
                    ClientFIO = rec.Client.ClientFIO,
                    ExcursionCreate = rec.ExcursionCreate,
                    Cost = rec.Final_Cost,
                    Remain = rec.Final_Cost-context.Orders.Where(recP => recP.ExcursionId == recP.Id).Select(recP => recP.Sum).Sum(),
                    Status = rec.Status,
                    RouteForExcursions = GetExRouteViewModel(rec)
                })
            .ToList();
            }
        }
        public static List<RouteForExcursionViewModel> GetExRouteViewModel(Excursion ex)
        {
            using (var context = new ZooDatabase())
            {
                var RouteForExcursions = context.RouteForExcursions
                    .Where(rec => rec.ExcursionId == ex.Id)
                    .Select(rec => new RouteForExcursionViewModel
                    {
                        Id = rec.Id,
                        ExcursionId = rec.ExcursionId,
                        RouteId = rec.RouteId,
                    }).ToList();
                foreach (var route in RouteForExcursions)
                {
                    var routeData = context.Routes.Where(rec => rec.Id == route.RouteId).FirstOrDefault();
                    route.RouteName = routeData.RouteName;
                    route.StartRoute = routeData.StartRoute;
                    route.Cost = routeData.Cost;
                }
                return RouteForExcursions;
            }
        }
    }
}

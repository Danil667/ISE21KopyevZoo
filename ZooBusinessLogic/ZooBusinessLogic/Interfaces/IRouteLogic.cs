using System;
using System.Collections.Generic;
using System.Text;
using ZooBusinessLogic.BindingModels;
using ZooBusinessLogic.ViewModels;

namespace ZooBusinessLogic.Interfaces
{
    public interface IRouteLogic
    {
        List<RouteViewModel> Read(RouteBindingModel model);
    }
}

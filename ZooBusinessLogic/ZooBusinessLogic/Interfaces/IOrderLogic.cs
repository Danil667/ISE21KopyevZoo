using System;
using System.Collections.Generic;
using System.Text;
using ZooBusinessLogic.BindingModels;
using ZooBusinessLogic.ViewModels;

namespace ZooBusinessLogic.Interfaces
{
    public interface IOrderLogic
    {
        List<OrderViewModel> Read(OrderBindingModel model);
        void CreateOrUpdate(OrderBindingModel model);
        void Delete(OrderBindingModel model);
    }
}

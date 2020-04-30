using System;
using System.Collections.Generic;
using System.Text;
using ZooBusinessLogic.BindingModels;
using ZooBusinessLogic.ViewModels;

namespace ZooBusinessLogic.Interfaces
{
    public interface IExcursionLogic
    {
        List<ExcursionViewModel> Read(ExcursionBindingModel model);
        void CreateOrUpdate(ExcursionBindingModel model);
        void Delete(ExcursionBindingModel model);
    }
}

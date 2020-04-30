using System;
using System.Collections.Generic;
using System.Text;
using ZooBusinessLogic.BindingModels;
using ZooBusinessLogic.ViewModels;

namespace ZooBusinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}

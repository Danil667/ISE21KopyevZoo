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
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                Client elem = context.Clients.FirstOrDefault(rec => rec.Login == model.Login && rec.Id != model.Id);
                if (elem != null)
                {
                    throw new Exception("Уже есть клиент с таким логином");
                }
                if (model.Id.HasValue)
                {
                    elem = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                    if (elem == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    elem = new Client();
                    context.Clients.Add(elem);
                }
                elem.Login = model.Login;
                elem.ClientFIO = model.ClientFIO;
                elem.Password = model.Password;
                context.SaveChanges();
            }
        }
        public void Delete(ClientBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                Client elem = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);

                if (elem != null)
                {
                    context.Clients.Remove(elem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                return context.Clients
                .Where(
                    rec => model == null
                    || rec.Id == model.Id
                    || rec.Login == model.Login && rec.Password == model.Password
                )
                .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    Login = rec.Login,
                    ClientFIO = rec.ClientFIO,
                    Password = rec.Password,
                    BlockStatus = rec.BlockStatus
                })
                .ToList();
            }
        }
    }
}

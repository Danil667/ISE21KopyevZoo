using System;
using System.Collections.Generic;
using System.Text;
using ZooBusinessLogic.ViewModels;

namespace ZooBusinessLogic.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<RouteViewModel> Routes { get; set; }
    }
}

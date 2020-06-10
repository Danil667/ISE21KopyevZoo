using System;
using System.Collections.Generic;
using System.Text;
using ZooBusinessLogic.ViewModels;

namespace ZooBusinessLogic.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ExcursionViewModel> Excursions { get; set; }
        public Dictionary<int, List<OrderViewModel>> Orders { get; set; }
    }
}
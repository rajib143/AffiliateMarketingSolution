using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMA.WEB.Models
{
    public class RequestModel
    {
        public string searchText { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public string sort { get; set; }
     
    }
}
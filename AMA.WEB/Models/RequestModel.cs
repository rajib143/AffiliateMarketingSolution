using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMA.WEB.Models
{
    public class OfferProductRequestModel
    {
        public string searchText { get; set; }
        public int catagoryID { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public string sort { get; set; }
     
    }

    public class FlipKartAllOffersRequestModel
    {
        public string searchText { get; set; }
        public DateTime endDate { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public string sort { get; set; }

    }
    public class CategoryRequestModel
    {
         public int catagoryID { get; set; }
        public int parentID { get; set; }
      
    }
}
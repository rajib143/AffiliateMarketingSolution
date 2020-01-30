using AMA.DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMA.WEB.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SiteName { get; set; }
        public int ProductCount { get; set; }
        public Category ParentCategory { get; set; }
        public List<CategoryModel> Children { get; set; }
    }
    
}
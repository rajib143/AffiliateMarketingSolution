using AMA.BusinessLayer.AbstractFactory;
using AMA.DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMA.WEB.Models
{
    public class AMAManager
    {
        public readonly AMAClient Client;
        public AMAManager()
        {
            Client = new AMAClient();

        }

        public static List<CategoryModel> FillRecursive(List<Category> flatObjects, int? parentId = null)
        {
            return flatObjects.Where(x => x.ParentId.Equals(parentId)).Select(item => new CategoryModel
            {
                Name = item.Name,
                Id = item.Id,
                Description=item.Description,
                SiteName=item.SiteName,
                ParentCategory=item,
                Children = FillRecursive(flatObjects, item.Id)
            }).ToList();
        }
    }
}
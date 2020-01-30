using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AMA.DataLayer.Data;
using AMA.WEB.Models;

namespace AMA.WEB.Controllers.API
{

    public class CategoryController : ApiController
    {
        private AMAManager _AMAManager = new AMAManager();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            // GET: api/Category
        public IQueryable<CategoryModel> GetCategories()
        {
            List<Category> categories = _AMAManager.Client.Admin.GetCategories(x => x.ParentId == 0, log).Result.ToList();
            List<CategoryModel> categoryModel = AMAManager.FillRecursive(categories, 0);

            return categoryModel.ToList().AsQueryable();
        }


        // GET: api/Category/5
        [HttpPost]
        [ResponseType(typeof(CategoryModel))]
        public async Task<IHttpActionResult> GetCategoryByParentID(CategoryRequestModel requestModel)
        {
            List<GetParentChildCategories_Result> ParentChildCategories = _AMAManager.Client.Admin.GetParentChildsCategories(requestModel.parentID, log).Result.ToList();

            List<Category> categories = ParentChildCategories.Select(x => new Category()
            {
                Id = x.Id.Value,
                Description = x.Description,
                Name = x.Name,
                ParentId = x.ParentId,
                SiteName = x.SiteName,
                ProductCount = x.ProductCount.Value
            }).ToList();

            List<CategoryModel> categoryModel = AMAManager.FillRecursive(categories, requestModel.parentID);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return Ok(categoryModel);
        }


    }
}
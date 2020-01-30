using System.Web;
using System.Web.Optimization;

namespace AMA.WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery-{version}.js",
                        //"~/Scripts/jquery-3.3.1.slim.min.js",
                        "~/Scripts/popper.min.js",
                         "~/Scripts/jquery-ui-1.12.1.js",
                         //"~/Content/Theme1/js/jquery-3.3.1.slim.min.js",
                         "~/Content/Theme1/js/Layout.js"

                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     // "~/Content/Theme1/js/bootstrap.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.unobtrusive-ajax.min.js"
                      ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/Theme1/css/bootstrap.min.css",
                       "~/Content/themes/base/jquery-ui.min.css",
                      "~/Content/Theme1/css/style.css"
                      ));

         BundleTable.EnableOptimizations = false;
        }
    }
}

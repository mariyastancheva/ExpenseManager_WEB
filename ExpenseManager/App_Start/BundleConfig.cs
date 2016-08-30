using System.Web;
using System.Web.Optimization;

namespace ExpenseManager
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                     "~/Content/PagedList.css",
                     "~/Content/timeline.css",
                     "~/Content/morris.css",
                     "~/Content/metisMenu.css",
                     "~/Content/font-awesome.css",
                     "~/Content/bootstrap-theme.css",
                     "~/Content/themes/jquery-ui.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                       "~/Scripts/morris-data.js",
                       "~/Scripts/raphael-min.js",
                       "~/Scripts/respond.js",
                       "~/Scripts/sb-admin-2.js",
                       "~/js/metisMenu.js",
                       "~/Scripts/morris.min.js"));
        }
    }
}

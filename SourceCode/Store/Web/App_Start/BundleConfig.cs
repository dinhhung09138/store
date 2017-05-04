using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
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
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Content/angular").Include(
                "~/Content/angularjs/angular.min.js",
                "~/Content/angularjs/angular-cookies.min.js",
                "~/Content/angularjs/angular-ui-router.min.js",
                "~/Content/angularjs/angular-animate.min.js",
                "~/Content/angularjs/angular-loader.min.js",
                "~/Content/angularjs/angular-messages.min.js",
                "~/Content/angularjs/angular-message-format.min.js",
                "~/Content/angularjs/angular-mocks.min.js",
                "~/Content/angularjs/angular-scenario.min.js"));

            #region " User "

            bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include(
                "~/Content/bootstrap/bootstrap.css"));
            bundles.Add(new ScriptBundle("~/Content/js/bootstrap").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Content/bootstrap/bootstrap.js"));

            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
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
                "~/Content/jquery-ui.min.css",
                "~/Content/bootstrap/bootstrap.min.css",
                "~/Content/bootstrap/bootstrap-select.min.css",
                "~/Content/bootstrap/bootstrap-datetimepicker.min.css",
                "~/Content/font-awsome/font-awesome.min.css",
                "~/Content/gritter/jquery.gritter.css",
                "~/Content/site.css",
                "~/Content/datatable/dataTables.bootstrap.min.css",
                "~/Content/datatable/responsive.bootstrap.min.css",
                "~/Content/datatable/buttons.dataTables.min.css"));

            bundles.Add(new ScriptBundle("~/Content/js/bootstrap").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Content/jquery-ui.min.js",
                "~/Content/bootstrap/bootstrap.min.js",
                "~/Content/bootstrap/bootstrap-select.min.js",
                "~/Content/moment.min.js",
                "~/Content/bootstrap/bootstrap-datetimepicker.min.js",
                "~/Content/gritter/jquery.gritter.min.js",
                "~/Content/slimScroll/jquery.slimscroll.min.js",
                "~/Content/base.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Content/datatable/jquery.dataTables.min.js",
                "~/Content/datatable/dataTables.bootstrap.min.js",
                "~/Content/datatable/dataTables.responsive.min.js",
                "~/Content/datatable/responsive.bootstrap.min.js"));

            #endregion

            BundleTable.EnableOptimizations = false;
        }
    }
}

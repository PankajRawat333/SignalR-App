using System.Web;
using System.Web.Optimization;

namespace ServerMonitoringApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapSignalR").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.signalR-2.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/vendor/angular/angular.min.js",
                      "~/Scripts/vendor/sweetalert/sweetalert.min.js",
                      "~/Scripts/vendor/angular-sweetalert/SweetAlert.min.js",
                      "~/Scripts/app/app.js",
                      "~/Scripts/app/dashboard/dashboard.module.js",
                      "~/Scripts/app/dashboard/services/factory.js",
                      "~/Scripts/app/dashboard/controllers/dashboardController.js"));

            bundles.Add(new ScriptBundle("~/bundles/smoothie").Include(
                      "~/Scripts/smoothie.js",
                      "~/Scripts/custom/CpuMemorySmoothieChart.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/app.css",
                      "~/Content/sb-admin-2.css"));

            
        }
    }
}

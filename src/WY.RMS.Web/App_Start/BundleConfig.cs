using System.Web.Optimization;

namespace WY.RMS.Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                //"~/Scripts/jquery-{version}.js",
                    "~/Scripts/AdminLTE2.3.0/plugins/jQuery/jQuery-2.1.4.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/AdminLTE2.3.0/bootstrap/js/bootstrap.js",
                    "~/Scripts/AdminLTE2.3.0/dist/js/app.js",
                    "~/Scripts/AdminLTE2.3.0/plugins/slimScroll/jquery.slimscroll.js",
                    "~/Scripts/AdminLTE2.3.0/plugins/fastclick/fastclick.js",
                    "~/Scripts/AdminLTE2.3.0/plugins/iCheck/icheck.js",
                   "~/Scripts/bootstraptable/dist/bootstrap-table.js",
                   "~/Scripts/bootstraptable/dist/locale/bootstrap-table-zh-CN.js",
                    "~/Scripts/Custom/WinMsg.js"));
            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
                        "~/Scripts/AdminLTE2.3.0/bootstrap/css/bootstrap.css",
                       "~/Scripts/AdminLTE2.3.0/dist/css/AdminLTE.css",
                       "~/Scripts/AdminLTE2.3.0/dist/css/skins/skin-blue.css",
                       "~/Scripts/AdminLTE2.3.0/plugins/iCheck/flat/blue.css",
                       "~/Scripts/Toastr/toastr.css",
                       "~/Scripts/bootstraptable/dist/bootstrap-table.css"));
            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}
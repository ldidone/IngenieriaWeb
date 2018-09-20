﻿using System.Web;
using System.Web.Optimization;

namespace TeLoBusco
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-notify.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/dataTables.bootstrap4.min.css",
                      "~/Content/datables.min.css",
                      "~/Content/dataTables.jqueryui.min.css",
                      "~/Content/sweetalert.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                    "~/Scripts/datatables.min.js",
                    "~/Scripts/dataTables.bootstrap4.min.js",
                    "~/Scripts/dataTables.jqueryui.min.js",
                    "~/Scripts/sweetalert.js"
                ));
        }
    }
}

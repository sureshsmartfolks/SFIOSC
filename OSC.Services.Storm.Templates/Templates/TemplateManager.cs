using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StormTemplates_API.Templates
{
    public class TemplateManager
    {
        public string AccountName { get; set; }

        public TemplateManager(int AccountID)
        {
            AccountName = "default";
        }

        public String getFeature(string featureName)
        {

            string css = "<style> " + versionGrab(featureName, "css") + "</style>";
            string js = "<script id='" + featureName + "'> " + versionGrab(featureName, "js") + "</script>";
            string markup = versionGrab(featureName, "html");

            return css + js + markup;
        }

        private String versionGrab(string featureName, string fileExtension)
        {
            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Features/" + featureName + "/" + AccountName + "." + fileExtension)))
            {
                return File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Features/" + featureName + "/" + AccountName + "." + fileExtension));
            }
            else return File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Features/" + featureName + "/" + "default." + fileExtension));
        }

    }
}
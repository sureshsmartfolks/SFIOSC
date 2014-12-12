using JwtAuthForWebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ProjectManagement_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //var builder = new SecurityTokenBuilder();
            //var jwtHandler = new JwtAuthenticationMessageHandler
            //{
            //    AllowedAudience = "urn:Storm-PM",
            //    Issuer = "http://oscid.osc-it.com/trust/IdentityServer",
            //    SigningToken = builder.CreateFromKey("jB0UJQ2HRcFkpogdauMLqHa9/+g/fCp2oxuPcRyT5rE=")
            //};

            //List<String> audiences = new List<String>();
            //audiences.Add("urn:Notification-Web");
            //audiences.Add("urn:ContentCentreWeb");
            //jwtHandler.AllowedAudiences = audiences;


            //config.MessageHandlers.Add(jwtHandler);
        }
    }
}

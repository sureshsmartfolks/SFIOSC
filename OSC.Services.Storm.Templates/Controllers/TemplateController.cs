using StormTemplates_API.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StormTemplates_API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TemplateController : ApiController
    {
        public HttpResponseMessage Options()
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        private int getCompanyIDFromOAuth()
        {
            //var x = Convert.FromBase64String(WebOperationContext.Current.IncomingRequest.Headers[System.Net.HttpRequestHeader.Authorization]);
            //var token1 = System.IdentityModel.Services.FederatedAuthentication.SessionAuthenticationModule.ReadSessionTokenFromCookie(x);
            var x = (System.Security.Claims.ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity;
            //int companyID = Convert.ToInt16(x.Claims.Single(c => c.Type == "http://identityserver.thinktecture.com/claims/profileclaims/companyid").Value);
            //return companyID;
            return 1;
        }

        [HttpGet]
        public HttpResponseMessage RetrieveTemplate(string templateName)
        {
            int GlobalAccountID = getCompanyIDFromOAuth();
            var response = new HttpResponseMessage();
            TemplateManager tm = new TemplateManager(GlobalAccountID);
            response.Content = new StringContent(tm.getFeature(templateName));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

    }
}

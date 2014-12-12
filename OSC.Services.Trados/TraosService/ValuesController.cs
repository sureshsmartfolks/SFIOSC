using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sdl.LanguagePlatform.TranslationMemory;

namespace TraosService
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get(string request)
        {
            //return new string[] { "value1", "value2" };
            Search search = new Search();
            bool searchTarget = false;

            var result = search.SearchForText(@"C:\AirFrance_SpecFrenchtoSpecEnglish.sdltm", request, SearchMode.NormalSearch);
            return result;
        }



        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

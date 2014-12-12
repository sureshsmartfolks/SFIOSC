using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace OwinSelfhostSample
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "https://localhost:10281/";

            // Start OWIN host
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values
                //HttpClient client = new HttpClient();
                Console.WriteLine("Server started on " + baseAddress);
                //HttpResponseMessage response = client.GetAsync(baseAddress + "api/values").Result;
                while (true)
                {
                }
                //Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.ReadLine();
        }
    }
}

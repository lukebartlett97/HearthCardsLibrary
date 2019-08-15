using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthCardsLibrary
{

    abstract class APIcaller
    {


        protected string BaseURL { get; set; }

        protected string PostRequest(string extURL, RestRequest request)
        {
            var client = new RestClient(BaseURL + extURL);
            Console.WriteLine("POST Request made to: " + BaseURL + extURL);
            IRestResponse response = client.Execute(request);
            Console.WriteLine("POST Response: " + response.Content);
            return response.Content;
        }

        protected string GetRequest(string extURL)
        {
            var client = new RestClient(BaseURL + extURL);
            var request = new RestRequest(Method.GET);
            Console.WriteLine("GET Request made to: " + BaseURL + extURL);
            IRestResponse response = client.Execute(request);
            Console.WriteLine("GET Response: " + response.Content);
            return response.Content;
        }

        protected byte[] DownloadData(string extURL, string savePath)
        {
            try
            {
                var client = new RestClient(BaseURL + extURL);
                var request = new RestRequest(Method.GET);
                byte[] response = client.DownloadData(request);
                File.WriteAllBytes(savePath, response);
                return response;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}

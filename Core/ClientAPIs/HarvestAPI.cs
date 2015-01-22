using Harvest.Net;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RoundTheClock.Core.ClientAPIs
{
    public class HarvestAPI
    {
        private readonly HarvestRestClient _client;

        public HarvestAPI() { }

        // smh: need this?
        public HarvestAPI(HarvestRestClient client)
        {
            _client = client;
        }

        public static bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public void GetProjects(string subdomain, string email, string password)
        {
            var url = string.Format("https://{0}.harvestapp.com/projects", subdomain);
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Accept = "application/xml";
            request.ContentType = "application/xml";
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(email + ":" + password)));
            request.MaximumAutomaticRedirections = 1;
            request.AllowAutoRedirect = true;
            request.UserAgent = "harvest_api_sample.cs";
            ServicePointManager.ServerCertificateValidationCallback = Validator;

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response != null)
                {
                    var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    var responseBody = reader.ReadToEnd();
                }
            }
        }
    }
}
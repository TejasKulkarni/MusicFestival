using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Music.World.Common
{
    public class ProxyService
    {
        private string _baseAddress;

        public ProxyService(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        public HttpResponseMessage Get(string url)
        {
            return ApiClient.GetAsync(url).Result;
        }

        protected virtual HttpClient ApiClient
        {
            get
            {
                var handler = new HttpClientHandler() { UseDefaultCredentials = true };

                var client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(_baseAddress)
                };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return client;
            }
        }
    }
}

using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MyClass
{
    public class MyClassClient
    {
        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:86.0) Gecko/20100101 Firefox/86.0";

        private CookieContainer _cookieContainer { get; }
        private HttpClientHandler _httpClientHandler { get; }
        private HttpClient _httpClient { get; }

        private bool _loggedIn { get; set; }

        public MyClassClient()
        {
            _cookieContainer = new CookieContainer();
            _httpClientHandler = new HttpClientHandler()
            {
                CookieContainer = _cookieContainer
            };

            _httpClient = new HttpClient(_httpClientHandler);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            _loggedIn = false;
        }

        public async Task<bool> Login(string username, string password)
        {
            if (!_loggedIn)
            {           
                Dictionary<string, string> param = new Dictionary<string, string>
                {
                    { "Username", username },
                    { "Password", password }
                };

                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(MyClassApi.Login),
                    Content = new FormUrlEncodedContent(param)
                };

                var response = await _httpClient.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();
                    var loginData = await JsonSerializer.DeserializeAsync<MyClassLoginData>(contentStream);

                    _loggedIn = loginData.Status;
                }
            }

            return _loggedIn;
        }

        public async Task<List<MyClassViconData>> GetViconSchedule()
        {
            List<MyClassViconData> schedule = new List<MyClassViconData>();

            if (_loggedIn)
            {
                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(MyClassApi.GetViconSchedule)
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();
                    schedule = await JsonSerializer.DeserializeAsync<List<MyClassViconData>>(contentStream);
                }
            }

            return schedule;
        }
    }
}

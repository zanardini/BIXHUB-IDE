using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace BixHubWrapper
{
    public class IdeCaller
    {
        private readonly string _url;
        private string _accessToken;

        public IdeCaller(string url) 
        {
            _url = url;
        }

        public async void Login(string clientId, string secret)
        {
            HttpClient client = new HttpClient();
            var request = new
            {
                client_Id = clientId,
                client_secret = secret,
                grant_Type = "client_credentials"
            };
            StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_url}/AuthorizationService/api/oauth/TokenFromUser", content);
            string json = await response.Content.ReadAsStringAsync();
            var accessTokenDto = JsonConvert.DeserializeObject<AccessTokenDto>(json);
            _accessToken = accessTokenDto.access_token;
        }

       



    }

    public class AccessTokenDto
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }        
    }
}
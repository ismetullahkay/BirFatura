using BirFatura.Models;
using Newtonsoft.Json;

namespace BirFatura.Services
{
    public class TokenService
    {
        
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TokenResponse> GetTokenAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var parameters = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "username", "test@test.com" },
                    { "password", "Test123." }
                };

                var content=new FormUrlEncodedContent(parameters);
                
                HttpResponseMessage response= await client.PostAsync("http://istest.birfatura.net/token", content);

                if (response.IsSuccessStatusCode)
                {
                    string json=await response.Content.ReadAsStringAsync();

                    TokenResponse tokenResponse=JsonConvert.DeserializeObject<TokenResponse>(json);
                    return tokenResponse;
                }
                else
                {
                    throw new Exception("Token Alınamadı. Status Code: "+response.StatusCode);
                }
           
        }
        
    }
}

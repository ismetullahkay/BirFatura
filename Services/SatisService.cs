using BirFatura.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BirFatura.Services
{
    public class SatisService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SatisService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<Satis>> GetSatislarAsync(string token)
        {
            var client =_httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PostAsync("http://istest.birfatura.net/api/test/SatislarGetir",null);

            if (response.IsSuccessStatusCode)
            {
                string json=await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response: " + json);
                var satislar=JsonConvert.DeserializeObject<List<Satis>>(json);
                if (satislar == null || satislar.Count == 0)
                {
                    Console.WriteLine("Veri boş geldi!");
                }
                return satislar;
            }
            else
            {
                throw new Exception("Satışlar getirilirken bir hata oluştu. Status Code: " + response.StatusCode);
            }
        }
    }
}

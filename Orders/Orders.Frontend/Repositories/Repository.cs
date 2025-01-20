
using System.Text;
using System.Text.Json;

namespace Orders.Frontend.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;
        private  JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public Repository(HttpClient HttpClient)
        {
            _httpClient = HttpClient;
        }
        public async Task<HttpResponseWraper<T>> GetAsync<T>(string url)
        {
            var responseHttp=await _httpClient.GetAsync(url);
            if (responseHttp.IsSuccessStatusCode)
            { 
                var response= await UnserializeAnswer<T>(responseHttp);
                return new HttpResponseWraper<T>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWraper<T>(default, true, responseHttp);

            }
            //throw new NotImplementedException();
        }

       

        public async Task<HttpResponseWraper<object>> PostAsync<T>(string url, T model)
        {
            var messageJson=JsonSerializer.Serialize(model);
            var messageContent= new StringContent(messageJson,Encoding.UTF8,"application/json");
            var responseHttp = await _httpClient.PostAsync(url,messageContent);
                      
                return new HttpResponseWraper<object>(default, !responseHttp.IsSuccessStatusCode, responseHttp);

           
            //throw new NotImplementedException();
        }

        public async Task<HttpResponseWraper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model)
        {
            var messageJson = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messageContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<TActionResponse>(responseHttp);
                return new HttpResponseWraper<TActionResponse>(response, false, responseHttp);
            } 
           
                return new HttpResponseWraper<TActionResponse>(default, true, responseHttp);

           
        }

        private async Task<T> UnserializeAnswer<T>(HttpResponseMessage responseHttp)
        {
           var response=await responseHttp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, _jsonDefaultOptions)!;
        }
    }
}

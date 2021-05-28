using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Nano35.Contracts;
using Nano35.HttpContext;
using Newtonsoft.Json;

namespace Nano35.WebClient.Services
{
    public class HttpGetRequest<TResponse>
        where TResponse : IHttpResponse
    {
        private readonly HttpClient _httpClient;
        private readonly string _uri;
        public HttpGetRequest(HttpClient httpClient, string uri)
        {
            _httpClient = httpClient;
            _uri = uri;
        }
        public async Task<TResponse> GetAsync()
        {
            var response = await _httpClient.GetAsync(_uri);
            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<TResponse>();
            else throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }

    public class HttpPostRequest<TBody, TResponse>
        where TBody : IHttpRequest
        where TResponse : IHttpResponse
    {
        private readonly HttpClient _httpClient;
        private readonly string _uri;
        public HttpPostRequest(HttpClient httpClient, string uri)
        {
            _httpClient = httpClient;
            _uri = uri;
        }
        public async Task<TResponse> PostAsync(TBody request)
        {
            var req = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_uri, req);
            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<TResponse>();
            else throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
    
    public class HttpPatchRequest<TBody, TResponse>
        where TBody : IHttpRequest
        where TResponse : IHttpResponse
    {
        private readonly HttpClient _httpClient;
        private readonly string _uri;
        public HttpPatchRequest(HttpClient httpClient, string uri)
        {
            _httpClient = httpClient;
            _uri = uri;
        }
        public async Task<TResponse> PatchAsync(TBody request)
        {
            var req = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync(_uri, req);
            if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<TResponse>();
            else throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
    
    public class HttpDeleteRequest< TResponse>
        where TResponse : IHttpResponse
        {
            private readonly HttpClient _httpClient;
            private readonly string _uri;
            public HttpDeleteRequest(HttpClient httpClient, string uri)
            {
                _httpClient = httpClient;
                _uri = uri;
            }
            public async Task<TResponse> DeleteAsync()
            {
                var response = await _httpClient.DeleteAsync(_uri);
                if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<TResponse>();
                else throw new Exception(await response.Content.ReadFromJsonAsync<string>());
            }
        }
}

// (await new HttpGetRequest<GetInstanceByIdHttpResponse>(HttpClient, $"{RequestManager.}/Instances/{id}").GetAsync()).Data;
// await new HttpPostRequest<CreateArticleHttpBody,CreateArticleSuccessHttpResponse>(HttpClient, $"{RequestManager.}/Articles").PostAsync(_model);
// await new HttpPatchRequest<UpdateArticleInfoHttpBody, UpdateArticleInfoSuccessHttpResponse>(HttpClient, $"{RequestManager.}/Articles/Info").PatchAsync(InfoEdit)
// await new HttpDeleteRequest<DeleteClientSuccessHttpResponse>(HttpClient, $"{RequestManager.}/Clients/{Client.Id}").DeleteAsync();

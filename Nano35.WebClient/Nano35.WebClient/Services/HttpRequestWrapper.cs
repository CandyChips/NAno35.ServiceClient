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
        
    public class HttpRequestWrapper<TRequest, TSuccess>
        where TRequest : IHttpRequest
        where TSuccess : IHttpResponse
    {
        private readonly HttpClient _httpClient;
        private readonly string _addr;

        public HttpRequestWrapper(HttpClient httpClient, string addr)
        {
            _httpClient = httpClient;
            _addr = addr;
        }

        public async Task<TSuccess> GetAsync()
        {
            var response = await _httpClient.GetAsync(_addr);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<TSuccess>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
        public async Task<TSuccess> PostAsync(TRequest request)
        {
            var req = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_addr, req);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<TSuccess>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
        public async Task<TSuccess> PatchAsync(TRequest request)
        {
            var req = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync(_addr, req);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<TSuccess>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}
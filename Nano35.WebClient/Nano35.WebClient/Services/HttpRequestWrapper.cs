using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext;
using Newtonsoft.Json;

namespace Nano35.WebClient.Services
{
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

        public async Task<HttpResponse<TSuccess>> GetAsync()
        {
            var response = await _httpClient.GetAsync(_addr);
            if (response.IsSuccessStatusCode)
            {
                return new HttpResponse<TSuccess>(await response.Content.ReadFromJsonAsync<TSuccess>());
            }
            else
            {
                
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
    public class HttpResponse<TSuccess> where TSuccess : IHttpResponse
    {
        public HttpResponse() { }
        public HttpResponse(TSuccess success) { Success = success; Error = (string) null; }
        public HttpResponse(string error) => this.Error = error;
        public bool IsSuccess() => this.Error == null;
        public TSuccess Success { get; private set; }
        public string Error { get; private set; }
    }
}
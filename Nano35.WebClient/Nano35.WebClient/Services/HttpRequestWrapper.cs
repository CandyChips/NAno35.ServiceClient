using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext;
using Newtonsoft.Json;
using Radzen;

namespace Nano35.WebClient.Services
{
    public class HttpGetRequest<TResponse>
        where TResponse : IHttpResponse, new()
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
            var a = _uri.Split("/");
            var uri = string.Join("/",a,0,5);
            var endpoint = string.Join("/", a, 5, a.Length - 5);
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
    
    public class HttpDeleteRequest<TResponse>
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

    public class HealthService
    {
        private readonly NotificationService _notificationService;
        private readonly HttpClient _httpClient;

        public HealthService(NotificationService notificationService, HttpClient httpClient)
        {
            _notificationService = notificationService;
            _httpClient = httpClient;
        }

        public async Task<bool> CheckAsync(string uri)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{uri}/health");
                if (response.IsSuccessStatusCode == false) _notificationService.Notify(NotificationSeverity.Warning, "Error access", uri);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
    public class HttpGet
    {
        private readonly HttpClient _httpClient;
        private readonly HealthService _healthService;
        public HttpGet(HttpClient httpClient, HealthService healthService)
        {
            _httpClient = httpClient;
            _healthService = healthService;
        }
        public async Task InvokeAsync<TResponse>(string uri, string endpoint, Action<UseCaseResponse<TResponse>> onResponse)
            where TResponse : IHttpResponse
        {
            if (await _healthService.CheckAsync(uri) == false)
            {
                onResponse.Invoke(new UseCaseResponse<TResponse>("Health check failed"));
                return;
            }
            var response = await _httpClient.GetAsync($"{uri}/{endpoint}");
            onResponse.Invoke(response.IsSuccessStatusCode ?
                new UseCaseResponse<TResponse>(await response.Content.ReadFromJsonAsync<TResponse>()) :
                new UseCaseResponse<TResponse>(await response.Content.ReadAsStringAsync()));
        }
    }
    
    public class HttpPost
    {
        private readonly HttpClient _httpClient;
        private readonly HealthService _healthService;
        public HttpPost(HttpClient httpClient, HealthService healthService)
        {
            _httpClient = httpClient;
            _healthService = healthService;
        }
        public async Task InvokeAsync<TResponse, TBody>(string uri, string endpoint, TBody request, Action<UseCaseResponse<TResponse>> onResponse)
            where TResponse : IHttpResponse
            where TBody : IHttpRequest
        {
            if (await _healthService.CheckAsync(uri) == false)
            {
                onResponse.Invoke(new UseCaseResponse<TResponse>("Health check failed"));
                return;
            }
            var req = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{uri}/{endpoint}", req);
            onResponse.Invoke(response.IsSuccessStatusCode ?
                new UseCaseResponse<TResponse>(await response.Content.ReadFromJsonAsync<TResponse>()) :
                new UseCaseResponse<TResponse>(await response.Content.ReadAsStringAsync()));
        }
    }
    
    public class HttpPatch
    {
        private readonly HttpClient _httpClient;
        private readonly HealthService _healthService;
        public HttpPatch(HttpClient httpClient, HealthService healthService)
        {
            _httpClient = httpClient;
            _healthService = healthService;
        }
        public async Task InvokeAsync<TResponse, TBody>(string uri, string endpoint, TBody request, Action<UseCaseResponse<TResponse>> onResponse)
            where TResponse : IHttpResponse
            where TBody : IHttpRequest
        {
            if (await _healthService.CheckAsync(uri) == false)
            {
                onResponse.Invoke(new UseCaseResponse<TResponse>("Health check failed"));
                return;
            }
            var req = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"{uri}/{endpoint}", req);
            onResponse.Invoke(response.IsSuccessStatusCode ?
                new UseCaseResponse<TResponse>(await response.Content.ReadFromJsonAsync<TResponse>()) :
                new UseCaseResponse<TResponse>(await response.Content.ReadAsStringAsync()));
        }
    }
    
    public class HttpDelete
    {
        private readonly HttpClient _httpClient;
        private readonly HealthService _healthService;
        public HttpDelete(HttpClient httpClient, HealthService healthService)
        {
            _httpClient = httpClient;
            _healthService = healthService;
        }
        public async Task InvokeAsync<TResponse>(string uri, string endpoint, Action<UseCaseResponse<TResponse>> onResponse)
            where TResponse : IHttpResponse
        {
            if (await _healthService.CheckAsync(uri) == false)
            {
                onResponse.Invoke(new UseCaseResponse<TResponse>("Health check failed"));
                return;
            }
            var response = await _httpClient.DeleteAsync($"{uri}/{endpoint}");
            onResponse.Invoke(response.IsSuccessStatusCode ?
                new UseCaseResponse<TResponse>(await response.Content.ReadFromJsonAsync<TResponse>()) :
                new UseCaseResponse<TResponse>(await response.Content.ReadAsStringAsync()));
        }
    }
}
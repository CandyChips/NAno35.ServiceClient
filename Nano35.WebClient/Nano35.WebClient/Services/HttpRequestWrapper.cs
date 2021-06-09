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
            onResponse.Invoke(await response.ReadAsync<UseCaseResponse<TResponse>>());
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
            onResponse.Invoke(await response.ReadAsync<UseCaseResponse<TResponse>>());
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
            onResponse.Invoke(await response.ReadAsync<UseCaseResponse<TResponse>>());
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
            onResponse.Invoke(await response.ReadAsync<UseCaseResponse<TResponse>>());
        }
    }
}
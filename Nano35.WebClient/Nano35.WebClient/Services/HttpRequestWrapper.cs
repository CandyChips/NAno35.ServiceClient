using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Nano35.Communication;
using Nano35.Contexts.Http;
using Newtonsoft.Json;
using Radzen;

namespace Nano35.WebClient.Services
{
    public class RequestManager
    {
        public RequestManager(HttpClient httpClient) => _httpClient = httpClient;

        private readonly string _proxyUri = "https://nano35.ru/api";
        //private readonly string _proxyUri = "http://localhost:8080";
        
        private readonly HttpClient _httpClient;
        public string ProxyUri => _proxyUri;
    }
    
    public class HealthService
    {
        private readonly NotificationService _notificationService;
        private readonly HttpClient _httpClient;
        private readonly RequestManager _requestManager;

        public HealthService(NotificationService notificationService, HttpClient httpClient)
        {
            _notificationService = notificationService;
            _httpClient = httpClient;
            _requestManager = new RequestManager(_httpClient);
        }

        public async Task<bool> CheckAsync()
        {
            try            {
                var response = await _httpClient.GetAsync($"{_requestManager.ProxyUri}/health");
                if (!response.IsSuccessStatusCode) _notificationService.Notify(NotificationSeverity.Warning, "Error access", _requestManager.ProxyUri);
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
        private readonly RequestManager _requestManager;
        private readonly NotificationService _notificationService;
        public HttpGet(HttpClient httpClient, NotificationService notificationService)
        {
            _httpClient = httpClient;
            _notificationService = notificationService;
            _requestManager = new RequestManager(_httpClient);
        }
        public async Task InvokeAsync<TResponse>(string endpoint, Action<UseCaseResponse<TResponse>> onResponse)
            where TResponse : IResult
        {
            var response = await _httpClient.GetAsync($"{_requestManager.ProxyUri}/{endpoint}");
            if(response.IsSuccessStatusCode)
            {
                onResponse.Invoke(await response.Content.ReadFromJsonAsync<UseCaseResponse<TResponse>>());
            }
            else
            {
                _notificationService.Notify(NotificationSeverity.Warning, "Error access", _requestManager.ProxyUri);
            }
        }
    }
    
    public class HttpPost
    {
        private readonly HttpClient _httpClient;
        private readonly RequestManager _requestManager;
        private readonly NotificationService _notificationService;
        public HttpPost(HttpClient httpClient, NotificationService notificationService)
        {
            _httpClient = httpClient;
            _notificationService = notificationService;
            _requestManager = new RequestManager(_httpClient);
        }
        public async Task InvokeAsync<TResponse, TBody>(string endpoint, TBody request, Action<UseCaseResponse<TResponse>> onResponse)
            where TResponse : IHttpResponse
            where TBody : IHttpRequest
        {
            var req = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_requestManager.ProxyUri}/{endpoint}", req);
            if(response.IsSuccessStatusCode)
            {
                onResponse.Invoke(await response.Content.ReadFromJsonAsync<UseCaseResponse<TResponse>>());
            }
            else
            {
                _notificationService.Notify(NotificationSeverity.Warning, "Error access", _requestManager.ProxyUri);
            }
        }
    }
    
    public class HttpPatch
    {
        private readonly HttpClient _httpClient;
        private readonly RequestManager _requestManager;
        private readonly NotificationService _notificationService;
        public HttpPatch(HttpClient httpClient, NotificationService notificationService)
        {
            _httpClient = httpClient;
            _notificationService = notificationService;
            _requestManager = new RequestManager(_httpClient);
        }
        public async Task InvokeAsync<TResponse, TBody>(string endpoint, TBody request, Action<UseCaseResponse<TResponse>> onResponse)
            where TResponse : IHttpResponse
            where TBody : IHttpRequest
        {
            var req = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"{_requestManager.ProxyUri}/{endpoint}", req);
            if(response.IsSuccessStatusCode)
            {
                onResponse.Invoke(await response.Content.ReadFromJsonAsync<UseCaseResponse<TResponse>>());
            }
            else
            {
                _notificationService.Notify(NotificationSeverity.Warning, "Error access", _requestManager.ProxyUri);
            }
        }
    }
    
    public class HttpDelete
    {
        private readonly HttpClient _httpClient;
        private readonly RequestManager _requestManager;
        private readonly NotificationService _notificationService;
        public HttpDelete(HttpClient httpClient,NotificationService notificationService)
        {
            _httpClient = httpClient;
            _notificationService = notificationService;
            _requestManager = new RequestManager(_httpClient);
        }
        public async Task InvokeAsync<TResponse>(string endpoint, Action<UseCaseResponse<TResponse>> onResponse)
            where TResponse : IHttpResponse
        {
            var response = await _httpClient.DeleteAsync($"{_requestManager.ProxyUri}/{endpoint}");
            if(response.IsSuccessStatusCode)
            {
                onResponse.Invoke(await response.Content.ReadFromJsonAsync<UseCaseResponse<TResponse>>());
            }
            else
            {
                _notificationService.Notify(NotificationSeverity.Warning, "Error access", _requestManager.ProxyUri);
            }
        }
    }
}
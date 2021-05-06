using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public interface IInstancesService
    {
        Task<GetAllInstancesSuccessHttpResponse> GetAllInstances();
    }
    
    public class InstancesService :
        IInstancesService
    {
        private readonly HttpClient _httpClient;
        private readonly IRequestManager _requestManager;

        public InstancesService(HttpClient httpClient, IRequestManager requestManager)
        {
            _httpClient = httpClient;
            _requestManager = requestManager;
        }

        public async Task<GetAllInstancesSuccessHttpResponse> GetAllInstances()
        {
            var response = await _httpClient.GetAsync($"{_requestManager.InstanceServer}/Instances/Current");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllInstancesSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
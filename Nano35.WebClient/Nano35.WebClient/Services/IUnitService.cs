using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public interface IUnitService
    {
        Task<GetAllUnitsSuccessHttpResponse> GetAllUnit(Guid id);
        Task<GetAllUnitTypesSuccessHttpResponse> GetAllUnitTypes();
    }

    public class UnitService : IUnitService
    {
        private readonly HttpClient _httpClient;
        private readonly IRequestManager _requestManager;

        public UnitService(HttpClient httpClient, IRequestManager requestManager)
        {
            _httpClient = httpClient;
            _requestManager = requestManager;
        }
        
        public async Task<GetAllUnitsSuccessHttpResponse> GetAllUnit(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_requestManager.InstanceServer}/Units?InstanceId={id}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllUnitsSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
        
        public async Task<GetAllUnitTypesSuccessHttpResponse> GetAllUnitTypes()
        {
            var response = await _httpClient.GetAsync($"{_requestManager.InstanceServer}/Units/GetAllUnitTypes");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllUnitTypesSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
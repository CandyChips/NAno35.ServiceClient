using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public interface IRepairOrdersService
    {
        Task<GetAllRepairOrdersSuccessHttpResponse> GetAllRepairOrders(Guid instanceId);
        Task<GetAllRepairOrdersDetailsSuccessHttpResponse> GetAllRepairOrdersDetails(Guid instanceId);

    }
    
    public class RepairOrdersService :
        IRepairOrdersService
    {
        private readonly HttpClient _httpClient;
        private readonly IRequestManager _requestManager;

        public RepairOrdersService(
            HttpClient httpClient,
            IRequestManager requestManager)
        {
            _httpClient = httpClient;
            _requestManager = requestManager;
            
        }

        public async Task<GetAllRepairOrdersSuccessHttpResponse> GetAllRepairOrders(Guid instanceId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5004/RepairOrder/GetAllRepairOrders?InstanceId={instanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllRepairOrdersSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
        
        public async Task<GetAllRepairOrdersDetailsSuccessHttpResponse> GetAllRepairOrdersDetails(Guid instanceId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5004/RepairOrder/GetAllRepairOrdersDetails?InstanceId={instanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllRepairOrdersDetailsSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
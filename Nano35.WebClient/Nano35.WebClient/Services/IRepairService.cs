using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.Repair;
using Nano35.HttpContext.storage;
using Nano35.WebClient.Pages;

namespace Nano35.WebClient.Services
{
    public interface IRepairOrdersService
    {
        Task<GetAllRepairOrdersSuccessHttpResponse> GetAllRepairOrders(Guid instanceId);
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
    }
}
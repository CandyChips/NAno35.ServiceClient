using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class UpdateRepairOrdersWorkRequest : 
        RequestProvider<CreateRepairOrderWorkBody, CreateRepairOrderWorkSuccessResponse>
    {
        public UpdateRepairOrdersWorkRequest(IRequestManager requestManager, HttpClient httpClient, CreateRepairOrderWorkBody request) : 
            base(requestManager, httpClient, request) {}

        public override async Task<CreateRepairOrderWorkSuccessResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"http://localhost:5004/RepairOrders/Work", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateRepairOrderWorkSuccessResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
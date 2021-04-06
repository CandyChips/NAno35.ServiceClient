using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class UpdateRepairOrdersWorkRequest : 
        RequestProvider<UpdateRepairOrdersWorkHttpBody, UpdateRepairOrdersWorkHttpResponse>
    {
        public UpdateRepairOrdersWorkRequest(IRequestManager requestManager, HttpClient httpClient, UpdateRepairOrdersWorkHttpBody request) : 
            base(requestManager, httpClient, request) {}

        public override async Task<UpdateRepairOrdersWorkHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"http://localhost:5004/RepairOrder/UpdateRepairOrdersWork", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<UpdateRepairOrdersWorkHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
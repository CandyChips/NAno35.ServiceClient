using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class UpdateRepairOrdersStateRequest : 
        RequestProvider<UpdateRepairOrdersStateHttpBody, UpdateRepairOrdersStateHttpResponse>
    {
        public UpdateRepairOrdersStateRequest(IRequestManager requestManager, HttpClient httpClient, UpdateRepairOrdersStateHttpBody request) : 
            base(requestManager, httpClient, request) {}

        public override async Task<UpdateRepairOrdersStateHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"http://localhost:5004/RepairOrder/UpdateRepairOrdersState", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<UpdateRepairOrdersStateHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
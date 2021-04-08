using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class SetCurrentUserAsPerformerOfRepairOrderRequest : 
        RequestProvider<SetCurrentUserAsPerformerOfRepairOrderQuery, SetCurrentUserAsPerformerOfRepairOrderSuccessResponse>
    {
        public SetCurrentUserAsPerformerOfRepairOrderRequest(IRequestManager requestManager, HttpClient httpClient, SetCurrentUserAsPerformerOfRepairOrderQuery request) : 
            base(requestManager, httpClient, request) { }

        public override async Task<SetCurrentUserAsPerformerOfRepairOrderSuccessResponse> Send()
        {
            var response = await HttpClient.GetAsync($"http://localhost:5004/Work/SetCurrentUserAsPerformerOfRepairOrder?CurrentUserId={Request.RepairOrderId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<SetCurrentUserAsPerformerOfRepairOrderSuccessResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}
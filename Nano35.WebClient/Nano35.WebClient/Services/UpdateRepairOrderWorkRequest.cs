using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;

namespace Nano35.WebClient.Services
{
    public class UpdateRepairOrderWorkRequest : 
        RequestProvider<CreateRepairOrderWorkHttpBody, CreateRepairOrderWorkHttpResponse>
    {
        public UpdateRepairOrderWorkRequest(IRequestManager requestManager, HttpClient httpClient, CreateRepairOrderWorkHttpBody request) : 
            base(requestManager, httpClient, request) {}

        public override async Task<CreateRepairOrderWorkHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.RepairOrdersServer}/RepairOrders/Work", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateRepairOrderWorkHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class CreateRepairOrderRequest : 
        RequestProvider<CreateRepairOrderHttpBody, CreateRepairOrderHttpResponse>
    {
        public CreateRepairOrderRequest(IRequestManager requestManager, HttpClient httpClient, CreateRepairOrderHttpBody request) : 
            base(requestManager, httpClient, request)
        { }

        public override async Task<CreateRepairOrderHttpResponse> Send()
        {         
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.RepairOrdersServer}/RepairOrders", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateRepairOrderHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetRepairOrderByIdRequest : 
        RequestProvider<GetRepairOrderByIdHttpQuery, GetRepairOrderByIdHttpResponse>
    {
        public GetRepairOrderByIdRequest(IRequestManager requestManager, HttpClient httpClient, GetRepairOrderByIdHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetRepairOrderByIdHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.RepairOrdersServer}/RepairOrder/GetRepairOrderById?Id={Request.Id}");
            if (response.IsSuccessStatusCode) 
            {
                return (await response.Content.ReadFromJsonAsync<GetRepairOrderByIdHttpResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}
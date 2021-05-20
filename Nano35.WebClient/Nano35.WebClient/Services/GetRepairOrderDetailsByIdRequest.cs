using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetRepairOrderDetailsByIdRequest : 
        RequestProvider<GetRepairOrderDetailsByIdHttpQuery, GetRepairOrderDetailsByIdHttpResponse>
    {
        public GetRepairOrderDetailsByIdRequest(IRequestManager requestManager, HttpClient httpClient, GetRepairOrderDetailsByIdHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetRepairOrderDetailsByIdHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.RepairOrdersServer}/RepairOrders/{Request.Id}/Details");
            if (response.IsSuccessStatusCode) 
            {
                return (await response.Content.ReadFromJsonAsync<GetRepairOrderDetailsByIdHttpResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}
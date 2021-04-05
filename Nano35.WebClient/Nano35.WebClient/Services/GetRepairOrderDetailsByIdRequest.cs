using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.Repair;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetRepairOrderDetailsByIdRequest : 
        RequestProvider<GetRepairOrderDetailsByIdHttpBody, GetRepairOrderDetailsByIdSuccessHttpResponse>
    {
        public GetRepairOrderDetailsByIdRequest(IRequestManager requestManager, HttpClient httpClient, GetRepairOrderDetailsByIdHttpBody request) : 
            base(requestManager, httpClient, request)
        {
        }

        public override async Task<GetRepairOrderDetailsByIdSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"http://localhost:5004/RepairOrder/GetRepairOrderDetailsById?Id={Request.Id}");
            if (response.IsSuccessStatusCode) 
            {
                return (await response.Content.ReadFromJsonAsync<GetRepairOrderDetailsByIdSuccessHttpResponse>());
            }
            throw new Exception(await response.Content.ReadFromJsonAsync<string>());
        }
    }
}
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllComingDetailsRequest : 
        RequestProvider<GetComingDetailsByIdHttpQuery, GetComingDetailsByIdSuccessHttpResponse>
    {
        public GetAllComingDetailsRequest(IRequestManager requestManager, HttpClient httpClient, GetComingDetailsByIdHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetComingDetailsByIdSuccessHttpResponse> Send()
        {
            var url = $"http://localhost:5003/Warehouse/GetComingDetails?ComingId={Request.Id}";
            var response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetComingDetailsByIdSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
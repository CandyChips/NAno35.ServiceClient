using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class GetAllInstanceTypesRequest : 
        RequestProvider<GetAllInstanceTypesHttpQuery, GetAllInstanceTypesSuccessHttpResponse>
    {
        public GetAllInstanceTypesRequest(IRequestManager requestManager, HttpClient httpClient, GetAllInstanceTypesHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetAllInstanceTypesSuccessHttpResponse> Send()
        {
            var response = await HttpClient.GetAsync($"{RequestManager.InstanceServer}/Instances/GetAllInstanceTypes");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllInstanceTypesSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
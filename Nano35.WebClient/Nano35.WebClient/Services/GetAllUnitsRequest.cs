using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;

namespace Nano35.WebClient.Services
{
    public class GetAllUnitsRequest :
        RequestProvider<GetAllUnitsHttpQuery, GetAllUnitsSuccessHttpResponse>
    {
        public GetAllUnitsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllUnitsHttpQuery request) :
            base(requestManager, httpClient, request)
        {

        }
        

        public override async Task<GetAllUnitsSuccessHttpResponse> Send()
        {
            var response =
                await HttpClient.GetAsync(
                    $"{RequestManager.InstanceServer}/Units?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllUnitsSuccessHttpResponse>());
            }

            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
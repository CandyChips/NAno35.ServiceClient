using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllMovesRequest :
        RequestProvider<GetAllMovesHttpQuery, GetAllMovesSuccessHttpResponse>
    {
        public GetAllMovesRequest(IRequestManager requestManager, HttpClient httpClient, GetAllMovesHttpQuery request) :
            base(requestManager, httpClient, request)
        {

        }

        public override async Task<GetAllMovesSuccessHttpResponse> Send()
        {
            var response =
                await HttpClient.GetAsync(
                    $"{RequestManager.StorageServer}/Moves?InstanceId={Request.InstanceId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllMovesSuccessHttpResponse>());
            }

            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
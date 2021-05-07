using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllComingsRequest :
        RequestProvider<GetAllComingsHttpQuery, GetAllComingsSuccessHttpResponse>
    {
        public GetAllComingsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllComingsHttpQuery request) :
            base(requestManager, httpClient, request)
        {

        }

        public override async Task<GetAllComingsSuccessHttpResponse> Send()
        {
            var response =
                await HttpClient.GetAsync(
                    $"{RequestManager.LocalStorageServer}/Comings?InstanceId={Request.InstanceId}"+ (Request.UnitId == Guid.Empty ? "" : $"&UnitId={Request.UnitId}"));
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllComingsSuccessHttpResponse>());
            }

            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
    
}
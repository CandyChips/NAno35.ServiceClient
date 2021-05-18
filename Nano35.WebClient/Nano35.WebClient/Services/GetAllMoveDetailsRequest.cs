using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class GetAllMoveDetailsRequest : 
        RequestProvider<GetAllMoveDetailsHttpQuery, GetAllMoveDetailsSuccessHttpResponse>
    {
        public GetAllMoveDetailsRequest(IRequestManager requestManager, HttpClient httpClient, GetAllMoveDetailsHttpQuery request) : 
            base(requestManager, httpClient, request)
        {
            
        }

        public override async Task<GetAllMoveDetailsSuccessHttpResponse> Send()
        {
            var url = $"{RequestManager.StorageServer}/Moves/{Request.MoveId}/Details";
            var response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<GetAllMoveDetailsSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
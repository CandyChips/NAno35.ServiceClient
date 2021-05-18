using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class CreateMoveRequest : 
        RequestProvider<CreateMoveHttpBody, CreateMoveSuccessHttpResponse>
    {
        public CreateMoveRequest(IRequestManager requestManager, HttpClient httpClient, CreateMoveHttpBody request) : 
            base(requestManager, httpClient, request)
        { }

        public override async Task<CreateMoveSuccessHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.StorageServer}/Moves", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateMoveSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
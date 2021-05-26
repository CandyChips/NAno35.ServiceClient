using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;
using Newtonsoft.Json;

namespace Nano35.WebClient.Services
{
    public class DeleteClientRequest : 
        RequestProvider<DeleteClientHttpBody, DeleteClientSuccessHttpResponse>
    {
        public DeleteClientRequest(IRequestManager requestManager, HttpClient httpClient, DeleteClientHttpBody request) : 
            base(requestManager, httpClient, request) {}
        

        public override async Task<DeleteClientSuccessHttpResponse> Send()
        {
            var response = await HttpClient.DeleteAsync($"{RequestManager.StorageServer}/Clients/{Request.ClientId}");
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<DeleteClientSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
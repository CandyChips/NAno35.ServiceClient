using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class CreateArticleRequest : 
        RequestProvider<CreateArticleHttpBody, CreateArticleSuccessHttpResponse>
    {
        public CreateArticleRequest(IRequestManager requestManager, HttpClient httpClient, CreateArticleHttpBody request) : 
            base(requestManager, httpClient, request)
        {  }

        public override async Task<CreateArticleSuccessHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.LocalStorageServer}/Articles/Article", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateArticleSuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
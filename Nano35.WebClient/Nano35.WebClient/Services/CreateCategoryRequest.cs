using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Nano35.HttpContext.storage;

namespace Nano35.WebClient.Services
{
    public class CreateCategoryRequest : 
        RequestProvider<CreateCategoryHttpBody, CreateCategorySuccessHttpResponse>
    {
        public CreateCategoryRequest(IRequestManager requestManager, HttpClient httpClient, CreateCategoryHttpBody request) : 
            base(requestManager, httpClient, request)
        {  }

        public override async Task<CreateCategorySuccessHttpResponse> Send()
        {
            var response = await HttpClient.PostAsJsonAsync($"{RequestManager.LocalStorageServer}/Category/CreateCategory", Request);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadFromJsonAsync<CreateCategorySuccessHttpResponse>());
            }
            throw new Exception((await response.Content.ReadFromJsonAsync<string>()));
        }
    }
}
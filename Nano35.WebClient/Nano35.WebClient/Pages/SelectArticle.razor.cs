using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Nano35.HttpContext.storage;
using Nano35.WebClient.Services;

namespace Nano35.WebClient.Pages
{
    public partial class SelectArticle : 
        ComponentBase
    {
        [Inject] private ISessionProvider SessionProvider { get; set; }
        [Inject] private IRequestManager RequestManager { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }
        [Parameter] public bool CanCreate { get; set; }
        [Parameter] public EventCallback<Guid> ArticlesIdChanged { get; set; }
        private Guid _value;
        [Parameter] public Guid ArticlesId 
        {
            get => _value;
            set
            {
                if (_value == value ) return;
                _value = value;
                Console.WriteLine(value);
                ArticlesIdChanged.InvokeAsync(value);
            }
        }
        
        private List<ArticleViewModel> Articles { get; set; }

        private List<ArticleViewModel> FiltratedArticles
        {
            get => Articles.Where(a => (a.Brand + a.Category + a.Model + a.Info).Contains(_filter)).ToList();
            set => FiltratedArticles = value;
        }
        private bool _isLoading = true;
        private bool _isNewArticleDisplay = false;
        private string _filter = string.Empty;
        
        protected override async Task OnInitializedAsync()
        {
            var request = new GetAllArticlesHttpQuery() {InstanceId = await SessionProvider.GetCurrentInstanceId()};
            Articles = (await new GetAllArticlesRequest(RequestManager, HttpClient, request).Send()).Data.ToList();
            _isLoading = false;
        }
    }
}
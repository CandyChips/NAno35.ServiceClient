using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Nano35.Contracts.Storage.Models;
using Nano35.HttpContext.storage;
using Nano35.WebClient.Services;

namespace Nano35.WebClient.Pages
{
    public partial class SelectStorageItem : 
        ComponentBase
    {
        [Inject] private ISessionProvider SessionProvider { get; set; }
        [Inject] private IRequestManager RequestManager { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }
        [Parameter] public bool CanCreate { get; set; }
        private Guid _value;
        [Parameter] public EventCallback<Guid> StorageItemIdChanged { get; set; }
        [Parameter] public Guid StorageItemId 
        {
            get => _value;
            set
            {
                if (_value == value ) return;
                _value = value;
                StorageItemIdChanged.InvokeAsync(value);
            }
        }
        
        private List<StorageItemViewModel> StorageItems { get; set; }
        private bool _isNewStorageItemDisplay = false;
        private bool _loading = true;

        protected override async Task OnInitializedAsync()
        {
            StorageItems = await GetAllStorageItems();
            _loading = false;
        }

        private async Task<List<StorageItemViewModel>> GetAllStorageItems() => 
            (await new GetAllStorageItemsRequest(RequestManager, HttpClient, new GetAllStorageItemsQuery() {InstanceId = await SessionProvider.GetCurrentInstanceId()}).Send()).Data.ToList();
    }
        
}
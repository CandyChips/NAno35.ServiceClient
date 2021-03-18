using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Nano35.Contracts.Instance.Models;
using Nano35.HttpContext.instance;
using Nano35.WebClient.Services;

namespace Nano35.WebClient.Pages
{
    public partial class ModalNewInstance : 
        ComponentBase
    {
        [Inject] private IRequestManager RequestManager { get; set; }
        [Inject] private IInstancesService InstancesService { get; set; }
        [Parameter] public EventCallback OnHideModalNewInstance { get; set; }

        private IEnumerable<IInstanceTypeViewModel> Types { get; set; }
        private IEnumerable<IRegionViewModel> Regions { get; set; }
        private CreateInstanceHttpBody _model = new CreateInstanceHttpBody();
        private bool _loading = true;
        private string _error = string.Empty;
        private bool _serverAvailable = false;

        protected override async Task OnInitializedAsync()
        {
            _serverAvailable = await RequestManager.HealthCheck(RequestManager.IdentityServer);
            Regions = (await InstancesService.GetAllRegions()).Data;
            Types = (await InstancesService.GetAllTypes()).Data;
            _loading = false;
        }

        private void HideModalNewStorageItem()
        {
            OnHideModalNewInstance.InvokeAsync();
        }
    }
}
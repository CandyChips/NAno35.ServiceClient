using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Nano35.WebClient.Services;
using InstanceViewModel = Nano35.HttpContext.instance.InstanceViewModel;

namespace Nano35.WebClient.Pages
{
    public partial class Instances :
        ComponentBase
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] private IRequestManager RequestManager { get; set; }
        [Inject] private IInstancesService InstancesService { get; set; }
        [Inject] private IInstanceService InstanceService { get; set; }
        [Inject] private ISessionProvider SessionProvider { get; set; }
        [Parameter] public EventCallback OnHideModalNewInstance { get; set; }
        [CascadingParameter] public IModalService Modal { get; set; }
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }

        private bool _serverAvailable = false;
        private bool _loading = true;

        private IEnumerable<InstanceViewModel> _data;
        
        protected override async Task OnInitializedAsync()
        {
            _serverAvailable = await RequestManager.HealthCheck(RequestManager.InstanceServer);
            _data = (await InstancesService.GetAllInstances()).Data;
            _loading = false;
        }

        private async Task OpenOrg(Guid id)
        {
            await InstanceService.SetInstanceById(id);         //get session
            await SessionProvider.SetCurrentInstanceId(id);    //set instance id
            NavigationManager.NavigateTo("/instance-view");
        }
    
        private async Task OpenNewInstance()
        {
            var moviesModal = Modal.Show<InstanceForm>("Новая организация");
            await moviesModal.Result;        
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.Repair;
using Nano35.WebClient.Services;

namespace Nano35.WebClient.Shared
{
    public partial class NavBar
    {
        //private List<Guid> _roles = new();

        //protected override async Task OnInitializedAsync()
        //{
        //    var id = (await AuthService.GetCurrentUser()).Data.Id; 
        //    _roles = (await new GetWorkerRolesByIdRequest(RequestManager, HttpClient, new GetAllRolesByUserHttpQuery() {UserId = id}).Send()).Roles;
        //}
    }
}
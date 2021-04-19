using geesRecorderClient.Client.Services;
using geesRecorderClient.Shared.DTOs;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace geesRecorderClient.Client.Pages
{
    public partial class Index : ComponentBase
    {

        [Inject]
        public Authentication Authentication { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }

        public SignUpDTO SignUpDTO { get; set; } = new SignUpDTO();

        public SignInDTO SignInDTO { get; set; } = new SignInDTO();

        public bool LoggedIn { get; set; }

        public bool SignUpToggle { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            LoggedIn = await Authentication.IsLoggedInAsync();
        }

        public async Task SignUpSubmit()
        {
            var result = await Authentication.SignUpAsync(SignUpDTO);
            if (result.Succeeded)
            {
                NavigationManager.NavigateTo(nameof(Dashboard));
            }
            else
            {
                Toaster.Add("Error!", MatToastType.Danger);
            }

        }

        public async Task SignInSubmit()
        {
            var result = await Authentication.SignInAsync(SignInDTO);
            if (result.Succeeded)
            {
                NavigationManager.NavigateTo(nameof(Dashboard));
            }
            else
            {
                Toaster.Add("Error!", MatToastType.Danger);
            }
        }

        public void Toggle() => SignUpToggle = !SignUpToggle;        
    }
}

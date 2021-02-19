using geesRecorderClient.Client.Services;
using geesRecorderClient.Shared.DTOs;
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

        public SignUpDTO SignUpDTO { get; set; } = new SignUpDTO();
        
        public bool LoggedIn { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LoggedIn = await Authentication.IsLoggedInAsync();
        }

        public async Task SignUpSubmit()
        {

        }

        public async Task SignInClicked()
        {

        }
    }
}

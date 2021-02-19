using Blazored.LocalStorage;
using geesRecorderClient.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace geesRecorderClient.Client.Services
{
    public class Authentication
    {
        private readonly HttpClient _httpClient;

        public Authentication(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoginAsync(SignInDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("client/logout", dto);
        }       

        public async Task LogoutAsync()
        {
            var response = await _httpClient.PostAsync("client/logout", new StringContent(""));
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var response = await _httpClient.GetAsync("client/state");
            return JsonSerializer.Deserialize<ServerStateDTO>(await response.Content.ReadAsStringAsync()).LoggedIn;
        }
    }
}
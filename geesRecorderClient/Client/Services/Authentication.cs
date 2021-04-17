using Blazored.LocalStorage;
using geesRecorderClient.Shared.DTOs;
using Microsoft.AspNetCore.Components;
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
            /*var response = */await _httpClient.PostAsJsonAsync("client/login", dto);
        }       

        public async Task LogoutAsync()
        {
            await _httpClient.PostAsync("client/logout", new StringContent(""));
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var response = await _httpClient.GetAsync("client/state");
            return JsonSerializer.Deserialize<ServerStateDTO>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }).LoggedIn;
        }

        public async Task<RouteLockDTO> GetRouteRestriction()
        {
            /*var response = */await _httpClient.GetAsync("client/state");
            return new RouteLockDTO
            {
                Locked = true,
                LockedRoute = "counter"
            };
        }
        
        public async Task SetRouteRestriction(RouteLockDTO dto)
        {
            /*var response = */await _httpClient.GetAsync("client/set-route-restriction");
        }

        public async Task<bool> VerifyPin(string inputPin)
        {
            var response = await _httpClient.GetAsync("client/state");
            string responseString = await response.Content.ReadAsStringAsync();
            var state = JsonSerializer.Deserialize<ServerStateDTO>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return state.Pin == inputPin;

        }
    }
}
using Blazored.LocalStorage;
using geesRecorderClient.Shared.DTOs;
using geesRecorderClient.Shared.Models;
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
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public Authentication(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var response = await _httpClient.GetAsync("auth/state");
            return JsonSerializer.Deserialize<ServerState>(await response.Content.ReadAsStringAsync(), _jsonOptions).LoggedIn;
        }

        public async Task<RouteLockDTO> GetRouteLockState()
        {
            var response = await _httpClient.GetAsync("auth/state");
            var state =  JsonSerializer.Deserialize<ServerState>(await response.Content.ReadAsStringAsync(), _jsonOptions);

            return new RouteLockDTO
            {
                Locked = state.RouteLockActivated,
                LockedRoute = state.LockedRoute
            };
        }
        
        public async Task LockRoute(string route)
        {
            var content = JsonContent.Create(new { Route = route });
            await _httpClient.PostAsync($"auth/lock-route?route={route}", content);
        }
        
        public async Task UnlockLockedRoute()
        {
            await _httpClient.PatchAsync("auth/unlock-route", new StringContent(""));
        }

        public async Task<bool> VerifyPin(string inputPin)
        {
            var response = await _httpClient.GetAsync("auth/state");
            string responseString = await response.Content.ReadAsStringAsync();
            var state = JsonSerializer.Deserialize<ServerState>(responseString, _jsonOptions);
            return state.Pin == inputPin;

        }

        public async Task<OperationResult> SignUpAsync(SignUpDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/signup", dto);
            if (response.IsSuccessStatusCode)
                return new OperationResult(true);
            
            string error = await response.Content.ReadAsStringAsync();
            return new OperationResult(error);
        }

        public async Task<OperationResult> SignInAsync(SignInDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/signin", dto);
            if (response.IsSuccessStatusCode)
                return new OperationResult(true);

            string error = await response.Content.ReadAsStringAsync();
            return new OperationResult(error);
        }
        
        public async Task<OperationResult> RefreshTokenAsync()
        {
            var response = await _httpClient.PostAsJsonAsync("auth/refresh-token", new { });
            if (response.IsSuccessStatusCode)
                return new OperationResult(true);

            string error = await response.Content.ReadAsStringAsync();
            return new OperationResult(error);
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using MindFree.Interfaces;
using MindFree.Models;

namespace MindFree.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly ICookie _cookie;
        private readonly NavigationManager _navigationManager;

        public UserService( HttpClient httpClient, ICookie cookie, NavigationManager navigation) { 
            _httpClient = httpClient;
            _cookie = cookie;
            _navigationManager = navigation;
        }
        public async Task<User> Login(User user)
        {
            User userData = new();

            var userResp = await _httpClient.PostAsJsonAsync("login", user);

            if(userResp != null)
            {
                userData = await userResp.Content.ReadFromJsonAsync<User>();
            }

            return userData;
        }

        public async Task Logout()
        {
            await _cookie.SetValue("app_token", "");
            _navigationManager.NavigateTo("login");
        }
    }
}

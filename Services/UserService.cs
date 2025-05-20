
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Net.Http.Headers;
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
        private Me _me { get; set; } = new();

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

        public async Task<Me> GetUserData()
        {
            string token = await _cookie.GetValue("app_token");
            if (!string.IsNullOrEmpty(token)) {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    _me = await _httpClient.GetFromJsonAsync<Me>("me");

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Unauthorized"))
                    {
                        Logout();
                    }
                    else
                    {
                        throw new Exception(ex.Message);
                    }
                        
                }
            }

            return _me;
        }
        public async Task UpdateUser(string id, string email, string name, string currentPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(currentPassword)) throw new Exception("O email, nome e senha são obrigatórios.");

            Dictionary<string, string> update = new();
            update.Add("email", email);
            update.Add("name", name);
            update.Add("currentPassword", currentPassword);

            string token = await _cookie.GetValue("app_token");

            if(!string.IsNullOrEmpty(token))
            {

                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await _httpClient.PutAsJsonAsync($"users/update/{id}", update);

                    if (response.StatusCode.ToString() == "Unauthorized") throw new Exception("Senha incorreta");

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}

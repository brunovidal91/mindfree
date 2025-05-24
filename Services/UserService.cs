
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using MindFree.Interfaces;
using MindFree.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MindFree.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly ICookie _cookie;
        private readonly NavigationManager _navigationManager;
        private Me _me { get; set; } = new();
        public UserService(HttpClient httpClient, ICookie cookie, NavigationManager navigation)
        {
            _httpClient = httpClient;
            _cookie = cookie;
            _navigationManager = navigation;
        }
        public async Task<User> Login(User user)
        {
            User userData = new();

            var userResp = await _httpClient.PostAsJsonAsync("login", user);

            if (userResp != null)
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
            if (!string.IsNullOrEmpty(token))
            {
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

            if (!string.IsNullOrEmpty(token))
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
        public async Task<List<UsersRequest>> GetUsers()
        {
            string token = await _cookie.GetValue("app_token");
            if (!string.IsNullOrEmpty(token))
            {
                List<UsersRequest> users;

                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    users = await _httpClient.GetFromJsonAsync<List<UsersRequest>>("users") ?? new List<UsersRequest>();
                    
                    return users;
                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                    
                }
            }
            else
            {
                List<UsersRequest> users = new();
                return users;
            }
        }
        public async Task CreateUser(string name, string email, string password, string confirmPassword)
        {
            if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("O nome é obrigatório");
            if(string.IsNullOrEmpty(email)) throw new ArgumentNullException("O email é obrigatório");
            if(string.IsNullOrEmpty(password)) throw new ArgumentNullException("A senha não foi informadada");
            if(string.IsNullOrEmpty(confirmPassword)) throw new ArgumentNullException("É obrigatório confirmar a senha");
            if (password != confirmPassword) throw new Exception("As senhas não conferem");

            string token = await _cookie.GetValue("app_token");
            UserCreate user = new UserCreate();
            user.Name = name;
            user.Email = email;
            user.Password = password;
            

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    await _httpClient.PostAsJsonAsync<UserCreate>("users/add", user);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("O id do usuário não foi informado");

            string token = await _cookie.GetValue("app_token");
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    await _httpClient.DeleteAsync("users/delete/"+id);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
    }

    public class UserCreate
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

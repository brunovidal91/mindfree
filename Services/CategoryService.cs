using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using MindFree.Interfaces;
using MindFree.Models;
using MindFree.Pages;

namespace MindFree.Services
{
    public class CategoryService 
    {

        private HttpClient? _httpClient { get; set; }
        private ICookie? _cookie { get; set; }


        public CategoryService(HttpClient httpClient, ICookie cookie)
        {
            _cookie = cookie;
            _httpClient = httpClient;
        }
        public async Task<List<Category>> GetCategories()
        {
            List<Category> categories = new List<Category>();
            string _token = await _cookie.GetValue("app_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            categories = await _httpClient.GetFromJsonAsync<List<Category>>("categories") ?? new List<Category>();

            return categories;
        }

        public async Task<HttpResponseMessage> CreateCategory(Category category)
        {
            //validar se tem categoria
            if (category is null) throw new ArgumentNullException("Categoria não informada");

            string _token = await _cookie.GetValue("app_token");

            if (string.IsNullOrEmpty(category.day)) category.day = "1";

            //category = new Category { title = "Teste", isIncome=false, isMonthly = false, amount = 30.25, day= "20" };
            category = new Category { title = category.title, isIncome=category.isIncome, isMonthly = category.isMonthly, amount = category.amount, day = category.day };

            await _cookie.SetValue("teste", $"title = {category.title}, isIncome={category.isIncome}, isMonthly = {category.isMonthly}, amount = {category.amount}, day = {category.day}");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("categories/add", category);

            return response;
        }

        public async Task DeleteCategory(string id)
        {
            //validar se tem id
            if(string.IsNullOrEmpty(id)) throw new ArgumentNullException("Categoria não informada");

            //Pegar o token
            string _token = await _cookie.GetValue("app_token");

            //Requisição
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            await _httpClient.DeleteAsync($"/categories/{id}");
        }
    }
}

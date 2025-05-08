using System.Net.Http;
using System.Net.Http.Json;
using MindFree.Connection;
using MindFree.Interfaces;
using MindFree.Models;

namespace MindFree.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<Category>> GetCategories()
        {
            List<Category> categories = new List<Category>();
            categories = await httpClient.GetFromJsonAsync<List<Category>>(Api.Url + "categories") ?? new List<Category>();

            return categories;

        }
    }
}

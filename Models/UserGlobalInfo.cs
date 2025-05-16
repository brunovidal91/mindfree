using System.Net;
using Microsoft.AspNetCore.Components;
using MindFree.Interfaces;

namespace MindFree.Models
{
    public static class UserGlobalInfo
    {
        [Inject]
        private static ICookie _cookie { get; set; }
        public static string? Name { get; set; }
        public static string? Email { get; set; }
        public static string? Id { get; set; }
        public static string? CreatedAt { get; set; }
        public static bool IsAdmin { get; set; }
        public static string? CurrentYear { get; set; }
        public static int InvestmentRate { get; set; }

        public static async Task<string> GetInfo()
        {
            return await _cookie.GetValue("userCode");
        }
    }
}

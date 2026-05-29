using MindFree.Models;
using MindFree.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace MindFree.Services
{
    public class TransactionService
    {
        private HttpClient _httpClient { get; set; } = default!;
        private ICookie _cookie { get; set; } = default!;

        private NavigationManager _navigationManager { get; set; }

        private TransactionResponse _transactionResponse { get; set; } = new();

        public TransactionService(HttpClient httpClient, ICookie cookie, NavigationManager navigationManager) { 
            _cookie = cookie;
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task<List<Transaction>> GetTransactions(string date, string datareq, string month, string year)
        {
            string _token = await _cookie.GetValue("app_token");
            List<Transaction> _transactions = new List<Transaction>();

            UserService userService = new UserService(_httpClient, _cookie, _navigationManager);

            if (!string.IsNullOrEmpty(_token))
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    _transactionResponse = await _httpClient.GetFromJsonAsync<TransactionResponse>($"transactions/{date}/{datareq}/{month}/{year}");
                    _transactions = _transactionResponse.Transactions;
                    
                    return _transactions;
                }

                catch (Exception ex)
                {
                    if (ex.Message.Contains("Unauthorized"))
                    {
                        await userService.Logout();
                        return _transactions;
                    }
                    else
                    {
                        throw new Exception(ex.Message);
                    }

                }
            }
            else
            {
                return new List<Transaction>();
            }
        }
        public async Task InsertTransaction(Transaction transaction)
        {
            if (transaction == null) throw new Exception("O lançamento não pode ser nula");
            if (string.IsNullOrEmpty(transaction.Category.id)) throw new Exception("A categoria não pode ser nula");
            if (transaction.Year == null) throw new Exception("O ano do lançamento não pode ser nulo");
            if (transaction.Date == null) throw new Exception("O dia do lançamento não pode ser nulo");
            if (transaction.Value == 0) throw new Exception("O valor precisa ser maior que zero.");

            //Tratamento para sempre ter duas casas decimais no valor
            string valuef = transaction.Value.ToString("F2");
            transaction.Value = double.Parse(valuef);

            string _token = await _cookie.GetValue("app_token");
            if (!string.IsNullOrEmpty(_token))
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    await _httpClient.PostAsJsonAsync<Transaction>("transactions", transaction);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public async Task DeleteTransaction(Transaction transaction)
        {
            if (transaction == null) throw new Exception("O lançamento não pode ser nulo.");
            if (string.IsNullOrEmpty(transaction.Id)) throw new Exception("O id do lançamento não pode ser nulo.");

            string _token = await _cookie.GetValue("app_token");
            if (!string.IsNullOrEmpty(_token)) {
                try
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    await _httpClient.DeleteAsync($"transactions/{transaction.Id}");

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
        public async Task EditTransaction(Transaction transaction)
        {
            if (transaction == null) throw new Exception("Para alteração é preciso informar uma transação.");


            transaction.isPaid = !transaction.isPaid;
            
            if(transaction.isPaid == true && transaction.Value <= 0) throw new Exception("Só é possível dar baixa em transações pagas.");


            string token = await _cookie.GetValue("app_token");

            TransactionEdit transactionEdit = new TransactionEdit { Id = transaction.Id, Value = transaction.Value, isPaid = transaction.isPaid };

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                await _httpClient.PutAsJsonAsync<TransactionEdit>("transactions", transactionEdit);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public class TransactionResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Status { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
    }

    public class TransactionEdit
    {
        public string Id { get; set; } = string.Empty;
        public double Value { get; set; }
        public bool? isPaid { get; set; }

    }
}

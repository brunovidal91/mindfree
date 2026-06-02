using Microsoft.AspNetCore.Components;
using MindFree.Interfaces;
using MindFree.Utils;
using System.Transactions;

namespace MindFree.Models
{
    public class Dashboard
    {
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<CategoryCompare> CategoriesCompare { get; set; } = new List<CategoryCompare>();
        public List<ExpectedPayment> NextPayments { get; set; } = new List<ExpectedPayment>();
        public List<MonthResult> MonthResults { get; set; } = new List<MonthResult>();
        public double EconomyCard { get; set; }
        public double ExpensesCard { get; set; }
        public double IncomeCard { get; set; }
        public double TotalCard { get; set; }
        public string CurrentMonth { get; set; } = (DateTime.Now.Month + 1).ToString();    
    
        public Dashboard(List<Transaction> transactions, string currentMonth, List<Category> categories, Boolean isIncomeList) { 
            Transactions = transactions;
            Categories = categories;
            CurrentMonth = currentMonth;



            if(Transactions.Where(item => item.Month == currentMonth).Count() > 0)
            {
                WriteResults();
                CalcCompare(isIncomeList);
                
            }

            if (Categories.Count() > 0) {
                GetNextPayments();
            }

            GetOlderMonthsResult();
        }     
    
        public void WriteResults()
        {
            ExpensesCard = Transactions.Where(item => item.Month == CurrentMonth).Where(item => item.Category!.isIncome == false).Sum(item => item.Value);
            IncomeCard = Transactions.Where(item => item.Month == CurrentMonth).Where(item => item.Category!.isIncome == true).Sum(item => item.Value);
            TotalCard = IncomeCard - ExpensesCard;
            EconomyCard = TotalCard * 100 / IncomeCard;
        }    
        
        public void CalcCompare(Boolean isIncomeList)
        {
            List<Transaction> expensesList = Transactions.Where(item => item.Month == CurrentMonth).Where(item => item.Category.isIncome == isIncomeList).ToList();

            var group = from transaction in expensesList
                        group transaction by transaction.Category.title into agroupedItems
                        select agroupedItems;

            string title = "";
            double value = 0;


            foreach (var transactions in group)
            {
                foreach (var transaction in transactions) {
                    title = transaction.Category!.title;
                    value += transaction.Value;
                }
                
                double percentage = value * 100 / ExpensesCard;

                CategoriesCompare.Add(new CategoryCompare {Title = title, Value = value, Percentage = percentage });
                
                title = "";
                value = 0;
                percentage = 0;
            }
        }
        
        public double GetTotalInvestment(double totalReq, double _investmentRate)
        {
            double rate = _investmentRate;
            double total = totalReq * rate / 100;

            return total;

        }
        public void GetNextPayments()
        {
            int today = DateTime.Now.Day;
            List<Category> nextPaymentsCategorie = Categories
                .Where(item => item.isIncome == false)
                .Where(item => item.isMonthly == true).ToList();
            //.Where(item => int.Parse(item.day) >= today).ToList();


            List<Transaction> transactions = Transactions.Where(item => item.Month == CurrentMonth).ToList();
            double value = 0;
            Transaction transaction = new Transaction();

            foreach (Category category in nextPaymentsCategorie) {
                //if(category.amount == 0)
                //{
                    if (Transactions.Count > 0) {

                        transaction = transactions.Where(item => item?.Category?.title == category.title).FirstOrDefault();
                        if (transaction != null) {

                            value = transaction.Value;

     
                 
                            
                            // Criar uma nova coluna nas transações payed=True/False. Criar uma nova prop no ExpectedPayment.
                        }
                        else
                        {
                            value = category.amount;

                        }
                    //}
                    }
                    else
                    {
                        value = category.amount;

                    }


                NextPayments.Add(new ExpectedPayment { Name = category.title, Value = value, Date = category.day, Transaction = transaction });

                value = 0;
            }
        }
    
        public void GetOlderMonthsResult()
        {
            MonthList monthList = new();


            List<Transaction> olderTransactions = Transactions.Where(item => int.Parse(item.Month) < DateTime.Now.Month).ToList();

            var olderTransationsPerMonth = from transaction in olderTransactions group transaction by int.Parse(transaction.Month);

            int monthIndex = 0;
            string monthName = string.Empty;
            double expenses = 0;
            double incomes = 0;
            double balance = 0;

            foreach (var transactionGroup in olderTransationsPerMonth) {

                monthIndex = int.Parse(transactionGroup.FirstOrDefault().Month);
                monthName = monthList.SelectedMonth(monthIndex).MonthName!;
                expenses = transactionGroup.Where(item => item.Category.isIncome == false).Sum(item => item.Value);
                incomes = transactionGroup.Where(item => item.Category.isIncome == true).Sum(item => item.Value);
                balance = incomes - expenses;

                MonthResults.Add(new MonthResult { MonthIndex = monthIndex, MonthName = monthName, Expenses = expenses, Incomes = incomes, Balance = balance });

                monthIndex = 0;
                monthName = string.Empty;
                expenses = 0;
                incomes = 0;
                balance = 0;
            }

        }
    }


}

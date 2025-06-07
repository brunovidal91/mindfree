namespace MindFree.Models
{
    public class Dashboard
    {
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<CategoryCompare> CategoriesCompare { get; set; } = new List<CategoryCompare>();
        public List<ExpectedPayment> ExpectedPayments { get; set; } = new List<ExpectedPayment>();
        public double EconomyCard { get; set; }
        public double ExpensesCard { get; set; }
        public double IncomeCard { get; set; }
        public double TotalCard { get; set; }
        public string CurrentMonth { get; set; } = (DateTime.Now.Month + 1).ToString();
    
    
        public Dashboard(List<Transaction> transactions, string currentMonth) { 
            Transactions = transactions;
            CurrentMonth = currentMonth;
            if(Transactions.Where(item => item.Month == currentMonth).Count() > 0)
            {
                WriteResults();
                CalcCompare();
            }
        }     
    
        public void WriteResults()
        {
            ExpensesCard = Transactions.Where(item => item.Month == CurrentMonth).Where(item => item.Category!.isIncome == false).Sum(item => item.Value);
            IncomeCard = Transactions.Where(item => item.Month == CurrentMonth).Where(item => item.Category!.isIncome == true).Sum(item => item.Value);
            TotalCard = IncomeCard - ExpensesCard;
            EconomyCard = TotalCard * 100 / IncomeCard;
        }    
        
        public void CalcCompare()
        {
            List<Transaction> expensesList = Transactions.Where(item => item.Month == CurrentMonth).Where(item => item.Category.isIncome == false).ToList();

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
    
    }


}

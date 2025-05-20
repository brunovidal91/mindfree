using System.Net.NetworkInformation;
using MindFree.Interfaces;
using MindFree.Models;

namespace MindFree.Utils
{
    public class MonthList : IMonthList
    {

        private List<Month> _months { get; set; } = new List<Month>();

        public MonthList() {
            Months();
        }

        public List<Month> Months()
        {

            _months.Add(new Month(1, "Janeiro"));
            _months.Add(new Month(2, "Fevereiro"));
            _months.Add(new Month(3, "Março"));
            _months.Add(new Month(4, "Abril"));
            _months.Add(new Month(5, "Maio"));
            _months.Add(new Month(6, "Junho"));
            _months.Add(new Month(7, "Julho"));
            _months.Add(new Month(8, "Agosto"));
            _months.Add(new Month(9, "Setembro"));
            _months.Add(new Month(10, "Outubro"));
            _months.Add(new Month(11, "Novembro"));
            _months.Add(new Month(12, "Dezembro"));

            return _months;
        }

        public Month SelectedMonth(int id)
        {
            return _months.Where(item => item.MonthPosition == id).FirstOrDefault()!;
        }
    }
}

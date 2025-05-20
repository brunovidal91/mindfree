using MindFree.Models;

namespace MindFree.Interfaces
{
    public interface IMonthList
    {
        public List<Month> Months();
        public Month SelectedMonth(int id);
    }
}

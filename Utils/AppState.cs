namespace MindFree.Utils
{
    public class AppState
    {
        private bool _isAdmin;
        private string? _userName;
        private string? _currentYear;

        //Global Prop IsAdmin
        public bool IsAdmin { 
            get { return _isAdmin; } 
            set { 
                _isAdmin = value;

                HasChanged(); //Notificando os componentes que a propriedade foi atualizada || se tivesse outras propriedades, basta chamar esse metodo igual foi feito aqui.
            } 
        }


        //Global Prop UserName 
        public string? UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                HasChanged();
            }

        }

        //Global Prop currentYear 
        public string? CurrentYear
        {
            get { return _currentYear; }
            set
            {
                _currentYear = value;
                HasChanged();
            }

        }

        public Action? Notification;    //Os componentes devem se inscrever nessa Action

        private void HasChanged()
        {
            Notification?.Invoke();
        }
    }
}

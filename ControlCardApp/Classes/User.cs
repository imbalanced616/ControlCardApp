using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ControlCardApp.Classes
{
    public class User
    {
        public static User CurrentUser { get; set; } = new User();
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        private string typeUser;
        public string TypeUser
        {
            get { return typeUser; }
            set
            {
                typeUser = value;
                OnPropertyChanged("TypeUser");
            }
        }
        public User()
        {
        }
        public User(string login, string password, string typeUser)
        {
            Login = login;
            Password = password;
            TypeUser = typeUser;
            CurrentUser = this;
        }
        public User(Users user)
        {
            Login = user.LoginUser;
            Password = user.PasswordUser;
            TypeUser = user.TypeUsers.NameType;
            CurrentUser = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

using ControlCardApp.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlCardApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (txbLogin.Text == "")
            {
                MessageBox.Show("Поле логин не заполнено!", "Ошибка полей ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (pbPassword.Password == "")
            {
                MessageBox.Show("Поле пароль не заполнено!", "Ошибка полей ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Users user = new Users();
            try
            {
                user = ControlCardEntities.GetContext().Users.FirstOrDefault(x => x.LoginUser == txbLogin.Text && x.PasswordUser == pbPassword.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            User.CurrentUser.Login = user.LoginUser;
            User.CurrentUser.TypeUser = user.TypeUsers.NameType;
            //ClassHelper.connection.Open();
            //SqlDataReader sqlDataReader = new SqlCommand($"SELECT LoginUser, PasswordUser, T.NameType FROM Users U JOIN TypeUsers T ON T.idTypeUser = U.idTypeUser WHERE LoginUser = '{txbLogin.Text}' AND PasswordUser = '{pbPassword.Password}';", ClassHelper.connection).ExecuteReader();
            //if (sqlDataReader.Read()) new User(sqlDataReader[0].ToString(), sqlDataReader[1].ToString(), sqlDataReader[2].ToString());
            //ClassHelper.connection.Close();
            ClassHelper.frmObj.Navigate(new PageLobby());
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageRegistration());
        }

        private void TxbLogin_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txbLogin.Focus();
        }

        private void PbPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            pbPassword.Focus();
        }

        private void PbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (((PasswordBox)sender).Password != "") txbPass.Visibility = Visibility.Hidden;
            else txbPass.Visibility = Visibility.Visible;
        }
    }
}

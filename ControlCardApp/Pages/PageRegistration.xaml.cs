using ControlCardApp.Classes;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        public PageRegistration()
        {
            InitializeComponent();
        }

        private void BtnSingUp_Click(object sender, RoutedEventArgs e)
        {
            if (txbLogin.Text == "")
            {
                MessageBox.Show("Поле логин не заполнено!", "Ошибка полей ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (pbPassword.Password == "" || pbPassword2.Password == "")
            {
                MessageBox.Show("Поле пароль не заполнено!", "Ошибка полей ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (pbPassword.Password != pbPassword2.Password)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка полей ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ControlCardEntities.GetContext().Users.FirstOrDefault(x => x.LoginUser == txbLogin.Text) != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка полей ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                txbLogin.Clear();
                return;
            }
            int TypeUser = 1;
            if (rbAssembling.IsChecked != true) TypeUser = 2;
            Users user = new Users() { LoginUser = txbLogin.Text, PasswordUser = pbPassword.Password, idTypeUser = TypeUser };
            ControlCardEntities.GetContext().Users.Add(user);
            try
            {
                ControlCardEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Новый пользователь успешно зарегистрирован!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            new User(user);
            ClassHelper.frmObj.Navigate(new PageLobby());
        }

        private void BtnSingIn_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageLogin());
        }

        private void TxbLogin_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txbLogin.Focus();
        }

        private void PbPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            pbPassword.Focus();
        }

        private void PbPassword2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            pbPassword2.Focus();
        }

        private void PbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (((PasswordBox)sender).Password != "") txbPass.Visibility = Visibility.Hidden;
            else txbPass.Visibility = Visibility.Visible;
        }

        private void PbPassword2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (((PasswordBox)sender).Password != "") txbPass2.Visibility = Visibility.Hidden;
            else txbPass2.Visibility = Visibility.Visible;
        }
    }
}

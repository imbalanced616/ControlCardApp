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
    /// Логика взаимодействия для PageLobby.xaml
    /// </summary>
    public partial class PageLobby : Page
    {
        public PageLobby()
        {
            InitializeComponent();

            //txtLogin.Text = User.CurrentUser.Login.ToString();
            //txtTypeUser.Text = User.CurrentUser.TypeUser.ToString();
        }

        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageLogin());
            //User.CurrentUser = null;
        }

        private void BtnDetails_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageDetails());
        }

        private void BtnTepmlates_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageTemplates());
        }

        private void BtnSections_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageSections());
        }

        private void BtnPoints_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PagePoints());
        }
    }
}

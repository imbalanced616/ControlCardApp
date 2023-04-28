using ControlCardApp.Pages;
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
using ControlCardApp.Classes;

namespace ControlCardApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ClassHelper.frmObj = FrameWindow;
            ClassHelper.frmObj.Navigate(new PageLogin());

            User _ = new User();
            DataContext = User.CurrentUser;
        }

        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageLogin());
            User.CurrentUser = null;
        }
    }
}

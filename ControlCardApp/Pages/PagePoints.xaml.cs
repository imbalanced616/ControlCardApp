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
    /// Логика взаимодействия для PagePoints.xaml
    /// </summary>
    public partial class PagePoints : Page
    {
        public PagePoints()
        {
            InitializeComponent();
            dtgPoints.ItemsSource = ControlCardEntities.GetContext().Points.ToList();
        }

        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new AddPagePoint(null));
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgPoints.SelectedItem != null)
                ClassHelper.frmObj.Navigate(new AddPagePoint((Points)dtgPoints.SelectedItem));
            else MessageBox.Show("Для изменения выберите пункт!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            dtgPoints.ItemsSource = ControlCardEntities.GetContext().Points.ToList();
        }

        private void MenuOrderBy_Click(object sender, RoutedEventArgs e)
        {
            dtgPoints.ItemsSource = ControlCardEntities.GetContext().Points.OrderBy(x => x.NamePoint).ToList();
        }

        private void MenuOrderByDescending_Click(object sender, RoutedEventArgs e)
        {
            dtgPoints.ItemsSource = ControlCardEntities.GetContext().Points.OrderByDescending(x => x.NamePoint).ToList();
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            List<Points> selectedItems = (List<Points>)dtgPoints.SelectedItems;
            if (MessageBox.Show($"Вы точно хотите удалить {selectedItems.Count} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ControlCardEntities.GetContext().Points.RemoveRange(selectedItems);
                    ControlCardEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageLobby());
        }
    }
}

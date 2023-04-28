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
    /// Логика взаимодействия для PageDetails.xaml
    /// </summary>
    public partial class PageDetails : Page
    {
        public PageDetails()
        {
            InitializeComponent();
            dtgDetails.ItemsSource = ControlCardEntities.GetContext().Details.ToList();
        }

        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new AddPageDetail(null));
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgDetails.SelectedItem != null)
                ClassHelper.frmObj.Navigate(new AddPageDetail((Details)dtgDetails.SelectedItem));
            else MessageBox.Show("Для изменения выберите деталь!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            dtgDetails.ItemsSource = ControlCardEntities.GetContext().Details.ToList();
        }

        private void MenuOrderBy_Click(object sender, RoutedEventArgs e)
        {
            dtgDetails.ItemsSource = ControlCardEntities.GetContext().Details.OrderBy(x => x.NameDetail).ToList();
        }

        private void MenuOrderByDescending_Click(object sender, RoutedEventArgs e)
        {
            dtgDetails.ItemsSource = ControlCardEntities.GetContext().Details.OrderByDescending(x => x.NameDetail).ToList();
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            Details selectedItem = (Details)dtgDetails.SelectedItem;
            if (MessageBox.Show($"Вы точно хотите удалить запись №{selectedItem.idDetail}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ClassHelper.connection.Open();
                    ControlCardEntities.GetContext().Details.Remove(selectedItem);
                    new SqlCommand($"DELETE FROM Templates WHERE idDetail = {selectedItem.idDetail};", ClassHelper.connection).ExecuteNonQuery();
                    ControlCardEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    ClassHelper.connection.Close();
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageLobby());
        }
    }
}

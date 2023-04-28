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
    /// Логика взаимодействия для PageSections.xaml
    /// </summary>
    public partial class PageSections : Page
    {
        public PageSections()
        {
            InitializeComponent();
            dtgSections.ItemsSource = ControlCardEntities.GetContext().Sections.ToList();
        }

        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new AddPageSection(null));
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgSections.SelectedItem != null)
                ClassHelper.frmObj.Navigate(new AddPageSection((Sections)dtgSections.SelectedItem));
            else MessageBox.Show("Для изменения выберите раздел!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            dtgSections.ItemsSource = ControlCardEntities.GetContext().Sections.ToList();
        }

        private void MenuOrderBy_Click(object sender, RoutedEventArgs e)
        {
            dtgSections.ItemsSource = ControlCardEntities.GetContext().Sections.OrderBy(x => x.NameSection).ToList();
        }

        private void MenuOrderByDescending_Click(object sender, RoutedEventArgs e)
        {
            dtgSections.ItemsSource = ControlCardEntities.GetContext().Sections.OrderByDescending(x => x.NameSection).ToList();
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            List<Sections> selectedItems = (List<Sections>)dtgSections.SelectedItems;
            if (MessageBox.Show($"Вы точно хотите удалить {selectedItems.Count} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ControlCardEntities.GetContext().Sections.RemoveRange(selectedItems);
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

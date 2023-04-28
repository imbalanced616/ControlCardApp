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
    /// Логика взаимодействия для PageTemplates.xaml
    /// </summary>
    public partial class PageTemplates : Page
    {
        public PageTemplates()
        {
            InitializeComponent();
            dtgTemplates.ItemsSource = ControlCardEntities.GetContext().Templates.ToList();
        }

        private void DtgTemplates_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageTemplateStructure((Templates)dtgTemplates.SelectedItem));
        }

        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new AddPageTemplate(null));
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dtgTemplates.SelectedItem != null)
                ClassHelper.frmObj.Navigate(new AddPageTemplate((Templates)dtgTemplates.SelectedItem));
            else MessageBox.Show("Для изменения выберите шаблон!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            dtgTemplates.ItemsSource = ControlCardEntities.GetContext().Templates.ToList();
        }

        private void MenuOrderBy_Click(object sender, RoutedEventArgs e)
        {
            dtgTemplates.ItemsSource = ControlCardEntities.GetContext().Templates.OrderBy(x => x.NameTemplate).ToList();
        }

        private void MenuOrderByDescending_Click(object sender, RoutedEventArgs e)
        {
            dtgTemplates.ItemsSource = ControlCardEntities.GetContext().Templates.OrderByDescending(x => x.NameTemplate).ToList();
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dtgTemplates.SelectedItems;
            if (MessageBox.Show($"Вы точно хотите удалить {selectedItems.Count} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ControlCardEntities.GetContext().Templates.RemoveRange((IEnumerable<Templates>)selectedItems);
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

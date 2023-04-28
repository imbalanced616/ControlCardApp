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
    /// Логика взаимодействия для AddPageSection.xaml
    /// </summary>
    public partial class AddPageSection : Page
    {
        private readonly Sections _currentItem = new Sections();
        public AddPageSection(Sections selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                tblTitle.Text = "Изменение раздела";
                btnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;

            cmbTemplate.ItemsSource = ControlCardEntities.GetContext().Templates.ToList();
            cmbTemplate.SelectedValuePath = "idTemplate";
            cmbTemplate.DisplayMemberPath = "NameTemplate";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentItem.idTemplate.ToString())) error.AppendLine("Укажите шаблон.");
            if (string.IsNullOrWhiteSpace(_currentItem.NameSection)) error.AppendLine("Укажите название раздела.");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (_currentItem.idSection == 0)
            {
                ControlCardEntities.GetContext().Sections.Add(_currentItem);
                try
                {
                    ControlCardEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                MessageBox.Show("Новый раздел успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                ClassHelper.frmObj.Navigate(new PageSections());

            }
            else
            {
                try
                {
                    ControlCardEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                MessageBox.Show("Раздел успешно изменён!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                ClassHelper.frmObj.Navigate(new PageSections());
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageSections());
        }
    }
}

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
    /// Логика взаимодействия для AddPagePoint.xaml
    /// </summary>
    public partial class AddPagePoint : Page
    {
        private readonly Points _currentItem = new Points();
        public AddPagePoint(Points selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                tblTitle.Text = "Изменение пункта";
                btnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;

            cmbSection.ItemsSource = ControlCardEntities.GetContext().Sections.ToList();
            cmbSection.SelectedValuePath = "idSection";
            cmbSection.DisplayMemberPath = "NameSection";
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentItem.idSection.ToString())) error.AppendLine("Укажите раздел.");
            if (string.IsNullOrWhiteSpace(_currentItem.NamePoint)) error.AppendLine("Укажите название пункта.");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (_currentItem.idPoint == 0)
            {
                ControlCardEntities.GetContext().Points.Add(_currentItem);
                try
                {
                    ControlCardEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                MessageBox.Show("Новый пункт успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                ClassHelper.frmObj.Navigate(new PagePoints());

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
                MessageBox.Show("Пункт успешно изменён!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                ClassHelper.frmObj.Navigate(new PagePoints());
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PagePoints());
        }
    }
}

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
    /// Логика взаимодействия для AddPageDetail.xaml
    /// </summary>
    public partial class AddPageDetail : Page
    {
        private readonly Details _currentItem = new Details();
        public AddPageDetail(Details selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                tblTitle.Text = "Изменение детали";
                btnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentItem.NameDetail)) error.AppendLine("Укажите название детали.");
            if (string.IsNullOrWhiteSpace(_currentItem.PlanDetail)) error.AppendLine("Укажите название чертежа детали.");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (_currentItem.idDetail == 0)
            {
                ControlCardEntities.GetContext().Details.Add(_currentItem);
                try
                {
                    ControlCardEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                MessageBox.Show("Новая деталь успешно добавлена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                ClassHelper.frmObj.Navigate(new PageDetails());

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
                MessageBox.Show("Деталь успешно изменена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                ClassHelper.frmObj.Navigate(new PageDetails());
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageDetails());
        }
    }
}

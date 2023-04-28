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
    /// Логика взаимодействия для AddPageTemplate.xaml
    /// </summary>
    public partial class AddPageTemplate : Page
    {
        private readonly Templates _currentItem = new Templates();
        private List<Sections> listSections = new List<Sections>();
        private List<Points> listPoints = new List<Points>();
        private bool flagSave = false;
        public AddPageTemplate(Templates selectedItem)
        {
            InitializeComponent();
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                tblTitle.Text = "Изменение шаблона";
                btnAdd.Content = "Изменить";
            }
            DataContext = _currentItem;
            LoadDataGrids();
            cmbDetail.ItemsSource = ControlCardEntities.GetContext().Details.ToList();
        }

        public void LoadDataGrids()
        {
            listSections = ControlCardEntities.GetContext().Sections.Where(x => x.idTemplate == _currentItem.idTemplate).ToList();
            dtgSection.ItemsSource = listSections;
            foreach(Sections section in ControlCardEntities.GetContext().Sections.Where(x => x.idTemplate == _currentItem.idTemplate).ToList())
                listPoints.AddRange(ControlCardEntities.GetContext().Points.Where(x => x.idSection == section.idSection).ToList());
            dtgPoints.ItemsSource = listPoints;
        }

        private void СmbSection_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            cmb.ItemsSource = listSections.Where(x => x.NameSection != "");
            cmb.SelectedValuePath = "idSection";
            cmb.DisplayMemberPath = "NameSection";
        }

        private void СmbSection_Initialized(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            cmb.ItemsSource = listSections.Where(x => x.NameSection != "");
            cmb.SelectedValuePath = "idSection";
            cmb.DisplayMemberPath = "NameSection";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentItem.NameTemplate)) error.AppendLine("Укажите название шаблона.");
            if (string.IsNullOrWhiteSpace(_currentItem.idDetail.ToString())) error.AppendLine("Укажите деталь.");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (_currentItem.idTemplate == 0)
            {
                if (ControlCardEntities.GetContext().Templates.FirstOrDefault(x => x.NameTemplate == _currentItem.NameTemplate) != null)
                {
                    MessageBox.Show("Шаблон с таким именем уже существует!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tbNameTamplate.Clear();
                    return;
                }
                ControlCardEntities.GetContext().Templates.Add(_currentItem);

                foreach (Sections s in listSections.Where(x => x.NameSection != ""))
                {
                    s.idTemplate = _currentItem.idTemplate;
                    if (s.idSection == 0) ControlCardEntities.GetContext().Sections.Add(s);
                    try { ControlCardEntities.GetContext().SaveChanges(); }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
                }
                listSections = ControlCardEntities.GetContext().Sections.Where(x => x.idTemplate == _currentItem.idTemplate).ToList();
                dtgSection.ItemsSource = listSections;
            }
            else
            {
                foreach (Sections s in dtgSection.ItemsSource)
                {
                    foreach (Sections c in listSections.Where(x => x.NameSection != ""))
                    {
                        c.idTemplate = _currentItem.idTemplate;
                        if (c.idSection == 0) ControlCardEntities.GetContext().Sections.Add(c);
                        try { ControlCardEntities.GetContext().SaveChanges(); }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
                    }
                    listSections = ControlCardEntities.GetContext().Sections.Where(x => x.idTemplate == _currentItem.idTemplate).ToList();
                    dtgSection.ItemsSource = listSections;
                }
            }
            MessageBox.Show("Сохранение произведено, можете добавлять пункты!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            dtgPoints.IsReadOnly = false;
            flagSave = true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (flagSave == false)
            {
                MessageBox.Show("Сперва сохраните изменения!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                foreach (Points p in listPoints.Where(x => x.NamePoint != ""))
                {
                    if (p.idPoint == 0) ControlCardEntities.GetContext().Points.Add(p);
                    try { ControlCardEntities.GetContext().SaveChanges(); }
                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
                }
                MessageBox.Show("Новый шаблон успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                ClassHelper.frmObj.Navigate(new PageTemplates());
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageTemplates());
        }
    }
}

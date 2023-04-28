using ControlCardApp.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ControlCardApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageTemplateStructure.xaml
    /// </summary>
    public partial class PageTemplateStructure : Page
    {
        private readonly Templates _currentTemplate = new Templates();
        public PageTemplateStructure(Templates selectedItem)
        {
            InitializeComponent();
            _currentTemplate = selectedItem;
            DataContext = _currentTemplate;

            foreach (Sections section in ControlCardEntities.GetContext().Sections.Where(x => x.idTemplate == _currentTemplate.idTemplate).ToList())
            {
                TreeViewItem item = new TreeViewItem() { Header = section.NameSection };
                foreach (Points point in ControlCardEntities.GetContext().Points.Where(x => x.idSection == section.idSection).ToList())
                    item.Items.Add(new TreeViewItem() { Header = point.NamePoint });
                trvTemplateStructure.Items.Add(item);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.frmObj.Navigate(new PageTemplates());
        }
    }
}

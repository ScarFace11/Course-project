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
using System.Text.RegularExpressions;
using Котельня.ApplicationData;

namespace Котельня.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageTypeWorkEdit.xaml
    /// </summary>
    public partial class PageTypeWorkEdit : Page
    {

        public WorkType _currentWorktype = new WorkType();
        public PageTypeWorkEdit(WorkType selectedWorktype)
        {
            InitializeComponent();
            if (selectedWorktype != null)
                _currentWorktype = selectedWorktype;
            DataContext = _currentWorktype;

            cmbwork.ItemsSource = KursavayaEntities.GetContext().Work.ToList();
        }
        private void btnSave_Click(object senver, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentWorktype.name))
            {
                errors.AppendLine("укажите название вида работы");
            }
            if (string.IsNullOrWhiteSpace(_currentWorktype.Price.ToString()))
            {
                errors.AppendLine("укажите цену вида работы");
            }
            if (_currentWorktype.Work == null)
            {
                errors.AppendLine("укажите работу");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            var exist = KursavayaEntities.GetContext().WorkType.FirstOrDefault(x => x.id == _currentWorktype.id);
            if (exist == null)
            {
                KursavayaEntities.GetContext().WorkType.Add(_currentWorktype);
                MessageBox.Show("вид работы добавлена!");
            }

            try
            {
                KursavayaEntities.GetContext().SaveChanges();
                AppFrame.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ToString());
            }

        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.GoBack();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Page_IsVisibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                
            }
        }
    }
}

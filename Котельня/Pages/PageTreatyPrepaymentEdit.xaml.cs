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
using Котельня.ApplicationData;
using System.Text.RegularExpressions;

namespace Котельня.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageTreatyPrepaymentEdit.xaml
    /// </summary>
    public partial class PageTreatyPrepaymentEdit : Page
    {
        public Treaty_prepayment _currentorey = new Treaty_prepayment();
        public PageTreatyPrepaymentEdit(Treaty_prepayment selectedprey)
        {
            InitializeComponent();
            if (selectedprey != null)
                _currentorey = selectedprey;
            DataContext = _currentorey;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.GoBack();
        }
        private void btnSave_Click(object senver, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentorey.value.ToString()))
            {
                errors.AppendLine("укажите цену предоплаты");
            }
            if (string.IsNullOrWhiteSpace(_currentorey.C_.ToString()))
            {
                errors.AppendLine("укажите процент предоплаты");
            }
            if (string.IsNullOrWhiteSpace(_currentorey.description))
            {
                errors.AppendLine("укажите описание");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            var id = KursavayaEntities.GetContext().Treaty_prepayment.FirstOrDefault(x => x.id == _currentorey.id);
            if (_currentorey != id)
            {
                KursavayaEntities.GetContext().Treaty_prepayment.Add(_currentorey);
                MessageBox.Show("Предоплата добавлена!");
            }
            else MessageBox.Show("Предоплата изменена!");

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
    }
}

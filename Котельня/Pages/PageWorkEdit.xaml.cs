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

namespace Котельня.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageWorkEdit.xaml
    /// </summary>
    public partial class PageWorkEdit : Page
    {
        public Work _currentWork = new Work();
        public PageWorkEdit(Work selectedWork)
        {
            InitializeComponent();
            if (selectedWork != null)
                _currentWork = selectedWork;
            DataContext = _currentWork;
        }
        private void btnSave_Click(object senver, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentWork.name))
            {
                errors.AppendLine("укажите название работы");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            
            var id = KursavayaEntities.GetContext().Work.FirstOrDefault(x => x.id == _currentWork.id);
            if (_currentWork != id)
            {
                KursavayaEntities.GetContext().Work.Add(_currentWork);
                MessageBox.Show("работа добавлена!");
            }
            else MessageBox.Show("работа изменена!");
            
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
    }
}

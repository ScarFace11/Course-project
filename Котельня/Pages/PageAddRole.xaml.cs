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
    /// Логика взаимодействия для PageAddRole.xaml
    /// </summary>
    public partial class PageAddRole : Page
    {
        public Role _currentRole = new Role();
        public PageAddRole(Role selectedRole)
        {
            InitializeComponent();
            if (selectedRole != null)
                _currentRole = selectedRole;
            DataContext = _currentRole;
        }
        private void btnSave_Click(object senver, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentRole.name))
            {
                errors.AppendLine("укажите название роли");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }


            var id = KursavayaEntities.GetContext().Role.FirstOrDefault(x => x.id == _currentRole.id);
            if (_currentRole != id)
            {
                KursavayaEntities.GetContext().Role.Add(_currentRole);
                MessageBox.Show("роль добавлена!");
            }
            else MessageBox.Show("роль изменена!");

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

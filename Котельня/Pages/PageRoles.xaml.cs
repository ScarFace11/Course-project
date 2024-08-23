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
    /// Логика взаимодействия для PageRoles.xaml
    /// </summary>
    public partial class PageRoles : Page
    {
        public PageRoles()
        {
            InitializeComponent();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageAddRole((sender as Button).DataContext as Role));
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageAddRole(null));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var RoleForRemoving = DGridRoles.SelectedItems.Cast<Role>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {RoleForRemoving.Count()} Элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursavayaEntities.GetContext().Role.RemoveRange(RoleForRemoving);
                    KursavayaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridRoles.ItemsSource = KursavayaEntities.GetContext().Role.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void Page_IsVisibleChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                KursavayaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridRoles.ItemsSource = KursavayaEntities.GetContext().Role.ToList();
            }
        }
    }
}

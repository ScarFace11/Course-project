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
    /// Логика взаимодействия для PageAllUsers.xaml
    /// </summary>
    public partial class PageAllUsers : Page
    {
        public PageAllUsers()
        {
            InitializeComponent();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //AppFrame.MainFrame.Navigate(new PageCreateProfile((sender as Button).DataContext as Users));
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageCreateAccount());
        }
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            var find = KursavayaEntities.GetContext().Users.Where(x => x.FIO == txtfind.Text).ToList();
            DtgAllUsers.ItemsSource = find;
        }
        private void btnSbross_Click(object sender, RoutedEventArgs e)
        {
            DtgAllUsers.ItemsSource = KursavayaEntities.GetContext().Users.ToList();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var UsersForRemoving = DtgAllUsers.SelectedItems.Cast<Users>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {UsersForRemoving.Count()} Элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursavayaEntities.GetContext().Users.RemoveRange(UsersForRemoving);
                    KursavayaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DtgAllUsers.ItemsSource = KursavayaEntities.GetContext().Users.ToList();
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
                DtgAllUsers.ItemsSource = KursavayaEntities.GetContext().Users.ToList();
            }
        }
    }
}

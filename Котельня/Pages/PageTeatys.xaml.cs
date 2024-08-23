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
    /// Логика взаимодействия для PageTeatys.xaml
    /// </summary>
    public partial class PageTeatys : Page
    {
        public PageTeatys()
        {
            InitializeComponent();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTreatyAdd((sender as Button).DataContext as Treaty));
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTreatyAdd(null));
        }
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            var us = KursavayaEntities.GetContext().Users.FirstOrDefault(x => x.FIO == txtfind.Text);
            if (us != null)
            {
                var find = KursavayaEntities.GetContext().Treaty.Where(x => x.User_id == us.id).ToList();
                DtgTreatysList.ItemsSource = find;
            }
            
        }
        private void btnSbross_Click(object sender, RoutedEventArgs e)
        {
            DtgTreatysList.ItemsSource = KursavayaEntities.GetContext().Treaty.ToList();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var TreatyForRemoving = DtgTreatysList.SelectedItems.Cast<Treaty>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {TreatyForRemoving.Count()} Элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursavayaEntities.GetContext().Treaty.RemoveRange(TreatyForRemoving);
                    KursavayaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DtgTreatysList.ItemsSource = KursavayaEntities.GetContext().Treaty.ToList();
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
                DtgTreatysList.ItemsSource = KursavayaEntities.GetContext().Treaty.ToList();
            }
        }
    }
}

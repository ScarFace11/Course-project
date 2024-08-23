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
    /// Логика взаимодействия для PageTreatyPrepaymentList.xaml
    /// </summary>
    public partial class PageTreatyPrepaymentList : Page
    {
        public PageTreatyPrepaymentList()
        {
            InitializeComponent();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTreatyPrepaymentEdit((sender as Button).DataContext as Treaty_prepayment));
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTreatyPrepaymentEdit(null));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var categoryForRemoving = DGridCategory.SelectedItems.Cast<Treaty_prepayment>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {categoryForRemoving.Count()} Элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursavayaEntities.GetContext().Treaty_prepayment.RemoveRange(categoryForRemoving);
                    KursavayaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridCategory.ItemsSource = KursavayaEntities.GetContext().Treaty_prepayment.ToList();
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
                DGridCategory.ItemsSource = KursavayaEntities.GetContext().Treaty_prepayment.ToList();
            }
        }
    }
}

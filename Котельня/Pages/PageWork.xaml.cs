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
    /// Логика взаимодействия для PageWork.xaml
    /// </summary>
    public partial class PageWork : Page
    {
        public PageWork()
        {
            InitializeComponent();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageWorkEdit((sender as Button).DataContext as Work));
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageWorkEdit(null));
        }
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            var find = KursavayaEntities.GetContext().Work.Where(x => x.name == txtfind.Text).ToList();
            DGridCategory.ItemsSource = find;
        }
        private void btnSbross_Click(object sender, RoutedEventArgs e)
        {
            DGridCategory.ItemsSource = KursavayaEntities.GetContext().Work.ToList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var categoryForRemoving = DGridCategory.SelectedItems.Cast<Work>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {categoryForRemoving.Count()} Элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KursavayaEntities.GetContext().Work.RemoveRange(categoryForRemoving);
                    KursavayaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridCategory.ItemsSource = KursavayaEntities.GetContext().Work.ToList();
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
                DGridCategory.ItemsSource = KursavayaEntities.GetContext().Work.ToList();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для PageTypeWork.xaml
    /// </summary>
    /// 
    
    
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public float Price { get; set; }
        public bool IsSelected { get; set; } // Добавлено свойство для отметки выбранных элементов
        public string Workid { get; set; }
        public string WorkTypeid { get; internal set; }
    }

    
    public partial class PageTypeWork : Page
    {
        //List<Item> items = new List<Item>();
        private ObservableCollection<Item> items = new ObservableCollection<Item>();
        public PageTypeWork()
        {
            InitializeComponent();

            DtgTypeWork.ItemsSource = null; // Очистка источника данных
            DtgTypeWork.Items.Clear(); // Очистка элементов в DataGrid, если они есть
        }
        private void LoadData()
        {
            using (var context = new KursavayaEntities())
            {
                items = new ObservableCollection<Item>(context.WorkType
                    .Select(w => new Item
                    {
                        name = w.name,
                        Price = w.Price,
                        id = w.id,
                        Workid = w.Work_id.ToString()
                    }).ToList());
            }

            DtgTypeWork.ItemsSource = items;

            foreach (var WT in items)
            {
                var a = KursavayaEntities.GetContext().Work.FirstOrDefault(x => x.id.ToString() == WT.Workid);
                WT.Workid = a?.name;
            }
        }
        DateTime thisDay = DateTime.Today;     
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new PageTypeWorkEdit(null));
        }
        private void btnZakaz_Click(object sender, RoutedEventArgs e)
        {
            var userdata = KursavayaEntities.GetContext().Treaty.FirstOrDefault(x => x.User_id.ToString() == AppString.id);
            if (userdata != null && thisDay <= userdata.Last_Date)
            {
                btnzakaz.Visibility = Visibility.Collapsed;

                var column = DtgTypeWork.Columns[1];
                column.Visibility = Visibility.Visible;

                btnoform.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Оформите или продлите договор!");
            }

        }

        private void btnoform_Click(object sender, RoutedEventArgs e)
        {

            List<Item> selectedItems = new List<Item>();

            // Получение выбранных элементов
            
            foreach (Item item in DtgTypeWork.Items)
            {
                if (item.IsSelected)
                {
                    selectedItems.Add(item);
                }
            }
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Вы ничего не заказали");
                return;
            }
            // Создание новой страницы и передача выбранных элементов
            PageAddUserOrder secondPage = new PageAddUserOrder(selectedItems);
            this.NavigationService.Navigate(secondPage);
        }

        private void DeleteSelectedItems(object sender, RoutedEventArgs e)
        {
            var selectedItems = DtgTypeWork.SelectedItems.Cast<Item>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {selectedItems.Count()} Элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var context = new KursavayaEntities())
                {
                    foreach (var selectedItem in selectedItems)
                    {
                        var itemToDelete = context.WorkType.FirstOrDefault(w => w.id == selectedItem.id);
                        if (itemToDelete != null)
                        {
                            context.WorkType.Remove(itemToDelete); // Удаление из базы данных
                            items.Remove(selectedItem); // Удаление из коллекции
                        }
                    }

                    context.SaveChanges(); // Сохранение изменений в базе данных
                    DtgTypeWork.ItemsSource = items;
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) // ФИИИИИИИИИИИИКСИТЬ!
        {
            var productForRemoving = DtgTypeWork.SelectedItems.Cast<WorkType>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {productForRemoving.Count()} Элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                
                try
                {
                    KursavayaEntities.GetContext().WorkType.RemoveRange(productForRemoving);
                    KursavayaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DtgTypeWork.ItemsSource = KursavayaEntities.GetContext().WorkType.ToList();
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
                LoadData();
                // выводит тольо определённую инфу, например только то чему равен id пользователю

                //var data = KursavayaEntities.GetContext().WorkType.Where(x => x.id.ToString() == AppString.id).ToList();
                //DtgTypeWork.ItemsSource = data;

                //-----------------

                //KursavayaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                //DtgTypeWork.ItemsSource = KursavayaEntities.GetContext().WorkType.ToList();

                if (AppString.Role_name == "Админ")
                {
                    btnadd.Visibility = Visibility.Visible;
                    btndel.Visibility = Visibility.Visible;
                }
                else if (AppString.Role_name == "Клиент")
                {
                    btnzakaz.Visibility = Visibility.Visible;
                }
                else
                {
                    btnadd.Visibility = Visibility.Collapsed;
                    btndel.Visibility = Visibility.Collapsed;
                    btnzakaz.Visibility = Visibility.Collapsed;

                }

            }
        }
    }
}

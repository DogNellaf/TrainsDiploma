using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.Enums;
using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses.OnlineSystem;
using RestaurantsClasses.WorkersSystem;
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
using System.Windows.Shapes;
using ui.Helper;

namespace ui.WorkerWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для WorkerOrders.xaml
    /// </summary>
    /// 
    public class OnlineOrderItem
    {
        public DateTime Created { get; set; }
        public int ClientId { get; set; }
        public string Address { get; set; }
        public string ButtonText { get; set; }
        public string ButtonText2 { get; set; }
    }

    public partial class OnlineOrders : Window
    {
        private Window _previous;
        private Worker _worker;
        private Client _client;

        public OnlineOrders(Window previous, Worker worker = null, Client client = null)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;
            _client = client;

            var orders = RequestClient.GetObjects<OnlineOrder>();
            if (client is not null)
            {
                orders = orders.Where(x => x.ClientId == client.id).ToList();
            }

            var showButtonTemplate = new FrameworkElementFactory(typeof(Button));
            showButtonTemplate.SetBinding(Button.NameProperty, new Binding("Id"));
            showButtonTemplate.SetBinding(Button.ContentProperty, new Binding("ButtonText2"));
            showButtonTemplate.AddHandler(Button.ClickEvent, new RoutedEventHandler((o, e) => showMealsButton(o, e)));

            ordersGrid.ItemsSource = orders;

            //ordersGrid.Columns.Add(
            //    new DataGridTextColumn()
            //    {
            //        Header = "Когда был создан",
            //        Binding = new Binding("Created"),
            //        Width = 200
            //    }
            //);

            //ordersGrid.Columns.Add(
            //    new DataGridTextColumn()
            //    {
            //        Header = "Id Клиента",
            //        Binding = new Binding("ClientId"),
            //        Width = 110
            //    }
            //);

            //ordersGrid.Columns.Add(
            //    new DataGridTextColumn()
            //    {
            //        Header = "Адрес",
            //        Binding = new Binding("Address"),
            //        Width = 110
            //    }
            //);

            if (client is null)
            {
                //var completeButtonTemplate = new FrameworkElementFactory(typeof(Button));
                //completeButtonTemplate.SetBinding(Button.NameProperty, new Binding("Id"));
                //completeButtonTemplate.SetBinding(Button.ContentProperty, new Binding("ButtonText"));
                //completeButtonTemplate.AddHandler(Button.ClickEvent, new RoutedEventHandler((o, e) => setCompleteButton(o, e)));
                //ordersGrid.Columns.Add(
                //    new DataGridTemplateColumn()
                //    {
                //        Header = "Отметить выполненным",
                //        CellTemplate = new DataTemplate() { VisualTree = completeButtonTemplate },
                //        Width = 200
                //    }
                //);
            }

            //ordersGrid.Columns.Add(
            //    new DataGridTemplateColumn()
            //    {
            //        Header = "Посмотреть блюда",
            //        CellTemplate = new DataTemplate() { VisualTree = showButtonTemplate },
            //        Width = 200
            //    }
            //);

            if (client is not null)
            {
                ordersGrid.IsReadOnly = false;
                orderIdBox.Visibility = Visibility.Hidden;
                idLabel.Visibility = Visibility.Hidden;
                setCompleteButton.Visibility = Visibility.Hidden;
            }


            //foreach (var order in orders)
            //{
            //    ordersGrid.Items.Add(new OnlineOrderItem()
            //    {
            //        Created = order.created,
            //        ClientId = order.ClientId,
            //        Address = order.address,
            //        ButtonText = $"Отметить выполненным заказ {order.id}",
            //        ButtonText2 = $"Нажмите для просмотра блюд заказа {order.id}"
            //    });
            //}
        }

        private void showMealsButton(object sender, RoutedEventArgs e)
        {
            string rawOrderId = ((Button)sender).Content.ToString().Split(' ').Last();
            if (!int.TryParse(rawOrderId, out int orderId))
                return;

            new OrderInfo(this, _worker, orderId).Show();
            Hide();
            //RequestClient.SetOrderComplete(orderId);
            //exitButton_Click(sender, e);
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ordersGrid.Columns.RemoveAt(0);
            ordersGrid.Columns.RemoveAt(ordersGrid.Columns.Count - 2);
            ordersGrid.Columns[ordersGrid.Columns.Count - 1].IsReadOnly = true;
            ordersGrid.Columns[ordersGrid.Columns.Count - 3].IsReadOnly = true;
        }

        private bool isManualEditCommit;

        private void ordersGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!isManualEditCommit)
            {
                isManualEditCommit = true;
                DataGrid grid = (DataGrid)sender;

                int selectedRowIndex = grid.SelectedIndex;

                grid.CommitEdit(DataGridEditingUnit.Row, true);

                var items = ((DataGrid)sender).Items;

                var orderData = (OnlineOrder)items[selectedRowIndex];

                if (orderData.address == string.Empty)
                {
                    MessageBox.Show("Перед сохранением введите адрес доставки");
                    return;
                }

                RequestClient.CreateOnlineOrder(_client.id, orderData.address);

                MessageBox.Show("Заказ был успешно создан");
                exitButton_Click(sender, null);


                isManualEditCommit = false;
            }
        }

        private void setCompleteButton_Click(object sender, RoutedEventArgs e)
        {
            string rawOrderId = orderIdBox.Text;
            if (!int.TryParse(rawOrderId, out int orderId))
                return;

            RequestClient.SetOnlineOrderComplete(orderId);
            exitButton_Click(sender, e);
        }
    }
}

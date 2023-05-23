using RestaurantsClasses.Enums;
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
    public partial class WorkerOrders : Window
    {
        private Window _previous;
        private Worker _worker;

        public WorkerOrders(Window previous, Worker worker)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;

            var orders = RequestClient.GetAllOfflineOrders().Where(x => x.ServerId == _worker.id && x.Status == OrderStatus.Оплачен);

            var completeButtonTemplate = new FrameworkElementFactory(typeof(Button));
            completeButtonTemplate.SetBinding(Button.NameProperty, new Binding("Id"));
            completeButtonTemplate.SetBinding(Button.ContentProperty, new Binding("ButtonText"));
            //buttonTemplate.Text = "Закрепить за собой";
            completeButtonTemplate.AddHandler(Button.ClickEvent, new RoutedEventHandler((o, e) => setCompleteButton(o, e)));

            var showButtonTemplate = new FrameworkElementFactory(typeof(Button));
            showButtonTemplate.SetBinding(Button.NameProperty, new Binding("Id"));
            showButtonTemplate.SetBinding(Button.ContentProperty, new Binding("ButtonText2"));
            //buttonTemplate.Text = "Закрепить за собой";
            showButtonTemplate.AddHandler(Button.ClickEvent, new RoutedEventHandler((o, e) => showMealsButton(o, e)));

            ordersGrid.Columns.Add(
                new DataGridTextColumn()
                {
                    Header = "Когда был создан",
                    Binding = new Binding("Created"),
                    Width = 200
                }
            );

            ordersGrid.Columns.Add(
                new DataGridTextColumn()
                {
                    Header = "Столик",
                    Binding = new Binding("TableNum"),
                    Width = 110
                }
            );

            ordersGrid.Columns.Add(
                new DataGridTemplateColumn()
                {
                    Header = "Отметить выполненным",
                    CellTemplate = new DataTemplate() { VisualTree = completeButtonTemplate },
                    Width = 200
                }
            );

            ordersGrid.Columns.Add(
                new DataGridTemplateColumn()
                {
                    Header = "Посмотреть блюда",
                    CellTemplate = new DataTemplate() { VisualTree = showButtonTemplate },
                    Width = 200
                }
            );


            foreach (var order in orders)
            {
                ordersGrid.Items.Add(new Item()
                {
                    Created = order.Created,
                    TableNum = order.TableId,
                    ButtonText = $"Отметить выполненным заказ {order.id}",
                    ButtonText2 = $"Нажмите для просмотра блюд заказа {order.id}"
                });
            }
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

        private void setCompleteButton(object sender, RoutedEventArgs e)
        {
            string rawOrderId = ((Button)sender).Content.ToString().Split(' ').Last();
            if (!int.TryParse(rawOrderId, out int orderId))
                return;

            RequestClient.SetOrderComplete(orderId);
            exitButton_Click(sender, e);
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }
    }
}

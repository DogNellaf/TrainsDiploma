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
    /// Логика взаимодействия для OrderInfo.xaml
    /// </summary>
    /// 

    public sealed class MealItem
    {
        public string Name { get; set; }
        public string ButtonText { get; set; }
    }

    public partial class OrderInfo : Window
    {
        private Window _previous;
        private Worker _worker;
        private int _orderId;
        public OrderInfo(Window previous, Worker worker, int orderId)
        {
            InitializeComponent();
            _orderId = orderId;
            _previous = previous;
            _worker = worker;

            nameLabel.Content = $"Блюда из заказа {orderId}";

            var meals = RequestClient.GetMealsByOrder(orderId);

            var buttonTemplate = new FrameworkElementFactory(typeof(Button));
            buttonTemplate.SetBinding(Button.ContentProperty, new Binding("ButtonText"));
            //buttonTemplate.Text = "Закрепить за собой";
            buttonTemplate.AddHandler(Button.ClickEvent, new RoutedEventHandler((o, e) => addCound(o, e)));

            dishesGrid.Columns.Add(
                new DataGridTextColumn()
                {
                    Header = "Название",
                    Binding = new Binding("Name"),
                    Width = 200
                }
            );

            dishesGrid.Columns.Add(
                new DataGridTemplateColumn()
                {
                    Header = "Отметить, что уже принесли",
                    CellTemplate = new DataTemplate() { VisualTree = buttonTemplate },
                    Width = 200
                }
            );


            foreach (var meal in meals)
            {
                dishesGrid.Items.Add(new MealItem()
                {
                    Name = meal.Name,
                    ButtonText = $"Нажмите, если принесли блюдо {meal.id}"
                });
            }
        }

        private void addCound(object sender, RoutedEventArgs e)
        {
            string rawMealId = ((Button)sender).Content.ToString().Split(' ').Last();
            if (!int.TryParse(rawMealId, out int mealId))
                return;

            RequestClient.DeliverOfflineMeal(_orderId, mealId);
            exitButton_Click(sender, e);
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }
    }
}

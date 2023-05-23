using Microsoft.Win32;
using RestaurantsClasses.WorkersSystem;
using System.IO;
using System.Windows;
using ui.Helper;
using ui.WorkerWorkspacePages;

namespace ui
{
    /// <summary>
    /// Логика взаимодействия для WorkerWorkspace.xaml
    /// </summary>
    public partial class WorkerWorkspace : Window
    {
        private Window _previous;
        private Worker _worker;
        public WorkerWorkspace(Window previous, Worker worker)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;

            nameLabel.Content = $"Добро пожаловать, {worker.FirstName} {worker.LastName}!";
            roleLabel.Content = $"{RequestClient.GetPositionName(worker.PositionId)}";
        }

        // кнопка выхода
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        // переход на страницу с новыми заказами
        private void newOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            new NewOrders(this, _worker).Show();
            Hide();
        }


        // посмотреть заказы текущего работника
        private void yourOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            new WorkerOrders(this, _worker).Show();
            Hide();
        }

        // посмотреть онлайн заказы
        private void onlineOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            new OnlineOrders(this, _worker).Show();
            Hide();
        }
    }
}

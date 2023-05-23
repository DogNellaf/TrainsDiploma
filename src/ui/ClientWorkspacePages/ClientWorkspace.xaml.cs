using RestaurantsClasses.OnlineSystem;
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
using ui.WorkerWorkspacePages;

namespace ui
{
    /// <summary>
    /// Логика взаимодействия для ClientWorkspace.xaml
    /// </summary>
    public partial class ClientWorkspace : Window
    {
        private Client _client;
        private Window _previous;
        public ClientWorkspace(Client client, Window previous)
        {
            _client = client;
            InitializeComponent();
            //nameLabel.Content = $"Добро пожаловать, {_client.FirstName} {_client.SecondName}!";
            nameLabel.Content = $"Добро пожаловать, {_client.Username}!";
            _previous = previous;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        private void addOrderButton_Click(object sender, RoutedEventArgs e)
        {
            new OnlineOrders(this, null, _client).Show();
            Hide();
        }
    }
}

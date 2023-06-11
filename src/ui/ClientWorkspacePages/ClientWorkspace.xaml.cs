using System.Windows;
using TrainsClasses;
using ui.AdminWorkspacePages;
using ui.Helper;

namespace ui
{
    /// <summary>
    /// Логика взаимодействия для ClientWorkspace.xaml
    /// </summary>
    public partial class ClientWorkspace : Window
    {
        private User _client;
        private Window _previous;
        public ClientWorkspace(User client, Window previous)
        {
            _client = client;
            InitializeComponent();
            nameLabel.Content = $"Добро пожаловать, {client.Login.Trim(' ')}!";
            _previous = previous;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        private void yourTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new TicketsByUser(this, _client).Show();
        }

        private void buyTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            new TicketsEditor(_client).Show();
        }

        private void dataButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void balanceButton_Click(object sender, RoutedEventArgs e)
        {
            new BalanceEditor(_client).Show();
        }

        private void calendarButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

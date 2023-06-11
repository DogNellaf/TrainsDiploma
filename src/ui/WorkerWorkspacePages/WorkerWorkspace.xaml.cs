using System.Windows;
using TrainsClasses;
using ui.AdminWorkspacePages;
using ui.Helper;

namespace ui
{
    /// <summary>
    /// Логика взаимодействия для WorkerWorkspace.xaml
    /// </summary>
    public partial class WorkerWorkspace : Window
    {
        private Window _previous;
        private User _user;
        public WorkerWorkspace(Window previous, User user)
        {
            InitializeComponent();
            _previous = previous;
            _user = user;

            nameLabel.Content = $"Добро пожаловать, {user.Login.Trim(' ')}!";
            roleLabel.Content = RequestClient.GetRole(user.RoleId).Name;
        }

        // кнопка выхода
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        private void offlineTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            new OfflineTicketsEditor(this, _user).Show();
        }

        private void ticketsButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new TicketsByUser(this, _user).Show();
        }

        private void routesButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new RouteEditor(this, _user).Show();
        }

        private void calendarButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new WorkerRouteEditor(this, _user).Show();
        }
    }
}

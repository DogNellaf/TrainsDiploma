using System.Windows;
using TrainsClasses;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для AdminWorkspace.xaml
    /// </summary>
    public partial class AdminWorkspace : Window
    {
        private Window _previous;
        private User _user;
        public AdminWorkspace(Window previous, User user)
        {
            InitializeComponent();
            _previous = previous;
            _user = user;

            nameLabel.Content = $"Добро пожаловать, {user.Login.Trim(' ')}!";
            roleLabel.Content = RequestClient.GetRole(user.RoleId).Name;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        public void EditorWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new WorkerEditor(this, _user).Show();
        }

        private void routesButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new RouteEditor(this, _user).Show();
        }

        private void citiesButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new CityEditor(this, _user).Show();
        }

        private void ticketsButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new TicketsByUser(this, _user).Show();
        }

        private void reportsButton_Click(object sender, RoutedEventArgs e)
        {
            new Reports(this, _user).Show();
        }
    }
}

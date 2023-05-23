using System.Windows;
using TrainsClasses;

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

            nameLabel.Content = $"Добро пожаловать, {user.Login}!";
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
    }
}

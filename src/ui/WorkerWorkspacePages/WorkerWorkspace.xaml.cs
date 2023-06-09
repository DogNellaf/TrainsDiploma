using System.Windows;
using TrainsClasses;
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
    }
}

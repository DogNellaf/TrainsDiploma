using System.Windows;
using ui.AdminWorkspacePages;
using ui.Helper;

namespace ui
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class WorkerAuth : Window
    {
        private Window _previous;
        public WorkerAuth(Window previous)
        {
            InitializeComponent();
            _previous = previous;
        }

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameTextBox.Text;
            var password = passwordTextBox.Text;
            var worker = RequestClient.Auth(username, password);
            if (worker is not null)
            {
                if (RequestClient.CheckAdmin(worker.Id))
                {
                    new AdminWorkspace(this, worker).Show();
                }
                else if (RequestClient.CheckWorker(worker.Id))
                {
                    new WorkerWorkspace(this, worker).Show();
                }
                else
                {
                    MessageBox.Show("Пользователь не является сотрудником");
                    return;
                }
                Hide();
            }
            else
            {
                MessageBox.Show("Введены неверные данные");
            }
        }

        private void clientButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }
    }
}

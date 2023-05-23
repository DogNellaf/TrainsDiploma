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
            var worker = RequestClient.AuthWorker(username, password);
            if (worker is not null)
            {
                if (RequestClient.CheckIsItAdmin(worker.Id))
                {
                    new AdminWorkspace(this, worker).Show();
                }
                else
                {
                    new WorkerWorkspace(this, worker).Show();
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

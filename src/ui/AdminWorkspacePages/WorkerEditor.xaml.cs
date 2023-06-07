using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using TrainsClasses;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для WorkerEditor.xaml
    /// </summary>
    /// 

    public class UserItem
    {
        public int Id { get; set;}
        public string Login { get; set;}
        public string Password { get; set; }
    }

    public partial class WorkerEditor : Window
    {
        private Window _previous;
        private User _admin;
        public WorkerEditor(Window previous, User admin)
        {
            InitializeComponent();
            _previous = previous;
            _admin = admin;

            var users = RequestClient.GetUsers();

            workersGrid.ItemsSource = users;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Hide(); 
        }

        private bool isManualEditCommit;

        private void workersGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (!isManualEditCommit)
            {
                isManualEditCommit = true;
                DataGrid grid = (DataGrid)sender;

                int selectedRowIndex = grid.SelectedIndex;

                grid.CommitEdit(DataGridEditingUnit.Row, true);

                var items = ((DataGrid)sender).Items;

                var userData = (UserItem)items[selectedRowIndex];

                if (string.IsNullOrEmpty(userData.Login))
                {
                    MessageBox.Show("Перед сохранением введите логин пользователя");
                    return;
                }

                if (string.IsNullOrEmpty(userData.Password))
                {
                    MessageBox.Show("Перед сохранением введите пароль");
                    return;
                }

                //if (string.IsNullOrEmpty(workerData.Login))
                //{
                //    MessageBox.Show("Перед сохранением введите фамилию пользователя");
                //    return;
                //} 

                var users = RequestClient.GetUsers();

                var user = users.Where(x => x.Login == userData.Login).FirstOrDefault();

                if (user is null)
                {
                    //TODO создание
                    RequestClient.CreateWorker(userData.Login, userData.Password, _admin.Token);

                    MessageBox.Show("Пользователь был успешно создан");

                    workerIdBox.Text = users.Last().Id.ToString();
                    changePasswordButton_Click(sender, null);
                }
                else
                {
                    //TODO сохранение
                    RequestClient.UpdateWorker(userData.Id, userData.Login, userData.Password, _admin.Token);

                    MessageBox.Show("Данные пользователя успешно обновлены");

                    exitButton_Click(sender, null);
                }

                isManualEditCommit = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            workersGrid.Columns.RemoveAt(workersGrid.Columns.Count - 2);
            workersGrid.Columns[workersGrid.Columns.Count-1].IsReadOnly = true;
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var rawWorkerId = workerIdBox.Text;

            if (!int.TryParse(rawWorkerId, out int workerId))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }
                

            var user = RequestClient.GetUsers().FirstOrDefault(x => x.Id == workerId);
            if (user is null)
            {
                MessageBox.Show("Такого сотрудника не существует");
                return;
            }

            string password = RequestClient.GenerateNewPassword(workerId, _admin.Token);
            newPasswordBox.Text = password;
            newPasswordBox.IsEnabled = true;
        }

        // удплить сотрудника по id
        private void removeWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            var rawWorkerId = workerIdBox.Text;

            if (!int.TryParse(rawWorkerId, out int workerId))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }


            var user = RequestClient.GetUsers().FirstOrDefault(x => x.Id == workerId);
            if (user is null)
            {
                MessageBox.Show("Такого сотрудника не существует");
                return;
            }

            if (user.Id == _admin.Id)
            {
                MessageBox.Show("Себя нельзя удалить");
                return;
            }

            RequestClient.DeleteWorker(user.Id);
            MessageBox.Show("Сотрудник успешно удален");
            exitButton_Click(null, null);
        }
    }
}

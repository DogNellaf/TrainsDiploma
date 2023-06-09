using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using TrainsClasses;
using ui.Helper;

namespace ui.AdminWorkspacePages
{

    public partial class WorkerEditor : Window
    {
        private Window _previous;
        private User _admin;
        public WorkerEditor(Window previous, User admin)
        {
            InitializeComponent();
            _previous = previous;
            _admin = admin;

            var users = RequestClient.GetObjects<User>();

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

                var userData = (User)items[selectedRowIndex];

                if (string.IsNullOrEmpty(userData.Login))
                {
                    MessageBox.Show("Перед сохранением введите логин пользователя");
                    return;
                }

                if (string.IsNullOrEmpty(userData.RoleId.ToString()) || userData.RoleId <= 0)
                {
                    MessageBox.Show("Перед сохранением введите правильный id роли");
                    return;
                }

                var role = RequestClient.GetObject<Role>(userData.RoleId);

                if (role is null)
                {
                    MessageBox.Show("Перед сохранением введите правильный id роли");
                    return;
                }

                var users = RequestClient.GetObjects<User>();

                var user = users.Where(x => x.Id == userData.Id).FirstOrDefault();

                if (user is null)
                {

                    // добавить считывание данных
                    RequestClient.CreateUser(userData.Login, userData.Token, userData.Balance, userData.RoleId, userData.PassportSeries, userData.PassportNumber);

                    MessageBox.Show("Пользователь был успешно создан");
                }
                else
                {
                    RequestClient.UpdateUser(userData.Id, userData.Login, userData.Token, userData.Balance, userData.RoleId, userData.PassportSeries, userData.PassportNumber);

                    MessageBox.Show("Данные пользователя успешно обновлены");
                }


                isManualEditCommit = false;
                exitButton_Click(sender, null);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            workersGrid.Columns[workersGrid.Columns.Count - 1].IsReadOnly = true;
            workersGrid.Columns[1].IsReadOnly = true;
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var rawWorkerId = workerIdBox.Text;

            if (!int.TryParse(rawWorkerId, out int workerId))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }

            RequestClient.GenerateNewPassword(workerId, newPasswordBox.Text, _admin.Token);
            MessageBox.Show("Пароль успешно изменен");
            exitButton_Click(null, null);
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


            var user = RequestClient.GetObjects<User>().FirstOrDefault(x => x.Id == workerId);
            if (user is null)
            {
                MessageBox.Show("Такой сотрудник не существует");
                return;
            }

            if (user.Id == _admin.Id)
            {
                MessageBox.Show("Себя нельзя удалить");
                return;
            }

            RequestClient.Delete<User>(user.Id);
            MessageBox.Show("Сотрудник успешно удален");
            exitButton_Click(null, null);
        }
    }
}

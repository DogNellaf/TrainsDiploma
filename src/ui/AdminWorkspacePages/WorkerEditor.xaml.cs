using RestaurantsClasses.WorkersSystem;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для WorkerEditor.xaml
    /// </summary>
    /// 

    public class WorkerItem
    {
        public int Id { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ButtonText { get; set; }
    }

    public partial class WorkerEditor : Window
    {
        private Window _previous;
        private Worker _admin;
        public WorkerEditor(Window previous, Worker admin)
        {
            InitializeComponent();
            _previous = previous;
            _admin = admin;

            var workers = RequestClient.GetWorkers();

            //var genNewPassword = new FrameworkElementFactory(typeof(Button));
            //genNewPassword.SetBinding(Button.NameProperty, new Binding("Id"));
            //genNewPassword.SetBinding(Button.ContentProperty, new Binding("ButtonText"));
            //genNewPassword.AddHandler(Button.ClickEvent, new RoutedEventHandler((o, e) => getNewPassword(o, e)));

            //workersGrid.Columns.Add(
            //    new DataGridTextColumn()
            //    {
            //        Header = "Имя",
            //        Binding = new Binding("FirstName"),
            //        Width = 200
            //    }
            //);

            //workersGrid.Columns.Add(
            //    new DataGridTextColumn()
            //    {
            //        Header = "Фамилия",
            //        Binding = new Binding("LastName"),
            //        Width = 110
            //    }
            //);

            //workersGrid.Columns.Add(
            //    new DataGridTextColumn()
            //    {
            //        Header = "Имя пользователя",
            //        Binding = new Binding("Username"),
            //        Width = 110
            //    }
            //);




            //foreach (var worker in workers)
            //{
            //    workersGrid.Items.Add(new WorkerItem()
            //    {
            //        Id = worker.id,
            //        FirstName = worker.FirstName,
            //        LastName = worker.LastName,
            //        Username = worker.Username,
            //        Password = worker.Password,
            //        ButtonText = $"Сгенерировать новый пароль для {worker.id}"
            //    });
            //}

            workersGrid.ItemsSource = workers;

            //workersGrid.Columns.Add(
            //    new DataGridTemplateColumn()
            //    {
            //        Header = "Сгенерировать пароль",
            //        CellTemplate = new DataTemplate() { VisualTree = genNewPassword },
            //        Width= 150
            //    }
            //);
        }

        //private void getNewPassword(object sender, RoutedEventArgs e)
        //{

        //    var buttonContent = ((Button)sender).Content.ToString();

        //    if (buttonContent == string.Empty)
        //        return;

        //    string rawWorkerId = buttonContent.Split(' ').Last();
        //    if (!int.TryParse(rawWorkerId, out int workerId))
        //        return;

        //    string password = RequestClient.GenerateNewPassword(workerId, _admin.id);
        //    newPasswordBox.Text = password;
        //    newPasswordBox.IsEnabled = true;
        //}

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

                var workerData = (Worker)items[selectedRowIndex];

                if (workerData.Username == string.Empty)
                {
                    MessageBox.Show("Перед сохранением введите логин пользователя");
                    return;
                }

                if (workerData.FirstName == string.Empty)
                {
                    MessageBox.Show("Перед сохранением введите имя пользователя");
                    return;
                }

                if (workerData.LastName == string.Empty)
                {
                    MessageBox.Show("Перед сохранением введите фамилию пользователя");
                    return;
                } 

                var workers = RequestClient.GetWorkers();

                var worker = workers.Where(x => x.Username == workerData.Username).FirstOrDefault();

                if (worker is null)
                {
                    //TODO создание
                    RequestClient.CreateWorker(workerData.Username, workerData.FirstName, workerData.LastName, workerData.Phone);

                    MessageBox.Show("Пользователь был успешно создан");

                    workerIdBox.Text = RequestClient.GetWorkers().Last().id.ToString();
                    changePasswordButton_Click(sender, null);
                    //_previous.editorWorkerButton_Click(sender, null);
                    //exitButton_Click(sender, null);
                }
                else
                {
                    //TODO сохранение
                    RequestClient.UpdateWorker(worker.id, workerData.Username, workerData.FirstName, workerData.LastName, workerData.Phone);

                    MessageBox.Show("Данные пользователя успешно обновлены");

                    exitButton_Click(sender, null);
                }

                isManualEditCommit = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            workersGrid.Columns.RemoveAt(workersGrid.Columns.Count - 2);
            //workersGrid.Columns.RemoveAt(workersGrid.Columns.Count - 1);
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
                

            var worker = RequestClient.GetWorkers().Where(x => x.id == workerId).FirstOrDefault();
            if (worker is null)
            {
                MessageBox.Show("Такого сотрудника не существует");
                return;
            }

            string password = RequestClient.GenerateNewPassword(workerId, _admin.id);
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


            var worker = RequestClient.GetWorkers().Where(x => x.id == workerId).FirstOrDefault();
            if (worker is null)
            {
                MessageBox.Show("Такого сотрудника не существует");
                return;
            }

            if (worker.id == _admin.id)
            {
                MessageBox.Show("Себя нельзя удалить");
                return;
            }

            RequestClient.DeleteWorker(worker.id);
            MessageBox.Show("Сотрудник успешно удален");
            exitButton_Click(null, null);
        }
    }
}

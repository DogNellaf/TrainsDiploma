using System.Windows;
using TrainsClasses;

namespace ui
{
    /// <summary>
    /// Логика взаимодействия для WorkerWorkspace.xaml
    /// </summary>
    public partial class WorkerWorkspace : Window
    {
        private Window _previous;
        private User _worker;
        public WorkerWorkspace(Window previous, User worker)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;

            nameLabel.Content = $"Добро пожаловать, {worker.Login}!";
            //roleLabel.Content = $"{RequestClient.GetPositionName(worker.PositionId)}";
        }

        // кнопка выхода
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }
    }
}

using System.Linq;
using System.Windows;
using TrainsClasses;
using ui.Helper;

namespace ui
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private Window _previous;
        public Register(Window previous)
        {
            _previous = previous;
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e) => BackToLogin();


        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            var login = usernameTextBox.Text;

            var user = RequestClient.GetObjects<User>().Where(x => x.Login.Trim() == login).FirstOrDefault();

            if (user is not null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }

            var password = passwordTextBox.Text;
            var client = RequestClient.Register(login, password);
            if (client is not null)
            {
                MessageBox.Show("Вы успешно зарегистрированы");
                BackToLogin();
            }
            else
            {
                MessageBox.Show("Не удалось зарегистрироваться");
            }
        }

        private void BackToLogin()
        {
            _previous.Show();
            Close();
        }

    }
}

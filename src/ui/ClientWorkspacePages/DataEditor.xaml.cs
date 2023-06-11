using System;
using System.Linq;
using System.Windows;
using TrainsClasses;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    public partial class DataEditor : Window
    {
        private User _user;
        public DataEditor(User user)
        {
            InitializeComponent();
            _user = user;
            loginBox.Text = user.Login;
            serialBox.Text = user.PassportSeries;
            numberBox.Text = user.PassportNumber;
        }

        private void passwordButton_Click(object sender, RoutedEventArgs e)
        {
            string password = DateTime.Now.ToFileTimeUtc().ToString();
            MessageBox.Show($"Новый пароль: {password} - обязательно запишите, восстановить невозможно");
            RequestClient.GenerateNewPassword(_user.Id, password, _user.Token);
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var login = loginBox.Text;
            var series = serialBox.Text;
            var number = numberBox.Text;

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Не был введен логин");
                return;
            }

            var user = RequestClient.GetObjects<User>().Where(x => x.Login == login && x.Id != _user.Id).FirstOrDefault();

            if (user is not null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                Close();
            }

            if (string.IsNullOrEmpty(series))
            {
                MessageBox.Show("У вас не заполнена серия паспорта");
                Close();
            }

            if (series.Length != 4)
            {
                MessageBox.Show("У вас некорректный формат серии паспорта (нет 4 знаков)");
                Close();
            }

            if (!int.TryParse(series, out int result))
            {
                MessageBox.Show("У вас некорректный формат серии паспорта (не только цифры)");
                Close();
            }

            if (string.IsNullOrEmpty(number))
            {
                MessageBox.Show("У вас не заполнен номер паспорта");
                Close();
            }

            if (number.Length != 6)
            {
                MessageBox.Show("У вас некорректный формат номера паспорта (нет 6 знаков)");
                Close();
            }

            if (!int.TryParse(number, out int result2))
            {
                MessageBox.Show("У вас некорректный формат номера паспорта (не только цифры)");
                Close();
            }

            _user.Login = login;
            _user.PassportSeries = series;
            _user.PassportNumber = number;
            RequestClient.UpdateUser(_user.Id, login, _user.Token, _user.Balance, _user.RoleId, series, number);
            MessageBox.Show("Данные успешно обновлены");
        }
    }
}

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
            loginBox.Text = user.Login.Trim();
            serialBox.Text = user.PassportSeries.Trim();
            numberBox.Text = user.PassportNumber.Trim();
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

            var user = RequestClient.GetObjects<User>().Where(x => x.Login.Trim() == login && x.Id != _user.Id).FirstOrDefault();

            if (user is not null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
                return;
            }

            if (string.IsNullOrEmpty(series))
            {
                MessageBox.Show("У вас не заполнена серия паспорта");
                return;
            }

            if (series.Length != 4)
            {
                MessageBox.Show("У вас некорректный формат серии паспорта (нет 4 знаков)");
                return;
            }

            if (!int.TryParse(series, out int result))
            {
                MessageBox.Show("У вас некорректный формат серии паспорта (не только цифры)");
                return;
            }

            if (string.IsNullOrEmpty(number))
            {
                MessageBox.Show("У вас не заполнен номер паспорта");
                return;
            }

            if (number.Length != 6)
            {
                MessageBox.Show("У вас некорректный формат номера паспорта (нет 6 знаков)");
                return;
            }

            if (!int.TryParse(number, out int result2))
            {
                MessageBox.Show("У вас некорректный формат номера паспорта (не только цифры)");
                return;
            }

            _user.Login = login;
            _user.PassportSeries = series;
            _user.PassportNumber = number;
            RequestClient.UpdateUser(_user.Id, login, _user.Token, _user.Balance, _user.RoleId, series, number);
            MessageBox.Show("Данные успешно обновлены");
        }
    }
}

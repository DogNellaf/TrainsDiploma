using System;
using System.Windows;
using TrainsClasses;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    public partial class BalanceEditor : Window
    {
        private User _user;
        public BalanceEditor(User user)
        {
            InitializeComponent();
            _user = user;

            if (string.IsNullOrEmpty(_user.PassportSeries))
            {
                MessageBox.Show("У вас не заполнена серия паспорта");
                Close();
            }

            if (_user.PassportSeries.Length != 4)
            {
                MessageBox.Show("У вас некорректный формат серии паспорта (нет 4 знаков)");
                Close();
            }

            if (!int.TryParse(_user.PassportSeries, out int result))
            {
                MessageBox.Show("У вас некорректный формат серии паспорта (не только цифры)");
                Close();
            }

            if (string.IsNullOrEmpty(_user.PassportNumber))
            {
                MessageBox.Show("У вас не заполнен номер паспорта");
                Close();
            }

            if (_user.PassportNumber.Length != 6)
            {
                MessageBox.Show("У вас некорректный формат номера паспорта (нет 6 знаков)");
                Close();
            }

            if (!int.TryParse(_user.PassportNumber, out int result2))
            {
                MessageBox.Show("У вас некорректный формат номера паспорта (не только цифры)");
                Close();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var value = float.Parse(balanceBox.Text);

            RequestClient.CreateTransaction(_user.Id, value, DateTime.Now, "Баланс", "Пополнение баланса", true);

            RequestClient.UpdateUser(_user.Id, _user.Login, _user.Token, _user.Balance + value, _user.RoleId, _user.PassportSeries, _user.PassportNumber);

            _user.Balance += value;

            MessageBox.Show("Баланс успешно пополнен");
            Close();
        }
    }
}

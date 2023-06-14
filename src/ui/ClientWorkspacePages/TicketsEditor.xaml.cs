using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TrainsClasses;
using TrainsClasses.Enums;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    public partial class TicketsEditor : Window
    {
        private User _user;
        public TicketsEditor(User user)
        {
            InitializeComponent();
            _user = user;
            _user.PassportSeries = _user.PassportSeries.Trim();
            _user.PassportNumber = _user.PassportNumber.Trim();
            datePicker.SelectedDate = DateTime.Now;
            ShowBalance();
        }

        private void ShowBalance()
        {
            balanceBox.Text = _user.Balance.ToString();
        }

        private void sellButton_Click(object sender, RoutedEventArgs e)
        {
            var date = datePicker.DisplayDate;
            var now = DateTime.Now;
            int routeId = -1;
            try
            {
                routeId = int.Parse(routesBox.SelectedItem.ToString().Split(' ')[1]);
            }
            catch
            {
                MessageBox.Show("Не выбран рейс");
            }

            var route = RequestClient.GetObjects<Route>().Where(x => x.Id == routeId).FirstOrDefault();

            if (route is null)
            {
                MessageBox.Show("Не выбран рейс");
                return;
            }

            // создание билета
            var ticket = new Ticket(-1, now, route.Price, route.Id, (int)Status.Created, _user.Id);

            // если не хватает средств
            if (_user.Balance < route.Price)
            {
                MessageBox.Show("Не хватает денег на балансе аккаунта");
                return;
            }
            // созданме транзакции
            RequestClient.CreateTransaction(_user.Id, -route.Price, now, "Баланс", "Оплата билета", true);

            // уменьшение баланса пользователя
            _user.Balance -= route.Price;
            ShowBalance();
            RequestClient.UpdateUser(_user.Id, _user.Login, _user.Token, _user.Balance, _user.RoleId, _user.PassportSeries, _user.PassportNumber);

            // создание билета
            ticket = RequestClient.CreateTicket(now, route.Price, route.Id, (int)Status.Created, _user.Id);

            MessageBox.Show("Билет успешно куплен");
        }

        // когда дата изменена, нужно подгрузить рейсы в этот день
        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var date = datePicker.SelectedDate.Value;
            var routes = RequestClient.GetObjects<Route>().
                Where(x => x.DepartureTime.Month == date.Month && x.DepartureTime.Year == date.Year && x.DepartureTime.Day == date.Day);
            var cities = RequestClient.GetObjects<City>();
            var routesNames = new List<string>();
            foreach (var route in routes)
            {
                var arrivalName = cities.Where(x => x.Id == route.ArrivalCityId).First().Name.Trim();
                var departureName = cities.Where(x => x.Id == route.DepartureCityId).First().Name.Trim();

                routesNames.Add($"Рейс {route.Id} {departureName} - {arrivalName} в {route.DepartureTime:T}");
            }
            routesBox.ItemsSource = routesNames;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
    }
}

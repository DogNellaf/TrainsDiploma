using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using TrainsClasses;
using ui.Helper;
using TrainsClasses.Enums;

namespace ui.AdminWorkspacePages
{

    public partial class OfflineTicketsEditor : Window
    {
        private Window _previous;
        private User _worker;
        private List<Route> _routes = new List<Route>();
        private ReportCreator _reportCreator;
        public OfflineTicketsEditor(Window previous, User worker)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;
            _reportCreator = new ReportCreator();
            datePicker.SelectedDate = DateTime.Now;
        }

        private void sellButton_Click(object sender, RoutedEventArgs e)
        {
            var login = loginBox.Text;
            var password = passwordBox.Text;
            var series = serialBox.Text;
            var number = numberBox.Text;
            var date = datePicker.DisplayDate;
            var now = DateTime.Now;
            var routeId = int.Parse(routesBox.SelectedItem.ToString().Split(' ')[1]);
            bool isNewUser = false;

            var route = RequestClient.GetObjects<Route>().Where(x => x.Id == routeId).FirstOrDefault();

            if (route is null)
            {
                MessageBox.Show("Не выбран рейс");
                return;
            }

            if (route.DepartureTime <= DateTime.Now)
            {
                MessageBox.Show("Рейс уже ушел, нельзя купить билет");
                return;
            }

            var tickets = RequestClient.GetRouteTickets(route.Id);
            if (tickets.Count > 1200)
            {
                MessageBox.Show("В поезде нет мест");
                return;
            }

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Не был введен логин");
                return;
            }

            var user = RequestClient.GetObjects<User>().Where(x => x.Login.Trim() == login).FirstOrDefault();

            if (user is null)
            {
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Не был введен пароль");
                    return;
                }

                if (string.IsNullOrEmpty(series))
                {
                    MessageBox.Show("Не была введена серия паспорта");
                    return;
                }

                if (series.Length != 4)
                {
                    MessageBox.Show("Серия должна содержать четыре цифры");
                    return;
                }

                if (!int.TryParse(series, out int result))
                {
                    MessageBox.Show("Серия должна состоять из цифр");
                    return;
                }

                if (string.IsNullOrEmpty(number))
                {
                    MessageBox.Show("Не был введен номер паспорта");
                    return;
                }

                if (number.Length != 6)
                {
                    MessageBox.Show("Номер должен содержать шесть цифр");
                    return;
                }

                if (!int.TryParse(number, out int result2))
                {
                    MessageBox.Show("Номер должен состоять из цифр");
                    return;
                }

                user = RequestClient.CreateUser(login, password, 0, 1, series, number);
                isNewUser = true;
                MessageBox.Show("Пользователь отсутствует в базе, создан новый");
            }

            // создание билета
            var ticket = new Ticket(-1, now, route.Price, route.Id, (int)Status.Created, user.Id);

            // если покупка с баланса
            if (balanceButton.IsChecked == true)
            {
                // если не хватает средств
                if (user.Balance < route.Price)
                {
                    MessageBox.Show("Не хватает денег на балансе аккаунта");
                    return;
                }
                else
                {
                    // созданме транзакции
                    RequestClient.CreateTransaction(user.Id, -route.Price, now, "Баланс", "Оплата билета", true);

                    // уменьшение баланса пользователя
                    user.Balance -= route.Price;
                    RequestClient.UpdateUser(user.Id, user.Login, user.Token, user.Balance, user.RoleId, user.PassportSeries, user.PassportNumber);
                }
            } 
            else
            {
                // созданме транзакции
                RequestClient.CreateTransaction(user.Id, route.Price, now, "Наличные", "Пополнение баланса наличными через диспетчера", true);
                RequestClient.CreateTransaction(user.Id, -route.Price, now, "Наличные", "Оплата билета", true);
            }

            // создание билета
            ticket = RequestClient.CreateTicket(now, route.Price, route.Id, (int)Status.Created, user.Id);

            // генерация билета
            _reportCreator.GenerateTicketFile(ticket);
            _reportCreator.Path = $"ticket_{DateTime.Now.ToFileTimeUtc()}.docx";

            MessageBox.Show($"Билет успешно продан, создан новый билет в файле {_reportCreator.Path}");
        }

        // когда дата изменена, нужно подгрузить рейсы в этот день
        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var date = datePicker.SelectedDate.Value;
            var _routes = RequestClient.GetObjects<Route>().
                Where(x => x.DepartureTime.Month == date.Month && x.DepartureTime.Year == date.Year && x.DepartureTime.Day == date.Day);
            var cities = RequestClient.GetObjects<City>();
            var routesNames = new List<string>();
            foreach (var route in _routes)
            {
                var arrivalName = cities.Where(x => x.Id == route.ArrivalCityId).First().Name.Trim();
                var departureName = cities.Where(x => x.Id == route.DepartureCityId).First().Name.Trim();

                routesNames.Add($"Рейс {route.Id} {departureName} - {arrivalName} в {route.DepartureTime:T}");
            }
            routesBox.ItemsSource = routesNames;
        }

        private void passwordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (passwordBox.Text.Length > 0)
            {
                balanceButton.IsEnabled = false;
            }
            else
            {
                balanceButton.IsEnabled = true;
            }
        }

        private void serialBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (passwordBox.Text.Length > 0)
            {
                balanceButton.IsEnabled = false;
            }
            else
            {
                balanceButton.IsEnabled = true;
            }
        }

        private void numberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (passwordBox.Text.Length > 0)
            {
                balanceButton.IsEnabled = false;
            }
            else
            {
                balanceButton.IsEnabled = true;
            }
        }

        private void findUser_Click(object sender, RoutedEventArgs e)
        {
            var login = loginBox.Text;

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Не был введен логин");
                return;
            }

            var user = RequestClient.GetObjects<User>().Find(x => x.Login.Trim() == login);

            if (user is null)
            {
                passwordBox.IsEnabled = true;
                numberBox.Text = "";
                numberBox.IsEnabled = true;
                serialBox.Text = "";
                serialBox.IsEnabled = true;
                MessageBox.Show("Пользователь не найден");
                return;
            }

            loginBox.IsReadOnly = true;
            passwordBox.IsEnabled = false;
            numberBox.Text = user.PassportNumber;
            numberBox.IsEnabled = false;
            serialBox.Text = user.PassportSeries;
            serialBox.IsEnabled = false;
            MessageBox.Show("Пользователь найден");
        }
    }
}

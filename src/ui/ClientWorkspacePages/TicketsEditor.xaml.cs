using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TrainsClasses;
using TrainsClasses.Enums;
using ui.Helper;
using Xceed.Document.NET;
using Xceed.Words.NET;

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

            if (routesBox.SelectedItem is null)
            {
                MessageBox.Show("Не выбран рейс");
                return;
            }

            var routeData = routesBox.SelectedItem.ToString().Split(' ');
            if (routeData.Length == 0)
            {
                MessageBox.Show("Не выбран рейс");
                return;
            }

            if (!int.TryParse(routeData[1], out int routeId))
            {
                MessageBox.Show("Некорректные данные рейса");
                return;
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

        // генерация билета
        private void GenerateDocxTicket()
        {
            //var path = $"ticket_{DateTime.Now.ToFileTimeUtc}";

            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}

            //var doc = DocX.Create(path);

            //var paragraph = doc.InsertParagraph(ticketsButton.Content.ToString(), false, titleFormat);
            //paragraph.LineSpacing = 15;

            //var tickets = RequestClient.GetObjects<Ticket>().Where(x => x.BuyTime.Month == DateTime.Now.Month && x.StatusId != 2).ToList();
            //var routes = RequestClient.GetObjects<Route>();
            //var cities = RequestClient.GetObjects<City>();

            //Table t = doc.AddTable(tickets.Count + 1, 3);
            //t.Alignment = Alignment.center;

            //t.Rows[0].Cells[0].Paragraphs.First().Append("Время оформления");
            //t.Rows[0].Cells[1].Paragraphs.First().Append("Цена");
            //t.Rows[0].Cells[2].Paragraphs.First().Append("Направление");

            //float sum = 0;

            //for (int i = 0; i < tickets.Count; i++)
            //{
            //    var ticket = tickets[i];
            //    sum += ticket.Price;

            //    var route = routes.Where(x => x.Id == ticket.RouteId).First();
            //    var cityTo = cities.Find(x => x.Id == route.ArrivalCityId).Name.Trim();
            //    var cityFrom = cities.Find(x => x.Id == route.DepartureCityId).Name.Trim();

            //    t.Rows[i + 1].Cells[0].Paragraphs.First().Append($"{ticket.BuyTime}");
            //    t.Rows[i + 1].Cells[1].Paragraphs.First().Append($"{ticket.Price}");
            //    t.Rows[i + 1].Cells[2].Paragraphs.First().Append($"Рейс {route.Id} {cityFrom} - {cityTo}");
            //}

            //doc.InsertTable(t);

            //doc.InsertParagraph($"Всего куплено билетов: {tickets.Count}", false, textFormat);

            //doc.InsertParagraph($"Куплено билетов на сумму: {sum}", false, textFormat);

            //doc.Save();

            //MessageBox.Show($"Отчет сохранен в файл {path}");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using TrainsClasses;
using ui.Helper;

namespace ui.AdminWorkspacePages
{

    public partial class OfflineTicketsEditor : Window
    {
        private Window _previous;
        private User _admin;
        public OfflineTicketsEditor(Window previous, User admin)
        {
            InitializeComponent();
            _previous = previous;
            _admin = admin;

            var routes = RequestClient.GetObjects<Route>();
            routesBox.ItemsSource = routes;
        }

        private void sellButton_Click(object sender, RoutedEventArgs e)
        {
            var login = loginBox.Text;
            var password = passwordBox.Text;
            var date = datePicker.DisplayDate;
            var route = (Route)routesBox.SelectedItem;

            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Не был введен логин");
                return;
            }

            //TODO перенести в создание
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Не был введен пароль");
                return;
            }


            if (date.Year <= 1970)
            {
                MessageBox.Show("Год должен быть больше 1970");
                return;
            }

            if (route is null)
            {
                MessageBox.Show("Не выбран рейс");
                return;
            }

            var user = RequestClient.GetObjects<User>().Where(x => x.Login == login).FirstOrDefault();

            if (user is null)
            {
                RequestClient.CreateUser(login, password, 0, 3, "", ""); // TODO: добавить считывание данных

                MessageBox.Show("Пользователь отсутствует в базе, создан новый");
            }

            MessageBox.Show("Билет успешно продан");
            Close();
        }

        // когда дата изменена, нужно подгрузить рейсы в этот день
        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var date = datePicker.SelectedDate.Value;
            routesBox.ItemsSource = RequestClient.GetObjects<Route>().
                Where(x => x.DepartureTime.Month == date.Month && x.DepartureTime.Year == date.Year && x.DepartureTime.Day == date.Day);
        }
    }
}

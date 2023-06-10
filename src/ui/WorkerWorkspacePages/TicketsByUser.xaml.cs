using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using TrainsClasses;
using ui.Helper;

namespace ui.AdminWorkspacePages
{

    public partial class TicketsByUser : Window
    {
        private Window _previous;
        private User _user;
        public TicketsByUser(Window previous, User user)
        {
            InitializeComponent();
            _previous = previous;
            _user = user;

            var role = RequestClient.GetRole(user.RoleId);

            if (role == null)
            {
                MessageBox.Show("Внутренняя ошибка: неизвестная роль. Досрочно закрываем окно.");
            }

            if (role.IsWorker)
            {
                idLabel.Content = "Login клиента";
                returnButton.Content = "Найти билеты";
                returnButton.Click -= returnButton_Click;
                returnButton.Click += FindClientTickets;
                ticketsGrid.ItemsSource = RequestClient.GetObjects<Ticket>();
            }
            else
            {
                //RemoveLogicalChild(allButton);
                //RemoveVisualChild(allButton);
                ticketsGrid.ItemsSource = RequestClient.GetUserTickets(user.Id, user);
                allButton.Content = "Только активные";
                allButton.Click -= allButton_Click;
                allButton.Click += FindMyActiveTickets;
                //ticketsGrid.ItemsSource = RequestClient.GetObjects<Ticket>();
            }
            ticketsGrid.IsReadOnly = true;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Hide(); 
        }

        private void FindClientTickets(object sender, RoutedEventArgs e)
        {
            var login = idBox.Text;
            var user = RequestClient.GetObjects<User>().Where(x => x.Login.Trim() == login).FirstOrDefault();

            if (user is null)
            {
                MessageBox.Show("Пользователь с таким логином не существует");
                return;
            }


            ticketsGrid.ItemsSource = RequestClient.GetUserTickets(user.Id, _user);
            ticketsGrid.IsReadOnly = true;
            MessageBox.Show("Билеты успешно выбраны");
        }

        private void FindMyActiveTickets(object sender, RoutedEventArgs e)
        {
            ticketsGrid.ItemsSource = RequestClient.GetUserTickets(_user.Id, _user).Where(x => x.StatusId == 1);
            ticketsGrid.IsReadOnly = true;
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            var rawId = idBox.Text;

            if (!int.TryParse(rawId, out int id))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }

            var tickets = RequestClient.GetUserTickets(_user.Id, _user);


            var ticket = tickets.FirstOrDefault(x => x.Id == id);
            if (ticket is null)
            {
                MessageBox.Show("Такой билет не существует");
                return;
            }

            RequestClient.ReturnTicket(id, _user);
            MessageBox.Show("Билет возвращен");
            exitButton_Click(null, null);
        }

        private void allButton_Click(object sender, RoutedEventArgs e)
        {
            ticketsGrid.ItemsSource = RequestClient.GetObjects<Ticket>();
            ticketsGrid.IsReadOnly = true;
            MessageBox.Show("Выбраны все билеты");
        }
    }
}

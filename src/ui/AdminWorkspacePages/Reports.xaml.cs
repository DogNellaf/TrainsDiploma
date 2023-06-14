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
using System.Diagnostics;
using Xceed.Words.NET;
using System.IO;
using Xceed.Document.NET;

namespace ui.AdminWorkspacePages
{

    public partial class Reports : Window
    {
        private Window _previous;
        private User _worker;
        private readonly ReportCreator _reportCreator;

        public Reports(Window previous, User admin)
        {
            InitializeComponent();
            _previous = previous;
            _worker = admin;
            _reportCreator = new ReportCreator();
        }
        
        private void ticketsButton_Click(object sender, RoutedEventArgs e)
        {
            _reportCreator.GenerateTicketReport();

            MessageBox.Show($"Отчет сохранен в файл {_reportCreator.Path}");
        }

        private void userButton_Click(object sender, RoutedEventArgs e)
        {
            var login = Microsoft.VisualBasic.Interaction.InputBox("Введите login:");

            var user = RequestClient.GetObjects<User>().Find(x => x.Login.Trim() == login);

            if (user is null)
            {
                MessageBox.Show("Такой пользователь не существует");
                return;
            }

            _reportCreator.GenerateUserTransactions(user);

            MessageBox.Show($"Отчет сохранен в файл {_reportCreator.Path}");
        }

        private void routeButton_Click(object sender, RoutedEventArgs e)
        {
            var routeIdRaw = Microsoft.VisualBasic.Interaction.InputBox("Введите номер рейса:");

            if (!int.TryParse(routeIdRaw, out int routeId))
            {
                MessageBox.Show("Введено некорректное число");
                return;
            }

            var route = RequestClient.GetObjects<Route>().Find(x => x.Id == routeId);

            if (route is null)
            {
                MessageBox.Show("Такой рейс не существует");
                return;
            }

            _reportCreator.GenerateRouteUsers(route);

            MessageBox.Show($"Отчет сохранен в файл {_reportCreator.Path}");
        }
    }
}

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

    public partial class RouteEditor : Window
    {
        private Window _previous;
        private User _admin;
        public RouteEditor(Window previous, User admin)
        {
            InitializeComponent();
            _previous = previous;
            _admin = admin;

            var routes = RequestClient.GetObjects<Route>();
            //if (routes.Count == 0)
            //{
            //    routes.Add(new Route(0, DateTime.Now, "", 0, ""));
            //}

            routesGrid.ItemsSource = routes;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Hide(); 
        }

        private bool isManualEditCommit;

        private void routesGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (!isManualEditCommit)
            {
                isManualEditCommit = true;
                DataGrid grid = (DataGrid)sender;

                int selectedRowIndex = grid.SelectedIndex;

                grid.CommitEdit(DataGridEditingUnit.Row, true);

                var items = ((DataGrid)sender).Items;
                var cities = RequestClient.GetObjects<City>();
                var data = (Route)items[selectedRowIndex];
                var departureCIty = cities.Where(x => x.Id == data.DepartureCityId).FirstOrDefault();

                if (departureCIty is null)
                {
                    // TODO создавать город
                    //MessageBox.Show("Перед сохранением введите город отправления");
                    //return;
                }

                var arrivalCity = cities.Where(x => x.Id == data.ArrivalCityId).FirstOrDefault();

                if (arrivalCity is null)
                {
                    // TODO создавать город
                    //MessageBox.Show("Перед сохранением введите город прибытия");
                    //return;
                }

                if (data.Price <= 0)
                {
                    MessageBox.Show("Цена должна быть больше 0");
                    return;
                }


                if (data.DepartureTime.Year <= 1970)
                {
                    MessageBox.Show("Год должен быть больше 1970");
                    return;
                }

                if (data.Duration <= 0)
                {
                    MessageBox.Show("Перед сохранением введите длительность поездки");
                    return;
                }

                var routes = RequestClient.GetObjects<Route>();

                var route = routes.Where(x => x.Id == data.Id).FirstOrDefault();

                if (route is null)
                {

                    RequestClient.CreateRoute(data.DepartureTime, data.DepartureCityId, data.Duration, data.ArrivalCityId, data.Price);

                    MessageBox.Show("Направление было успешно создано");
                }
                else
                {
                    RequestClient.UpdateRoute(data.Id, data.DepartureTime, data.DepartureCityId, data.Duration, data.ArrivalCityId, data.Price);

                    MessageBox.Show("Направление было успешно обновлено");
                }

                isManualEditCommit = false;
                exitButton_Click(sender, null);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            routesGrid.Columns[routesGrid.Columns.Count - 1].IsReadOnly = true;
            //routesGrid.Columns[1].IsReadOnly = true;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            var rawWorkerId = routeIdBox.Text;

            if (!int.TryParse(rawWorkerId, out int workerId))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }


            var route = RequestClient.GetObjects<Route>().FirstOrDefault(x => x.Id == workerId);
            if (route is null)
            {
                MessageBox.Show("Такое неправление не существует");
                return;
            }

            RequestClient.Delete<Route>(route.Id);
            MessageBox.Show("Направление успешно удалено");
            exitButton_Click(null, null);
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //if (e.PropertyType == typeof(DateTime))
            //    (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy hh:mm:ss";
        }
    }
}

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TrainsClasses;
using ui.Helper;

namespace ui.AdminWorkspacePages
{

    public partial class WorkerRouteEditor : Window
    {
        private Window _previous;
        private User _admin;
        public WorkerRouteEditor(Window previous, User admin)
        {
            InitializeComponent();
            _previous = previous;
            _admin = admin;

            routesGrid.ItemsSource = RequestClient.GetObjects<Route>().Where(x => x.DepartureTime >= DateTime.Now && x.DepartureTime <= DateTime.Now.AddDays(14)).ToList();
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
                var data = (Route)items[selectedRowIndex];

                if (data.DepartureTime.Year <= 1970)
                {
                    MessageBox.Show("Год должен быть больше 1970");
                    return;
                }

                RequestClient.UpdateRoute(data.Id, data.DepartureTime, data.DepartureCityId, data.Duration, data.ArrivalCityId, data.Price);

                MessageBox.Show("Направление было успешно обновлено");

                isManualEditCommit = false;
                exitButton_Click(sender, null);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            routesGrid.Columns[1].IsReadOnly = true;
            routesGrid.Columns[2].IsReadOnly = true;
            routesGrid.Columns[3].IsReadOnly = true;
            routesGrid.Columns[4].IsReadOnly = true;
            routesGrid.Columns[5].IsReadOnly = true;
        }

        private void routesGrid_Loaded(object sender, RoutedEventArgs e)
        {
            routesGrid.Columns[0].Header = "Время отправления";
            routesGrid.Columns[1].Header = "Id города отправления";
            routesGrid.Columns[2].Header = "Длительность (в минутах)";
            routesGrid.Columns[3].Header = "Город прибытия";
            routesGrid.Columns[4].Header = "Цена билета";
            routesGrid.Columns[5].Header = "Идентификатор";
        }
    }
}

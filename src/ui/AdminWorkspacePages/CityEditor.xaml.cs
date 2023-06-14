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

    public partial class CityEditor : Window
    {
        private Window _previous;
        private User _admin;
        public CityEditor(Window previous, User admin)
        {
            InitializeComponent();
            _previous = previous;
            _admin = admin;

            citiesGrid.ItemsSource = RequestClient.GetObjects<City>();
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
                var data = (City)items[selectedRowIndex];

                if (string.IsNullOrEmpty(data.Name))
                {
                    MessageBox.Show("Перед сохранением введите название города");
                    return;
                }

                var cities = RequestClient.GetObjects<City>();

                var city = cities.Where(x => x.Id == data.Id).FirstOrDefault();

                if (city is null)
                {

                    RequestClient.CreateCity(data.Name);

                    MessageBox.Show("Город был успешно создан");
                }
                else
                {
                    RequestClient.UpdateCity(data.Id, data.Name);

                    MessageBox.Show("Город был успешно обновлен");
                }

                isManualEditCommit = false;
                exitButton_Click(sender, null);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            citiesGrid.Columns[0].Header = "Название города";
            citiesGrid.Columns[1].Header = "Идентификатор";
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            var rawId = routeIdBox.Text;

            if (!int.TryParse(rawId, out int id))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }


            var city = RequestClient.GetObjects<City>().FirstOrDefault(x => x.Id == id);
            if (city is null)
            {
                MessageBox.Show("Такой город не существует");
                return;
            }

            RequestClient.Delete<City>(city.Id);
            MessageBox.Show("Город успешно удалено");
            exitButton_Click(null, null);
        }
    }
}

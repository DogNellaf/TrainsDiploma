using Microsoft.Win32;
using Newtonsoft.Json;
using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses.WorkersSystem;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для AdminWorkspace.xaml
    /// </summary>
    public partial class AdminWorkspace : Window
    {
        private Window _previous;
        private Worker _worker;
        public AdminWorkspace(Window previous, Worker worker)
        {
            InitializeComponent();
            _previous = previous;
            _worker = worker;

            nameLabel.Content = $"Добро пожаловать, {worker.FirstName} {worker.LastName}!";
            roleLabel.Content = $"{RequestClient.GetPositionName(worker.PositionId)}";
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Close();
        }

        public void editorWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new WorkerEditor(this, _worker).Show();
        }

        public void dishesEditorButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MealsEditor(this, _worker).Show();
        }

        public void ingredientsEditorButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new IngredientsEditor(this).Show();
        }


        // экпорт всех оффлайн и онлайн заказов в csv формате
        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            // запись в файл
            var saveFileDialog1 = new SaveFileDialog()
            {
                Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == true)
            {
                // считывание из базы 
                var orders = RequestClient.GetAllOfflineOrders();
                var csv = "Id;ServerId;TableId;Status;Created\n";

                foreach (var order in orders)
                {
                    csv += $"{order.id};{order.ServerId};{order.TableId};{order.Status};{order.Created}\n";
                }

                File.WriteAllText(saveFileDialog1.FileName, csv);
                MessageBox.Show($"Файл успешно сохранен по пути: {saveFileDialog1.FileName}");
            }
        }

        // сделать резеврную копию заказов
        private void backupButton_Click(object sender, RoutedEventArgs e)
        {
            // запись в файл
            var dialog = new SaveFileDialog()
            {
                Filter = "json files (*.json)|*.json|All files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == true)
            {
                // считывание из базы 
                var orders = RequestClient.GetObjects<Ingredient>();
                var json = JsonConvert.SerializeObject(orders);

                File.WriteAllText(dialog.FileName, json);
                MessageBox.Show($"Резеврная копия успешно сохранена по пути: {dialog.FileName}");
            }
        }

        // загрузить резервную копию
        private void loadBackupButton_Click(object sender, RoutedEventArgs e)
        {
            // запись в файл
            var dialog = new OpenFileDialog()
            {
                Filter = "json files (*.json)|*.json|All files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == true)
            {
                // считывание из базы 
                var json = File.ReadAllText(dialog.FileName);
                var ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(json);
                MessageBox.Show($"Резеврная копия успешно считана");
                int newCount = 0;
                int updatedCount = 0;
                foreach (var ingredient in ingredients)
                {
                    var sameIngredient = RequestClient.GetObjects<Ingredient>().Where(x => x.id == ingredient.id).FirstOrDefault();
                    if (sameIngredient is not null)
                    {
                        RequestClient.UpdateIngredient(ingredient.id, ingredient.Name);
                        updatedCount++;
                    }
                    else
                    {
                        RequestClient.CreateIngredient(ingredient.Name);
                        newCount++;
                    }
                }
                MessageBox.Show($"Было создано {newCount} и обновлено {updatedCount} ингредиентов");
            }
        }
    }
}

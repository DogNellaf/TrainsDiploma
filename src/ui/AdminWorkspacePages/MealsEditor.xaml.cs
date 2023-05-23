using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.WorkersSystem;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для WorkerEditor.xaml
    /// </summary>
    /// 

    //public class WorkerItem
    //{
    //    public int Id { get; set;}
    //    public string FirstName { get; set;}
    //    public string LastName { get; set; }
    //    public string Phone { get; set; }
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    public string ButtonText { get; set; }
    //}

    public partial class MealsEditor : Window
    {
        private Window _previous;
        private Worker _admin;
        public MealsEditor(Window previous, Worker admin)
        {
            InitializeComponent();
            _previous = previous;
            _admin = admin;

            var meals = RequestClient.GetObjects<Meal>();

            mealsGrid.ItemsSource = meals;
        }

        //private void getNewPassword(object sender, RoutedEventArgs e)
        //{

        //    var buttonContent = ((Button)sender).Content.ToString();

        //    if (buttonContent == string.Empty)
        //        return;

        //    string rawWorkerId = buttonContent.Split(' ').Last();
        //    if (!int.TryParse(rawWorkerId, out int workerId))
        //        return;

        //    string password = RequestClient.GenerateNewPassword(workerId, _admin.id);
        //    newPasswordBox.Text = password;
        //    newPasswordBox.IsEnabled = true;
        //}

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Hide(); 
        }

        private bool isManualEditCommit;

        private void workersGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (!isManualEditCommit)
            {
                isManualEditCommit = true;
                DataGrid grid = (DataGrid)sender;

                int selectedRowIndex = grid.SelectedIndex;

                grid.CommitEdit(DataGridEditingUnit.Row, true);

                var items = ((DataGrid)sender).Items;

                var mealData = (Meal)items[selectedRowIndex];

                if (mealData.Name == string.Empty)
                {
                    MessageBox.Show("Перед сохранением введите название блюда");
                    return;
                }

                if (mealData.Cost <= 0)
                {
                    MessageBox.Show("Перед сохранением введите стоимость блюда");
                    return;
                }

                if (mealData.Weight <= 0)
                {
                    MessageBox.Show("Перед сохранением введите вес блюда");
                    return;
                }

                if (mealData.ServingsNumber <= 0)
                {
                    MessageBox.Show("Перед сохранением введите количество порций");
                    return;
                }

                var meals = RequestClient.GetObjects<Meal>();

                var worker = meals.Where(x => x.Name == mealData.Name).FirstOrDefault();

                if (worker is null)
                {
                    RequestClient.CreateMeal(mealData.Name, (float)mealData.Cost, (float)mealData.Weight, mealData.ServingsNumber);

                    MessageBox.Show("Блюдо был успешно создано");
                    exitButton_Click(sender, null);
                }
                else
                {
                    RequestClient.UpdateMeal(worker.id, mealData.Name, (float)mealData.Cost, (float)mealData.Weight, mealData.ServingsNumber);

                    MessageBox.Show("Данные блюда успешно обновлены");
                }

                
                isManualEditCommit = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mealsGrid.Columns[mealsGrid.Columns.Count-1].IsReadOnly = true;
        }

        // удплить блюдо по id
        private void removeMealButton_Click(object sender, RoutedEventArgs e)
        {
            var rawMealId = mealIdBox.Text;

            if (!int.TryParse(rawMealId, out int mealId))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }

            var meal = RequestClient.GetObjects<Meal>().Where(x => x.id == mealId).FirstOrDefault();
            if (meal is null)
            {
                MessageBox.Show("Такое блюдо не существует");
                return;
            }

            RequestClient.DeleteMeal(meal.id);
            MessageBox.Show("Блюдо успешно удалено");
            exitButton_Click(null, null);
        }

        private void showIngredientsButton_Click(object sender, RoutedEventArgs e)
        {
            var rawMealId = mealIdBox.Text;

            if (!int.TryParse(rawMealId, out int mealId))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }

            var meal = RequestClient.GetObjects<Meal>().Where(x => x.id == mealId).FirstOrDefault();
            if (meal is null)
            {
                MessageBox.Show("Такое блюдо не существует");
                return;
            }

            new IngredientInMealEditor(this, meal).Show();
            Hide();
        }
    }
}

using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.KontragentsSystem;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    public partial class IngredientInMealEditor : Window
    {
        private Window _previous;
        private Meal _meal;
        public IngredientInMealEditor(Window previous, Meal meal)
        {
            InitializeComponent();
            _previous = previous;
            _meal = meal;

            ingredientsGrid.ItemsSource = RequestClient.GetIngredientsByMeal(meal);
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _previous.Show();
            Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ingredientsGrid.Columns[ingredientsGrid.Columns.Count - 2].IsReadOnly = true;
            ingredientsGrid.Columns[ingredientsGrid.Columns.Count - 1].IsReadOnly = true;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            var rawId = ingredientIdBox.Text;

            if (!int.TryParse(rawId, out int id))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }

            var ingredient = RequestClient.GetObjects<Ingredient>().Where(x => x.id == id).FirstOrDefault();
            if (ingredient is null)
            {
                MessageBox.Show("Такой ингредиент не существует");
                return;
            }

            RequestClient.DeleteIngredientByMeal(_meal.id, ingredient.id);
            exitButton_Click(null, null);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var rawId = ingredientIdBox.Text;

            if (!int.TryParse(rawId, out int id))
            {
                MessageBox.Show("Введите корректное число");
                return;
            }

            var ingredient = RequestClient.GetObjects<Ingredient>().Where(x => x.id == id).FirstOrDefault();
            if (ingredient is null)
            {
                MessageBox.Show("Такой ингредиент не существует");
                return;
            }

            RequestClient.AddIngredientsToMeal(_meal.id, ingredient.id);
            MessageBox.Show("Ингредиент добавлен в блюдо");
            exitButton_Click(null, null);
        }
    }
}

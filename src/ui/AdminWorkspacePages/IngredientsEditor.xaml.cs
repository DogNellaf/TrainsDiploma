using RestaurantsClasees.OrderSystem;
using RestaurantsClasses.KontragentsSystem;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ui.Helper;

namespace ui.AdminWorkspacePages
{
    /// <summary>
    /// Логика взаимодействия для WorkerEditor.xaml
    /// </summary>
    /// 
    /// <summary>
    /// Логика взаимодействия для WorkerEditor.xaml
    /// </summary>
    /// 
    public partial class IngredientsEditor : Window
    {
        private Window _previous;
        public IngredientsEditor(Window previous)
        {
            InitializeComponent();
            _previous = previous;

            ingredientsGrid.ItemsSource = RequestClient.GetObjects<Ingredient>();
        }

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

                var ingredientData = (Ingredient)items[selectedRowIndex];

                if (ingredientData.Name == string.Empty)
                {
                    MessageBox.Show("Перед сохранением введите название ингредиента");
                    return;
                }

                var ingredients = RequestClient.GetObjects<Ingredient>();

                var ingredient = ingredients.Where(x => x.Name == ingredientData.Name).FirstOrDefault();

                if (ingredient is null)
                {
                    RequestClient.CreateIngredient(ingredientData.Name);

                    MessageBox.Show("Ингредиент был успешно создан");
                    exitButton_Click(sender, null);
                }
                //else
                //{
                //    RequestClient.UpdateIngredient(ingredient.id, ingredientData.Name);

                //    MessageBox.Show("Данные ингредиента успешно обновлены");
                //}


                isManualEditCommit = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

            RequestClient.DeleteIngredient(ingredient.id);
            exitButton_Click(null, null);
        }
    }
}

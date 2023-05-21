using System.Data;
using System.Net;
using System.Text;
using System.Xml.Linq;
using TrainsClasses;

namespace RestaurantsClasses
{
    // класс взаимодействия с базой данных
    public static class Database
    {
        //функция получения объектов из базы, где Т - любой наследник класса Model
        public static List<T> GetObject<T>(string where = "", string name = "") where T : Model
        {
            // создаем пустой список объектов
            List<T> objects = new();

            if (name == "")
                name = typeof(T).Name;

            // проверяем, есть ли условие
            string query = $"SELECT * FROM public.\"{name}\"";
            if (where != "")
            {
                query = $"SELECT * FROM public.\"{name}\" where {where}";
            }

            // кидаем запрос на выборку
            DataTable table = ExecuteQuery(query);

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in table.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = new object[1];
                parameters[0] = row.ItemArray;

                // создаем новый объект класса Т
                T? element = Activator.CreateInstance(typeof(T), parameters) as T;

                // добавляем в список
                objects.Add(element);
            }

            //возвращаем результат
            return objects;
        }

        // общая функция удаления объектов
        public static void Delete(string name, int id)
        {
            name = name[0].ToString().ToUpper() + name.TrimStart(name[0]);
            ExecuteQuery($"DELETE FROM public.\"{name}\" WHERE id = {id}");
        }

        // общая функция удаления объектов
        public static void DeleteIngredientByMeal(int meal_id, int id) => ExecuteQuery($"DELETE FROM public.\"Ingredient_to_Meal\" WHERE meal_id = {meal_id} AND ingredient_id = {id}");

        // функция отправки запроса в базу данных
        private static DataTable ExecuteQuery(string query)
        {
            // пустая таблица
            DataTable result = new();

            // пытаемся выполнить кол
            try
            {
                // используя соединение, выполняем дальнейшие команды
                using var connection = new NpgsqlConnection(_connectionString);

                // создаем SQL команду по тексту
                NpgsqlCommand command = new(query, connection);

                // Создаем считывающий элемент
                NpgsqlDataAdapter adapter = new(command);

                // заполняем таблицу
                adapter.Fill(result);
            }

            // если словили ошибку
            catch (Exception ex)
            {
                // закрываем соединение
                Console.WriteLine(ex.Message);
            }

            // возвращаем результат - таблицу
            return result;
        }

        // функция получения ингредиентов по блюду
        public static Dictionary<Ingredient, double> GetIngredients(Meal meal)
        {
            var rawData = ExecuteQuery($"SELECT * FROM public.\"Ingredient_to_Meal\" WHERE meal_id = {meal.id}");
            var result = new Dictionary<Ingredient, double>();

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in rawData.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = row.ItemArray;

                int id = (int)parameters[0];
                var weight = (double)parameters[2];

                // создаем новый объект класса Т
                var ingredient = GetObject<Ingredient>($"id = {id}").FirstOrDefault();

                // добавляем в список
                result.Add(ingredient, weight);
            }
            return result;
        }

        // функция получения ингредиентов по контрагенту
        public static Dictionary<Ingredient, (double weight, double cost)> GetGoods(Kontragent kontragent)
        {
            var rawData = ExecuteQuery($"SELECT * FROM public.\"Ingredient_to_Kontragent\" WHERE kontragent_id = {kontragent.id}");
            var result = new Dictionary<Ingredient, (double weight, double cost)>();

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in rawData.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = row.ItemArray;

                int id = (int)parameters[0];
                var weight = (double)parameters[2];
                var cost = (double)parameters[3];

                // создаем новый объект класса Т
                var ingredient = GetObject<Ingredient>($"id = {id}").FirstOrDefault();

                // добавляем в список
                result.Add(ingredient, (weight, cost));
            }
            return result;
        }

        // функция получения блюд по онлайн заказу
        public static List<Meal> GetOfflineMeals(int order_id)
        {
            var rawData = ExecuteQuery($"SELECT * FROM public.\"Meal_to_Order\" WHERE order_id = {order_id}");
            var result = new List<Meal>();

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in rawData.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = row.ItemArray;
                 
                int id = (int)parameters[0];
                var count = (int)parameters[2];
                var del_count = (int)parameters[3];
                var meal = GetObject<Meal>($"id = {id}").FirstOrDefault();

                for (int i = 0; i < count - del_count; i++)
                {
                    // добавляем в список
                    result.Add(meal);
                }
            }
            return result;
        }

        // функция получения блюд по онлайн заказу
        public static List<Ingredient> GetIngredientsByMeal(int meal_id)
        {
            var rawData = ExecuteQuery($"SELECT * FROM public.\"Ingredient_to_Meal\" WHERE meal_id = {meal_id}");
            var result = new List<Ingredient>();

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in rawData.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = row.ItemArray;

                int id = (int)parameters[0];
                var ingredient = GetObject<Ingredient>($"id = {id}").FirstOrDefault();
                result.Add(ingredient);
            }
            return result;
        }


        // функция добавления ингридента в блюдо
        public static void AddIngredientsToMeal(int meal_id, int ingredient_id) => ExecuteQuery($"INSERT INTO public.\"Ingredient_to_Meal\" VALUES ({ingredient_id}, {meal_id}, 1)");



        // функция получения блюд по офлайн заказу
        //public static Dictionary<Meal, int> GetOfflineMeals(int order_id)
        //{
        //    var rawData = ExecuteQuery($"SELECT * FROM \"Meal_to_Order\" WHERE online_order_id = {order_id}");
        //    var result = new Dictionary<Meal, int>();

        //    // проходимся по каждой строчке таблицы-результата
        //    foreach (DataRow row in rawData.Rows)
        //    {
        //        // в конструктор передаем единственный параметр - все столбцы строки
        //        var parameters = row.ItemArray;

        //        int id = (int)parameters[0];
        //        var count = (int)parameters[2];

        //        // создаем новый объект класса Т
        //        var meal = GetObject<Meal>($"id = {id}").FirstOrDefault();

        //        // добавляем в список
        //        result.Add(meal, count);
        //    }
        //    return result;
        //}

        // функция добавления пользователя
        public static Client AddUser(string username, string password)
        {
            var clients = GetObject<Client>();
            int id = 1;
            if (clients.Count > 0)
            {
                id = clients.Last().id + 1;
            }
            ExecuteQuery($"INSERT INTO public.\"Client\" VALUES ({id}, '{username}', '', '', '{password}')");
            return GetObject<Client>($"id = {id}").FirstOrDefault();
        }

        // функция закрепления оффлайн заказа за сотрудником
        public static void SetOrderToWorker(int order_id, int worker_id) => ExecuteQuery($"UPDATE public.\"Order\" SET worker_id = {worker_id}, status_id = 2 WHERE id = {order_id}");

        // функция закрепления оффлайн заказа за сотрудником
        public static void SetOrderComplete(int order_id) => ExecuteQuery($"UPDATE public.\"Order\" SET status_id = 3 WHERE id = {order_id}");

        // функция доставки блюда в офлайн заказе
        public static void DeliverOfflineMeal(int order_id, int meal_id) => ExecuteQuery($"UPDATE public.\"Meal_to_Order\" SET received_count = received_count + 1 WHERE meal_id = {meal_id} AND order_id = {order_id}");
        // функция генерации сотруднику нового пароля
        public static string GenerateNewPassword(int worker_id, int admin_id)
        {
            var admin = GetObject<Worker>($"id = {admin_id}").FirstOrDefault();

            if (admin is null)
                return "";

            var position = GetObject<Position>($"id = {admin.PositionId}").FirstOrDefault();

            if (position is null)
                return "";

            if (position.Role != WorkerRole.Admin)
                return "";

            string password = Generator.GenerateNewPassword();

            string hash = Encoder.Encode(password);

            ExecuteQuery($"UPDATE public.\"Worker\" SET password = '{hash}' WHERE id = {worker_id}");

            return password;
        }

        // создать нового сотрудника
        public static void CreateWorker(string username, string firstName, string secondName, long phone)
        {
            int id = GetObject<Worker>().Count() + 1;

            //TODO брать должность из базы
            ExecuteQuery($"INSERT INTO public.\"Worker\" VALUES ({id}, '{firstName}', '{secondName}', {phone}, 3, '{username}', '')");
        }

        // обновить существующего сотрудника
        public static void UpdateWorker(int worker_id, string username, string firstName, string secondName, long phone)
        {
            var worker = GetObject<Worker>().Where(x => x.id == worker_id).FirstOrDefault();
            if (worker == null)
                return;

            //TODO брать должность из базы
            ExecuteQuery($"UPDATE public.\"Worker\" WHERE id = {worker_id} SET first_name = '{firstName}', last_name = '{secondName}', phone = {phone}, username = '{username}'");
        }

        // создать новое блюдо
        public static void CreateMeal(string name, float cost, float weight, int servnumber)
        {
            int id = GetObject<Meal>().Count() + 1;

            ExecuteQuery($"INSERT INTO public.\"Meal\" VALUES ({id}, '{name}', {cost}, {weight}, {servnumber})");
        }

        // обновить существующее блюдо
        public static void UpdateMeal(int meal_id, string name, float cost, float weight, int servnumber)
        {
            var meal = GetObject<Meal>().Where(x => x.id == meal_id).FirstOrDefault();
            if (meal == null)
                return;

            ExecuteQuery($"UPDATE public.\"Meal\" SET name = '{name}', cost = {cost}, weight = {weight}, servings_number = '{servnumber}' WHERE id = {meal_id} ");
        }

        // создать новый ингредиент
        public static void CreateIngredient(string name)
        {
            int id = GetObject<Ingredient>().Count() + 1;

            ExecuteQuery($"INSERT INTO public.\"Ingredient\" VALUES ({id}, '{name}')");
        }

        // обновить существующий ингредиент
        public static void UpdateIngredient(int id, string name)
        {
            var ingredient = GetObject<Ingredient>().Where(x => x.id == id).FirstOrDefault();
            if (ingredient == null)
                return;

            ExecuteQuery($"UPDATE public.\"Order\" SET name = '{name}' WHERE id = {id}");
        }


        // создать новый ингредиент
        public static void CreateOnlineOrder(int client_id, string address)
        {
            int id = GetObject<OnlineOrder>().Count() + 1;

            ExecuteQuery($"INSERT INTO public.\"OnlineOrder\" VALUES ({id}, '{DateTime.Now:yyyy-MM-dd)}', {client_id}, '{address}', False)");
        }

        // обновить существующий ингредиент
        public static void UpdateOnlineOrder(int id, string address)
        {
            var ingredient = GetObject<OnlineOrder>().Where(x => x.id == id).FirstOrDefault();
            if (ingredient == null)
                return;

            ExecuteQuery($"UPDATE public.\"OnlineOrder\" SET address = '{address}' WHERE id = {id}");
        }

        public static void SetOnlineOrderComplete(int order_id) => ExecuteQuery($"UPDATE public.\"OnlineOrder\" SET is_complited = True WHERE id = {order_id}");
    }
}

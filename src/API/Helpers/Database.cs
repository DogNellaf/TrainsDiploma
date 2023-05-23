using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
        // строка подключения
        private static string _connectionString = "Server=localhost;Database=TrainsDiploma;";

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

        // функция отправки запроса в базу данных
        private static DataTable ExecuteQuery(string query)
        {
            // пустая таблица
            DataTable result = new();

            // пытаемся выполнить кол
            try
            {
                // используя соединение, выполняем дальнейшие команды
                using var connection = new SqlConnection(_connectionString);

                // создаем SQL команду по тексту
                SqlCommand command = new(query, connection);

                // Создаем считывающий элемент
                SqlDataAdapter adapter = new(command);

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

        // функция генерации сотруднику нового пароля
        //public static string GenerateNewPassword(int worker_id, int admin_id)
        //{
        //    var admin = GetObject<Worker>($"id = {admin_id}").FirstOrDefault();

        //    if (admin is null)
        //        return "";

        //    var position = GetObject<Position>($"id = {admin.PositionId}").FirstOrDefault();

        //    if (position is null)
        //        return "";

        //    if (position.Role != WorkerRole.Admin)
        //        return "";

        //    string password = Generator.GenerateNewPassword();

        //    string hash = Encoder.Encode(password);

        //    ExecuteQuery($"UPDATE public.\"Worker\" SET password = '{hash}' WHERE id = {worker_id}");

        //    return password;
        //}

        // создать нового сотрудника
        public static void CreateUser(string login, string password, string role)
        {
            int id = GetObject<User>().Count() + 1;

            //TODO брать должность из базы
            ExecuteQuery($"INSERT INTO public.\"Worker\" VALUES ({id}, '{login}', '{password}', {phone}, 3, '{username}', '')");
        }

        //обновить существующего сотрудника
        public static void UpdateUser(int id, string login, string password)
        {
            var worker = GetObject<User>().Where(x => x.Id == id).FirstOrDefault();
            if (worker == null)
                return;

            //TODO брать должность из базы
            ExecuteQuery($"UPDATE public.\"Worker\" WHERE id = {id} SET login = '{login}', password = '{password}'");
        }
    }
}

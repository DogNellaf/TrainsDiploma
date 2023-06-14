using API.Helpers;
using Microsoft.Data.SqlClient;
using System.Data;
using TrainsClasses;

namespace RestaurantsClasses
{
    // класс взаимодействия с базой данных
    public static class Database
    {
        // строка подключения
        private static string _connectionString = "Server=.\\SQLEXPRESS;Database=TrainsDiploma;Trusted_Connection=True;TrustServerCertificate=True";

        //функция получения объектов из базы, где Т - любой наследник класса Model
        public static List<T> GetObjects<T>(string where = "") where T : Model
        {
            // создаем пустой список объектов
            List<T> objects = new();

            var name = typeof(T).Name;

            // проверяем, есть ли условие
            string query = $"SELECT * FROM dbo.\"{name}\"";
            if (where != "")
            {
                query = $"SELECT * FROM dbo.\"{name}\" where {where}";
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

        //функция получения объектов из базы, где Т - любой наследник класса Model
        public static T? GetObject<T>(string where = "") where T : Model
        {
            var name = typeof(T).Name;

            // проверяем, есть ли условие
            string query = $"SELECT * FROM dbo.\"{name}\"";
            if (where != "")
            {
                query = $"SELECT * FROM dbo.\"{name}\" where {where}";
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
                return Activator.CreateInstance(typeof(T), parameters) as T;
            }

            return null;
        }

        // общая функция удаления объектов
        public static void Delete<T>(int id)
        {
            //name = name[0].ToString().ToUpper() + name.TrimStart(name[0]);
            ExecuteQuery($"DELETE FROM dbo.\"{typeof(T).Name}\" WHERE id = {id}");
        }

        // общая функция обновления объектов
        public static void Update<T>(int id, string values)
        {
            ExecuteQuery($"UPDATE dbo.\"{typeof(T).Name}\" SET {values} WHERE id = {id}");
        }

        // общая функция создания объектов
        public static T Create<T>(string values) where T: Model
        {
            ExecuteQuery($"INSERT INTO dbo.\"{typeof(T).Name}\" {values}");
            return GetObjects<T>().Last();
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

        // проверить, является ли пользователь админом, по id
        public static bool CheckAdmin(int id)
        {
            DataTable table = ExecuteQuery($"SELECT IsAdmin\r\nFROM \"User\" as u\r\njoin Role as r on u.RoleId = r.Id \r\nWHERE u.id = {id}");

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in table.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = new object[1];
                parameters[0] = row.ItemArray;

                // создаем новый объект класса Т
                return (bool)row.ItemArray[0];
            }

            return false;
        }

        // проверить, является ли пользователь админом, по id
        public static bool CheckWorker(int id)
        {
            DataTable table = ExecuteQuery($"SELECT IsWorker\r\nFROM \"User\" as u\r\njoin Role as r on u.RoleId = r.Id \r\nWHERE u.id = {id}");

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in table.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = new object[1];
                parameters[0] = row.ItemArray;

                // создаем новый объект класса Т
                return (bool)row.ItemArray[0];
            }

            return false;
        }

        public static List<Ticket> GetUserTickets(int id)
        {
            DataTable table = ExecuteQuery($"SELECT * FROM Ticket WHERE UserId = {id}");
            var result = new List<Ticket>();

            foreach (DataRow row in table.Rows)
            {
                result.Add(new Ticket(row.ItemArray));
            }

            return result;
        }

        // проверить, является ли пользователь админом, по id
        public static List<User> GetUsersByRoute(int id)
        {
            DataTable table = ExecuteQuery($"SELECT u.*\r\nFROM dbo.\"Ticket\" as t\r\nJOIN dbo.\"Route\" as r on t.routeId = r.Id\r\nJOIN dbo.\"User\" as u on u.Id = t.UserId\r\nWHERE r.Id = {id}");
            var result = new List<User>();

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in table.Rows)
            {
                result.Add(new User(row.ItemArray));
            }

            return result;
        }

        // установить новый пароль для пользователя
        public static void ChangePassword(int id, string password)
        {
            var token = Encoder.Encode(password);
            ExecuteQuery($"UPDATE dbo.\"User\" SET token = '{token}' WHERE id = {id}");
        }

        // прикрепить билет к пользователю
        public static void AddTicketToUser(int userId, int ticketId)
        {
            ExecuteQuery($"INSERT INTO dbo.UserToTicket VALUES ({userId}, {ticketId})");
        }

        // вернуть билет
        public static void ReturnTicket(int ticketId)
        {
            ExecuteQuery($"UPDATE dbo.Ticket SET StatusId = 2 WHERE Id = {ticketId}");
        }
    }
}

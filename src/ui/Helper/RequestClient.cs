using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using TrainsClasses;

namespace ui.Helper
{
    internal static class RequestClient
    {
        // адрес сервера
        private static string _server = "http://localhost:5199/";

        // функция отправки запроса на сервер и получения списка объектов
        public static List<T>? GetObjects<T>() where T: Model
        {
            string raw = SendRequest($"{typeof(T).Name}", "GET");

            return JsonConvert.DeserializeObject<List<T>>(raw);
        }

        // функция отправки запроса на сервер и получения объекта
        public static T? GetObject<T>(int id) where T : Model
        {
            string raw = SendRequest($"{typeof(T).Name}/{id}", "GET");

            return JsonConvert.DeserializeObject<T>(raw);
        }

        // проверка авторизации пользователя 
        public static User? Auth(string username, string password)
        {
            var result = SendRequest($"user/auth?login={username}&password={password}", "GET");

            try
            {
                return JsonConvert.DeserializeObject<User>(result);
            }
            catch
            {
                return null;
            }
           
        }

        // получить название роли по id
        public static Role GetRole(int id)
        {
            string raw = SendRequest($"role/{id}", "GET");

            return JsonConvert.DeserializeObject<Role>(raw);
        }

        // регистрация клиента
        public static User? Register(string login, string password) => CreateUser(login, password, 0, 1, "", "");

        // проверить, является ли пользователь админом по id
        public static bool CheckAdmin(int id) => bool.Parse(SendRequest($"user/isadmin?&id={id}", "GET"));

        // проверить, является ли пользователь сотрудником по id
        public static bool CheckWorker(int id) => bool.Parse(SendRequest($"user/isworker?&id={id}", "GET"));

        // сгенерировать новый пароль TODO
        public static string GenerateNewPassword(int id, string password, string token) => SendRequest($"user/{id}/password?password={password}&token={token}", "PUT");

        // создать пользователя
        public static User CreateUser(string login, string token, float balance, int roleId, string series, string number)
        {
            var user = new User(0, login, token, balance, roleId, series, number);
            var json = JsonConvert.SerializeObject(user);
            var result = SendRequest($"user", "POST", json);

            return JsonConvert.DeserializeObject<User>(result);
        }

        // обновить пользователя
        public static User UpdateUser(int id, string login, string token, float balance, int roleId, string series, string number)
        {
            var user = new User(id, login, token, balance, roleId, series, number);
            var json = JsonConvert.SerializeObject(user);
            var result = SendRequest($"user", "PUT", json);

            return JsonConvert.DeserializeObject<User>(result);
        }

        // создать пользователя
        public static Transaction CreateTransaction(int userId, float value, DateTime date, string paymentType, string comment, bool isComplited)
        {
            var transaction = new Transaction(-1, userId, value, isComplited, date, paymentType, comment);
            var json = JsonConvert.SerializeObject(transaction);
            var result = SendRequest($"transaction", "POST", json);

            return JsonConvert.DeserializeObject<Transaction>(result);
        }

        // обновить пользователя
        public static Transaction UpdateTransaction(int id, int userId, float value, DateTime date, string paymentType, string comment)
        {
            var transaction = new Transaction(id, userId, value, false, date, paymentType, comment);
            var json = JsonConvert.SerializeObject(transaction);
            var result = SendRequest($"transaction", "PUT", json);

            return JsonConvert.DeserializeObject<Transaction>(result);
        }

        // создать билет
        public static Ticket CreateTicket(DateTime time, float price, int routeId, int statusId)
        {
            var ticket = new Ticket(-1, time, price, routeId, statusId);
            var json = JsonConvert.SerializeObject(ticket);
            var result = SendRequest($"ticket", "POST", json);

            return JsonConvert.DeserializeObject<Ticket>(result);
        }

        // создать направление
        public static Route CreateRoute(DateTime departureTime, int departureCityId, int durationInMinutes, int arrivalCityId, float price)
        {
            var route = new Route(0, departureTime, departureCityId, durationInMinutes, arrivalCityId, price);
            var json = JsonConvert.SerializeObject(route);
            var result = SendRequest($"route", "POST", json);

            return JsonConvert.DeserializeObject<Route>(result);
        }

        // обновить направление
        public static Route UpdateRoute(int id, DateTime departureTime, int departureCityId, int durationInMinutes, int arrivalCityId, float price)
        {
            var route = new Route(id, departureTime, departureCityId, durationInMinutes, arrivalCityId, price);
            var json = JsonConvert.SerializeObject(route);
            var result = SendRequest($"route", "PUT", json);

            return JsonConvert.DeserializeObject<Route>(result);
        }

        // создать направление
        public static City CreateCity(string name)
        {
            var route = new City(0, name);
            var json = JsonConvert.SerializeObject(route);
            var result = SendRequest($"city", "POST", json);

            return JsonConvert.DeserializeObject<City>(result);
        }

        // обновить направление
        public static City UpdateCity(int id, string name)
        {
            var route = new City(id, name);
            var json = JsonConvert.SerializeObject(route);
            var result = SendRequest($"city", "PUT", json);

            return JsonConvert.DeserializeObject<City>(result);
        }

        // удалить пользователя
        public static void Delete<T>(int id) where T: Model
        {
            SendRequest($"{typeof(T).Name}/{id}", "DELETE");
        }

        // добавить билет пользователю
        public static void AddTicket(int userId, int ticketId, User worker)
        {
            SendRequest($"ticket/user?userId={userId}&ticketId={ticketId}&token={worker.Token}", "POST");
        }

        // вернуть билет
        public static void ReturnTicket(int ticketId, User client)
        {
            SendRequest($"ticket/user?ticketId={ticketId}&token={client.Token}", "PUT");
        }

        // получить билеты пользователю
        public static List<Ticket> GetUserTickets(int userId, User worker)
        {
            var raw = SendRequest($"ticket/user?userId={userId}&token={worker.Token}", "GET");
            return JsonConvert.DeserializeObject<List<Ticket>>(raw); 
        }


        // отправить запрос - общая функция
        private static string SendRequest(string url, string method, string body = "")
        {
            var request = WebRequest.Create($"{_server}{url}");

            request.Method = method;

            if (!string.IsNullOrEmpty(body))
            {
                var data = Encoding.Default.GetBytes(body);

                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                var newStream = request.GetRequestStream(); // get a ref to the request body so it can be modified
                newStream.Write(data, 0, data.Length);
                newStream.Close();
            }

            try
            {
                var response = request.GetResponse();

                var dataStream = response.GetResponseStream();

                var reader = new StreamReader(dataStream);

                return reader.ReadToEnd();
            } 
            catch
            {
                return "";
            }
        }
    }
}

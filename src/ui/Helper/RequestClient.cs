using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TrainsClasses;

namespace ui.Helper
{
    internal static class RequestClient
    {
        // адрес сервера
        private static string _server = "https://localhost:7173";

        //private static string _server = "http://92.53.115.41:5001";

        // порт
        //private static int _port = 7173;

        // функция отправки запроса на сервер и получения списка объектов
        public static List<T> GetObjects<T>() where T: Model
        {
            string raw = SendRequest($"api/{typeof(T).Name}", "GET");

            return JsonConvert.DeserializeObject<List<T>>(raw);
        }

        private static string SendRequest(string url, string method)
        {
            return "";
            //var request = WebRequest.Create($"{_server}/{url}");

            //request.Method = method;

            //var response = request.GetResponse();

            //var dataStream = response.GetResponseStream();

            //var reader = new StreamReader(dataStream);

            //return reader.ReadToEnd();
        }

        // проверка авторизации пользователя 
        public static User Auth(string username, string password)
        {
            var result = SendRequest($"api/user/auth?username={username}&password={password}", "GET");

            try
            {
                return JsonConvert.DeserializeObject<User>(result);
            }
            catch
            {
                return null;
            }
           
        }

        // проверка авторизации сотрудника 
        public static User AuthWorker(string username, string password)
        {
            var result = SendRequest($"api/user/worker_auth?username={username}&password={password}", "GET");

            try
            {
                return JsonConvert.DeserializeObject<User>(result);
            }
            catch
            {
                return null;
            }

        }

        // регистрация
        public static User Register(string username, string password)
        {
            var result = SendRequest($"api/user/add?username={username}&password={password}", "POST");

            return JsonConvert.DeserializeObject<User>(result);
        }

        // получение списка сотрудников
        public static List<User> GetUsers()
        {
            var result = SendRequest($"api/worker", "GET");

            return JsonConvert.DeserializeObject<List<User>>(result);
        }


        // получение уровня доступа по id человека
        public static bool CheckIsItAdmin(int id) 
            => bool.Parse(SendRequest($"api/user/{id}/admin", "GET"));
        public static string GenerateNewPassword(int id, string token) 
            => SendRequest($"api/user/{id}/generate_password?token={token}", "PUT");
        public static string CreateWorker(string login, string password, string token) 
            => SendRequest($"api/user?login={login}&password={password}&token={token}", "POST");
        public static string UpdateWorker(int id, string login, string password, string token) 
            => SendRequest($"api/user/{id}?login={login}&password={password}&token={token}", "PUT");
        public static string DeleteWorker(int id) 
            => SendRequest($"api/user/{id}", "DELETE");
    }
}

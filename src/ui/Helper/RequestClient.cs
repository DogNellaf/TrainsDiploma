using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TrainsClasses;

namespace ui.Helper
{
    internal static class RequestClient
    {
        // адрес сервера
        private static string _server = "http://localhost:5199/";

        //private static string _server = "http://92.53.115.41:5001";

        // порт
        //private static int _port = 7173;

        // функция отправки запроса на сервер и получения списка объектов
        public static List<T> GetObjects<T>() where T: Model
        {
            string raw = SendRequest($"{typeof(T).Name}", "GET");

            return JsonConvert.DeserializeObject<List<T>>(raw);
        }

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

            var response = request.GetResponse();

            var dataStream = response.GetResponseStream();

            var reader = new StreamReader(dataStream);

            return reader.ReadToEnd();
        }

        // проверка авторизации пользователя 
        public static User Auth(string username, string password)
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

        // регистрация
        public static User Register(string username, string password)
        {
            var user = new User(0, username, password, 0, 1);
            var json = JsonConvert.SerializeObject(user);
            var result = SendRequest($"user", "POST", json);

            return JsonConvert.DeserializeObject<User>(result);
        }

        // получение списка сотрудников
        public static List<User> GetUsers()
        {
            var result = SendRequest($"api/worker", "GET");

            return JsonConvert.DeserializeObject<List<User>>(result);
        }


        public static bool CheckAdmin(int id) 
            => bool.Parse(SendRequest($"user/isadmin?&id={id}", "GET"));

        // получение уровня доступа по id человека
        public static bool CheckWorker(int id)
            => bool.Parse(SendRequest($"user/isworker?&id={id}", "GET"));
        public static string GenerateNewPassword(int id, string token) 
            => SendRequest($"user/{id}/generate_password?token={token}", "PUT");
        public static string CreateWorker(string login, string password, string token) 
            => SendRequest($"user?login={login}&password={password}&token={token}", "POST");
        public static string UpdateWorker(int id, string login, string password, string token) 
            => SendRequest($"user/{id}?login={login}&password={password}&token={token}", "PUT");
        public static string DeleteWorker(int id) 
            => SendRequest($"user/{id}", "DELETE");
    }
}

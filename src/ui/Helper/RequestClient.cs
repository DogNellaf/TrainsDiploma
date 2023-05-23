using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestaurantsClasees;
using RestaurantsClasees.OrderSystem;
using RestaurantsClasses;
using RestaurantsClasses.KontragentsSystem;
using RestaurantsClasses.OnlineSystem;
using RestaurantsClasses.WorkersSystem;

namespace ui.Helper
{
    internal static class RequestClient
    {
        // адрес сервера
        //private static string _server = "https://localhost:7173";

        private static string _server = "http://92.53.115.41:5001";

        // порт
        //private static int _port = 7173;

        // функция отправки запроса на сервер и получения списка объектов
        public static List<T> GetObjects<T>() where T: Model
        {
            string raw = SendRequest($"api/get{typeof(T).Name}s");

            return JsonConvert.DeserializeObject<List<T>>(raw);
        }

        private static string SendRequest(string url)
        {
            var request = WebRequest.Create($"{_server}/{url}");

            var response = request.GetResponse();

            var dataStream = response.GetResponseStream();

            var reader = new StreamReader(dataStream);

            return reader.ReadToEnd();
        }

        // проверка авторизации пользователя 
        public static Client Auth(string username, string password)
        {
            var result = SendRequest($"api/auth?username={username}&password={password}");

            try
            {
                return JsonConvert.DeserializeObject<Client>(result);
            }
            catch
            {
                return null;
            }
           
        }

        // проверка авторизации сотрудника 
        public static Worker AuthWorker(string username, string password)
        {
            var result = SendRequest($"api/authworker?username={username}&password={password}");

            try
            {
                return JsonConvert.DeserializeObject<Worker>(result);
            }
            catch
            {
                return null;
            }

        }

        // регистрация
        public static Client Register(string username, string password)
        {
            var result = SendRequest($"api/adduser?username={username}&password={password}");

            return JsonConvert.DeserializeObject<Client>(result);
        }

        // получение заказов по пользователю
        public static List<OnlineOrder> GetOnlineOrdersByClient(Client client)
        {
            var result = SendRequest($"api/onlineorders?client_id={client.id}");

            return JsonConvert.DeserializeObject<List<OnlineOrder>>(result);
        }

        // получение заказов по пользователю
        public static List<OfflineOrder> GetAllOfflineOrders()
        {
            var result = SendRequest($"api/offlineorders");

            return JsonConvert.DeserializeObject<List<OfflineOrder>>(result);
        }

        // получение заказов по пользователю
        public static List<OfflineOrder> NewOrders()
        {
            var result = SendRequest($"api/neworders");

            return JsonConvert.DeserializeObject<List<OfflineOrder>>(result);
        }

        // получение списка сотрудников
        public static List<Worker> GetWorkers()
        {
            var result = SendRequest($"api/GetWorkers");

            return JsonConvert.DeserializeObject<List<Worker>>(result);
        }

        public static List<Ingredient> GetIngredientsByMeal(Meal meal)
        {
            var result = SendRequest($"api/GetIngredientsByMeal?id={meal.id}");

            return JsonConvert.DeserializeObject<List<Ingredient>>(result);
        }

        public static void AddIngredientsToMeal(int meal_id, int ingredient_id) => SendRequest($"api/AddIngredientsToMeal?meal_id={meal_id}&ingredient_id={ingredient_id}");


        // получение блюд по заказу
        public static List<Meal> GetMealsByOrder(int order_id, bool is_online=false)
        {
            var result = "";
            if (is_online)
            {
                result = SendRequest($"api/GetOnlineMeals?order_id={order_id}");
            }
            else
            {
                result = SendRequest($"api/GetOfflineMeals?order_id={order_id}");
            }
            

            return JsonConvert.DeserializeObject<List<Meal>>(result);
        }

        // отметить, что блюдо уже принесли
        public static string DeliverOfflineMeal(int order_id, int meal_id) => SendRequest($"api/deliverofflinemeal?order_id={order_id}&meal_id={meal_id}");

        // получение названия должности по id
        public static string GetPositionName(int id) => SendRequest($"api/getpositionname?id={id}");

        // получение уровня доступа по id должности
        public static bool CheckIsItAdmin(int id) => bool.Parse(SendRequest($"api/isitadmin?id={id}"));

        public static void SetOrderToWorker(int order_id, int worker_id) => SendRequest($"api/setordertoworker?order_id={order_id}&worker_id={worker_id}");
        public static void SetOrderComplete(int order_id) => SendRequest($"api/setordercomplete?order_id={order_id}");
        public static void SetOnlineOrderComplete(int order_id) => SendRequest($"api/SetOnlineOrderComplete?order_id={order_id}");
        public static string GenerateNewPassword(int worker_id, int admin_id) => SendRequest($"api/GenerateNewPassword?worker_id={worker_id}&admin_id={admin_id}");
        public static string CreateWorker(string username, string firstName, string secondName, long phone) => SendRequest($"api/createworker?username={username}&firstName={firstName}&secondName={secondName}&phone={phone}");
        public static string UpdateWorker(int worker_id, string username, string firstName, string secondName, long phone) => SendRequest($"api/updateworker?worker_id={worker_id}&username={username}&firstName={firstName}&secondName={secondName}&phone={phone}");
        public static string CreateMeal(string name, float cost, float weight, int servnumber) => SendRequest($"api/createmeal?name={name}&cost={cost}&weight={weight}&servnumber={servnumber}");
        public static string UpdateMeal(int meal_id, string name, float cost, float weight, int servnumber) => SendRequest($"api/updatemeal?meal_id={meal_id}&name={name}&cost={cost}&weight={weight}&servnumber={servnumber}");
        public static string CreateIngredient(string name) => SendRequest($"api/CreateIngredient?name={name}");
        public static string UpdateIngredient(int id, string name) => SendRequest($"api/UpdateIngredient?id={id}&name={name}");
        public static string DeleteWorker(int id) => SendRequest($"api/delete?name=Worker&id={id}");
        public static string DeleteMeal(int id) => SendRequest($"api/delete?name=Meal&id={id}");
        public static string DeleteIngredient(int id) => SendRequest($"api/delete?name=Ingredient&id={id}");
        public static string DeleteIngredientByMeal(int meal_id, int id) => SendRequest($"api/DeleteIngredientByMeal?meal_id={meal_id}&id={id}");

        public static string CreateOnlineOrder(int client_id, string address) => SendRequest($"api/CreateOnlineOrder?client_id={client_id}&address={address}");
    }
}

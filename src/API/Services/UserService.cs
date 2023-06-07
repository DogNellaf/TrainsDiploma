using API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantsClasses;
using TrainsClasses;

namespace API.Services
{
    // сервис взаимодействия с низкоуровневыми классами для пользователей
    public class UserService
    {
        // список всех пользователей
        public List<User> Users => Database.GetObject<User>();

        // получить пользователя по id
        public User? GetUser(int id) => Database.GetObject<User>($"id = {id}", "User").FirstOrDefault();

        // проверить корректность введенного пароля
        public bool CheckPassword(int id, string password)
        {
            var client = Database.GetObject<User>($"id = '{id}'").FirstOrDefault();

            if (client is null)
                return false;

            return Encoder.CheckHash(password, client.Token);
        }

        // валидация данных нового пользователя
        public bool Validate(User user)
        {
            return true;
        }

        // добавление пользователя
        public User Add(User user)
        {
            return Database.CreateUser(user.Login, user.Token, user.Balance, user.Role);
        }
    }
}

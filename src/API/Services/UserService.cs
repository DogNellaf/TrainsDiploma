using API.Helpers;
using RestaurantsClasses;
using TrainsClasses;

namespace API.Services
{
    // сервис взаимодействия с низкоуровневыми классами для пользователей
    public class UserService: Service<User>
    {
        private readonly RoleService _roleService;

        public UserService(RoleService roleService)
        {
            _roleService = roleService;
        }
        // добавление пользователя
        public override User Add(User user)
        {
            user.Token = Encoder.Encode(user.Token);
            return base.Add(user);
        }

        // проверить корректность введенного пароля
        public bool CheckPassword(int id, string password)
        {
            var client = Database.GetObject<User>($"id = '{id}'");

            if (client is null)
                return false;

            return Encoder.CheckHash(password, client.Token);
        }

        // валидация данных нового пользователя
        public override bool Validate(User user)
        {
            return true;
        }

        // проверить является ли пользователь админом по токену
        public bool CheckAdminByToken(string token)
        {
            var user = Database.GetObject<User>($"token like '{token}'");

            if (user is null)
            {
                return false;
            }

            return Database.CheckAdmin(user.Id);
        }

        // проверить является ли пользователь админом по id
        public bool CheckAdminById(int id)
        {
            return Database.CheckAdmin(id);
        }

        // проверить является ли пользователь админом по id
        public bool CheckWorkerById(int id)
        {
            return Database.CheckWorker(id);
        }

        // найти пользователя по логину и паролю
        public User? GetByLoginAndPassword(string login, string password)
        {
            var token = Encoder.Encode(password);
            return Database.GetObject<User>($"login = '{login}' and token like '{token}'");
        }

        // обновить пароль пользователя
        public void ChangePassword(int id, string password)
        {
            Database.ChangePassword(id, password);
        }

        public override string GetUpdateValues(User user)
        {
            return $"login = '{user.Login}', token = '{user.Token}', balance = {user.Balance}, roleId = {user.RoleId}";
        }

        public override string GetCreateValues(User user)
        {
            return $"(login, token, balance, roleId) VALUES ('{user.Login}', '{user.Token}', {user.Balance}, {user.RoleId})";
        }
    }
}

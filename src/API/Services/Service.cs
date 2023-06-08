using RestaurantsClasses;
using TrainsClasses;

namespace API.Services
{
    public class Service<T> where T:Model
    {

        public List<T> Objects => Database.GetObjects<T>();

        // получить пользователя по id
        public virtual T? Get(int id)
        {
            return Database.GetObject<T>($"id = {id}");
        }

        // валидация данных
        public virtual bool Validate(T element)
        {
            return true;
        }

        // добавление пользователя
        public virtual T Add(T element)
        {
            return Database.Create<T>(GetCreateValues(element));
        }

        // удаление пользователя
        public virtual void Delete(int id)
        {
            Database.Delete<T>(id);
        }

        // обновление пользователя
        public virtual void Update(T element)
        {
            Database.Update<T>(element.Id, GetUpdateValues(element));
        }

        public virtual string GetUpdateValues(T element)
        {
            throw new NotImplementedException();
        }

        public virtual string GetCreateValues(T element)
        {
            throw new NotImplementedException();
        }
    }
}

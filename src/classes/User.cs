namespace TrainsClasses
{
    public class User:Model
    {
        public string Login { get; }
        public string Token { get; }
        public float Balance { get; }
        public User(int id, string login, string token, float balance): base(id)
        {
            Login = login;
            Token = token;
            Balance = balance;
        }
    }
}

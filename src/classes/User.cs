using System.Xml.Linq;

namespace TrainsClasses
{
    public class User:Model
    {
        public string Login { get; }
        public string Token { get; }
        public float Balance { get; }
        public string Role { get;}

        public User(): base(0)
        {

        }

        public User(object[] items) : base((int)items[0])
        {
            Login = (string)items[1];
            Token = (string)items[2];
            Role = (string)items[4];
            Balance = (int)items[3];
        }

        public User(int id, string login, string token, float balance, string role) : base(id)
        {
            Login = login;
            Token = token;
            Balance = balance;
            Role = role;
        }
    }
}

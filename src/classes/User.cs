using System.Runtime.Serialization;
using System.Xml.Linq;

namespace TrainsClasses
{
    [DataContract]
    public class User:Model
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public float Balance { get; set; }
        [DataMember]
        public int RoleId { get; set; }

        public User(): base(0)
        {

        }

        public User(object[] items) : base((int)items[0])
        {
            Login = (string)items[1];
            Token = (string)items[2];
            //if (items[4] != DBNull.Value)
            //{
            //    Role = (string)items[4];
            //}
            Balance = Convert.ToSingle(items[3]);
            RoleId = (int)items[4];
        }

        public User(int id, string login, string token, float balance, int roleId) : base(id)
        {
            Login = login;
            Token = token;
            Balance = balance;
            RoleId = roleId;
        }
    }
}

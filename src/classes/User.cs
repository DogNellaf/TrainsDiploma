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
        [DataMember]
        public string PassportSeries { get; set; }
        [DataMember]
        public string PassportNumber { get; set; }

        public User(): base(-1)
        {

        }

        public User(object[] items) : base((int)items[0])
        {
            Login = (string)items[1];
            Token = (string)items[2];
            Balance = Convert.ToSingle(items[3]);
            RoleId = (int)items[4];
            PassportSeries = (string)items[5];
            PassportNumber = (string)items[6];
        }

        public User(int id, string login, string token, float balance, int roleId, string series, string number) : base(id)
        {
            Login = login;
            Token = token;
            Balance = balance;
            RoleId = roleId;
            PassportSeries = series;
            PassportNumber = number;
        }
    }
}

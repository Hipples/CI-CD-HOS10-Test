using System;
using System.Linq;

// add SQLite package
using System.Data.SQLite;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    // add inheritance
    public class LoginService : SqliteService
    {
        public int loginstat { get; set; }
        private static MemberData? userdata { get; set; }

        // set the constructor, by default, login status is 0
        // 0 == not logged in
        public LoginService()
        {
            loginstat = 0;
            //username is instance created from MemberData class in MemberData.cs
            userdata = new MemberData();
        }
        public void setlogin(int login) { this.loginstat = login; }
        public int getlogin() { return this.loginstat; }
        // return logged in user data
        public MemberData getLoggedinUserInfo() { return userdata!; } 
        // return login status
        public Task<int> GetLoginAsync() { return Task.FromResult(getlogin()); }
        // check if inputs on the Login page already belong to an existing account or not
        public Task<bool> LoginVerify(string username, string passwd)
        {
            string stm = "SELECT * FROM member";
            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            string name = "";
            string password = "";

            while(rdr.Read())
            {
                name = rdr.GetString(1);
                password = rdr.GetString(2);

                if (name == username && password == passwd)
                {
                    setlogin(1);
                    userdata!.Name = username;
                    userdata.Password = passwd;
                    return Task.FromResult(true);
                }
            }
            return Task.FromResult(false);
        }
    }
}

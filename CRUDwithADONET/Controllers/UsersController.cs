using CRUDwithADONET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace CRUDwithADONET.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public const string CONNECTINGSTRING = "Server=127.0.0.1;Port=5432;Database=forapi;User Id=postgres;Password=dfrt43i0";
        [HttpGet]
        public List<UserClass> GetUsers()
        {
            #region
            using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(CONNECTINGSTRING))
            {
                string query = "select * from users";
                npgsqlConnection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query,npgsqlConnection);
                var reader=cmd.ExecuteReader();
                List<UserClass> users = new List<UserClass>();
                while (reader.Read())
                {
                    users.Add(new UserClass()
                    {
                        Id = (int)reader[0],
                        Name = (string)reader[1],
                        PhoneNumber= (string)reader[2],
                        Email= (string)reader[3],
                        Password= (string)reader[4]

                    });
                }
                return users;
            }
            #endregion
        }
        [HttpPost]
        public string PostUsers(UserClassTDO user)
        {
            #region
            string query = $"insert into users(name,phonenumber,email,password) values" +
                $"('{user.Name}','{user.PhoneNumber}','{user.Email}','{user.Password}')";
            using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(CONNECTINGSTRING))
            {
                npgsqlConnection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query,npgsqlConnection);
                var rows=cmd.ExecuteNonQuery();
            }
                return $"malumot qoshildi";
            #endregion
        }
        [HttpDelete]
        public string DeleteUsers(int id)
        {
            #region
            string query = $"delete from users where id={id}";
            using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(CONNECTINGSTRING))
            {
                npgsqlConnection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, npgsqlConnection);
                var rows=cmd.ExecuteNonQuery();
            }
            return $"{id} ochirildi";
            #endregion
        }
        [HttpPut]
        public string PutUsers(int id,UserClassTDO user)
        {
            #region
            string query = $"update users set " +
                $"name='{user.Name}'," +
                $"phonenumber='{user.PhoneNumber}'," +
                $"email='{user.Email}'," +
                $"password='{user.Password}' where id={id}";
            using(NpgsqlConnection npgsqlConnection = new NpgsqlConnection(CONNECTINGSTRING))
            {
                npgsqlConnection.Open() ;
                NpgsqlCommand cmd = new NpgsqlCommand(query ,npgsqlConnection);
                var rows=cmd.ExecuteNonQuery();
            }
            return $"{id}-idli user o'zgartirildi";
            #endregion
        }

    }
}

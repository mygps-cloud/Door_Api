using Register.ViewModels;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Registration.Helpers
{
    public class CommonHelper
    {
        private IConfiguration _confg;

        public CommonHelper(IConfiguration confg)
        {
            _confg = confg;
        }
        public int DMLTransaction(string Query)
        {
            int result;
            string ConnectionString = _confg["ConnectionStrings:UserDefaultDb"];
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = Query;
                SqlCommand command = new SqlCommand(sql, connection);
                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result;
        }
        public bool UserAlreadyExists(string Query)
        {
            bool flag = false;
            string ConnectionString = _confg["ConnectionStrings:UserDefaultDb"];
            using(SqlConnection connection=new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = Query;
                SqlCommand command = new SqlCommand(sql,connection);
                SqlDataReader rd=command.ExecuteReader();
                if(rd.HasRows)
                {
                    flag= true;
                }
                connection.Close();
            }
            return flag;
        }
        public List<UserModel> GetPassword(string Query)
        {
            List<UserModel> result = new List<UserModel>();
            string ConnectionString = _confg["ConnectionStrings:UserDefaultDb"];
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = Query;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader rd = command.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        result.Add(new UserModel
                        {
                            Username = rd["Username"].ToString(),
                            PasswordHash = rd["Password"].ToString()
                        });
                    }
                }
                connection.Close();
            }
            return result;
        }
    }
}

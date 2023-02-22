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
        public async Task<bool> UserAlreadyExistsAsync(string Query)
        {
            bool flag = false;
            string configuration = _confg["ConnectionStrings:UserDefaultDb"];
            using (SqlConnection connection = new SqlConnection(configuration))
            {
                await connection.OpenAsync();

                string sql = Query;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader rd = await command.ExecuteReaderAsync();
                if (rd.HasRows)
                {
                    flag = true;
                }
                await connection.CloseAsync();
            }
            return flag;
        }
        public async Task<int> InsertAsync(string Query)
        {
            int result;
            string configuration = _confg["ConnectionStrings:UserDefaultDb"];
            using (SqlConnection connection = new SqlConnection(configuration))
            {
                await connection.OpenAsync();
                string sql = Query;
                SqlCommand command = new SqlCommand(sql, connection);
                result = await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
            return result;
        }
        public async Task<List<UserModel>> GetPassword(string Query)
        {
            string configuration = _confg["ConnectionStrings:UserDefaultDb"];
            List<UserModel> result = new List<UserModel>();
            using (SqlConnection connection = new SqlConnection(configuration))
            {
                await connection.OpenAsync();

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
                await connection.CloseAsync();
            }
            return result;
        }
    }
}

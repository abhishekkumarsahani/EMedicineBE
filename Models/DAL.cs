using System.Data.SqlClient;
using System.Data;

namespace EMedicineBE.Models
{
    public class DAL
    {
        public Response register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status", "Pending");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User Registered Succesfully";

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User resgiter failed";
            }
            return response;
        }
        public Response Login(Users users, SqlConnection connection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            adapter.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            Response response = new Response();
            if(dt.Rows.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User is Valid";
            }
            else
            {
                response.StatusCode =100;
                response.StatusMessage = "User is invalid";
            }
            return response;
        }
    }
}

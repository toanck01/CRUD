using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace webCore1.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientsInfo clientInfo = new ClientsInfo();
        public String errMess = "";
        public String successMess = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                               
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                errMess = ex.Message;
            }
        }
        public void OnPost()
        {
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];
            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                errMess = "All the fields are required";
                return;
            }
            try
            {
                string CS = "Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(CS))
                {
                    connection.Open();
                    string sql = "Update clients "+ 
                        "set name=@name, email=@email, phone=@phone, address=@address "+
                        "where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@id", clientInfo.id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errMess = ex.Message;
                return;
            }
           
            Response.Redirect("/Clients/index");

        }
    }
}

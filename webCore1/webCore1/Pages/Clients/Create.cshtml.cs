using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace webCore1.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientsInfo clientInfo = new ClientsInfo();
        public String errMess = "";
        public String successMess = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"]; 
            if(clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0||clientInfo.address.Length==0)
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
                    string sql = "Insert into clients " + "(name, email, phone, address) values " + "(@name, @email, @phone, @address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errMess = ex.Message;
                return;
            }
            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            successMess = "New client add successfull";
            Response.Redirect("/Clients/index");
        }
    }
}

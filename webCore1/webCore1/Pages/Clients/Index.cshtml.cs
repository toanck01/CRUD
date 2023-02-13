using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace webCore1.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientsInfo> ListClients = new List<ClientsInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using(SqlCommand command= new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader= command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                ClientsInfo clientInfo = new ClientsInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email= reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address=reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();
                                ListClients.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class ClientsInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
    }
}

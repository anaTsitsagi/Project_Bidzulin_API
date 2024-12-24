using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.AccessControl;

using System.Data.SqlClient;



namespace SER_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IConfiguration _configuration;
        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetPersonnal")]
        public JsonResult GetPersonnal()
        {
            string query = "select * from dbo.Personnal";
            DataTable data = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SERConnectionString");
            SqlDataReader sqlreader;
            using (SqlConnection sqlconn = new SqlConnection(sqlDataSource))
            {
                sqlconn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlconn))
                {
                    sqlreader= sqlCommand.ExecuteReader();
                    data.Load(sqlreader);
                    sqlreader.Close();
                    sqlconn.Close();
                }
            }

            return new JsonResult(data);
        }


        [HttpPost]
        [Route("AddPersonnal")]
        public JsonResult AddPersonnal([FromForm] string FirstName, [FromForm] string LastName, [FromForm] string IdentityNumber, [FromForm] string ProfessionalCategory)
        {
            string query = "insert into dbo.Personnal values(@FirstName,@LastName,@IdentityNumber,@ProfessionalCategory)";
            DataTable data = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SERConnectionString");
            SqlDataReader sqlreader;
            using (SqlConnection sqlconn = new SqlConnection(sqlDataSource))
            {
                sqlconn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlconn))
                {
                    sqlCommand.Parameters.AddWithValue("@FirstName", FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", LastName);
                    sqlCommand.Parameters.AddWithValue("@IdentityNumber", IdentityNumber);
                    sqlCommand.Parameters.AddWithValue("@ProfessionalCategory", ProfessionalCategory);
                    sqlreader = sqlCommand.ExecuteReader();
                    data.Load(sqlreader);
                    sqlreader.Close();
                    sqlconn.Close();
                }
            }

            return new JsonResult("Added successfully");
        }


        [HttpDelete]
        [Route("DeletePersonnal")]
        public JsonResult DeletePersonnal(int id)
        {
            string query = "delete from dbo.Personnal where id=@id";
            DataTable data = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SERConnectionString");
            SqlDataReader sqlreader;
            using (SqlConnection sqlconn = new SqlConnection(sqlDataSource))
            {
                sqlconn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlconn))
                {
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlreader = sqlCommand.ExecuteReader();
                    data.Load(sqlreader);
                    sqlreader.Close();
                    sqlconn.Close();
                }
            }

            return new JsonResult("Deleted successfully");
        }



    }
}

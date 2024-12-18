using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace todo_APP.Controllers
{
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TodoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("get_tasks")]
        public JsonResult get_tasks()
        {
            string query = "select * from todo";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("mydb"); // Correct key
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();

                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                }
            }


            return new JsonResult(table);
            

        }

        [HttpPost("add_task")]
        public JsonResult add_task([FromForm] string task)
        {
            string query = "insert into todo values (@task)";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("mydb");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();

                // Correct usage: SqlCommand instead of SqlConnection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@task", task);
                    myReader = myCommand.ExecuteReader(); // ExecuteReader is a method of SqlCommand
                    table.Load(myReader); // Load the data into the DataTable
                }
            }

            return new JsonResult("Added Successfuly");


        }

        [HttpPost("delete_task")]
        public JsonResult delete_task([FromForm] string id)
        {
            string query = "delete from todo where id=@id";
            DataTable table = new DataTable();
            string SqlDatasource = _configuration.GetConnectionString("mydb");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(SqlDatasource))
            {
                myCon.Open();

                // Correct usage: SqlCommand instead of SqlConnection
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader(); // ExecuteReader is a method of SqlCommand
                    table.Load(myReader); // Load the data into the DataTable
                }
            }

            return new JsonResult("Deleted Successfuly");
        }

        }
}

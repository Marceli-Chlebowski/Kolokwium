using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace WebApplication2;
[Route("api")]
[ApiController]
public class BookControler : ControllerBase
{
    private readonly IConfiguration _configuration;

    public BookControler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("{IDbook}")]

    public IActionResult GetBook(int IDbook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("default"));
            connection.Open();

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT 1 FROM Books where PK = @IDbook";

            var reader = command.ExecuteReader();

            List<Books> Book = new List<Books>();
            while (reader.Read())
            {
                Book.Add(new Books()
                {
                    ID = reader.GetInt32(reader.GetOrdinal("IdAnimal")),
                    title = reader.GetString(reader.GetOrdinal("Name"))
                });
            }
            
            return Ok(Book);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] AddBook addbook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("default"));
            connection.Open();

            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Books (PK, title) VALUES (@Title)";
            command.Parameters.AddWithValue("@Title", addbook.Title ?? string.Empty);
            command.CommandText = "insert into authors(first_name, last_name ) values (@first_name, @last_name) ";
            command.Parameters.AddWithValue("first_na", addbook.authorname ?? string.Empty);
            command.Parameters.AddWithValue("last_na", addbook.authorsurname ?? string.Empty);
            command.ExecuteNonQuery();

            return Created("", null);
        }
}
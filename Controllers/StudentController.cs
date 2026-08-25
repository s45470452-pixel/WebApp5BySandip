using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApp5BySandip.Models;

namespace WebApp5BySandip.Controllers
{
    public class StudentController : Controller
    {
        private readonly string connectionString =
            @"Server=.\SQLEXPRESS;Database=NetCentricLab;Trusted_Connection=True;TrustServerCertificate=True;";

        // READ
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();

            using SqlConnection connection =
                new SqlConnection(connectionString);

            string query =
                "SELECT Id, Name, Course, Age FROM Student";

            using SqlCommand command =
                new SqlCommand(query, connection);

            connection.Open();

            using SqlDataReader reader =
                command.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new Student
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()!,
                    Course = reader["Course"].ToString()!,
                    Age = Convert.ToInt32(reader["Age"])
                });
            }

            return View(students);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
        public IActionResult Create(Student student)
        {
            using SqlConnection connection =
                new SqlConnection(connectionString);

            string query = """
                INSERT INTO Student (Name, Course, Age)
                VALUES (@Name, @Course, @Age)
                """;

            using SqlCommand command =
                new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Course", student.Course);
            command.Parameters.AddWithValue("@Age", student.Age);

            connection.Open();
            command.ExecuteNonQuery();

            return RedirectToAction("Index");
        }

        // EDIT - GET
        public IActionResult Edit(int id)
        {
            Student student = new Student();

            using SqlConnection connection =
                new SqlConnection(connectionString);

            string query =
                "SELECT Id, Name, Course, Age FROM Student WHERE Id=@Id";

            using SqlCommand command =
                new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            using SqlDataReader reader =
                command.ExecuteReader();

            if (reader.Read())
            {
                student.Id = Convert.ToInt32(reader["Id"]);
                student.Name = reader["Name"].ToString()!;
                student.Course = reader["Course"].ToString()!;
                student.Age = Convert.ToInt32(reader["Age"]);
            }

            return View(student);
        }

        // EDIT - POST
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            using SqlConnection connection =
                new SqlConnection(connectionString);

            string query = """
                UPDATE Student
                SET Name=@Name,
                    Course=@Course,
                    Age=@Age
                WHERE Id=@Id
                """;

            using SqlCommand command =
                new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@Name", student.Name);
            command.Parameters.AddWithValue("@Course", student.Course);
            command.Parameters.AddWithValue("@Age", student.Age);

            connection.Open();
            command.ExecuteNonQuery();

            return RedirectToAction("Index");
        }

        // DELETE - GET
        public IActionResult Delete(int id)
        {
            Student student = new Student();

            using SqlConnection connection =
                new SqlConnection(connectionString);

            string query =
                "SELECT Id, Name, Course, Age FROM Student WHERE Id=@Id";

            using SqlCommand command =
                new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            using SqlDataReader reader =
                command.ExecuteReader();

            if (reader.Read())
            {
                student.Id = Convert.ToInt32(reader["Id"]);
                student.Name = reader["Name"].ToString()!;
                student.Course = reader["Course"].ToString()!;
                student.Age = Convert.ToInt32(reader["Age"]);
            }

            return View(student);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using SqlConnection connection =
                new SqlConnection(connectionString);

            string query =
                "DELETE FROM Student WHERE Id=@Id";

            using SqlCommand command =
                new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            command.ExecuteNonQuery();

            return RedirectToAction("Index");
        }
    }
}
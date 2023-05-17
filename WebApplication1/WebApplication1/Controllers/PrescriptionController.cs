using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {

     

        
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            List<Doctor> lekarze = new List<Doctor>();

            using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=codefirst;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"))
            {
                connection.Open();

                string query = "SELECT * From Doctor where id = $\"cos {parametr}\""; 

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int IdDoctor = (int)reader["IdDoctor"];
                            string FirstName = (string)reader["FirstName"];
                            string LastName = (string)reader["LastName"];
                            string Email = (string)reader["email"];

                            Doctor lekarz = new Doctor { IdDoctor=IdDoctor,FirstName =FirstName,LastName = LastName, Email = Email };
                            lekarze.Add(lekarz);
                        }
                    }
                }
            }

            return Ok(lekarze);
        }
    }
}

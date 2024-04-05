using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Viola.Models;

namespace Viola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(Caregiver caregiver)
        {
            try
            {
                string query = @"select * from caregiver where phone = @Phone";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    cnn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@Phone", MySqlDbType.String).Value = caregiver.Phone;
                        using (MySqlDataReader myReader = cmd.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                if (caregiver.Password == Convert.ToString(myReader["password"]))
                                {
                                    return Ok("Login"); // 200 (OK)
                                }
                                else
                                {
                                    return Unauthorized("Incorrect Password");   // 401 (Unauthorized)
                                }
                            }
                            else
                            {
                                return NotFound("User Not Found");   // 404 (Not Found)
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // 500 (Internal Server Error)
            }
        }
    }
}

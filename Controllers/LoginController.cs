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
                Boolean success = false;
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    cnn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@Phone", MySqlDbType.String).Value = caregiver.Phone;
                        using (MySqlDataReader myReader = cmd.ExecuteReader())
                        {
                            if (myReader.Read() && caregiver.Password == Convert.ToString(myReader["password"]))
                            {
                                success = true;
                                return Ok(success); // 200 (OK)
                            }
                            else if (myReader.Read())
                            {

                                return StatusCode(401, "Incorrect Password");   // 401 (Unauthorized)
                            }
                            else
                            {
                                return StatusCode(404, "User Not Found");   // 404 (Not Found)

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

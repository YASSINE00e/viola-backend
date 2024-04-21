using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Viola.Models;

namespace Viola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginApiModel caregiver)
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
                                    return Ok(new { status = 200, message = "Login" }); // 200 (OK)
                                }
                                else
                                {
                                    return Unauthorized(new { status = 401, message = "Incorrect Password" });   // 401 (Unauthorized)
                                }
                            }
                            else
                            {
                                return NotFound(new { status = 404, message = "User Not Found" });   // 404 (Not Found)
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message }); // 500 (Internal Server Error)
            }
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(RegistrationApiModel caregiver)
        {
            try
            {
                
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");
                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    string checkQuery = @"SELECT COUNT(*) FROM caregiver WHERE Phone = @CaregiverPhone";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, cnn))
                    {
                        checkCmd.Parameters.Add("@CaregiverPhone", MySqlDbType.VarChar).Value = caregiver.Phone;
                        cnn.Open();
                        int existingAccounts = Convert.ToInt32(checkCmd.ExecuteScalar());
                        cnn.Close();

                        if (existingAccounts > 0)
                        {
                            // Account already exists
                            return StatusCode(409, new { status = 409, message = "Account already exists" }); // 409 (Conflict)
                        }
                    }
                    string query = @"INSERT INTO caregiver (Name, Surname, Phone, Mail, Password)
                     VALUES (@CaregiverName, @CaregiverSurname, @CaregiverPhone, @CaregiverMail, @CaregiverPassword)";
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@CaregiverName", MySqlDbType.VarChar).Value = caregiver.Name;
                        cmd.Parameters.Add("@CaregiverSurname", MySqlDbType.VarChar).Value = caregiver.Surname;
                        cmd.Parameters.Add("@CaregiverPhone", MySqlDbType.VarChar).Value = caregiver.Phone;
                        cmd.Parameters.Add("@CaregiverMail", MySqlDbType.VarChar).Value = caregiver.Mail;
                        cmd.Parameters.Add("@CaregiverPassword", MySqlDbType.VarChar).Value = caregiver.Password;
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                return Ok(new { status = 200, message = "Signup" }); // 200 (OK)
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message }); // 500 (Internal Server Error)
            }
        }
    }
}

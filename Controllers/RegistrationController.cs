using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Viola.Models;

namespace Viola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Registration(Caregiver caregiver)
        {
            try
            {
                string query = @"INSERT INTO caregiver (Name, Surname, Phone, Mail, Password)
                     VALUES (@CaregiverName, @CaregiverSurname, @CaregiverPhone, @CaregiverMail, @CaregiverPassword)";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
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
                return Ok(); // 200 (OK)
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // 500 (Internal Server Error)
            }
        }
    }
}

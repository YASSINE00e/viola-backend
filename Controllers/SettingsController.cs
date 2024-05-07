using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Viola.Models;

namespace Viola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SettingsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        [Route("GetUserData")]
        public IActionResult GetUserData(int id)
        {
            try
            {
                SettingsApiModel caregiver = new SettingsApiModel();
                string query = @"select * from caregiver where idCareGiver = @CaregiverId";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@CaregiverId", MySqlDbType.Int32).Value = id;
                        cnn.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                caregiver.Name = reader["Name"].ToString();
                                caregiver.Surname = reader["Surname"].ToString();
                                caregiver.Phone = reader["Phone"].ToString();
                                caregiver.Mail = reader["Mail"].ToString();
                                caregiver.Password = reader["Password"].ToString();
                            }
                        }
                        cnn.Close();
                    }
                }
                if (caregiver != null)
                {
                    return Ok(new { status = 200, data = caregiver });
                }
                else
                {
                    return NotFound(new { status = 404, data = caregiver });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("ChangeData")]
        public IActionResult ChangeData(ChangeDataApiModel caregiver)
        {
            try
            {
                string query = @"update caregiver set
                            Name = @CaregiverName, Surname = @CaregiverSurname,
                            Phone = @CaregiverPhone, Mail =@CaregiverMail,  Password = @CaregiverPassword
                            where idCareGiver = @CaregiverId";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    cnn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@CaregiverId", MySqlDbType.Int32).Value = caregiver.CaregiverId;
                        cmd.Parameters.Add("@CaregiverName", MySqlDbType.VarChar).Value = caregiver.Name;
                        cmd.Parameters.Add("@CaregiverSurname", MySqlDbType.VarChar).Value = caregiver.Surname;
                        cmd.Parameters.Add("@CaregiverPhone", MySqlDbType.VarChar).Value = caregiver.Phone;
                        cmd.Parameters.Add("@CaregiverMail", MySqlDbType.VarChar).Value = caregiver.Mail;
                        cmd.Parameters.Add("@CaregiverPassword", MySqlDbType.VarChar).Value = caregiver.Password;           
                        cmd.ExecuteNonQuery();
                    }
                    cnn.Close();
                }
                return Ok(new { status = 200, message = "Updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Route("DeleteUser")]
        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                string query = @"delete from caregiver where idCareGiver = @CaregiverId";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");
                Console.WriteLine("kjhv");
                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    cnn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@CaregiverId", MySqlDbType.Int32).Value = id;
                        cmd.ExecuteNonQuery();
                    }
                    cnn.Close();
                }
                return Ok(new { status = 200, message = "Deleted Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}

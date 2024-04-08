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

        [HttpPut]
        [Route("ChangeName")]
        public IActionResult ChangeName(int id, string name)
        {
            try
            {
                string query = @"update caregiver set Name = @CaregiverName where idCareGiver = @CaregiverId";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@CaregiverId", MySqlDbType.Int32).Value = id;
                        cmd.Parameters.Add("@CaregiverName", MySqlDbType.VarChar).Value = name;
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("ChangeSurname")]
        public IActionResult ChangeSurname(int id, string surname)
        {
            try
            {
                string query = @"update caregiver set Name = @CaregiverName where idCareGiver = @CaregiverId";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@CaregiverId", MySqlDbType.Int32).Value = id;                        
                        cmd.Parameters.Add("@CaregiverSurname", MySqlDbType.VarChar).Value = surname;
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("ChangePhone")]
        public IActionResult ChangePhone(int id, string phone)
        {
            try
            {
                string query = @"update caregiver set Name = @CaregiverName where idCareGiver = @CaregiverId";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@CaregiverId", MySqlDbType.Int32).Value = id;
                        cmd.Parameters.Add("@CaregiverPhone", MySqlDbType.VarChar).Value = phone;
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("ChangeMail")]
        public IActionResult ChangeMail(int id, string mail)
        {
            try
            {
                string query = @"update caregiver set Name = @CaregiverName where idCareGiver = @CaregiverId";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@CaregiverId", MySqlDbType.Int32).Value = id;
                        cmd.Parameters.Add("@CaregiverMail", MySqlDbType.VarChar).Value = mail;
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(int id, string password)
        {
            try
            {
                string query = @"update caregiver set Name = @CaregiverName where idCareGiver = @CaregiverId";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@CaregiverId", MySqlDbType.Int32).Value = id;
                        cmd.Parameters.Add("@CaregiverPassword", MySqlDbType.VarChar).Value = password;
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [Route("DeleteUser")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                string query = @"delete from caregiver where idCareGiver = @CaregiverId";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@CaregiverId", MySqlDbType.Int32).Value = id;
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Data;
using Viola.Models;

namespace Viola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaregiverController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CaregiverController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                string query = @"select * from caregiver";
                List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    cnn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        using (MySqlDataReader myReader = cmd.ExecuteReader())
                        {
                            while (myReader.Read())
                            {
                                var row = new Dictionary<string, object>();
                                for (int i = 0; i < myReader.FieldCount; i++)
                                {
                                    row[myReader.GetName(i)] = myReader.GetValue(i);
                                }
                                result.Add(row);
                            }
                        }
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        public IActionResult Post(Caregiver caregiver)
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
                return Ok("Added Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        public IActionResult Put(Caregiver caregiver)
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
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@CaregiverId", MySqlDbType.Int32).Value = caregiver.Id;
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
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                string query = @"delete from caregiver
                            where idCareGiver = @CaregiverId";
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Data;
using Viola.Models;

namespace Viola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PatientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                string query = @"select * from patient";
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
        public IActionResult Post(Patient patient)
        {
            try
            {
                string query = @"INSERT INTO patient (Name, Surname, Age, HouseLocation, BloodType, Weight)
                     VALUES (@PatientName, @PatientSurname, @PatientAge, @PatientHouseLocation, @PatientBloodType, @PatientWeight)";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@PatientName", MySqlDbType.VarChar).Value = patient.Name;
                        cmd.Parameters.Add("@PatientSurname", MySqlDbType.VarChar).Value = patient.Surname;
                        cmd.Parameters.Add("@PatientAge", MySqlDbType.Int32).Value = patient.Age;
                        cmd.Parameters.Add("@PatientHouseLocation", MySqlDbType.VarChar).Value = patient.HouseLocation;
                        cmd.Parameters.Add("@PatientBloodType", MySqlDbType.VarChar).Value = patient.BloodType;
                        cmd.Parameters.Add("@PatientWeight", MySqlDbType.Float).Value = patient.Weight;
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
        public IActionResult Put(Patient patient)
        {
            try
            {
                string query = @"update patient set
                            Name = @PatientName, Surname = @PatientSurname,
                            Age = @PatientAge, HouseLocation =@PatientHouseLocation,  
                            BloodType = @PatientBloodType, Weight = @PatientWeight
                            where idPatient = @PatientId";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@PatientId", MySqlDbType.Int32).Value = patient.Id;
                        cmd.Parameters.Add("@PatientName", MySqlDbType.VarChar).Value = patient.Name;
                        cmd.Parameters.Add("@PatientSurname", MySqlDbType.VarChar).Value = patient.Surname;
                        cmd.Parameters.Add("@PatientAge", MySqlDbType.Int32).Value = patient.Age;
                        cmd.Parameters.Add("@PatientHouseLocation", MySqlDbType.VarChar).Value = patient.HouseLocation;
                        cmd.Parameters.Add("@PatientBloodType", MySqlDbType.VarChar).Value = patient.BloodType;
                        cmd.Parameters.Add("@PatientWeight", MySqlDbType.Float).Value = patient.Weight;
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
                string query = @"delete from patient
                            where idPatient = @PatientId";
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@PatientId", MySqlDbType.Int32).Value = id;
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

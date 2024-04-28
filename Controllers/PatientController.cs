using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
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
        [Route("GetPatients")]
        public IActionResult GetPatients(int id)
        {
            try
            {
                string query = @"SELECT * FROM patient, relation WHERE patient.ViolaId = relation.ViolaId AND relation.idcaregiver = @id";
                List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");

                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    cnn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
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
                return Ok(new { status = 200, data = result, nbPatients = result.Count });  //200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message }); // 500 (Internal Server Error)
            }
        }



        [HttpPost]
        [Route("AddPatient")]
        public IActionResult AddPatient(AddPatientApiModel patient)
        {
            try
            {
                string sqlDataSource = _configuration.GetConnectionString("ViolaDBCon");
                using (MySqlConnection cnn = new MySqlConnection(sqlDataSource))
                {
                    cnn.Open();
                    string query = @"INSERT INTO patient (ViolaId, Name, Surname, Age, HouseLocation, BloodType, Weight)
                     VALUES (@ViolaId, @PatientName, @PatientSurname, @PatientAge, @PatientHouseLocation, @PatientBloodType, @PatientWeight)";
                    using (MySqlCommand cmd = new MySqlCommand(query, cnn))
                    {
                        cmd.Parameters.Add("@ViolaId", MySqlDbType.Int32).Value = patient.ViolaId;
                        cmd.Parameters.Add("@PatientName", MySqlDbType.VarChar).Value = patient.Name;
                        cmd.Parameters.Add("@PatientSurname", MySqlDbType.VarChar).Value = patient.Surname;
                        cmd.Parameters.Add("@PatientAge", MySqlDbType.Int32).Value = patient.Age;
                        cmd.Parameters.Add("@PatientHouseLocation", MySqlDbType.VarChar).Value = patient.HouseLocation;
                        cmd.Parameters.Add("@PatientBloodType", MySqlDbType.VarChar).Value = patient.BloodType;
                        cmd.Parameters.Add("@PatientWeight", MySqlDbType.Float).Value = patient.Weight;
                        cmd.ExecuteNonQuery();
                    }


                    string relationQuery = @"INSERT INTO relation (idCaregiver, ViolaId) VALUES (@idCaregiver, @ViolaId)";
                    using (MySqlCommand cmd = new MySqlCommand(relationQuery, cnn))
                    {
                        cmd.Parameters.Add("@idCaregiver", MySqlDbType.Int32).Value = patient.caregiverId;
                        cmd.Parameters.Add("@ViolaId", MySqlDbType.Int32).Value = patient.ViolaId;
                        cmd.ExecuteNonQuery();
                    }
                    cnn.Close();
                }
                return Ok(new { status = 200, message = "Added Successfully" }); // 200 (OK)
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message }); // 500 (Internal Server Error)
            }

        }
    }
}

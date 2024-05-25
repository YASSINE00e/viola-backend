using Microsoft.AspNetCore.Mvc;
using Mscc.GenerativeAI;
using Viola.Models;

namespace Viola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        [HttpPost]
        [Route("Analysis")]
        public async Task<IActionResult> Analysis(AnalysisApiModel patient)
        {


            string prompt = $"I'm a {patient.Age}-year-old person, my movement is {patient.Movement}, and my heart rate is {patient.HeartRate}. Give me a small analysis of my body situation in a maximum of 15 words.";
            var apiKey = "AIzaSyCb0ioR3CfdSj8wsKSC0Hq1KVBKTkjdjsU";
            var genAi = new GoogleAI(apiKey);
            var model = genAi.GenerativeModel(Mscc.GenerativeAI.Model.GeminiPro);
            var quote = await model.GenerateContent(prompt);
            var candidates = quote.Candidates[0];
            var response = candidates.Content.Text.Split("\n", 2)[0];
            

            return Ok(new {status = 200, res = response });
        }


        [HttpGet]
        [Route("Quote")]
        public async Task<IActionResult> Quote()
        {
            try
            {
                string prompt = "Give me a quote/information about Alzheimer's";
                var apiKey = "AIzaSyCb0ioR3CfdSj8wsKSC0Hq1KVBKTkjdjsU";
                var genAi = new GoogleAI(apiKey);
                var model = genAi.GenerativeModel(Mscc.GenerativeAI.Model.GeminiPro);
                var quote = await model.GenerateContent(prompt);
                var candidates = quote.Candidates[0];
                var lines = candidates.Content.Text.Split("\n", 2);
                string response = lines.Length > 0 ? lines[0].Replace("**Quote:**", "").Trim() : "";
                return Ok(new { status = 200, res = response });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }


    }
}

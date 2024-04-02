using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using Viola.Models;

namespace Viola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {/*
        [HttpPost]
        public async Task<IActionResult> Analysis(Patient patient)
        {
            string prompt = $"I'm a {patient.Age}-year-old person, my movement is {patient.Mv}, and my heart rate is {patient.Hm}. Give me a small analysis of my body situation in a maximum of 15 words.";
            string answer = string.Empty;
            var openai = new OpenAIAPI("sk - JsNWZkygoR5L61pddFHjT3BlbkFJSrJ8Pgsr17ateSHXrAIY");
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = prompt;
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 200;

            var result = openai.Completions.CreateCompletionsAsync(completion);
            foreach (var item in result.Result.Completions)
            {
                answer = item.Text;
            }
            return Ok(answer);
        }*/

        [HttpPost]
        public async Task<IActionResult> Quote()
        {
            try
            {
                string prompt = "Give me a quote/information about Alzheimer's";
                string answer = string.Empty;
                var openai = new OpenAIAPI("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");
                CompletionRequest completion = new CompletionRequest();
                completion.Prompt = prompt;
                completion.Model = OpenAI_API.Models.Model.DavinciText;
                completion.MaxTokens = 200;
                var result = await openai.Completions.CreateCompletionsAsync(completion);
                if (result?.Completions != null && result.Completions.Any())
                {
                    answer = result.Completions.First().Text;
                }

                return Ok(answer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }


    }
}

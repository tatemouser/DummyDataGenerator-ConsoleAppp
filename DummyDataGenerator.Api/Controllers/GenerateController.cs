// DummyDataGenerator.Api/Controllers/GenerateController.cs
using Microsoft.AspNetCore.Mvc;
using DummyDataGenerator.Services;
using DummyDataGenerator.Api.Models;

namespace DummyDataGenerator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerateController : ControllerBase
    {
        private readonly SqlDataBuilder _builder = new SqlDataBuilder();

        [HttpPost]
        public IActionResult Post([FromBody] GenerateRequest request)
        {
            if (request.Format.ToLower() == "csv")
            {
                using var sw = new StringWriter();
                var headers = string.Join(",", request.Fields.Select(f => f.Name));
                sw.WriteLine(headers);

                for (int i = 0; i < request.RowCount; i++)
                {
                    var row = request.Fields.Select(field =>
                        _builder.KnownTemplates.Contains(field.Name.ToLower())
                            ? _builder.GetValueForType(field.Name.ToLower(), i)
                            : new Random().Next(1000, 9999).ToString()
                    );

                    sw.WriteLine(string.Join(",", row));
                }

                return File(System.Text.Encoding.UTF8.GetBytes(sw.ToString()), "text/csv", "data.csv");
            }
            else
            {
                var data = _builder.BuildInMemory(request.Fields, request.RowCount);
                return Ok(new GenerateResponse { JsonData = data });
            }
        }
    }
}

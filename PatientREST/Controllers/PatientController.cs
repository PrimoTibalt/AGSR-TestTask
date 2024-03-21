using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace PatientREST.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PatientController : ControllerBase
	{
		[HttpGet("{id:guid}")]
		public ActionResult<PatientViewModel> Get(Guid id)
		{
			return Ok(new PatientViewModel
			{
				Active = true,
				BirthDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
				Gender = Gender.Other.ToString().ToLowerInvariant(),
				Name = new NameViewModel
				{
					Family = "Срулькины",
					Given =
					[
						"Абуль",
						"Абинхабиб",
						"-",
						"Сарам"
					],
					Id = id,
					Use = "Unofficial",
				}
			});
		}
	}
}

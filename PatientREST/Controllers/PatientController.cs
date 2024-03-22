using Application.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace PatientREST.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PatientController : ControllerBase
	{
		private readonly IMapper _mapper;

		public PatientController(IMapper mapper)
		{
			_mapper = mapper;
		}

		[HttpGet("{id:guid}")]
		public ActionResult<PatientViewModel> Get(Guid id)
		{
			var patient = new PatientViewModel
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
			};

			var dbPatient = _mapper.Map<Patient>(patient);
			patient = _mapper.Map<PatientViewModel>(dbPatient);
			return Ok(patient);
		}
	}
}

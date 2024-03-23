using Application.Patients;
using Application.Services.Patient;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace PatientREST.Controllers
{
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class PatientController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;
		private readonly ICreatePatientService _createService;
		private readonly IEditPatientService _editService;

		public PatientController(IMapper mapper, IMediator mediator, ICreatePatientService createService, IEditPatientService editService)
		{
			_mapper = mapper;
			_mediator = mediator;
			_createService = createService;
			_editService = editService;
		}

		[HttpGet("{id:guid}", Name = "Get")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<PatientViewModel>> GetAsync(Guid id)
		{
			var patient = await _mediator.Send(new Details.Query { Id = id });
			if (patient is null) return NotFound();
			var viewModel = _mapper.Map<PatientViewModel>(patient);
			return Ok(viewModel);
		}

		[HttpGet("list", Name = "List")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<List<PatientViewModel>>> GetAllAsync(CancellationToken token)
		{
			var patients = await _mediator.Send(new List.Query(), token);
			return _mapper.Map<List<PatientViewModel>>(patients);
		}

		[HttpPost(Name = "Create")]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Create(PatientViewModel model)
		{
			if (_createService.TryCreate(model, out Patient patient))
				return CreatedAtAction("Get", "Patient", new { id = patient.Id }, _mapper.Map<PatientViewModel>(patient));
			else
				return BadRequest();
		}

		[HttpDelete("{id:guid}", Name = "Delete")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public IActionResult Delete(Guid id)
		{
			_mediator.Send(new Delete.Query { Id = id });
			return NoContent();
		}

		[HttpPut("{id:guid}", Name = "Edit")]
		[Consumes("application/json")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Update(Guid id, PatientViewModel model)
		{
			if (_editService.TryEditPatient(id, model, out Patient patient))
				return CreatedAtAction("Get", "Patient", new { id = patient.Id }, _mapper.Map<PatientViewModel>(patient));
			else
				return BadRequest();
		}
	}
}

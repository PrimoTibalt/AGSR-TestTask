using Application.Patients;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Services.Patient
{
	internal sealed class EditPatientService : IEditPatientService
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;

		public EditPatientService(IMapper mapper, IMediator mediator)
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		public async Task<Persistence.Patient> EditPatient(Guid patientId, PatientViewModel viewModel)
		{
			try
			{
				var patientChanged = _mapper.Map<Persistence.Patient>(viewModel);
				var patientUnchanged = _mediator.Send(new Details.Query { Id = patientId });
				patientChanged.Name.GivenNames = new List<GivenName>();
				foreach (var given in viewModel.Name.Given)
				{
					var givenId = await _mediator.Send(new Givens.Create.Command { Text = given });
					var gn = new GivenName { NameId = patientChanged.NameId, Name = patientChanged.Name, GivenId = givenId };
					patientChanged.Name.GivenNames.Add(gn);
				}

				
				return await _mediator.Send(new Edit.Command { Id = patientId, Patient = patientChanged });
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
	}
}

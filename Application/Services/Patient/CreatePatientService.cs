using Application.ViewModels;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Services.Patient
{
	internal sealed class CreatePatientService : ICreatePatientService
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;

		public CreatePatientService(IMapper mapper, IMediator mediator)
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		public async Task<Persistence.Patient> CreateAsync(PatientViewModel viewModel)
		{
			try
			{
				var patientNew = _mapper.Map<Persistence.Patient>(viewModel);
				var list = new List<GivenName>();
				patientNew.Name.GivenNames = new List<GivenName>();
				foreach (var gn in viewModel.Name.Given)
				{
					var givenName = new GivenName 
					{
						NameId = patientNew.NameId,
						GivenId = await _mediator.Send(new Givens.Create.Command { Text = gn })
					};
					list.Add(givenName);
				}

				patientNew.Name.GivenNames = list;
				await _mediator.Send(new Patients.Create.Command { Patient = patientNew });
				return await _mediator.Send(new Patients.Details.Query { Id = patientNew.Id });
			} 
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
	}
}

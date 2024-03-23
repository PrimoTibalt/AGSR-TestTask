using Application.Patients;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.Patient
{
	internal sealed class CreatePatientService : ICreatePatientService
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;
		private readonly DataContext _dataContext;

		public CreatePatientService(IMapper mapper, IMediator mediator, DataContext dataContext)
		{
			_mapper = mapper;
			_mediator = mediator;
			_dataContext = dataContext;
		}

		public bool TryCreate(PatientViewModel viewModel, out Persistence.Patient patient)
		{
			try
			{
				var givens = _dataContext.GivenName.Include(gn => gn.Given).Select(g => g.Given.Text).ToList();
				var patientNew = _mapper.Map<Persistence.Patient>(viewModel);
				var list = new List<GivenName>();
				patientNew.Name.GivenNames = new List<GivenName>();
				foreach (var gn in viewModel.Name.Given)
				{
					var givenName = new GivenName { NameId = patientNew.NameId };
					if (givens.Contains(gn))
						givenName.Given = _dataContext.Given.First(g => g.Text == gn);
					else
						givenName.Given = new Given { Text = gn };

					list.Add(givenName);
				}

				patientNew.Name.GivenNames = list;
				_mediator.Send(new Create.Command { Patient = patientNew });
				patient = patientNew;
				return true;
			} 
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				patient = null;
				return false;
			}
		}
	}
}

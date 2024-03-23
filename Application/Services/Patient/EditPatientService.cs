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
		private readonly DataContext _dataContext;

		public EditPatientService(IMapper mapper, IMediator mediator, DataContext dataContext)
		{
			_mapper = mapper;
			_mediator = mediator;
			_dataContext = dataContext;
		}

		public bool TryEditPatient(Guid patientId, PatientViewModel viewModel, out Persistence.Patient patient)
		{
			try
			{
				var patientChanged = _mapper.Map<Persistence.Patient>(viewModel);
				var patientUnchanged = _mediator.Send(new Details.Query { Id = patientId });
				patientChanged.Name.GivenNames = new List<GivenName>();
				var maxGivenId = _dataContext.Given.Max(g => g.Id);
				foreach (var given in viewModel.Name.Given)
				{
					var item = _dataContext.Given.FirstOrDefault(g => g.Text == given);
					var gn = new GivenName { NameId = patientChanged.NameId, Name = patientChanged.Name };
					if (item is not null)
					{
						gn.GivenId = item.Id;
						gn.Given = item;
					}
					else
					{
						gn.GivenId = ++maxGivenId;
						gn.Given = new Given { Id = gn.GivenId, Text = given };
						_dataContext.Given.Add(gn.Given);
						_dataContext.SaveChanges();
					}

					patientChanged.Name.GivenNames.Add(gn);
				}

				_mediator.Send(new Edit.Command { Id = patientId, Patient = patientChanged });
				patient = patientChanged;
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

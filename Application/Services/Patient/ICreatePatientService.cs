using Application.ViewModels;

namespace Application.Services.Patient
{
	public interface ICreatePatientService
	{
		public Task<Persistence.Patient> CreateAsync(PatientViewModel viewModel);
	}
}

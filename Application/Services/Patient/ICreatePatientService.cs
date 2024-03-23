using Application.ViewModels;

namespace Application.Services.Patient
{
	public interface ICreatePatientService
	{
		public bool TryCreate(PatientViewModel viewModel, out Persistence.Patient patient);
	}
}

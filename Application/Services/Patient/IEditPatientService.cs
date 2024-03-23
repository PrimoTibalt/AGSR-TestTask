using Application.ViewModels;

namespace Application.Services.Patient
{
	public interface IEditPatientService
	{
		bool TryEditPatient(Guid patientId, PatientViewModel viewModel, out Persistence.Patient patient);
	}
}

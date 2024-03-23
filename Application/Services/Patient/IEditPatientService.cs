using Application.ViewModels;

namespace Application.Services.Patient
{
	public interface IEditPatientService
	{
		Task<Persistence.Patient> EditPatient(Guid patientId, PatientViewModel viewModel);
	}
}

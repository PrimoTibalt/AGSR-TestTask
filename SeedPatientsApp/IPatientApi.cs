using Application.ViewModels;
using Refit;

namespace SeedPatientsApp
{
    public interface IPatientApi
    {
        [Post("/api/patient")]
        Task Create([Body]PatientViewModel patientViewModel);
    }
}
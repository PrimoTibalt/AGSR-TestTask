using Application.Services.Patient;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extension
{
	public static class ApplicationServiceExtensions
	{
		public static void AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<ICreatePatientService, CreatePatientService>();
			services.AddScoped<IEditPatientService, EditPatientService>();
		}
	}
}

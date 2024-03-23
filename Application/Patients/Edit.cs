using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Patients
{
	public class Edit
	{
		public class Command : IRequest<Patient>
		{
			public Guid Id { get; set; }
			public Patient Patient { get; set; }
		}

		public class Handler : IRequestHandler<Command, Patient>
		{
			private readonly DataContext _context;

			public Handler(DataContext context)
			{
				_context = context;
			}

			public async Task<Patient> Handle(Command request, CancellationToken cancellationToken)
			{
				var patient = request.Patient;
				var dbValue = await _context.Patients.FindAsync(request.Id);
				dbValue.BirthDate = patient.BirthDate;
				dbValue.Gender = patient.Gender;
				dbValue.Active = patient.Active;
				dbValue.Name.Family = patient.Name.Family;
				dbValue.Name.Use = patient.Name.Use;
				dbValue.Name.GivenNames = patient.Name.GivenNames;
				await _context.SaveChangesAsync();
				return patient;
			}
		}
	}
}

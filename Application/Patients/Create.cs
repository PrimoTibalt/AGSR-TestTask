using MediatR;
using Persistence;

namespace Application.Patients
{
	public class Create
	{
		public class Command : IRequest 
		{
			public Patient Patient { get; set; }
		}

		public class Handler : IRequestHandler<Command>
		{
			private readonly DataContext _context;

			public Handler(DataContext context)
			{
				_context = context;
			}

			public async Task Handle(Command request, CancellationToken cancellationToken)
			{
				var patient = request.Patient;
				_context.Patients.Add(patient);
				await _context.SaveChangesAsync();
			}
		}
	}
}

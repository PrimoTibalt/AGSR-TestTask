using MediatR;
using Persistence;

namespace Application.Patients
{
	public class Delete
	{
		public class Query : IRequest
		{
			public Guid Id { get; set; }
		}

		public class Handler : IRequestHandler<Query>
		{
			private readonly DataContext _context;

			public Handler(DataContext context)
			{
				_context = context;
			}

			public async Task Handle(Query request, CancellationToken cancellationToken)
			{
				var patient = await _context.Patients.FindAsync(request.Id, cancellationToken);
				_context.Patients.Remove(patient);
				await _context.SaveChangesAsync();
			}
		}
	}
}

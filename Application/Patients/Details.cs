using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Patients
{
	public class Details
	{
		public class Query : IRequest<Patient> 
		{
			public Guid Id { get; set; }
		}

		public class Handler : IRequestHandler<Query, Patient>
		{
			private readonly DataContext _context;

			public Handler(DataContext context)
			{
				_context = context;
			}

			public async Task<Patient> Handle(Query request, CancellationToken cancellationToken)
			{
				return await _context.Patients.Include(p => p.Name).ThenInclude(n => n.GivenNames).ThenInclude(gn => gn.Given).FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
			}
		}
	}
}

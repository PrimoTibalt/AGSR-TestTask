using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Patients
{
	public class List
	{
		public class Query : IRequest<List<Patient>> { }

		public class Handler : IRequestHandler<Query, List<Patient>> 
		{
			private readonly DataContext _context;

			public Handler(DataContext context)
			{
				_context = context;
			}

			public async Task<List<Patient>> Handle(Query request, CancellationToken cancellationToken)
			{
				return await _context.Patients.Include(p => p.Name).ThenInclude(n => n.GivenNames).ThenInclude(gn => gn.Given).ToListAsync(cancellationToken);
			}
		}
	}
}

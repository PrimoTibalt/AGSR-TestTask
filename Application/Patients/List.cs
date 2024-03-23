using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Patients
{
	public class List
	{
		public class Query : IRequest<List<Patient>> 
		{
			public List<Func<IQueryable<Patient>, IQueryable<Patient>>> Filters { get; set; } 
		}

		public class Handler : IRequestHandler<Query, List<Patient>> 
		{
			private readonly DataContext _context;

			public Handler(DataContext context)
			{
				_context = context;
			}

			public async Task<List<Patient>> Handle(Query request, CancellationToken cancellationToken)
			{
				var query = _context.Patients.Include(p => p.Name).ThenInclude(n => n.GivenNames).ThenInclude(gn => gn.Given).AsQueryable();
				if (request.Filters is not null)
					foreach (var filter in request.Filters)
						query = filter(query);

				return await query.ToListAsync(cancellationToken);
			}
		}
	}
}

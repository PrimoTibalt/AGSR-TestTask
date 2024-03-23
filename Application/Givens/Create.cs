using MediatR;
using Persistence;

namespace Application.Givens
{
	public class Create
	{
		public class Command : IRequest<int>
		{
			public string Text { get; set; }
		}

		public class Handler : IRequestHandler<Command, int>
		{
			private readonly DataContext _context;

			public Handler(DataContext context)
			{
				_context = context;
			}

			public async Task<int> Handle(Command request, CancellationToken cancellationToken)
			{
				if (_context.Given.Where(g => g.Text == request.Text).Any())
					return _context.Given.First(g => g.Text == request.Text).Id;

				var given = new Given { Text = request.Text };
				var result = _context.Given.AddAsync(given);
				await _context.SaveChangesAsync();
				return given.Id;
			}
		}
	}
}

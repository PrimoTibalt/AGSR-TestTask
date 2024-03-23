
namespace PatientREST.Filters.DateBoundariesEntities
{
	public class NeBoundaryEntity : IDateBoundaryFactory
	{
		public DateTime GetEndDate(DateTime parsed, SmallestBound smallestBound)
		{
			if (smallestBound is SmallestBound.Minute)
				return parsed;

			return DateFilter.IncludeLastDay(parsed);
		}

		public DateTime GetStartDate(DateTime parsed, SmallestBound smallestBound)
		{
			return parsed;
		}
	}
}

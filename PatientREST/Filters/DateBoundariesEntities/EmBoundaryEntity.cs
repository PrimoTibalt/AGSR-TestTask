
namespace PatientREST.Filters.DateBoundariesEntities
{
	public class EmBoundaryEntity : IDateBoundaryFactory
	{
		public DateTime GetEndDate(DateTime parsed, SmallestBound smallestBound)
		{
			if (smallestBound is SmallestBound.Hour) 
				return parsed.AddHours(1);

			if (smallestBound is SmallestBound.Minute)
				return parsed.AddMinutes(1);

			return DateFilter.IncludeLastDay(parsed);
		}

		public DateTime GetStartDate(DateTime parsed, SmallestBound smallestBound)
		{
			return parsed;
		}
	}
}


namespace PatientREST.Filters.DateBoundariesEntities
{
	public class LtBoundaryEntity : IDateBoundaryFactory
	{
		public DateTime GetEndDate(DateTime parsed, SmallestBound smallestBound)
		{
			switch (smallestBound)
			{
				case SmallestBound.Year:
					parsed = parsed.AddYears(-1);
					break;
				case SmallestBound.Month:
					parsed = parsed.AddMonths(-1);
					break;
				case SmallestBound.Day:
					parsed = parsed.AddDays(-1);
					break;
			}

			return smallestBound != SmallestBound.Hour && smallestBound != SmallestBound.Minute ? DateFilter.IncludeLastDay(parsed) : parsed;
		}

		public DateTime GetStartDate(DateTime parsed, SmallestBound smallestBound)
		{
			return DateTime.MinValue;
		}
	}
}

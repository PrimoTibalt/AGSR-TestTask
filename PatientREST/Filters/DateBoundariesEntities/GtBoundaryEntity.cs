namespace PatientREST.Filters.DateBoundariesEntities
{
	public class GtBoundaryEntity : IDateBoundaryFactory
	{
		public DateTime GetEndDate(DateTime parsed, SmallestBound smallestBound)
		{
			return DateTime.MaxValue;
		}

		public DateTime GetStartDate(DateTime parsed, SmallestBound smallestBound)
		{
			switch (smallestBound)
			{
				case SmallestBound.Year:
					parsed = parsed.AddYears(1);
					break;
				case SmallestBound.Month | SmallestBound.Year:
					parsed = parsed.AddMonths(1);
					break;
				case SmallestBound.Day | SmallestBound.Month | SmallestBound.Year:
					parsed = parsed.AddDays(1);
					break;
			}

			return parsed;
		}
	}
}

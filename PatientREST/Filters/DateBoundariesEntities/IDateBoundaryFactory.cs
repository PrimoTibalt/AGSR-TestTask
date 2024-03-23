namespace PatientREST.Filters.DateBoundariesEntities
{
	public interface IDateBoundaryFactory
	{
		DateTime GetStartDate(DateTime parsed, SmallestBound smallestBound);
		DateTime GetEndDate(DateTime parsed, SmallestBound smallestBound);
	}
}
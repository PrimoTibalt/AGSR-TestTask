namespace PatientREST.Filters.DateBoundariesEntities
{
	public static class DatePrefixBoundariesFactory
	{
		public static IDateBoundaryFactory GetFactory(DateComparison dc)
			=> dc switch
			{
				DateComparison.Equal => new EmBoundaryEntity(),
				DateComparison.Not => new NeBoundaryEntity(),
				DateComparison.GreaterThan => new GtBoundaryEntity(),
				DateComparison.LessThan => new LtBoundaryEntity(),
				_ => throw new NotSupportedException("Unsupported type of comparision")
			};
	}
}

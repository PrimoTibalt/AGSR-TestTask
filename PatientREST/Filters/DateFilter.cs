using PatientREST.Filters.DateBoundariesEntities;
using Persistence;
using System.Globalization;

namespace PatientREST.Filters
{
	public class DateFilter
	{
		public DateComparison Prefix { get; private set; }
		public DateTime StartDate { get; private set; }
		public DateTime EndDate { get; private set; }

		public DateFilter(string prefix, string dateStr)
		{
			Prefix = ParsePrefix(prefix);
			ParseDate(dateStr, Prefix);
		}

		public static IQueryable<Patient> ApplyDateFilter(IQueryable<Patient> query, DateFilter filter)
		{
			return filter.Prefix switch
			{
				DateComparison.Equal =>
					query.Where(p => p.BirthDate >= filter.StartDate && p.BirthDate <= filter.EndDate),
				DateComparison.GreaterThan =>
					query.Where(p => p.BirthDate > filter.StartDate),
				DateComparison.LessThan =>
					query.Where(p => p.BirthDate < filter.EndDate),
				DateComparison.Not => 
					query.Where(p => p.BirthDate < filter.StartDate || p.BirthDate > filter.EndDate),
				_ => throw new NotSupportedException($"{filter.Prefix} is not supported"),
			};
		}

		private DateComparison ParsePrefix(string prefix)
		{
			switch (prefix)
			{
				case "eq":
					return DateComparison.Equal;
				case "gt":
					return DateComparison.GreaterThan;
				case "lt":
					return DateComparison.LessThan;
				case "ne":
					return DateComparison.Not;
				default:
					throw new ArgumentException("Invalid prefix.", nameof(prefix));
			}
		}

		private void ParseDate(string dateStr, DateComparison dc)
		{
			// Add to be able to play with include/not include days in filter.
			var boundaryFactory = DatePrefixBoundariesFactory.GetFactory(dc);

			// Can improve code by moving part of functionality to boundaryFactories abstract class. 3 point estimation:).
			if (DateTime.TryParseExact(dateStr, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fullDateAndTime))
			{
				SmallestBound smallestBound;
				if (fullDateAndTime.Minute == 0)
					smallestBound = SmallestBound.Hour;
				else
					smallestBound = SmallestBound.Minute;

				StartDate = boundaryFactory.GetStartDate(fullDateAndTime, smallestBound);
				EndDate = boundaryFactory.GetEndDate(fullDateAndTime, smallestBound);
			}
			else if (DateTime.TryParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fullDate))
			{
				StartDate = boundaryFactory.GetStartDate(fullDate, SmallestBound.Day);
				EndDate = boundaryFactory.GetEndDate(fullDate, SmallestBound.Day);
			}
			else if (DateTime.TryParseExact(dateStr, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var yearMonthDate))
			{
				StartDate = boundaryFactory.GetStartDate(new DateTime(yearMonthDate.Year, yearMonthDate.Month, 1), SmallestBound.Month);
				EndDate = boundaryFactory.GetEndDate(new DateTime(yearMonthDate.Year, yearMonthDate.Month, 1).AddMonths(1).AddDays(-1), SmallestBound.Month);
			}
			else if (DateTime.TryParseExact(dateStr, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var yearDate))
			{
				StartDate = boundaryFactory.GetStartDate(new DateTime(yearDate.Year, 1, 1), SmallestBound.Year);
				EndDate = boundaryFactory.GetEndDate(new DateTime(yearDate.Year, 12, 31), SmallestBound.Year);
			}
			else
			{
				throw new ArgumentException("Invalid date format.", nameof(dateStr));
			}
		}

		/// <summary>
		/// Edge case handling.
		/// </summary>
		/// <returns></returns>
		public static DateTime IncludeLastDay(DateTime date)
		{
			return date.AddHours(23).AddMinutes(59).AddSeconds(59);
		}
	}

	public enum DateComparison
	{
		Equal = 0,
		GreaterThan,
		LessThan,
		Not
	}

	public enum SmallestBound
	{
		Year = 0,
		Month,
		Day,
		Hour,
		Minute
	}
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using PatientREST.Filters;

namespace PatientREST.Binders
{
	public class DateFilterModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			var queryString = bindingContext.HttpContext.Request.QueryString.Value;

			var dateStrings = queryString.Replace("?", string.Empty)
				.Split('&')
				.Select(part => part.Replace("birthDate=", string.Empty))
				.ToList();

			var dateFilters = new List<DateFilter>();

			foreach (var rawDate in dateStrings)
			{
				try
				{
					var prefix = rawDate.Substring(0, 2);
					var dateStr = rawDate.Substring(2);
					var filter = new DateFilter(prefix, dateStr);
					dateFilters.Add(filter);
				}
				catch (Exception ex)
				{
					bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, ex.Message);
				}
			}

			bindingContext.Result = ModelBindingResult.Success(dateFilters);
			return Task.CompletedTask;
		}
	}
}

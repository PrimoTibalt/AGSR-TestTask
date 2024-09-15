using Application.ViewModels;
using Persistence;
using Refit;

namespace SeedPatientsApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var tasks = new List<Task>();

			foreach (var _ in Enumerable.Range(0, 100))
			{
				var patientData = GeneratePatientData();
				tasks.Add(PostDataAsync(patientData));
			}

			await Task.WhenAll(tasks);
			Console.ReadLine();
		}

		static async Task PostDataAsync(PatientViewModel model)
		{
			var client = RestService.For<IPatientApi>("http://localhost:5000");
			try
			{
				await client.Create(model);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static PatientViewModel GeneratePatientData()
		{
			return new PatientViewModel
			{
				Name = new NameViewModel
				{
					Id = Guid.NewGuid(),
					Use = "official",
					Family = GenerateRandomLastname(),
					Given = GenerateRandomGiven()
				},
				Gender = GenerateRandomGender(),
				BirthDate = GenerateRandomDate().ToString("yyyy-MM-ddTHH:mm:ss"),
				Active = true
			};
		}

		static DateTime GenerateRandomDate()
		{
			var rnd = new Random();
			var startDate = new DateTime(1902, 1, 1);
			var range = (DateTime.Today - startDate).Days;
			return startDate.AddDays(rnd.Next(range)).AddHours(rnd.Next(25)).AddMinutes(rnd.Next(61)).AddSeconds(rnd.Next(61));
		}

		static string GenerateRandomGender()
		{
			var rnd = new Random();
			return Enum.GetName(typeof(Gender), rnd.Next(4)).ToLowerInvariant();
		}

		static string GenerateRandomLastname()
		{
			var rnd = new Random();
			return StaticData.RussianLastNames[rnd.Next(100)];
		}

		static string[] GenerateRandomGiven()
		{
			var rnd = new Random();
			var list = new List<string>();
			foreach (var _ in Enumerable.Range(0, rnd.Next(7)))
				list.Add(StaticData.SlavicNames[rnd.Next(20)]);

			return list.ToArray();
		}
	}
}
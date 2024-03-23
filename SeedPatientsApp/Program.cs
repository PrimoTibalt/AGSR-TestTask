using Application.ViewModels;
using Persistence;
using System.Text.Json;

namespace SeedPatientsApp
{
	class Program
	{
		static readonly HttpClient client = new HttpClient();
		static async Task Main(string[] args)
		{
			var baseAddress = "http://localhost:5000/api/patient";
			var tasks = new List<Task>();

			foreach (var _ in Enumerable.Range(0, 100))
			{
				var patientData = GeneratePatientData();
				var json = JsonSerializer.Serialize(patientData);
				tasks.Add(PostDataAsync(baseAddress, json));
			}

			await Task.WhenAll(tasks);
			Console.ReadLine();
		}

		static async Task PostDataAsync(string uri, string json)
		{
			var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
			try
			{
				var response = await client.PostAsync(uri, content);
				if (!response.IsSuccessStatusCode)
				{
					Console.WriteLine($"Failed to post data: {json}");
				}
			}
			catch (Exception)
			{
				Console.WriteLine(json);
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
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
	public class PatientViewModel
	{
		[Required]
		public NameViewModel Name { get; set; }

		public string Gender { get; set; }

		[Required]
		public string BirthDate { get; set; }

		public bool Active { get; set; }
	}
}

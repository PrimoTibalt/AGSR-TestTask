using System.ComponentModel.DataAnnotations;

namespace Persistence
{
	public class Name
	{
		[Key]
		public Guid Id { get; set; }
		public string Use { get; set; }
		public string Family { get; set; }
		public ICollection<GivenName> GivenNames { get; set; }
	}

	public class Given
	{
		[Key]
		public int Id { get; set; }
		public string Text { get; set; }
		public ICollection<GivenName> NameAssociations { get; set; }
	}
}
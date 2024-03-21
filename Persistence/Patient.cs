namespace Persistence
{
	public class Patient
	{
		public Guid Id { get; set; }

		public Guid NameId { get; set; }
		public Name Name { get; set; }

		public Gender Gender { get; set; }

		public DateTime BirthDate { get; set; }

		public bool Active { get; set; }
	}
}
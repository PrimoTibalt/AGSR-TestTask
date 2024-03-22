using System.Runtime.Serialization;

namespace Persistence
{
	public enum Gender
	{
		[EnumMember(Value = "unknown")]
		Unknown = 0,
		[EnumMember(Value = "other")]
		Other,
		[EnumMember(Value = "female")]
		Female,
		[EnumMember(Value = "male")]
		Male
	}
}
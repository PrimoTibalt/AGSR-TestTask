using Application.ViewModels;
using AutoMapper;
using Persistence;

namespace Application.Mapping
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles() : base()
		{
			CreateMap<Name, NameViewModel>()
				.ForMember(nv => nv.Given, m => m.MapFrom(n => n.GivenNames.Select(g => g.Given.Text).ToArray()));

			CreateMap<NameViewModel, Name>()
				.ForMember(n => n.GivenNames, m => m.Ignore());

			CreateMap<Patient, PatientViewModel>()
				.ForMember(pv => pv.BirthDate, m => m.MapFrom(p => p.BirthDate.ToString("yyyy-MM-ddTHH:mm:ss")))
				.ForMember(pv => pv.Gender, m => m.MapFrom(p => p.Gender.ToString().ToLowerInvariant()));

			CreateMap<PatientViewModel, Patient>()
				.ForMember(p => p.BirthDate, m => m.MapFrom(pv => DateTime.SpecifyKind(DateTime.Parse(pv.BirthDate), DateTimeKind.Utc)))
				.ForMember(p => p.Gender, m => m.MapFrom(pv => (Gender)Enum.Parse(typeof(Gender), pv.Gender, true)))
				.ForMember(p => p.NameId, m => m.MapFrom(pv => pv.Name.Id))
				.ForMember(p => p.Id, m => m.Ignore());

			CreateMap<Patient, Patient>();
		}
	}
}

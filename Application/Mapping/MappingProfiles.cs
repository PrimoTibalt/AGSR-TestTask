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
				.ForMember(nv => nv.Given, m => m.MapFrom(n => n.Given.Select(g => g.Text).ToArray()));

			CreateMap<NameViewModel, Name>()
				.ForMember(n => n.Given, m => m.MapFrom(nv => nv.Given.Select(g => new Given { Text = g })));

			CreateMap<Patient, PatientViewModel>()
				.ForMember(pv => pv.BirthDate, m => m.MapFrom(p => p.BirthDate.ToString("yyyy-MM-ddTHH:mm:ss")))
				.ForMember(pv => pv.Gender, m => m.MapFrom(p => p.Gender.ToString().ToLowerInvariant()));

			CreateMap<PatientViewModel, Patient>()
				.ForMember(p => p.Id, m => m.Ignore())
				.ForMember(p => p.BirthDate, m => m.MapFrom(pv => DateTime.Parse(pv.BirthDate)))
				.ForMember(p => p.Gender, m => m.MapFrom(pv => (Gender)Enum.Parse(typeof(Gender), pv.Gender, true)))
				.ForMember(p => p.NameId, m => m.Ignore())
				.ForMember(p => p.Id, m => m.Ignore());
		}
	}
}

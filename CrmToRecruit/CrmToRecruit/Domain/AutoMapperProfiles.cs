using AutoMapper;
using CrmToRecruit.Domain;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CrmToRecruitEntity, CrmToRecruitExtendedEntity>()
            .ForMember(dest => dest.ClosingDate, opt => opt.Ignore())
            .ForMember(dest => dest.StageOfClosed, opt => opt.Ignore());
    }
}

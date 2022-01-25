using AutoMapper;
using Core.Models;

namespace Core.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ProfileModel, EditProfileModel>();
    }
}

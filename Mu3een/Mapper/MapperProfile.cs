using AutoMapper;
using Mu3een.Entities;
using Mu3een.Models;

namespace Mu3een.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Volunteer, VolunteerModel>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(x => x.PhoneNumber));
            CreateMap<Institution, InstitutionModel>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(x => x.PhoneNumber));
            CreateMap<SocialEventVolunteer, SocialEventVolunteerModel>();
            CreateMap<SocialEventType, SocialEventTypeModel>();
            CreateMap<SocialEvent, SocialEventModel>();
            CreateMap<Reward, RewardModel>();
            CreateMap<Admin, AdminModel>();

            //request
            CreateMap<AdminRequestModel, Admin>();
            CreateMap<RewardAddRequestModel, Reward>();
            CreateMap<SocialEventAddRequestModel, SocialEvent>();
            CreateMap<InstitutionRegisterModel, Institution>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(x => x.Phone));
        }
    }
}

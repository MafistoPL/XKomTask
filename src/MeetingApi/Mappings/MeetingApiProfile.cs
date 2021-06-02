using AutoMapper;
using MeetingApi.Dtos;
using Persistence.EF.Entities;

namespace MeetingApi.Mappings
{
    public class MeetingApiProfile : Profile
    {
        public MeetingApiProfile()
        {
            CreateMap<MeetingDto, Meeting>()
                .ReverseMap();
        }
    }
}
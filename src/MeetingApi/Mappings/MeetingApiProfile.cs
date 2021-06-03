using AutoMapper;
using MeetingApi.Dtos;
using MeetingApi.Dtos.Meeting;
using Persistence.EF.Entities;

namespace MeetingApi.Mappings
{
    public class MeetingApiProfile : Profile
    {
        public MeetingApiProfile()
        {
            CreateMap<CreateMeetingDto, Meeting>()
                .ReverseMap();

            CreateMap<Meeting, MeetingDto>();
        }
    }
}
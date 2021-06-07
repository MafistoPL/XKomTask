using AutoMapper;
using MeetingApi.Dtos;
using MeetingApi.Mappings;
using MeetingApi.Services;
using Persistence.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XKomTaskTests.Mocks;
using Xunit;

namespace XKomTaskTests.MeetingApiTests
{
    public class ParticipantServiceTests
    {
        private readonly IMapper _mapper;

        public ParticipantServiceTests()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MeetingApiProfile>());
            _mapper = new Mapper(mapperConfiguration);
        }

        // MethodUnderTest_SHOULD_ExpectedBehavior_WHEN_StateUnderTest
        [Fact]
        public async Task RegisterParticipant_Should_AddParticipantToMeeting_When_Invoked()
        {
            var dbContext = RepositoryMocks.GetApplicationContext(
                    "RegisterParticipant_Should_AddParticipantToMeeting_When_Invoked");
            var participantRepository = new ParticipantRepository(dbContext);

            var participantService = new ParticipantService(participantRepository, _mapper);

            var registerParticipantDto = new RegisterParticipantDto()
            {
                Email = "example@example.domain",
                Name = "example",
                MeetingId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            };

            var participantsCountsBeforeAdd = (await participantRepository.GetAllAsync()).Count;
            var result = await participantService.RegisterParticipant(registerParticipantDto);
            var participantsCountsAfterAdd = (await participantRepository.GetAllAsync()).Count;

            Assert.True(participantsCountsBeforeAdd + 1 == participantsCountsAfterAdd);
            Assert.Equal(registerParticipantDto.MeetingId, result.MeetingId);
        }
    }
}

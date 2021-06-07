using MeetingApi.Services;
using Persistence.EF.Repositories;
using System;
using System.Threading.Tasks;
using XKomTaskTests.Mocks;
using Xunit;

namespace XKomTaskTests.MeetingApiTests
{
    public class MeetingServiceTests
    {
        // MethodUnderTest_SHOULD_ExpectedBehavior_WHEN_StateUnderTest
        [Fact]
        public async Task RemoveMeetingAsync_Should_CascadeDeleteItems_When_IdIsCorrect()
        {
            var dbContext = RepositoryMocks.GetApplicationContext(
                "RemoveMeetingAsync_Should_RemoveObject_When_IdIsCorrect");
            var meetingRepository = new MeetingRepository(dbContext);
            var participantRepository = new ParticipantRepository(dbContext);

            var meetingService = new MeetingService(meetingRepository);

            var meetingsCountsBeforeDelete = (await meetingRepository.GetAllAsync()).Count;
            var participantsCountsBeforeDelete = (await participantRepository.GetAllAsync()).Count;
            await meetingService.RemoveMeetingAsync(Guid.Parse("00000000-0000-0000-0000-000000000002"));
            var meetingsCountsAfterDelete = (await meetingRepository.GetAllAsync()).Count;
            var participantsCountsAfterDelete = (await participantRepository.GetAllAsync()).Count;

            Assert.True(meetingsCountsBeforeDelete == meetingsCountsAfterDelete + 1);
            Assert.True(participantsCountsBeforeDelete == participantsCountsAfterDelete + 2);
        }

        [Fact]
        public async Task RemoveMeetingAsync_Should_ThrowException_When_IdIsNotCorrect()
        {
            var dbContext = RepositoryMocks.GetApplicationContext(
                "RemoveMeetingAsync_Should_ThrowException_When_IdIsNotCorrect");
            var meetingRepository = new MeetingRepository(dbContext);

            var meetingService = new MeetingService(meetingRepository);

            Guid guid = Guid.Parse("00000000-0000-0000-0000-000000000BAD");

            var meeting = await meetingRepository.GetByIdAsync(guid);

            Task task = Task.Run(() =>  meetingService.RemoveMeetingAsync(guid));

            var exception = await Record.ExceptionAsync(async () => await task);

            Assert.NotNull(exception);
            Assert.Equal(
                $"Meeting with given id='{guid.ToString()}' does not exist",
                exception.Message
            );
        }
    }
}

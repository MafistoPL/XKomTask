using Persistence.EF.Entities;
using Persistence.EF.Repositories;
using System;
using System.Threading.Tasks;
using XKomTaskTests.Mocks;
using Xunit;

namespace XKomTaskTests
{
    public class MeetingRepositoryTests
    {
        // MethodUnderTest_SHOULD_ExpectedBehavior_WHEN_StateUnderTest
        [Fact]
        public async Task GetByIdAsync_Should_ReturnObject_When_IdIsCorrect()
        {
            var meetingRepository = RepositoryMocks.GetMeetingRepository();

            var result = await meetingRepository.Object
                .GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000001"));

            Assert.NotNull(result);
            Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000001"), result.Id);
            Assert.Equal("Rozmowa kwalifikacyjna z Mateuszem Porębą", result.Subject);
        }

        [Fact]
        public async Task GetByIdAsync_Should_ReturnNull_When_IdIsNotCorrect()
        {
            var meetingRepository = RepositoryMocks.GetMeetingRepository();

            var result = await meetingRepository.Object
                .GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000BAD"));

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnAllObject_When_Invoked()
        {
            var meetingRepository = RepositoryMocks.GetMeetingRepository();

            var result = await meetingRepository.Object.GetAllAsync();

            Assert.Equal(2, result.Count);
            Assert.Equal("Rozmowa kwalifikacyjna z Mateuszem Porębą", result[0].Subject);
            Assert.Equal("Bardzo ważne spotkanie dotyczące ważnego projektu", result[1].Subject);
        }

        [Fact]
        public async Task AddAsync_Should_AddItem_When_Invoked()
        {
            var meetingRepository = RepositoryMocks.GetMeetingRepository();
            var meetingToAdd = new Meeting()
            {
                Id = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1),
                Place = "Some test place",
                Subject = "Some cool meeting ;)"
            };

            var countBeforeAdd = (await meetingRepository.Object.GetAllAsync()).Count;
            var addedMeeting = await meetingRepository.Object.AddAsync(meetingToAdd);
            var countAfterAdd = (await meetingRepository.Object.GetAllAsync()).Count;
            var meetingFromRepository = await meetingRepository.Object.GetByIdAsync(meetingToAdd.Id);

            Assert.Equal(addedMeeting.Id, meetingToAdd.Id);
            Assert.True(countBeforeAdd + 1 == countAfterAdd);
            Assert.Equal(meetingToAdd.Place, meetingFromRepository.Place);
        }

        [Fact]
        public async Task DeleteAsync_Should_DeleteItem_When_ExistingObjectIsPassed()
        {
            var meetingRepository = RepositoryMocks.GetMeetingRepository();
            var meetingToDelete = await meetingRepository.Object
                .GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000002"));

            var countBeforeDelete = (await meetingRepository.Object.GetAllAsync()).Count;
            await meetingRepository.Object.DeleteAsync(meetingToDelete);
            var countAfterADelete = (await meetingRepository.Object.GetAllAsync()).Count;

            Assert.True(countBeforeDelete == countAfterADelete + 1);

            var result = await meetingRepository.Object.GetByIdAsync(meetingToDelete.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_Should_NotDeleteItem_When_NotExistingObjectIsPassed()
        {
            var meetingRepository = RepositoryMocks.GetMeetingRepository();
            var meetingToDelete = await meetingRepository.Object
                .GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000002"));

            await meetingRepository.Object.DeleteAsync(meetingToDelete);

            var countBeforeDelete = (await meetingRepository.Object.GetAllAsync()).Count;
            await meetingRepository.Object.DeleteAsync(meetingToDelete);
            var countAfterADelete = (await meetingRepository.Object.GetAllAsync()).Count;

            Assert.True(countBeforeDelete == countAfterADelete);
        }

        [Fact]
        public async Task DeleteAsync_Should_CascadeDeleteItems_When_ExistingObjectIsPassed()
        {
            await using var dbContext = RepositoryMocks.GetApplicationContext(databaseName:
                "DeleteAsync_Should_CascadeDeleteItems_When_ExistingObjectIsPassed");

            var meetingRepository = new MeetingRepository(dbContext);
            var participantRepository = new ParticipantRepository(dbContext);

            var meetingToDelete = await meetingRepository
                .GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000002"));

            var meetingsCountsBeforeDelete = (await meetingRepository.GetAllAsync()).Count;
            var participantsCountsBeforeDelete = (await participantRepository.GetAllAsync()).Count;
            await meetingRepository.DeleteAsync(meetingToDelete);
            var meetingsCountsAfterDelete = (await meetingRepository.GetAllAsync()).Count;
            var participantsCountsAfterDelete = (await participantRepository.GetAllAsync()).Count;

            Assert.True(meetingsCountsBeforeDelete == meetingsCountsAfterDelete + 1);
            Assert.True(participantsCountsBeforeDelete == participantsCountsAfterDelete + 2);
        }

        [Fact]
        public async Task UpdateAsync_Should_UpdateItem_When_ExistingObjectIsPassed()
        {
            var meetingRepository = RepositoryMocks.GetMeetingRepository();
            var meetingToUpdate = await meetingRepository.Object
                .GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000002"));

            meetingToUpdate.Subject = "Bardzo ważne spotkanie dotyczące bardzo ważnego projektu";

            await meetingRepository.Object.UpdateAsync(meetingToUpdate);

            var updatedMeeting = await meetingRepository.Object
                .GetByIdAsync(meetingToUpdate.Id);

            Assert.Equal(meetingToUpdate.Id, updatedMeeting.Id);
            Assert.Equal("Bardzo ważne spotkanie dotyczące bardzo ważnego projektu",
                updatedMeeting.Subject);
        }
    }
}

using Persistence.EF.Entities;
using System;
using System.Threading.Tasks;
using XKomTaskTests.Mocks;
using Xunit;

namespace XKomTaskTests
{
    public class ParticipantRepositoryTests
    {
        // MethodUnderTest_SHOULD_ExpectedBehavior_WHEN_StateUnderTest
        [Fact]
        public async Task Add_Should_ReturnObject_When_IdIsCorrect()
        {
            var participantRepository = RepositoryMocks.GetParticipantRepository();

            var result = await participantRepository.Object
                .GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000002"));

            Assert.NotNull(result);
            Assert.Equal(Guid.Parse("00000000-0000-0000-0000-000000000002"), result.Id);
            Assert.Equal("mateuszporeba@fake_domain.com", result.Email);
        }

        [Fact]
        public async Task Add_Should_ReturnNull_When_IdIsNotCorrect()
        {
            var participantRepository = RepositoryMocks.GetParticipantRepository();

            var result = await participantRepository.Object
                .GetByIdAsync(Guid.Parse("00000000-0000-0000-0000-000000000BAD"));

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_Should_RerutnAllObject_When_Invoked()
        {
            var participantRepository = RepositoryMocks.GetParticipantRepository();

            var result = await participantRepository.Object.GetAllAsync();

            Assert.Equal(4, result.Count);
            Assert.Equal("JanKowalski@x-kom.pl", result[0].Email);
            Assert.Equal("mateuszporeba@fake_domain.com", result[1].Email);
            Assert.Equal("JanKowalski@x-kom.pl", result[2].Email);
            Assert.Equal("EasterEgg@x-kom.pl", result[3].Email);
        }

        [Fact]
        public async Task AddAsync_Should_AddItem_When_Invoked()
        {
            var participantRepository = RepositoryMocks.GetParticipantRepository();
            var participantToAdd = new Participant()
            {
                Id = Guid.NewGuid(),
                Email = "example@example.domain",
                Name = "example",
            };

            var countBeforeAdd = (await participantRepository.Object.GetAllAsync()).Count;
            var addedParticipant = await participantRepository.Object.AddAsync(participantToAdd);
            var countAfterAdd = (await participantRepository.Object.GetAllAsync()).Count;
            var participantFromRepo = await participantRepository.Object.GetByIdAsync(participantToAdd.Id);

            Assert.Equal(addedParticipant.Id, participantToAdd.Id);
            Assert.True(countBeforeAdd + 1 == countAfterAdd);
            Assert.Equal(participantToAdd.Email, participantFromRepo.Email);
        }

        [Fact]
        public async Task DeleteAsync_Should_DeleteItem_When_ExistingObjectIsPassed()
        {
            var participantRepository = RepositoryMocks.GetParticipantRepository();
            var participantToDelete = await participantRepository.Object
                .GetByIdAsync(Guid.Parse("506F7A64-7261-7769-616D-20584B6F6D21"));

            var countBeforeDelete = (await participantRepository.Object.GetAllAsync()).Count;
            await participantRepository.Object.DeleteAsync(participantToDelete);
            var countAfterADelete = (await participantRepository.Object.GetAllAsync()).Count;

            Assert.True(countBeforeDelete == countAfterADelete + 1);
            var result = await participantRepository.Object.GetByIdAsync(participantToDelete.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_Should_NotDeleteItem_When_NotExistingObjectIsPassed()
        {
            var participantRepository = RepositoryMocks.GetParticipantRepository();
            var participantToDelete = await participantRepository.Object
                .GetByIdAsync(Guid.Parse("506F7A64-7261-7769-616D-20584B6F6D21"));

            await participantRepository.Object.DeleteAsync(participantToDelete);

            var countBeforeDelete = (await participantRepository.Object.GetAllAsync()).Count;
            await participantRepository.Object.DeleteAsync(participantToDelete);
            var countAfterADelete = (await participantRepository.Object.GetAllAsync()).Count;

            Assert.True(countBeforeDelete == countAfterADelete);
            var result = await participantRepository.Object.GetByIdAsync(participantToDelete.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_Should_UpdateItem_When_ExistingObjectIsPassed()
        {
            var participantRepository = RepositoryMocks.GetParticipantRepository();
            var participantToUpdate = await participantRepository.Object
                .GetByIdAsync(Guid.Parse("506F7A64-7261-7769-616D-20584B6F6D21"));

            participantToUpdate.Email = "EasterEggIsHereSomewhere@x-kom.pl";

            await participantRepository.Object.UpdateAsync(participantToUpdate);

            var updatedParicipant = await participantRepository.Object
                .GetByIdAsync(Guid.Parse("506F7A64-7261-7769-616D-20584B6F6D21"));

            Assert.Equal(participantToUpdate.Id, updatedParicipant.Id);
            Assert.Equal(Guid.Parse("506F7A64-7261-7769-616D-20584B6F6D21"),
                updatedParicipant.Id);
            Assert.Equal("EasterEggIsHereSomewhere@x-kom.pl", updatedParicipant.Email);
        }
    }
}

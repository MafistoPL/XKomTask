using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence.EF;
using Persistence.EF.Entities;
using Persistence.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XKomTaskTests.Mocks
{
    class RepositoryMocks
    {
        private static string DateFormat = "yyyy-MM-dd HH:mm";

        public static Mock<IMeetingRepository> GetMeetingRepository()
        {
            var meetings = GetMeetings();

            var mockMeetingRepository = new Mock<IMeetingRepository>();

            mockMeetingRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(meetings);

            mockMeetingRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    return meetings.FirstOrDefault(m => m.Id == id);
                });

            mockMeetingRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Meeting>()))
                .ReturnsAsync((Meeting meeting) =>
                {
                    meetings.Add(meeting);
                    return meeting;
                });

            mockMeetingRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<Meeting>()))
                .Callback<Meeting>((entitiy) => meetings.Remove(entitiy));

            mockMeetingRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Meeting>()))
                .Callback<Meeting>((entity) =>
                {
                    meetings.Remove(entity);
                    meetings.Add(entity);
                });

            return mockMeetingRepository;
        }

        private static List<Meeting> GetMeetings()
        {
            var meetings = new List<Meeting>()
            {
                new Meeting()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    CreatedDate = DateTime.ParseExact("2021-06-10 13:19", DateFormat, null),
                    StartDate = DateTime.ParseExact("2021-06-15 12:00", DateFormat, null),
                    EndDate = DateTime.ParseExact("2021-06-15 13:00", DateFormat, null),
                    Place = "Mała salka konferencyjna",
                    Subject = "Rozmowa kwalifikacyjna z Mateuszem Porębą"
                },
                new Meeting()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.ParseExact("2021-06-10 10:59", DateFormat, null),
                    StartDate = DateTime.ParseExact("2021-07-12 11:00", DateFormat, null),
                    EndDate = DateTime.ParseExact("2021-07-12 14:00", DateFormat, null),
                    Place = "Duża salka konferencyjna",
                    Subject = "Bardzo ważne spotkanie dotyczące ważnego projektu"
                }
            };

            return meetings;
        }

        public static Mock<IParticipantRepository> GetParticipantRepository()
        {
            var participants = GetParticipants();

            var mockParticipantsRepository = new Mock<IParticipantRepository>();

            mockParticipantsRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(participants);

            mockParticipantsRepository
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    return participants.FirstOrDefault(p => p.Id == id);
                });

            mockParticipantsRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Participant>()))
                .ReturnsAsync((Participant participant) =>
                {
                    participants.Add(participant);
                    return participant;
                });

            mockParticipantsRepository
                .Setup(repo => repo.DeleteAsync(It.IsAny<Participant>()))
                .Callback((Participant participant) =>
                {
                    participants.Remove(participant);
                });

            mockParticipantsRepository
                .Setup(repo => repo.UpdateAsync(It.IsAny<Participant>()))
                .Callback((Participant participant) =>
                {
                    participants.Remove(participant);
                    participants.Add(participant);
                });

            return mockParticipantsRepository;
        }

        private static List<Participant> GetParticipants()
        {
            var meetings = GetMeetings();

            var participants = new List<Participant>()
            {
                new Participant()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    CreatedDate = DateTime.ParseExact("2021-06-10 13:37", DateFormat, null),
                    Email = "JanKowalski@x-kom.pl",
                    Name = "Jan Kowalski",
                    LastModifiedDate = null,
                    MeetingId = meetings[0].Id,
                    Meeting = meetings[0]
                },
                new Participant()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    CreatedDate = DateTime.ParseExact("2021-06-10 13:37", DateFormat, null),
                    Email = "mateuszporeba@fake_domain.com",
                    Name = "Mateusz Poręba",
                    LastModifiedDate = null,
                    MeetingId = meetings[0].Id,
                    Meeting = meetings[0]
                },
                new Participant()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    CreatedDate = DateTime.ParseExact("2021-07-01 13:37", DateFormat, null),
                    Email = "JanKowalski@x-kom.pl",
                    Name = "Jan Kowalski",
                    LastModifiedDate = null,
                    MeetingId = meetings[1].Id,
                    Meeting = meetings[1]
                },
                new Participant()
                {
                    Id = Guid.Parse("506F7A64-7261-7769-616D-20584B6F6D21"),
                    CreatedDate = DateTime.ParseExact("2021-07-01 13:37", DateFormat, null),
                    Email = "EasterEgg@ascii.pl",
                    Name = "Easter Egg",
                    LastModifiedDate = null,
                    MeetingId = meetings[1].Id,
                    Meeting = meetings[1]
                },
            };

            return participants;
        }

        public static ApplicationContext GetApplicationContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: databaseName)
               .Options;

            var context = new ApplicationContext(options);

            var meetings = GetMeetings();
            var participants = GetParticipants();

            var firstMeeting = context.Add(meetings[0]).Entity;
            var secondMeeting = context.Add(meetings[1]).Entity;

            participants[0].MeetingId = firstMeeting.Id;
            participants[0].Meeting = firstMeeting;
            participants[1].MeetingId = firstMeeting.Id;
            participants[1].Meeting = firstMeeting; 
            participants[2].MeetingId = secondMeeting.Id;
            participants[2].Meeting = secondMeeting;
            participants[3].MeetingId = secondMeeting.Id;
            participants[3].Meeting = secondMeeting;

            foreach (var participant in participants)
            {
                context.Add(participant);
            }
            context.SaveChanges();

            return context;
        }
    }
}

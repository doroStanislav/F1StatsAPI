using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Threading.Tasks;
using F1StatsAPI.Services;
using F1StatsAPI.Repositories;
using F1StatsAPI.Models;

namespace F1StatsAPI.UnitTests.Services
{
    public class DriverStatsServiceTests
    {
        List<Result> fakeResults = new List<Result>
        {
            new Result
            {
                DriverId = 1,
                Points = 30,
                Driver = new Driver
                {
                    GivenName = "Charles",
                    FamilyName = "Leclerc",
                    Country = "Monaco",
                    IsActive = true
                },
                Team = new Team
                {
                    Name = "Ferrari"
                },
                GrandPrix = new GrandPrix
                {
                    Date = new DateTime(2023, 05, 21)
                }
            },
            new Result
            {
                DriverId = 1,
                Points = 25,
                Driver = new Driver
                {
                    GivenName = "Charles",
                    FamilyName = "Leclerc",
                    Country = "Monaco",
                    IsActive = true
                },
                Team = new Team
                {
                    Name = "Ferrari"
                },
                GrandPrix = new GrandPrix
                {
                    Date = new DateTime(2023, 06, 04)
                }
            },
            new Result
            {
                DriverId = 44,
                Points = 12,
                Driver = new Driver
                {
                    GivenName = "Lewis",
                    FamilyName = "Hamilton",
                    Country = "United Kingdom",
                    IsActive = true
                },
                Team = new Team
                {
                    Name = "Ferrari"
                },
                GrandPrix = new GrandPrix
                {
                    Date = new DateTime(2023, 06, 18)
                }
            }
        };

        [Fact]
        public async Task GetStandingDTOsAsync_ReturnsAllDrivers()
        {
            var mockRepo = new Mock<IResultRepository>();

            mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(fakeResults);

            var service = new DriverStatsService(mockRepo.Object);

            var result = (await service.GetStandingDTOsAsync()).ToList();

            Assert.Equal(2, result.Count);

            Assert.Equal(1, result[0].DriverId);
            Assert.Equal("Charles Leclerc", result[0].DriverName);
            Assert.Equal(55, result[0].TotalPoints);
            Assert.Equal(1, result[0].Position);

            Assert.Equal(44, result[1].DriverId);
            Assert.Equal("Lewis Hamilton", result[1].DriverName);
            Assert.Equal(12, result[1].TotalPoints);
            Assert.Equal(2, result[1].Position);
        }
    }
}

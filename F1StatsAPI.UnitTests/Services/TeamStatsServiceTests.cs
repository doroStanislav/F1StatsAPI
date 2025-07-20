using F1StatsAPI.Models;
using F1StatsAPI.Repositories;
using F1StatsAPI.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1StatsAPI.UnitTests.Services
{
    public class TeamStatsServiceTests
    {
        List<Result> fakeResults = new List<Result>
        {
            new Result
            {
                TeamId = 1,
                Points = 25,
                Team = new Team
                {
                    Name = "Ferrari"
                }
            },
            new Result
            {
                TeamId = 1,
                Points = 18,
                Team = new Team
                {
                    Name = "Ferrari"
                }
            },
            new Result
            {
                TeamId = 2,
                Points = 15,
                Team = new Team
                {
                    Name = "McLaren"
                }
            },
            new Result
            {
                TeamId = 2,
                Points = 15,
                Team = new Team
                {
                    Name = "McLaren"
                }
            }
        };

        [Fact]
        public async Task GetStandingDTOsAsync_ReturnAllTeams()
        {
            var mockRepo = new Mock<IResultRepository>();

            mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(fakeResults);

            var service = new TeamStatsService(mockRepo.Object);

            var result = (await service.GetTeamStandingDTOsAsync()).ToList();

            Assert.Equal(2, result.Count);

            Assert.Equal(1, result[0].TeamId);
            Assert.Equal("Ferrari", result[0].TeamName);
            Assert.Equal(43, result[0].TotalPoints);
            Assert.Equal(1, result[0].Position);

            Assert.Equal(2, result[1].TeamId);
            Assert.Equal("McLaren", result[1].TeamName);
            Assert.Equal(30, result[1].TotalPoints);
            Assert.Equal(2, result[1].Position);
        }
    }
}

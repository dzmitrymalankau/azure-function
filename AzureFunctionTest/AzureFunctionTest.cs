using Model.Entity;
using Model.Repository;
using Model.Service;
using Moq;

namespace AzureFunctionTest
{
    public class AzureFunctionTest
    {
        public SportsTeamService SportsTeamService;

        [Fact]
        public void Expects_All_Teams_When_City_Is_Empty()
        {
            var res = SportsTeamService.GetTeams("");
            Assert.Equal("Bulls,Bears,Jaguars", res);
        }

        [Fact]
        public void Expects_Chicago_Teams_When_City_Is_Chicago()
        {
            // ReSharper disable once StringLiteralTypo
            var res = SportsTeamService.GetTeams("CHicaGo");
            Assert.Equal("Bulls,Bears", res);
        }

        [Fact]
        public void Expects_Nothing_When_No_Teams_For_City_Specified()
        {
            var res = SportsTeamService.GetTeams("NotA_RealCity");
            Assert.Equal(0, res.Length);
        }

        public AzureFunctionTest()
        {
            var dapperConnectionProvider = new Mock<IConnectionProvider>();

            var sportsTeamRepository = new Mock<DapperRepository<SportsTeam>>(dapperConnectionProvider.Object);
            sportsTeamRepository
                .Setup(repository => repository.Get("SELECT * FROM dbo.Teams"))
                .Returns(new List<SportsTeam>
                {
                    new("Bulls", "Chicago"),
                    new("Bears", "Chicago"),
                    new("Jaguars", "Jacksonville")
                });

            sportsTeamRepository
                // ReSharper disable once StringLiteralTypo
                .Setup(repository => repository.Get("SELECT * FROM dbo.Teams WHERE city = 'CHicaGo'"))
                .Returns(new List<SportsTeam>
                {
                    new("Bulls", "Chicago"),
                    new("Bears", "Chicago")
                });

            sportsTeamRepository
                .Setup(repository => repository.Get("SELECT * FROM dbo.Teams WHERE city = 'NotA_RealCity'"))
                .Returns(new List<SportsTeam>());

            SportsTeamService = new SportsTeamService(sportsTeamRepository.Object);
        }
    }
}
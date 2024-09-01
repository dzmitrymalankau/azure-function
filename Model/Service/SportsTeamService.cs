using System.Net;
using System.Text;
using Model.Entity;
using Model.Repository;

namespace Model.Service
{
    public class SportsTeamService(IRepository<SportsTeam> sportsTeamRepository)
    {
        public string GetTeams(string? city)
        {
            var encodedCity = WebUtility.HtmlEncode(city);
            var query = "SELECT * FROM dbo.Teams";
            if (!string.IsNullOrWhiteSpace(encodedCity))
            {
                query = $"{query} WHERE city = '{encodedCity}'";
            }

            var teams = sportsTeamRepository.Get(query).Select(t => t.TeamName);

            return new StringBuilder().AppendJoin(",", teams).ToString();
        }
    }
}

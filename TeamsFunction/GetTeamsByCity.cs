using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Model.Service;

namespace TeamsFunction
{
    public class GetTeamsByCity(IContextService contextService, SportsTeamService sportsTeamService, ILogger<GetTeamsByCity> logger)
    {
        [Function("GetTeamsByCity")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req, [FromQuery]string? city = null)
        {
            try
            {
                var cityOrAllTeams = string.IsNullOrWhiteSpace(city) ? "all teams" : city;
                logger.LogInformation($"Request for {cityOrAllTeams} has been logged by the context service {contextService.CorrelationId}.");

                var teams = sportsTeamService.GetTeams(city);

                return string.IsNullOrWhiteSpace(teams)
                    ? new NotFoundResult()
                    : new OkObjectResult(teams);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

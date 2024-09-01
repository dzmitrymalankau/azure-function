namespace Model.Entity
{
    public class SportsTeam(string teamName, string city)
    {
        public SportsTeam() : this(string.Empty, string.Empty)
        {}

        public string TeamName { get; init; } = teamName;
        public string City { get; init; } = city;
    }
}

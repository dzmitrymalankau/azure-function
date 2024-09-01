namespace Model.Repository
{
    public class DbSettings
    {
        public const string SectionName = "AppSettings";
        public required string ConnectionString { get; init; }
    }
}

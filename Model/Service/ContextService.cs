namespace Model.Service
{
    public class ContextService : IContextService
    {
        public string CorrelationId => Guid.NewGuid().ToString();
    }
}

namespace AzureServiceBus.Salary.Infrastructure.Configuration.Interfaces
{
    public interface ISqlDbDataServiceConfiguration
    {
        string ConnectionString { get; set; }
    }
}

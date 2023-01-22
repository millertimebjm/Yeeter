
namespace Yeeter.Business;

public interface IYeeterConfiguration
{
    string GetYeeterInMemoryDatabaseConnectionString();
    string GetYeeterSqlServerConnectionString();
    string GetYeeterCosmosConnectionString();
}
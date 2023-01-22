
namespace Yeeter.Business;

public interface IYeeterConfiguration
{
    string GetYeeterInMemoryDatabaseConnectionString();
    string GetYeeterSqlServerConnectionString();
    string GetYeeterCosmosConnectionString();
    void SetYeeterDataInitialized(bool dataInitialized);
    bool GetYeeterDataInitialized();
}
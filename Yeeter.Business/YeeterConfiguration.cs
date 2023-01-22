
namespace Yeeter.Business;

public class YeeterConfiguration : IYeeterConfiguration
{
    private string _yeeterInMemoryDatabaseConnectionString;
    public string GetYeeterInMemoryDatabaseConnectionString()
    {
        return _yeeterInMemoryDatabaseConnectionString;
    }

    private string _yeeterSqlServerConnectionString;
    public string GetYeeterSqlServerConnectionString()
    {
        return _yeeterSqlServerConnectionString;
    }

    private string _yeeterCosmosConnectionString;
    public string GetYeeterCosmosConnectionString()
    {
        return _yeeterCosmosConnectionString;
    }

    private bool _dataInitialized;
    public bool GetYeeterDataInitialized()
    {
        return _dataInitialized;
    }
    public void SetYeeterDataInitialized(bool dataInitialized)
    {
        _dataInitialized = dataInitialized;
    }

    public YeeterConfiguration(
        string yeeterInMemoryDatabaseConnectionString = null,
        string yeeterSqlServerConnectionString = null,
        string yeeterCosmosConnectionString = null
    )
    {
        _yeeterInMemoryDatabaseConnectionString = yeeterInMemoryDatabaseConnectionString;
        _yeeterSqlServerConnectionString = yeeterSqlServerConnectionString;
        _yeeterCosmosConnectionString = yeeterCosmosConnectionString;
    }
}
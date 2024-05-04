using System.Data;
using System.Text;
using AnVatCanTho.DataAccess.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AnVatCanThoWeb.Areas.Admin.Common;

public sealed class GetAdminDatabaseSchemaService
{
    private string? _schema = null;
    private readonly IServiceProvider _serviceProvider;

    public GetAdminDatabaseSchemaService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public string? GetDatabaseSchema()
    {
        if (_schema is not null)
        {
            return _schema;
        }
        
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()!;

        var connection = dbContext.Database.GetDbConnection();
        var databaseName = connection.Database;
        
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_CATALOG = @p1";
        command.CommandType = CommandType.Text;
        command.Parameters.Add(new SqlParameter("p1", databaseName));
        
        dbContext.Database.OpenConnection();

        using var reader = command.ExecuteReader();
        if (!reader.HasRows) return _schema;

        var tableDict = new Dictionary<string, List<string>>();
        
        while (reader.Read())
        {
            var tableName = reader.GetString(2);
            var columnName = reader.GetString(3);

            if (tableName.Contains("MigrationsHistory"))
            {
                continue;
            }
            
            if (tableDict.TryGetValue(tableName, out var cols))
            {
                cols.Add(columnName);
                tableDict[tableName] = cols;
            }
            else
            {
                var newCols = new List<string> { columnName };
                tableDict[tableName] = newCols;
            }
        }

        var schema = new StringBuilder();
        foreach (var pair in tableDict)
        {
            schema.Append($"{pair.Key}({string.Join(',', pair.Value)})\n");
        }

        _schema = schema.ToString();

        return _schema;
    }
}
using System.Data;
using System.Data.Common;
using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Areas.Admin.Common;
using AnVatCanThoWeb.Common.Authentication;
using DotnetGeminiSDK.Client.Interfaces;
using DotnetGeminiSDK.Model.Request;
using DotnetGeminiSDK.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnVatCanThoWeb.Areas.Admin.Controllers;

[Area(AdminAreaName.Value)]
[Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.AdminScheme)]
public class HomeController : Controller
{
    private readonly GetAdminDatabaseSchemaService _service;
    private readonly IGeminiClient _geminiClient;
    private readonly ApplicationDbContext _dbContext;

    public HomeController(GetAdminDatabaseSchemaService service, IGeminiClient geminiClient,
        ApplicationDbContext dbContext)
    {
        _service = service;
        _geminiClient = geminiClient;
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        _service.GetDatabaseSchema();
        return View();
    }

    [HttpPost("/api/[area]/natural-query")]
    public async Task<IActionResult> ReceiveAndExecuteNaturalQuery([FromForm] string naturalQuery)
    {
        var sqlQuery = await TurnNaturalQueryIntoSqlQuery(naturalQuery);
        if (sqlQuery is null)
        {
            return BadRequest();
        }

        var isSqlValid = await VerifySqlQuery(sqlQuery);

        if (!isSqlValid)
        {
            return BadRequest();
        }

        var command = _dbContext.Database.GetDbConnection().CreateCommand();
        command.CommandText = sqlQuery;
        command.CommandType = CommandType.Text;

        try
        {
            await _dbContext.Database.OpenConnectionAsync();

            await using var reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
            {
                return Ok(Enumerable.Empty<object>());
            }

            var returnValues = SerializeDbDataReader(reader);

            return Ok(returnValues);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    private async Task<string?> TurnNaturalQueryIntoSqlQuery(string naturalQuery)
    {
        var schema = _service.GetDatabaseSchema();
        if (schema is null)
        {
            return null;
        }

        var prompt = schema
                     + "\nDựa vào lược đồ cơ sở dữ liệu Microsoft SQL Server trên, viết truy vấn Transact-SQL đáp ứng yêu cầu sau:\n"
                     + naturalQuery;

        GeminiMessageResponse? sqlResult;
        try
        {
            sqlResult = await _geminiClient.TextPrompt(prompt, new GenerationConfig
            {
                Temperature = 0D,
                MaxOutputTokens = 256,
                TopP = 0.99D,
            });
        }
        catch (Exception)
        {
            return null;
        }
        
        if (sqlResult is null
            || !sqlResult.Candidates.Any()
            || !sqlResult.Candidates.First().Content.Parts.Any())
        {
            return null;
        }

        var sql = sqlResult.Candidates.First().Content.Parts.First().Text;
        sql = sql.Replace("```", "");
        sql = sql.TrimStart('s','q','l');
        return sql;
    }

    private async Task<bool> VerifySqlQuery(string sqlQuery)
    {
        // var schema = _service.GetDatabaseSchema();
        // if (schema is null)
        // {
        //     return false;
        // }

        var prompt = "Verify that the SQL query below is a valid Transact-SQL query or not. If valid, response '1':\n"
                     + sqlQuery;

        GeminiMessageResponse? sqlResult;
        try
        {
            sqlResult = await _geminiClient.TextPrompt(prompt, new GenerationConfig
            {
                Temperature = 0D,
                MaxOutputTokens = 256,
                TopP = 0.99D,
            });
        }
        catch (Exception)
        {
            return false;
        }

        if (sqlResult is null
            || !sqlResult.Candidates.Any()
            || !sqlResult.Candidates.First().Content.Parts.Any())
        {
            return false;
        }

        return sqlResult.Candidates.First().Content.Parts.First().Text.Contains('1');
    }

    private IEnumerable<Dictionary<string, object>> SerializeDbDataReader(DbDataReader reader)
    {
        var results = new List<Dictionary<string, object>>();

        var cols = new List<string>();
        for (var i = 0; i < reader.FieldCount; ++i)
        {
            cols.Add(reader.GetName(i));
        }

        while (reader.Read())
        {
            results.Add(SerializeRow(cols, reader));
        }
        
        return results;
    }

    private Dictionary<string, object> SerializeRow(IEnumerable<string> cols, DbDataReader reader)
    {
        return cols.ToDictionary(col => col, col => reader[col]);
    }
}
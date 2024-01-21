using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// GET /
app.MapGet("/", () =>
{
    var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

    var sqlSuccess = false;

    // Test SQL connection
    if (!string.IsNullOrWhiteSpace(connectionString))
    {
        var query = Environment.GetEnvironmentVariable("SQL_QUERY_1");
        if (string.IsNullOrWhiteSpace(query))
            query = "SELECT 1";

        if (!query.ToUpper().StartsWith("SELECT "))
            return Results.Problem("Query must start with 'SELECT '. Got: {query}");

        try
        {
            // Connect to SQL server
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            // Execute SQL query
            using var cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            
            sqlSuccess = true;
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }

    }

    return Results.Problem($"Hello World! sqlSuccess={sqlSuccess}", statusCode: 200);
});

// Start listening on PORT
app.Run($"http://*:{Environment.GetEnvironmentVariable("PORT") ?? "8080"}");

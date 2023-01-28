using Yeeter.Business;
using Yeeter.Business.EntityFrameworkRepository;
using Microsoft.AspNetCore.Mvc;
using Yeeter.Common;

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.AllowAnyOrigin();
            //builder.WithOrigins("http://localhost:5107", "http://localhost:5079");
        });
});

builder.Services.AddSingleton<IYeeterConfiguration>(_ =>
    new YeeterConfiguration(yeeterInMemoryDatabaseConnectionString: "YeeterDatabase"));
builder.Services.AddDbContext<YeeterDbContext>();
builder.Services.AddScoped<IYeeterRepository, EntityFrameworkYeeterRepository>();

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/yeets", async ([FromServices] IYeeterRepository yeeterRepository, int? count) =>
{
    var sanitizedCount = Yeeter.Common.InputSanitation.SanitizeCount(count);
    var yeets = await yeeterRepository.GetYeets(sanitizedCount);
    return Results.Text(ApiSerializer.SerializeForMapGet(yeets),
        contentType: "application/json");
});

app.MapGet("/yeets/{yeetId}", async ([FromServices] IYeeterRepository yeeterRepository, string yeetId, int? count) =>
{
    if (string.IsNullOrWhiteSpace(yeetId))
        return Results.NotFound();

    var sanitizedCount = Yeeter.Common.InputSanitation.SanitizeCount(count);
    var yeet = await yeeterRepository.GetYeet(yeetId);

    if (yeet is null)
        return Results.NotFound();

    return Results.Text(ApiSerializer.SerializeForMapGet(yeet),
        contentType: "application/json");
});

app.MapGet("/users/{userId}", async ([FromServices] IYeeterRepository yeeterRepository, string userId, int? count) =>
{
    if (string.IsNullOrWhiteSpace(userId))
        return Results.NotFound();

    var sanitizedCount = Yeeter.Common.InputSanitation.SanitizeCount(count);
    var user = await yeeterRepository.GetYeetsByUserId(userId, sanitizedCount);

    if (user is null)
        return Results.NotFound();

    return Results.Text(ApiSerializer.SerializeForMapGet(user),
            contentType: "application/json");
});

app.Run();

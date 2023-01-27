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

app.MapGet("/", async ([FromServices] IYeeterRepository yeeterRepository, int? count) =>
{
    if (count.HasValue && count.Value > 0)
        count = count.Value > 20 ? 20 : count;
    else
        count = 5;

    var yeets = await yeeterRepository.GetYeets(count.Value);
    return Results.Text(ApiSerializer.SerializeForMapGet(yeets),
        contentType: "application/json");
});

app.MapGet("/u/{userId}", async ([FromServices] IYeeterRepository yeeterRepository, string userId, int? count) =>
{
    if (count.HasValue && count.Value > 0)
        count = count.Value > 20 ? 20 : count;
    else
        count = 5;

    var yeets = await yeeterRepository.GetYeetsByUserId(userId, count.Value);
    return Results.Text(ApiSerializer.SerializeForMapGet(yeets),
        contentType: "application/json");
});


app.Run();

using Faker;
using Yeeter.Models;
using Microsoft.EntityFrameworkCore;
using Yeeter.Business;
using Yeeter.Business.EntityFrameworkRepository;

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

app.MapGet("/", (int? count) =>
{
    if (count.HasValue && count.Value > 0)
    {
        count = count.Value > 20 ? 20 : count;
    }
    else
    {
        count = 5;
    }

    var yeets = new List<Yeet>();
    for (int i = 0; i < count; i++)
    {
        yeets.Add(new Yeet()
        {
            Text = string.Join(" ", Faker.Lorem.Sentences(3)),
            CreatedDate = DateTime.UtcNow,
            User = new User()
            {
                Name = Faker.Name.FullName(),
                Handle = "@" + Faker.Name.Last() + Faker.Name.First(),
            },
        });
    }
    return yeets;
});

app.Run();

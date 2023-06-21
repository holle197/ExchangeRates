using ExchangeRates.Core.Fetchers;
using ExchangeRates.Data.DataContext;
using ExchangeRates.Data.DataManaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbDataContext>
    (
        d => d.UseNpgsql(builder.Configuration.GetConnectionString("ExchangeRatesDb"))
    );
builder.Services.AddScoped<IDataManager, DataManager>();
builder.Services.AddSingleton<IFetcher>(opt =>new FixerFetcher(builder.Configuration.GetValue<string>("ApiURLS:FixerUrls:APIKey"), builder.Configuration.GetValue<string>("ApiURLS:FixerUrls:URL")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

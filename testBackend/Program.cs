using DataAccess;
using DataAccess.Data;
using Domain.QueryHandler;
using Domain;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using Domain.CommandHandler;
using ElasticSearchProject.Interface;
using ElasticSearchProject.Service;
using KafkaApacheProject;
using DataAccess.Dtos;

var builder = WebApplication.CreateBuilder(args);

var server = builder.Configuration.GetSection("SqlServer:DbServer").Value;
var user = builder.Configuration.GetSection("SqlServer:DbUser").Value;
var password = builder.Configuration.GetSection("SqlServer:Password").Value;
var database = builder.Configuration.GetSection("SqlServer:Database").Value;

var topic = builder.Configuration.GetSection("KafkaConfiguration:Topic").Value;
var bootstrapServer = builder.Configuration.GetSection("KafkaConfiguration:BootstrapServers").Value;

var connectionString = $"Initial Catalog={database}; Data Source={server};User ID={user};Password={password}";

builder.Services.AddCors();

// Add services to the container.
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>(option => option.UseSqlServer(connectionString, b => b.MigrationsAssembly("testBackend")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IQueryHandler<Permission>, GetPermissionQueryHandler>();
builder.Services.AddScoped<ICommandHandler<Permission>, RequestPermissionCommandHandler>();
builder.Services.AddScoped<IElasticsearchService, ElasticsearchService>();

builder.Services.AddKafkaMessageBus();

builder.Services.AddKafkaProducer<string, PermissionDto>(p =>
{
    p.Topic = topic;
    p.BootstrapServers = bootstrapServer;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true)
               .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Data;
using Data.Repositories;
using Services.Implementations;
using Services.Interfaces;
using System.Reflection;

var pathDirectory = Environment.CurrentDirectory;
var locationProject = pathDirectory.Substring(0, pathDirectory.IndexOf("AnyDo"));
string file = @"AnyDo\Data\AnyDoDB.db";
string locationDb = locationProject + file;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddTransient<IListService, ListService>();
builder.Services.AddTransient<IListRepository, ListRepository>(provider => new ListRepository("Data Source=" + locationDb));
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<ITaskRepository, TaskRepository>(provider => new TaskRepository("Data Source=" + locationDb));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

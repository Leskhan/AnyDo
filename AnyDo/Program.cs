using Data;
using Data.Repositories;
using Services.Implementations;
using Services.Interfaces;

var pathDirectory = Environment.CurrentDirectory;
var locationProject = pathDirectory.Substring(0, pathDirectory.IndexOf("AnyDo"));
string file = @"AnyDo\Data\AnyDoDB.db";
string locationDb = locationProject + file;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IListService, ListService>();
builder.Services.AddTransient<IListRepository, ListRepository>(provider => new ListRepository("Data Source=" + locationDb));


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

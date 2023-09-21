using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Laba2;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Configuration.AddIniFile("config/microsoft.ini");
var Microsoft = new Company();
app.Configuration.Bind(Microsoft);

builder.Configuration.AddJsonFile("config/google.json");
var Google = new Company();
app.Configuration.Bind(Google);

builder.Configuration.AddXmlFile("config/apple.xml");
var Apple = new Company();
app.Configuration.Bind(Apple);


Company[] companies = new Company[3];
companies[0] = Microsoft;
companies[1] = Apple;
companies[2] = Google;
int Id_max = 0;
for (int i = 0; i < companies.Length - 1; i++)
{
    if (companies[i].Nperson < companies[i + 1].Nperson) { Id_max = i + 1; } else { Id_max = i; }
}
app.MapGet("1/", (IConfiguration appConfig) => $"{companies[Id_max].Name} - {companies[Id_max].Nperson}");


builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
 {
     {"name", "Evgeniy" },
     { "age", "21" }
 }
);


app.MapGet("2/", (IConfiguration appConfig) => $"{appConfig["name"]} - {appConfig["age"]}");


app.Run();

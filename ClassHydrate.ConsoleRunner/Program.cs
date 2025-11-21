using ClassHydrate.ConsoleRunner.Models;
using ClassHydrate.Net.Extensions;

var demoClassType = typeof(DemoClass);
var demoClassTypePropertyResults = demoClassType.GetTypePropertyTypeResult();
var demoClassTypeProperties = demoClassType.GetPropertyNames();

Console.WriteLine("DemoClass Properties:");
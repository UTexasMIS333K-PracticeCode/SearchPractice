//add a using statement for currency
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

//TODO: Update these namespaces to match your project name 
//Be sure to remove the []
using SearchPracticeNETv7.DAL;
using SearchPracticeNETv7.Models;

//create a web application builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//TODO: Add database on Azure so you have a connection string
//TODO: Add a connection string here once you have created it on Azure
String connectionString = "Server=tcp:fa23robertsonexamples.database.windows.net,1433;Initial Catalog=SearchPractice;Persist Security Info=False;User ID=MISadmin;Password=Password123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

//NOTE: This tells your application how to get a connection to the database
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


//build the app
var app = builder.Build();

//These lines allow you to see more detailed error messages
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();

//This line allows you to use static pages like style sheets and images
app.UseStaticFiles();

//This marks the position in the middleware pipeline where a routing decision
//is made for a URL.
app.UseRouting();

//TODO: (HW4 & Beyond) Once you have added Identity into your project, you will 
//need to uncomment these lines
app.UseAuthentication();
app.UseAuthorization();


//This allows the data annotations for currency to work on Macs
app.Use(async (context, next) =>
{
    CultureInfo.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
    CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;

    await next.Invoke();
});


//This method maps the controllers and their actions to a patter for
//requests that's known as the default route. This route identifies
//the Home controller as the default controller and the Index() action
//method as the default action. The default route also identifies a 
//third segment of the URL that's a parameter named id.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
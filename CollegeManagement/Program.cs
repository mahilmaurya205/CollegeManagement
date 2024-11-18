using Microsoft.EntityFrameworkCore;
using CollegeManagement.Models;
using CollegeManagement.Entities;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("CollegeManagementDB");
Console.WriteLine($"Connection String: {connectionString}");
builder.Services.AddDbContext<CollegeManagementDBContext>(options =>
    options.UseSqlServer(connectionString));


// Configure DbContext with connection string from appsettings.json
builder.Services.AddDbContext<CollegeManagementDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeManagementDB")));

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<CollegeManagementDBContext>(options =>
//    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run("https://localhost:9080");


using BeautySalonWebApplication.Data;
using BeautySalonWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BeautySalonWebApplication.Services;
using BeautySalonWebApplication.Configuration;
using System.Net.Mail;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add Identity services with custom ApplicationUser
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    // Configure identity options if needed
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultUI();
builder.Services.AddTransient<IEmailService, EmailService>();
// Configure SmtpSettings options
builder.Configuration.AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true);
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
// Add SmtpEmailSender service
builder.Services.AddTransient<ISmtpEmailSender, SmtpEmailSender>();
builder.Services.AddTransient<IViewRenderService, ViewRenderService>();
builder.Services.AddTransient<SmtpClient>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

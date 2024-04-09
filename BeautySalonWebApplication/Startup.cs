using BeautySalonWebApplication.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BeautySalonWebApplication.Configuration;
using BeautySalonWebApplication.Data;
using BeautySalonWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonWebApplication
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Database context
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add logging services
            services.AddLogging(loggingBuilder =>
            {
                // Configure logging options here
                loggingBuilder.AddConsole(); // Example: Log to the console
                // Add other logging providers as needed
            });

            // Register EmailService as a transient service
            services.AddTransient<IEmailService, EmailService>();
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            // Other service registrations...
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Other configuration...

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}

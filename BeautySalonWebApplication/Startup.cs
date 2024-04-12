using BeautySalonWebApplication.Configuration;
using BeautySalonWebApplication.Data;
using BeautySalonWebApplication.Models;
using BeautySalonWebApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;



namespace BeautySalonWebApplication
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Database context
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

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

            // Add ASP.NET Core Identity email sender
            services.AddTransient<IEmailSender, SmtpEmailSender>();

            // Other service registrations...
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            services.AddAuthorization();

            services.AddControllersWithViews();
            services.AddRazorPages(); // Add this line to configure Razor Pages services
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages(); // Add this line to map Razor Pages endpoints
            });
        }

    }
}

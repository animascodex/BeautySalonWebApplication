using BeautySalonWebApplication.Configuration;
using BeautySalonWebApplication.Data;
using BeautySalonWebApplication.Models;
using BeautySalonWebApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace BeautySalonWebApplication
{
    public class Startup(IConfiguration configuration, IWebHostEnvironment env)
	{
        private readonly IConfiguration _configuration = configuration;
		private readonly IWebHostEnvironment _env = env;

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

            // Other service registrations...
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();
            services.AddAuthorization();
            // Register EmailService as a transient service
            services.Configure<SmtpSettings>(_configuration.GetSection("SmtpSettings"));
			// Load sensitive settings from appsettings.secrets.json
			var secretsConfig = new ConfigurationBuilder()
				.SetBasePath(_env.ContentRootPath)
				.AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true)
				.Build();

			services.Configure<SmtpSettings>(secretsConfig.GetSection("SmtpSettings"));
			services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ISmtpEmailSender, SmtpEmailSender>();
			services.AddTransient<SmtpClient>();
			services.AddTransient<IViewRenderService, ViewRenderService>();

			services.AddControllersWithViews();
            services.AddRazorPages().AddRazorOptions(options =>
			{
				// Add additional search paths for views within the Areas
				options.AreaViewLocationFormats.Add("/Areas/Identity/Pages/{1}/{0}.cshtml");
				options.AreaViewLocationFormats.Add("/Areas/Identity/Pages/Shared/{0}.cshtml");
			});
            // Add this line to configure Razor Pages services
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

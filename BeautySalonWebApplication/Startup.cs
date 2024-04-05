using BeautySalonWebApplication.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BeautySalonWebApplication.Configuration;

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

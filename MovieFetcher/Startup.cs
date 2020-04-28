using Autofac;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using EmailConfirmationService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MovieFetcher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            EmailConfigurationModel emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfigurationModel>();
            emailConfig.Password = "Fake_Imdb_3@";//Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
            services.AddSingleton(emailConfig);

            services.AddDbContextPool<MovieContext>(option => { option.UseSqlServer(Configuration.GetConnectionString("Default")); });
            services.AddHostedService<MovieFetcher>();
            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<MovieContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(config =>
            {
                config.Password.RequiredLength = 6;
                config.Password.RequireDigit = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Autofac configuration
            builder.RegisterModule<DALAutofacModule>();
        }
    }
}

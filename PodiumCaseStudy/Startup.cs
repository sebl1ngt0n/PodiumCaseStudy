using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PodiumCaseStudy.Context;
using PodiumCaseStudy.Services.Mortgage;
using PodiumCaseStudy.Services.User;

namespace PodiumCaseStudy
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
            services.AddControllers();
            services.AddSpaStaticFiles(options => options.RootPath = "client-app/dist");
            services.AddSingleton(x =>
            {
                var opt = new DbContextOptionsBuilder<PodiumCaseStudyContext>();
                var dbName = Configuration.GetValue<string>("dbName") ?? "PodiumCaseStudyContext";
                opt.UseInMemoryDatabase(dbName);
                return new PodiumCaseStudyContext(opt.Options);
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMortgageService, MortgageService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";

                if (env.IsDevelopment())
                {
                    spa.UseVueDevelopmentServer();
                }
            });

            var context = app.ApplicationServices.GetService<PodiumCaseStudyContext>();
            context.SeedContext();
        }
    }
}

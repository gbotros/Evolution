using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Evolution.Apis
{
    public class Startup
    {
        private const string corsAllowedHosts = "corsAllowedHosts";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Evolution.Apis", Version = "v1" });
            });

            services.AddApplicationServices(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy(name: corsAllowedHosts,
                    builder =>
                    {
                        var hosts = Configuration.GetValue<string>("corsAllowedHosts")
                            .Split(";")
                            .Where(s=> !string.IsNullOrWhiteSpace(s))
                            .ToArray();
                        builder
                            .WithOrigins(hosts)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Evolution.Apis v1"));
            }

            app.UseCors(corsAllowedHosts);

            app.UseHttpsRedirection();
            app.UseHsts();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}

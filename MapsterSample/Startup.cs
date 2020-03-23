using Mapster;
using MapsterMapper;
using MapsterSampleTest.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MapsterSample
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
            services.AddControllersWithViews();

            services.AddSingleton(GetAdapterConfig());
            services.AddScoped<IMapper, ServiceMapper>();
        }

        private TypeAdapterConfig GetAdapterConfig()
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<ClassA, ClassB>()
                  .Map(b => b.Name, a => $"{a.Id}_{a.Price}");

            config.NewConfig<ClassB, ClassC>()
                  .Ignore(c => c.Name)
                  .Map(c => c.Id, b => b.Id + 2);

            return config;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                                             name: "default",
                                             pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class ClassAConfig
    {
    }
}
using BabbApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using BabbApi.Services;
using BabbApi.Services.Impl;
using BabbApi.Helpers;

namespace BabbApi
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

			var currentFolder = System.IO.Directory.GetCurrentDirectory();
			var dbPath = System.IO.Path.Combine(currentFolder, "BabbApi.db");

			services.AddControllers();
			services.AddDbContext<AppDbContext>(opt => opt.UseSqlite($"Data Source={dbPath}"));
			services.AddScoped<IUserService, UserService>();
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "BabbApi", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				// app.UseSwagger();
				// app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BabbApi v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();
			app.UseMiddleware<JwtMiddleware>();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

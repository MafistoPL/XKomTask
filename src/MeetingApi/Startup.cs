using FluentValidation;
using FluentValidation.AspNetCore;
using MeetingApi.Dtos;
using MeetingApi.Dtos.Validators;
using MeetingApi.Middleware;
using MeetingApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence.EF;
using Persistence.EF.Entities;
using Persistence.EF.Repositories;
using System.IO;

namespace MeetingApi
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
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"));
            });

            services.AddScoped<IAsyncRepository<Meeting>, MeetingRepository>();
            services.AddScoped<IAsyncRepository<Participant>, ParticipantRepository>();
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddAutoMapper(GetType().Assembly);
            services.AddScoped<IValidator<RegisterParticipantDto>, RegisterParticipantDtoValidator>();
            services.AddScoped<IValidator<CreateMeetingDto>, CreateMeetingDtoValidator>();
            services.AddControllers().AddFluentValidation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeetingApi", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "MeetingApi.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeetingApi v1"));
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

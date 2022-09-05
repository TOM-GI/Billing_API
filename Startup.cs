using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Billing_API.Common.Attributes;
using Billing_Api.Core.Database;
using Billing_API.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Scrutor;

namespace Billing_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Scan(ScanAssemblyAndRegisterServices);
            services.AddControllers().AddJsonOptions(x => { });
            services.AddFluentValidationAutoValidation();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Billing_API", Version = "v1" }); });
            services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContext<BillingApiDbContext>(x => x.UseInMemoryDatabase("DB"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Billing_API v1"));
            }
            app.UseHttpsRedirection();
            
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void ScanAssemblyAndRegisterServices(ITypeSourceSelector selector)
        {
            selector.FromApplicationDependencies()
                    .AddClasses(q => q.WithAttribute<InjectServiceAttribute>(w => w.As == InjectAs.Self))
                    .AsSelf().WithScopedLifetime()
                    .AddClasses(q => q.WithAttribute<InjectServiceAttribute>(w => w.As == InjectAs.ImplementingInterface))
                    .AsImplementedInterfaces().WithScopedLifetime();
        }
    }
}
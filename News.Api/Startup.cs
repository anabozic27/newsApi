using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using News.BL.Infrastructure.AutoMapper;
using News.BL.Interfaces;
using News.BL.Models.Request;
using News.BL.Services;
using News.DAL;
using News.DAL.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace News.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<LoginModel>();
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
            });

            services.AddDbContext<ArticleContext>(item => item.UseSqlServer(Configuration.GetConnectionString("ArticleDatabaseConnectionString")));            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));            services.AddTransient(typeof(IArticleService), typeof(ArticleService));
            services.AddTransient(typeof(ICategoryService), typeof(CategoryService));
            services.AddTransient(typeof(IUserService), typeof(UserService));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "News API", Version = "v1" });
                // Adds fluent validation rules to swagger
                c.AddFluentValidationRules();
                c.CustomSchemaIds(i => i.FullName);
                c.EnableAnnotations();
                c.OrderActionsBy(a => a.GroupName);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });


            // configure jwt authentication 
            var key = Encoding.ASCII.GetBytes("articleAplication_2022@_secret");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler();

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "News API");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
                c.EnableFilter();
            });
        }
    }
}

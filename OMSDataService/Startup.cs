using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OMSDataService.EF;
using OMSDataService.EntityMapper;
//using OMSDataService.IocMapper;
using OMSDataService.DomainObjects.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using OMSDataService.IocMapper;

namespace OMSDataService
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

            services.AddDbContext<ApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"),
                                              sqlServerOptions => sqlServerOptions.CommandTimeout(180)));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                    //.AllowCredentials()
                );
            });
           
            services.AddMvc().AddNewtonsoftJson();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "OMSDataService API", Version = "v1" });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                                   Enter 'Bearer' [space] and then your token in the text input below.
                                   \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddAutoMapper(typeof(MappingProfile));

            services.Configure<LdapConfig>(Configuration.GetSection("Ldap"));

            services.AddScoped<IAuthenticationService, LdapAuthenticationService>();

            services.Configure<AuthOptions>(Configuration.GetSection("AuthOptions"));

            var authOptions = Configuration.GetSection("AuthOptions").Get<AuthOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidAudience = authOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SecureKey)),
                    ClockSkew = TimeSpan.Zero
                };

                /*
                // could use this method to renew the token if that was design
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
                */
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to AddAutofac in the Program.Main
            // method or this won't be called.
            builder.RegisterModule(new IocRegistry());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OMSDataService V1");
            });

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

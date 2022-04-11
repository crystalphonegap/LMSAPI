using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.AspNetCore;
using HRJ.LMS.API.Middleware;
using HRJ.LMS.API.Schedulers;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Application.User;
using HRJ.LMS.Domain;
using HRJ.LMS.Infrastructure.Security;
using HRJ.LMS.Infrastructure.TimedJob;
using HRJ.LMS.Infrastructure.Utilities;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HRJ.LMS.API
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
            services.AddControllers().AddFluentValidation(options => {
                    options.RegisterValidatorsFromAssemblyContaining<Login>();
                });;

            //adding database service
            /* services.AddDbContext<AppDbContext>(opts => {
                opts.UseMySql(Configuration.GetConnectionString("MySQLConnection"));
            }); */

            services.AddDbContext<AppDbContext>(opts => {
                opts.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection"));
            });

            //adding MediatR and Automapper Assembly
            services.AddMediatR(typeof(Login.Handler).Assembly);
            services.AddAutoMapper(typeof(Login.Handler));

            //adding authentication and JWT service
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts => {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            var identityBuilder = services.AddIdentityCore<AppUser>().AddRoles<AppUserRole>();
            identityBuilder.AddEntityFrameworkStores<AppDbContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();
            identityBuilder.AddDefaultTokenProviders();

            //adding authorization and policy


            //adding CORS
            services.AddCors(opts => {
                opts.AddPolicy("CORSPolicy", policy => {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(
                        "http://localhost",
                        "http://localhost:4200",
                        "https://www.enduratiles.com",
                        "https://enduratiles.com"                        
                        )
                    .WithExposedHeaders("Content-Disposition");
                });
            });

            //add scope - singleton - transient
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<ICronIndiaMartLead, CronIndiaMartLead>();
            services.AddScoped<IExcelFileAccessor, ExcelFileAccessor>();
            services.AddScoped<ILeadActivityLog, LeadActivityLog>();
            services.AddScoped<IExcelReportAccessor, ExcelReportAccessor>();

            //Scheduler
            services.AddHostedService<TimedHostedService>();

            //adding swagger configuration
            services.AddSwaggerGen(s => {
                s.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "HRJ - Lead Management System - API",
                    Description = "Lead Management System - API developed using Asp.Net Core 3.1.",
                    Contact = new OpenApiContact
                    {
                        Name = "Prism Johnson Limited - IT team",
                        Email = "jitesh.nikale@primsjohnson.in"
                    }
                });
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
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSecurityHeaders(GetHeaderPolicyCollection());

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("../swagger/V1/swagger.json", "HRJ - Lead Management System - API");
            });

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CORSPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }

        private HeaderPolicyCollection GetHeaderPolicyCollection()
        {
            var policyCollection = new HeaderPolicyCollection()
            .AddPermissionsPolicy(builder => {
                builder.AddFullscreen().All();
                builder.AddCamera().None();
                builder.AddAccelerometer().None();
                builder.AddAutoplay().Self();
                builder.AddGeolocation().None();
                builder.AddGyroscope().None();
                builder.AddMagnetometer().None();
                builder.AddPictureInPicture().None();
                builder.AddMicrophone().Self();
                builder.AddMidi().Self();
                builder.AddPayment().None();
            })
            .AddFrameOptionsDeny()
            .AddXssProtectionBlock()
            .AddContentTypeOptionsNoSniff()
            .AddStrictTransportSecurityMaxAgeIncludeSubDomains(maxAgeInSeconds: 60 * 60 * 24 * 365) // maxage = one year in seconds
            .AddReferrerPolicyStrictOriginWhenCrossOrigin()
            .RemoveServerHeader()
            .AddContentSecurityPolicy(builder =>
            {
                builder.AddObjectSrc().None();
                builder.AddFormAction().Self();
                builder.AddFrameAncestors().None();
                builder.AddBlockAllMixedContent();
                builder.AddStyleSrc().Self().UnsafeInline();
                builder.AddScriptSrc().Self().UnsafeInline();
                /* builder.AddStyleSrc().Self().WithHash256("wkAU1AW/h8YFx0XlzvpTllAKnFEO2tw8aKErs5a26LY=");
                builder.AddScriptSrc().Self()
                    .WithHash256("Tui7QoFlnLXkJCSl1/JvEZdIXTmBttnWNxzJpXomQjg=") 
                    .WithHash256("hXgTHxh/UT3FHj+xEUdIDBncZzV6HFREcMV6iSTMEaw="); */
                builder.AddFontSrc().Self().UnsafeInline();
                builder.AddFormAction().Self();
                builder.AddFrameAncestors().Self();
                builder.AddImgSrc().Self().From("data:");  
            });
            return policyCollection;
        }
    }
}

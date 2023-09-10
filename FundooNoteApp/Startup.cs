using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepoLayer.Context;
using RepoLayer.Interfaces;
using RepoLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApp
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
            services.AddControllers();
            services.AddHttpContextAccessor();


            //CORS SERVICE CONFIGURATION:-
            services.AddCors(data =>
            {
                data.AddPolicy(
                    name: "AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            //DATABASE CONFIGURATION:-
            services.AddDbContext<FundooContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:FundooDataBase"]));


            // USER TABLE CONFIGURATION:-
            services.AddTransient<IUserBusiness , UserBusiness>();
            services.AddTransient<IUserRepo, UserRepo>();


            // NOTE TABLE CONFIGURATION:-
            services.AddTransient<INoteBusiness, NoteBusiness>();
            services.AddTransient<INoteRepo, NoteRepo>();


            // COLLAB TABLE CONFIGURATION:-
            services.AddTransient<ICollabBusiness , CollabBusiness>();
            services.AddTransient<ICollabRepo, CollabRepo>();


            // LABEL TABLE CONFIGURATION:-
            services.AddTransient<ILabelBusiness , LabelBusiness>();
            services.AddTransient<ILabelRepo, LabelRepo>();


            // SWAGGER SERVICES IMPLEMENTATION:-
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BookStore App",
                    Version = "v1",
                    Description = "API's for BookStore Application",
                });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                        securitySchema, new[] { "Bearer" } }
                });
            });



            //CONFIGURATION OF JWT AUTHENTICATION:-
            var jwtSettings = Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtSettings:Issuer"],
                    ValidAudience = Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });


            //CLOUDINARY CONFIGURATION
            IConfigurationSection configurationSection = Configuration.GetSection("CloudinaryConnection");
            Account cloudinaryAccount = new Account(
                configurationSection["cloud_name"],
                configurationSection["cloud_api_key"],
                configurationSection["cloud_api_secret"]
                );
            Cloudinary cloudinary = new Cloudinary(cloudinaryAccount);
            services.AddSingleton(cloudinary);

            services.AddTransient<FileService, FileService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            app.UseCors("AllowOrigin");


            //SWAGGGER MIDDLEWARE IMPLEMENTATION:-
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Register v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

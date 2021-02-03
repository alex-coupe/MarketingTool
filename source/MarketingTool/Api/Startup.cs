using Api.Services;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ApplicationLayer
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddDbContext<DatabaseContext>(options =>
            options.UseLazyLoadingProxies()
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("DataAccess")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MarketingAppAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RootUsers", policy => policy.RequireClaim("RoleId", RolesEnum.Root.ToString("d"));
                
                options.AddPolicy("AdminUsers", policy => policy.RequireClaim("RoleId", RolesEnum.Admin.ToString("d"), RolesEnum.Founder.ToString("d"))
                .RequireClaim("PermissionId", PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("NotArchived", policy => policy.RequireClaim("Archived", "0"));

                options.AddPolicy("AddCampaign", policy => 
                policy.RequireClaim("PermissionId", PermissionsEnum.AddCampaigns.ToString("d"), PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("EditCampaign", policy =>
                policy.RequireClaim("PermissionId", PermissionsEnum.EditCampaigns.ToString("d"), PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("AddList", policy =>
                policy.RequireClaim("PermissionId", PermissionsEnum.AddLists.ToString("d"), PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("EditList", policy =>
                policy.RequireClaim("PermissionId", PermissionsEnum.EditLists.ToString("d"), PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("AddRecpient", policy =>
                policy.RequireClaim("PermissionId", PermissionsEnum.AddRecipients.ToString("d"), PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("EditRecipient", policy =>
                policy.RequireClaim("PermissionId", PermissionsEnum.EditRecipients.ToString("d"), PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("AddTemplate", policy =>
                policy.RequireClaim("PermissionId", PermissionsEnum.AddTemplates.ToString("d"), PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("EditTemplate", policy =>
                policy.RequireClaim("PermissionId", PermissionsEnum.EditTemplates.ToString("d"), PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("AddTemplateSynonym", policy =>
                policy.RequireClaim("PermissionId", PermissionsEnum.AddTemplateSynonyms.ToString("d"), PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("EditTemplateSynonym", policy =>
                policy.RequireClaim("PermissionId", PermissionsEnum.EditTemplateSynonyms.ToString("d"), PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("EditSchema", policy =>
                policy.RequireClaim("PermissionId", PermissionsEnum.EditSchema.ToString("d"), PermissionsEnum.Global.ToString("d")));

                options.AddPolicy("ImportRecipient", policy =>
               policy.RequireClaim("PermissionId", PermissionsEnum.ImportRecipients.ToString("d"), PermissionsEnum.Global.ToString("d")));

            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:62594",
                                        "https://localhost:44319")
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod();
                });
            });

            services.AddTransient<IRepository<CampaignJobHistory>, CampaignJobHistoryRepository>();
            
            services.AddTransient<IRepository<Campaign>, CampaignRepository>();
            services.AddTransient<IRepository<Client>, ClientRepository>();
            services.AddTransient<IRepository<EmailStatus>, EmailStatusRepository>();
            services.AddTransient<IRepository<List>, ListRepository>();
            services.AddTransient<IRepository<PasswordReset>, PasswordResetRepository>();
            services.AddTransient<IRepository<Recipient>, RecipientRepository>();
            services.AddTransient<IRepository<RecipientSchema>, RecipientSchemaRepository>();
            services.AddTransient<IRepository<SubscriptionLevel>, SubscriptionLevelRepository>();
            services.AddTransient<IRepository<Template>, TemplateRepository>();
            services.AddTransient<IRepository<TemplateHistory>, TemplateHistoryRepository>();
            services.AddTransient<IRepository<TemplateSynonym>, TemplateSynonymRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<UserInvite>, UserInviteRepository>();
            services.AddTransient<IRepository<ListRecipient>, ListRecipientRepository>();
            services.AddTransient<IRepository<UserPermission>, UserPermissionRepository>();
            services.AddSingleton<EmailService>();
            services.AddSingleton<PasswordResetService>();
            services.AddHostedService(provider => provider.GetService<PasswordResetService>());
            services.AddSingleton<UserInviteService>();
            services.AddHostedService(provider => provider.GetService<UserInviteService>());
            services.AddSingleton<CampaignService>();
            services.AddHostedService(provider => provider.GetService<CampaignService>());
         


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarketingTool");

            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

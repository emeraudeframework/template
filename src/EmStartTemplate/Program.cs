using System.Threading.Tasks;
using Emeraude;
using Emeraude.Configuration.Options;
using Emeraude.Defaults.Options;
using Emeraude.Defaults.Persistence;
using Emeraude.Extensions;
using EmStartTemplate.Admin.Adapters;
using EmStartTemplate.Admin.Mapping;
using EmStartTemplate.Application.Persistence;
using EmStartTemplate.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EmStartTemplate;

public class Program
{
    public static async Task Main(string[] args)
    {
        await EmeraudeApplication.RunAsync(
            args,
            builder =>
            {
                // Here we configure our ASP.NET web application builder

                builder
                    .ConfigureEmeraude(setup =>
                    {
                        setup.MainOptions.ApplicationAssembly = "EmStartTemplate.Application";
                        setup.MainOptions.InfrastructureAssembly = "EmStartTemplate.Infrastructure";
                        setup.MainOptions.DomainAssembly = "EmStartTemplate.Domain";
                        setup.MainOptions.AdminAssembly = "EmStartTemplate.Admin";
                        setup.MainOptions.ProjectName = "EmStartTemplate";
                        setup.MainOptions.BaseUri = "http://localhost:5052";
                        setup.MainOptions.ExecuteMigrations = true;
                        setup.MainOptions.IncludeEmeraudeDefaultsAssembly();

                        setup.ApplicationsOptions.AddMappingProfile<EmStartTemplateAdminAssemblyMappingProfile>();
                        
                        setup.IdentityOptions.AccessTokenOptions.Key = builder.Configuration["AccessTokenOptions:Key"];
                        setup.IdentityOptions.AccessTokenOptions.Issuer = builder.Configuration["AccessTokenOptions:Issuer"];

                        setup.PersistenceOptions.SetContext<IEntityContext, EntityContext>();
                        setup.PersistenceOptions.ContextProvider = DatabaseContextProvider.PostgreSql;
                        setup.PersistenceOptions.ConnectionString = builder.Configuration.GetConnectionString("EntityContext");
                        setup.PersistenceOptions.AddDatabaseInitializer<ApplicationDatabaseInitializer>();
                        
                        setup.AdminOptions.SetAdminMenusBuilder<AdminMenusBuilder>();

                        setup.PortalGatewayOptions.GatewayId = builder.Configuration["PortalGateway:GatewayId"];
                    })
                    .EmeraudePostConfigure(settingsBuilder =>
                    {
                        // Here comes the post-configuration of the framework
                    });
            },
            app =>
            {
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/error/400");

                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseStatusCodePagesWithReExecute("/error/{0}");

                app.UseHttpsRedirection();

                app.UseRouting();
                
                app.UseCors();

                app.UseAuthentication();

                app.UseAuthorization();
            });
    }
}
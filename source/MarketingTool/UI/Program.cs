using DataTransfer.Interfaces;
using DataTransfer.Services;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IDataService<SubscriptionLevelViewModel>, SubscriptionLevelService>()
                .AddScoped<IDataService<EmailAddressViewModel>, ForgottenPasswordService>()
                .AddScoped<IDataService<ResetPasswordViewModel>, ResetPasswordService>()
                .AddScoped<IDataService<RecipientViewModel>, RecipientService>()
                .AddScoped<IDataService<RecipientSchemaViewModel>, RecipientSchemaService>()
                .AddScoped<IDataService<DashboardViewModel>, DashboardService>()
                .AddScoped<IDataService<TemplateViewModel>, TemplateService>()
                .AddScoped<IDataService<ListViewModel>, ListService>()
                .AddScoped<IDataService<CampaignViewModel>, CampaignService>()
                .AddScoped<IDataService<TemplateHistoryViewModel>, TemplateHistoryService>()
                .AddScoped<IDataService<TemplateSynonymViewModel>, TemplateSynonymService>()
                .AddScoped<IDataService<UserInviteViewModel>, UserInviteService>()
                .AddScoped<IDataService<UserViewModel>, UserService>()
                .AddScoped<IDataService<NewUserViewModel>, NewUserService>()
                .AddScoped<ILocalStorageService, LocalStorageService>();

            builder.Services.AddScoped(x => {
                var apiUrl = new Uri(builder.Configuration["ApiUrl"]);
                return new HttpClient() { BaseAddress = apiUrl };
            });

            var host = builder.Build();

            var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            await authenticationService.Initialize();

            await host.RunAsync();
        }
    }

}

using HttpClient_Esercitazione.Services;
using HttpClient_Esercitazione.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using System.Windows;

namespace HttpClient_Esercitazione
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();

            ServiceProvider = host.Services;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            //Services
            services.AddHttpClient("reqres", client =>
            {
                client.BaseAddress = new Uri("https://reqres.in/api/"); 
            })
            .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            {
                // Logica di ri-tentativi automatici dopo x secondi a seguito di errori di tipo
                // HTTP 5XX status codes (server errors)
                // HTTP 408 status code (timeout della richiesta)
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
            }));

            services.AddSingleton<IReqresService, ReqresService>();

            //Views
            services.AddSingleton<MainWindow>();

            //Viewmodels
            services.AddSingleton<MainViewModel>();
        }
    }
}

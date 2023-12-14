using Microsoft.Extensions.DependencyInjection;

namespace HttpClient_Esercitazione.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>(); 
    }
}

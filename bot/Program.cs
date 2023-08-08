using System;
using System.Text;
using System.Threading.Tasks;
using bot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

namespace bot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;


            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) 
                .UseConsoleLifetime() 
                .Build(); 

            Console.WriteLine("Сервис запущен");

            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("6200167109:AAExqmCiop9WY8KLuSgO7WyZIZizG39yVzc"));

            services.AddHostedService<Bot>();
        }
    }
}
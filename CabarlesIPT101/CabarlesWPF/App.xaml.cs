using CabarlesWPF.HostBuilders;
using CabarlesWPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Windows;

namespace CabarlesWPF
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .AddDbContext()
                .AddServices()
                .AddViewModels()
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                await _host.StartAsync();

                // Apply migrations to ensure database is up to date
                var dbContextFactory = _host.Services.GetRequiredService<Cabarles_IPT.Framework.DbContextFactory.PosDbContextFactory>();
                using (var context = dbContextFactory.CreateDbContext())
                {
                    // Drop and recreate database with migrations (one-time fix)
                    // Comment out these lines after first successful run
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();
                    
                    // For normal use, just use this:
                    // context.Database.Migrate();
                }

                var mainViewModel = _host.Services.GetRequiredService<MainViewModel>();
                
                MainWindow = new MainWindow
                {
                    DataContext = mainViewModel
                };
                MainWindow.Show();

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start application:\n\n{ex.Message}\n\nInner Exception:\n{ex.InnerException?.Message}", 
                    "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}

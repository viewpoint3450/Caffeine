using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Caffeine.Core;
using Caffeine.ViewModels;
using System;
using System.Threading;

namespace Caffeine
{
    public partial class App : Application
    {
        private readonly Mutex _mutex;

        public App()
        {
            DataContext = new AppViewModel();
            _mutex = new Mutex(true, "6d5ea5a4-726d-47e8-855a-572ce6a2434c", out bool singleton);
            if (!singleton)
                throw new ApplicationException("The application is already running!");
            CaffeineController.KeepSystemAwake();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow(DataContext as AppViewModel);
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
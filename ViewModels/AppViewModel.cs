using Avalonia.Controls;
using Caffeine.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Caffeine.ViewModels
{
    public sealed class AppViewModel : INotifyPropertyChanged
    {
        private bool _trayVisible = false;
        public bool TrayVisible
        {
            get => _trayVisible;
            set
            {
                if (_trayVisible != value)
                {
                    _trayVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand TrayExitCommand { get; }
        public ICommand TrayClickCommand { get; }

        public AppViewModel()
        {
            TrayExitCommand = new ExitCommand();
            TrayClickCommand = new TrayClickCommand(this);
        }

        internal void RegisterMainWindow(MainWindow mainWindow)
        {
            mainWindow.PropertyChanged += MainWindow_PropertyChanged;
        }

        private void MainWindow_PropertyChanged(object sender, Avalonia.AvaloniaPropertyChangedEventArgs e)
        {
            if (sender is MainWindow mainWindow && e.NewValue is WindowState windowState && windowState == WindowState.Minimized)
            {
                mainWindow.Hide();
                this.TrayVisible = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
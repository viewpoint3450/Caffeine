using Caffeine.ViewModels;
using System;
using System.Windows.Input;

namespace Caffeine.Commands
{
    public sealed class TrayClickCommand : ICommand
    {
        private readonly AppViewModel _appVm;
        private DateTimeOffset _ts = DateTimeOffset.MinValue;

        public TrayClickCommand(AppViewModel appVm)
        {
            _appVm = appVm;
        }

        public bool CanExecute(object parameter)
        {
            if (MainWindow.Instance is null)
                return false;
            var now = DateTimeOffset.Now;
            var delay = now - _ts;
            _ts = now;
            return delay < TimeSpan.FromMilliseconds(300); // Double Click
        }

        public void Execute(object parameter)
        {
            if (MainWindow.Instance is MainWindow window)
            {
                window.Show();
                window.WindowState = Avalonia.Controls.WindowState.Normal;
                _appVm.TrayVisible = false;
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}

using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Caffeine.Commands
{
    public sealed class AboutInfoCommand : ICommand
    {
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            Process.Start(new ProcessStartInfo("https://github.com/tolzyfrolz/Caffeine")
            {
                UseShellExecute = true
            });
        }

        public event EventHandler CanExecuteChanged;
    }
}

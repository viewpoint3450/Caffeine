using Caffeine.Commands;
using Caffeine.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Caffeine.ViewModels
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        private bool _keepDisplayAwake;
        public bool KeepDisplayAwake
        {
            get => _keepDisplayAwake;
            set
            {
                if (_keepDisplayAwake != value)
                {
                    _keepDisplayAwake = value;
                    OnPropertyChanged();
                    OnKeepDisplayAwakeChanged();
                }
            }
        }
        public ICommand AboutInfoClicked { get; }

        public MainWindowViewModel()
        {
            AboutInfoClicked = new AboutInfoCommand();
        }

        private void OnKeepDisplayAwakeChanged()
        {
            CaffeineController.KeepSystemAwake(keepDisplayAwake: _keepDisplayAwake);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using Avalonia.Controls;
using Caffeine.ViewModels;

namespace Caffeine
{
    public partial class MainWindow : Window
    {
        internal static MainWindow Instance { get; private set; }

        public MainWindow(AppViewModel appVm)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            appVm.RegisterMainWindow(this);
            Instance = this;
        }
    }
}
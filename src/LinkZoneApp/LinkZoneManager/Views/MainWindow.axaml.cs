using Avalonia;
using Avalonia.Controls;
using FluentAvalonia.UI.Controls;

namespace LinkZoneManager.Views
{
    public partial class MainWindow : CoreWindow
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
    }
}
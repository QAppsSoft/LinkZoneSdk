using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LinkZoneManager.Controls
{
    public partial class SignalStrengthIconControl : UserControl
    {
        public SignalStrengthIconControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

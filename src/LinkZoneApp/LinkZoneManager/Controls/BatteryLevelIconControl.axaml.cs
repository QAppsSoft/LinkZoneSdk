using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LinkZoneManager.Controls
{
    public partial class BatteryLevelIconControl : UserControl
    {
        public BatteryLevelIconControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LinkZoneManager.Pages
{
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

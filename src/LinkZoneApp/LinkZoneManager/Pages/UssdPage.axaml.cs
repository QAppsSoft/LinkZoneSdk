using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LinkZoneManager.Pages
{
    public partial class UssdPage : UserControl
    {
        public UssdPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

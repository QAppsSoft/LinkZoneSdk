using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using FluentAvalonia.Styling;
using FluentAvalonia.UI.Controls;

namespace LinkZoneManager.Views
{
    public partial class MainWindow : CoreWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            FixWhiteOutline();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);
            SetTitleBar(this);
        }

        private void SetTitleBar(CoreWindow cw)
        {
            // Grab the ICoreApplicationViewTitleBar attached to the CoreWindow object
            // On Windows, this will never be null. On Mac/Linux, it will be - make sure
            // to null check
            var titleBar = cw.TitleBar;
            if (titleBar != null)
            {
                // Tell CoreWindow we want to remove the default TitleBar and set our own
                titleBar.ExtendViewIntoTitleBar = true;

                // Retrieve reference to the Xaml element we're using a TitleBar
                if (this.FindControl<Grid>("TitleBarHost") is Grid g)
                {
                    // Call SetTitleBar to tell CoreWindow the element we want to use as the TitleBar
                    cw.SetTitleBar(g);
                    // Set the margin of the Custom TitleBar so it doesn't overlap with the CaptionButtons
                    g.Margin = new Thickness(0, 0, titleBar.SystemOverlayRightInset, 0);

                    // You can optionally subscribe to LayoutMetricsChanged to be notified of when TitleBar bounds change
                    // Right now, it doesn't do much. It will be more important when RTL layouts are supported as that will
                    // notify you of a change in the SystemOverlay[Left/Right]Inset properties and require adjusting
                    // that margin
                }
            }
        }
        
        private void FixWhiteOutline()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var thm = AvaloniaLocator.Current.GetService<FluentAvaloniaTheme>();
                thm?.ForceWin32WindowToTheme(this);
            }
        }
    }
}
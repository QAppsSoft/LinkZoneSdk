using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using LinkZoneManager.ViewModels;
using ReactiveUI;

namespace LinkZoneManager.Pages
{
    public partial class HomePage : ReactiveUserControl<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();

            this.WhenActivated(disposables => { });
        }

        private void InitializeComponent()
        {
           
            AvaloniaXamlLoader.Load(this);
        }
    }
}

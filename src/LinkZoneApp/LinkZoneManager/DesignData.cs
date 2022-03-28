using LinkZoneManager.ViewModels;
using LinkZoneManager.ViewModels.Interfaces;

namespace LinkZoneManager;

internal class DesignData
{
    public static MainWindowViewModel MainWindowViewModel => new MainWindowViewModel(new IPageViewModel[]{new HomeViewModel()});

}
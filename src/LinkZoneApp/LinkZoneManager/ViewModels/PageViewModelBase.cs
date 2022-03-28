using LinkZoneManager.ViewModels.Interfaces;

namespace LinkZoneManager.ViewModels
{
    public abstract class PageViewModelBase : ViewModelBase, IPageViewModel
    {
        public abstract int Order { get; }
        public abstract string Name { get; }
        public abstract string Icon { get; }
    }
}
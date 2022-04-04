using LinkZoneManager.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI.Fody.Helpers;

namespace LinkZoneManager.ViewModels
{
    public sealed class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IEnumerable<IPageViewModel> pages)
        {
            Pages = pages.OrderBy(page => page.Order).ToList();

            CurrentPage = Pages.First();
        }

        public List<IPageViewModel> Pages { get; }
        
        [Reactive]
        public IPageViewModel CurrentPage { get; set; }
    }
}
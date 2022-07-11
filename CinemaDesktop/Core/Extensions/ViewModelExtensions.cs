using CinemaDesktop.MVVM.ViewModel;
using DryIoc;

namespace CinemaDesktop.Core.Extensions;

public static class ViewModelExtensions
{
    public static void  ShowNewView(object newViewModel)
    {
        var mainViewModel = App.Container.Resolve<MainViewModel>();
        mainViewModel.CurrentView = newViewModel;
        if (!Equals(newViewModel, mainViewModel.VM.Peek()))
        {
            mainViewModel.VM.Push(mainViewModel.CurrentView);
        }
    }
}
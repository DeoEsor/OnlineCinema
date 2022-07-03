using CinemaDesktop.Core;
using CinemaDesktop.MVVM.Commands;

namespace CinemaDesktop.MVVM.ViewModel;

public class MainViewModel: ObservableObject
{
    public RelayCommand ContentViewCommand { get; set; }
    
    public RelayCommand UserViewCommand { get; set; }

    public ContentViewModel ContentVM;
    
    public UserViewModel UserVM;

    private object _currentView;

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public MainViewModel()
    {
        ContentVM = new ContentViewModel();
        UserVM = new UserViewModel();
        
        CurrentView = ContentVM;

        ContentViewCommand = new RelayCommand(o =>
        {
            CurrentView = ContentVM;
        });
        
        UserViewCommand = new RelayCommand(o =>
        {
            CurrentView = UserVM;
        });
    }
}
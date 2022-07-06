using System.Collections.Generic;
using static CinemaDesktop.Core.Extensions.ViewModelExtensions;
using CinemaDesktop.Core;
using CinemaDesktop.MVVM.Commands;
using DryIoc;

namespace CinemaDesktop.MVVM.ViewModel;

public class MainViewModel: ObservableObject
{
    public RelayCommand ContentViewCommand { get; set; }
    
    public RelayCommand BackViewCommand { get; set; }
    
    public RelayCommand UserViewCommand { get; set; }

    public ContentViewModel ContentVM;
    
    public Stack<object> VM = new Stack<object>();
    
    public UserViewModel UserVM{ get; set; }

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
        
        UserVM = App.Container.Resolve<UserViewModel>();
        
        CurrentView = ContentVM;
        VM.Push(CurrentView);

        ContentViewCommand = new RelayCommand(o =>
        {
            CurrentView = ContentVM;
            VM.Push(CurrentView);
        });
        
        UserViewCommand = new RelayCommand(o =>
        {
            ShowNewView(UserVM);
        });

        BackViewCommand = new RelayCommand(o =>
        {
            if (VM.Count > 1)
            {
                VM.Pop();
                CurrentView = VM.Peek();
            }
        });
    }
}
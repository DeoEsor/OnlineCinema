using System.Windows.Controls;
using System.Windows.Input;
using CinemaDesktop.MVVM.Commands;
using CinemaDesktop.MVVM.Model;
using CinemaDesktop.MVVM.ViewModel;
using DryIoc;

namespace CinemaDesktop.UserControls;

public partial class ContentCardControl : UserControl
{
    public ICommand OnClick { get; set; }
    
    public ContentCardControl()
    {
        Model = new ContentModel
        {
            ContentName = "Гарри Поттер и филосовский камень",
            ImageSource =
                "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQXKskA949YDC85-yvsGsHYgkXOh_og1Lt12vyGa5KthpCXnvti",
            Cast = "кеква9o86jhg653f42d tgy56cgh 7ythbv5thj677k7j676"
        };
        InitializeComponent();
    }

    public ContentCardControl(ContentModel model)
    {
        Model = model;
        OnClick = new RelayCommand(o =>
        {
            var mainViewModel = App.Container.Resolve<MainViewModel>();
            mainViewModel.CurrentView = new ContentCardViewModel(model);
            mainViewModel.VM.Push(mainViewModel.CurrentView);
        });
        
        InitializeComponent();
    }
    public ContentModel Model { get; set; }
}
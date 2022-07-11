using System.Windows.Controls;
using System.Windows.Input;
using CinemaDesktop.MVVM.Commands;
using CinemaDesktop.MVVM.Model;
using CinemaDesktop.MVVM.ViewModel;
using static CinemaDesktop.Core.Extensions.ViewModelExtensions;


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
        OnClick = new RelayCommand(o =>
        {
            ShowNewView(new ContentCardViewModel(Model));
        });
        InitializeComponent();
    }

    public ContentCardControl(ContentModel model)
    {
        Model = model;
        OnClick = new RelayCommand(o =>
        {
            ShowNewView(new ContentCardViewModel(Model));
        });
        
        InitializeComponent();
    }
    public ContentModel Model { get; set; }
}
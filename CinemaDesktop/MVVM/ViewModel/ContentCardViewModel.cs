using System.Windows.Input;
using CinemaDesktop.MVVM.Commands;
using CinemaDesktop.MVVM.Model;
using static CinemaDesktop.Core.Extensions.ViewModelExtensions;

namespace CinemaDesktop.MVVM.ViewModel;

public class ContentCardViewModel
{
    public ContentModel Model { get; set; }
    public ICommand OnClick { get; set; }

    public ContentCardViewModel(ContentModel model)
    {
        Model = model;
        OnClick = new RelayCommand(o =>
        {
            ShowNewView(new VideoPlayerViewModel(Model));
        });
    }

    public ContentCardViewModel()
    {
        Model = new ContentModel
        {
            ContentName = "Гарри Поттер и филосовский камень",
            ImageSource =
                "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQXKskA949YDC85-yvsGsHYgkXOh_og1Lt12vyGa5KthpCXnvti",
            Cast = "Richard Harris, Maggie Smith, Robbie Coltrane, Daniel Radcliffe, Rupert Grint, Emma Watson",
            IMDb = "8.8",
            RottenTomatoes = "87",
            Data = "2001",
            Runtime = "2h 32m",
            Directors = "Chris Columbus",
            Writers = "J.K. Rowling, Steve Kloves",
            Description = "This is the tale of Harry Potter (Daniel Radcliffe), an ordinary eleven-year-old boy serving as a sort of slave for his aunt and uncle who learns that he is actually a wizard and has been invited to attend the Hogwarts School for Witchcraft and Wizardry. Harry is snatched away from his mundane existence by Rubeus Hagrid (Robbie Coltrane), the groundskeeper for Hogwarts, and quickly thrown into a world completely foreign to both him and the viewer. Famous for an incident that happened at his birth, Harry makes friends easily at his new school. He soon finds, however, that the wizarding world is far more dangerous for him than he would have imagined, and he quickly learns that not all wizards are ones to be trusted.—Carly"
        };
        OnClick = new RelayCommand(o =>
        {
            ShowNewView(new VideoPlayerViewModel(Model));
        });
    }
}
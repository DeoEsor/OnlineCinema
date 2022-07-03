using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CinemaDesktop.MVVM.Model;

namespace CinemaDesktop.MVVM.ViewModel;

public class ContentViewModel
{
    public ObservableCollection<ContentModel> Content { get; set; } = new ObservableCollection<ContentModel>();

    public ContentViewModel()
    {
        for (var i = 0; i < 16; i++)
        {
            Content.Add(new ContentModel
            {
                ContentName = "Гарри Поттер и филосовский камень",
                ImageSource = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQXKskA949YDC85-yvsGsHYgkXOh_og1Lt12vyGa5KthpCXnvti",
                ContentGenre = "кеква9o86jhg653f42d tgy56cgh 7ythbv5thj677k7j676"
            });
        }
    }

    public ContentViewModel(IEnumerable<ContentModel> contentModels)
    {
        Content = new ObservableCollection<ContentModel>(contentModels);
    }
}
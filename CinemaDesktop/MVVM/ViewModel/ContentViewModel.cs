using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CinemaDesktop.MVVM.Model;
using CinemaDesktop.UserControls;

namespace CinemaDesktop.MVVM.ViewModel;

public class ContentViewModel
{
    public ObservableCollection<ContentCardControl> Content { get; set; } = new ObservableCollection<ContentCardControl>();

    public ContentViewModel()
    {
        for (var i = 0; i < 5; i++)
        {
            Content.Add(
                new ContentCardControl(
                    new ContentModel
                    {
                        ContentName = "Доктор Стрэндж: В мультивселенной безумия",
                        ImageSource = "https://upload.wikimedia.org/wikipedia/ru/7/77/%D0%94%D0%BE%D0%BA%D1%82%D0%BE%D1%80_%D0%A1%D1%82%D1%80%D1%8D%D0%BD%D0%B4%D0%B6_%D0%92_%D0%BC%D1%83%D0%BB%D1%8C%D1%82%D0%B8%D0%B2%D1%81%D0%B5%D0%BB%D0%B5%D0%BD%D0%BD%D0%BE%D0%B9_%D0%B1%D0%B5%D0%B7%D1%83%D0%BC%D0%B8%D1%8F_%D1%82%D0%B8%D0%B7%D0%B5%D1%80_%D0%BF%D0%BE%D1%81%D1%82%D0%B5%D1%80.jpeg",
                        Cast = "Benedict Cumberbatch, Elizabeth Olsen, Chiwetel Ejiofor",
                        IMDb = "7.1",
                        RottenTomatoes = "97",
                        Data = "2022",
                        Runtime = "2h 6m",
                        Directors = "Sam Raimi",
                        Writers = "Michael Waldron, Stan Lee, Steve Ditko",
                        Description = "Doctor Strange teams up with a mysterious teenage girl from his dreams who can travel across multiverses, to battle multiple threats, including other-universe versions of himself, which threaten to wipe out millions across the multiverse. They seek help from Wanda the Scarlet Witch, Wong and others."
                    }
                )
            );
        }
        for (var i = 0; i < 5; i++)
        {
            Content.Add(
                new ContentCardControl(
                    new ContentModel
                    {
                        ContentName = "Гарри Поттер и филосовский камень",
                        ImageSource = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQXKskA949YDC85-yvsGsHYgkXOh_og1Lt12vyGa5KthpCXnvti",
                        Cast= "ке ква9o  86jhg  653f42d   tgy56cgh 7ythbv5thj677k7j676          ",
                        IMDb = "7.6",
                        RottenTomatoes = "87",
                        Data = "2001",
                        Runtime = "2h 32m",
                        Directors = "Chris Columbus",
                        Writers = "J.K. Rowling, Steve Kloves",
                        Description = "This is the tale of Harry Potter (Daniel Radcliffe), an ordinary eleven-year-old boy serving as a sort of slave for his aunt and uncle who learns that he is actually a wizard and has been invited to attend the Hogwarts School for Witchcraft and Wizardry. Harry is snatched away from his mundane existence by Rubeus Hagrid (Robbie Coltrane), the groundskeeper for Hogwarts, and quickly thrown into a world completely foreign to both him and the viewer. Famous for an incident that happened at his birth, Harry makes friends easily at his new school. He soon finds, however, that the wizarding world is far more dangerous for him than he would have imagined, and he quickly learns that not all wizards are ones to be trusted.—Carly"

                    }
                )
            );
        }
    }

    public ContentViewModel(IEnumerable<ContentCardControl> contentModels)
    {
        Content = new ObservableCollection<ContentCardControl>(contentModels);
    }
}
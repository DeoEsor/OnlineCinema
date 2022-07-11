using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CinemaDesktop.Annotations;
using CinemaDesktop.MVVM.Commands;
using CinemaDesktop.MVVM.Model;
using MonoTorrent;
using MonoTorrent.Client;

namespace CinemaDesktop.MVVM.ViewModel;

public class VideoPlayerViewModel : INotifyPropertyChanged
{
    private Stream _stream;
    public ContentModel Model { get; set; }
    public ICommand PlayCommand { get; set; }

    public Stream Stream
    {
        get => _stream;
        set
        {
            _stream = value;
            OnPropertyChanged();
        }
    }
    
    private string name;
    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged();
        }
    }

    private double progress;
    public double Progress
    {
        get => progress;
        set
        {
            progress = value;
            OnPropertyChanged();
        }
    }

    private TorrentState state;
    public TorrentState State
    {
        get => state;
        set
        {
            state = value;
            OnPropertyChanged();
        }
    }


    public VideoPlayerViewModel(ContentModel model)
    {
        Model = model;
        PlayCommand = new RelayCommand(Play);
    }

    private void Play(object o)
    {
        Task.Run(() => 
            StartDownload("magnet:?xt=urn:btih:88594aaacbde40ef3e2510c47374ec0aa396c08e&dn=bbb_sunflower_1080p_30fps_normal.mp4&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337&tr=udp%3A%2F%2Fexplodie.org%3A6969&tr=udp%3A%2F%2Ftracker.empire-js.us%3A1337&tr=wss%3A%2F%2Ftracker.btorrent.xyz&tr=wss%3A%2F%2Ftracker.openwebtorrent.com&ws=http%3A%2F%2Fdistribution.bbb3d.renderfarming.net%2Fvideo%2Fmp4%2Fbbb_sunflower_1080p_30fps_normal.mp4"));
    }

    public VideoPlayerViewModel()
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
        PlayCommand = new RelayCommand(Play);
    }
    
    public async Task StartDownload(string magnetLinkURL)
    {
        var engine = new ClientEngine();

        if (!magnetLinkURL.Contains("magnet:?xt=urn:btih:"))
            throw new ArgumentException("Invalid magnet link", nameof(magnetLinkURL));
        
        var magnetLink = new MagnetLink(InfoHash.FromHex(magnetLinkURL.Substring(20, 60)));
        var manager = await engine.AddStreamingAsync(magnetLink, @"C:\Users\Rulia\Downloads\");
        await manager.StartAsync();
        await manager.WaitForMetadataAsync();

        var task = Task.Run(() =>
        {
            Name = manager.Torrent.Name;
            while (engine.IsRunning)
            {
                Progress = manager.Progress;
                State = manager.State;
            }
        });

        var file = manager.Files.FirstOrDefault( s => s.FullPath.EndsWith(".mp4"));
        if (file == null)
            throw new ArgumentException(nameof(magnetLink));
        var stream = await manager.StreamProvider.CreateStreamAsync(file, CancellationToken.None);
        Stream = stream;

        await task;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
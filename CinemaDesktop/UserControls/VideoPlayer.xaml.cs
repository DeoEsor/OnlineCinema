using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Core;

namespace CinemaDesktop.UserControls;

public partial class VideoPlayer : UserControl
{
    public static readonly DependencyProperty StreamProperty =
        DependencyProperty.Register("Stream", typeof(Stream), typeof(VideoPlayer),
            new FrameworkPropertyMetadata(null, OnFirstPropertyChanged));

    public VideoPlayer()
    {
        PauseCommand = new RelayCommand(o => Pause());
        PlayCommand = new RelayCommand(o => Play());
        MuteCommand = new RelayCommand(o => Mute());
        FullscreenCommand = new RelayCommand(o => Pause());
        InitializeComponent();
    }

    public ICommand PauseCommand { get; set; }
    public ICommand PlayCommand { get; set; }
    public ICommand MuteCommand { get; set; }
    public ICommand FullscreenCommand { get; set; }

    public bool IsPaused { get; set; }

    public Stream Stream
    {
        get => (Stream)GetValue(StreamProperty);
        set => SetValue(StreamProperty, value);
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var vlcLibDirectory = new DirectoryInfo(Path.Combine(AppContext.BaseDirectory, "libvlc",
            IntPtr.Size == 4 ? "win-x86" : "win-x64"));
        Media.SourceProvider.CreatePlayer(vlcLibDirectory);
    }

    private static void OnFirstPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is VideoPlayer player)
            player.Play(player.Stream);
    }

    private void Play(Stream stream)
    {
        Media.SourceProvider.MediaPlayer.Play(Stream);
        IsPaused = false;
    }

    private void Play()
    {
        Media.SourceProvider.MediaPlayer.Play();
        IsPaused = false;
    }

    private void Pause()
    {
        Media.SourceProvider.MediaPlayer.Pause();
        IsPaused = true;
    }

    private void Mute()
    {
    }

    private void FullScreen()
    {
        Media.SourceProvider.MediaPlayer.Video.FullScreen = !Media.SourceProvider.MediaPlayer.Video.FullScreen;
    }

    /// <summary>
    ///     Volume Slider Value Changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void Slider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (Media.SourceProvider.MediaPlayer != null)
            Media.SourceProvider.MediaPlayer.Audio.Volume = (int)e.NewValue;
    }

    private void MediaControl_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (IsPaused == false)
            Pause();
        else
            Play();
    }

    private void MediaControl_OnMouseMove(object sender, MouseEventArgs e)
    {
    }

    private void PlayerSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (Media.SourceProvider.MediaPlayer != null)
            Media.SourceProvider.MediaPlayer.Position = (float)e.NewValue;
    }
}
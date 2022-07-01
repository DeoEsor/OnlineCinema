using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;

namespace CinemaDesktop.UserControls;

public partial class VideoPlayer : UserControl
{
    private readonly string _fileAudioPattern = "Audio(mp3,wma,wav)|*.mp3;*.MP3;*.wma;*.wav";

    private readonly string _fileMultiMediaPattern = "Multimedia(mp3,mp4,mpg)|*.mp3;*.MP3;*.mpg;*.MPG;*.wma;*.wav;*mp4;*.MP4;*avi;*.wmv;*.DAT";

    private readonly string _fileVideoPattern = "Video(MP4,MPG,AVI,WMV,DAT)|*mp4;*,*.mpg,*.MPG,*.MP4;*avi;*.wmv;*.DAT";

    private readonly DispatcherTimer _timer;

    private readonly DispatcherTimer _visibleTimer = new();

    private int _contador;

    private string _defaultFileName = "";

    private bool _fullscreen;
    private bool _intro;

    private bool _mediaPausa;
    private bool _multiselect;
    private bool _mute;
    private string _tipoMedia = "";

    public VideoPlayer()
    {
        InitializeComponent();
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1) // Tick se dispara cada segundo.
        };
        _timer.Tick += timer_Tick;
        BtPausa.Visibility = Visibility.Collapsed;
        //Deshabilitar Controles al inicio
        BorderMediaControls.IsEnabled = false;

        //TIMER
        _visibleTimer.Tick += VisibleTimerTick;
        _visibleTimer.Interval = new TimeSpan(0, 0, 1);
    }

    public bool Repeat { get; set; } = false;

    private void VisibleTimerTick(object sender, EventArgs e)
    {
        _contador += 1;
        if (_contador == 3)
        {
            BorderMediaControls.Visibility = Visibility.Collapsed;
            _visibleTimer.Stop();
        }
    }

    //Metodos
    /// <summary>
    ///     Metodo que carga Video por defeault al inciar
    /// </summary>
    private void CargarIntro()
    {
        MediaControl.Source = new Uri(@"../../Resources/intro.mp4", UriKind.Relative);
        MediaControl.Play();
    }

    /// <summary>
    ///     Metodo para Abrir Archivos y Cargarlos en la Lista
    /// </summary>
    private void AbrirArchivos()
    {
        var dialogo = new OpenFileDialog();
        dialogo.Filter = _defaultFileName;
        dialogo.Multiselect = _multiselect;
        dialogo.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        dialogo.Title = "Seleccione Uno o Varios Archivos";
        var resultado = dialogo.ShowDialog();
        try
        {
            if (resultado == true)
            {
                _intro = false;
                _multiselect = false;
                Bordelista.Visibility = Visibility.Visible;
                Stop();
                MediaControl.Source = null;
                LbLista.Items.Clear();
                string[] selectedFiles = dialogo.FileNames;
                Cargar(selectedFiles);
            }
            else
            {
                _multiselect = false;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private void Cargar(string[] selecteds)
    {
        foreach (var file in selecteds)
            if (Path.GetExtension(file) == ".mp3" ||
                Path.GetExtension(file) == ".MP3" ||
                Path.GetExtension(file) == ".mpg" ||
                Path.GetExtension(file) == ".MPG" ||
                Path.GetExtension(file) == ".mp4" ||
                Path.GetExtension(file) == ".MP4" ||
                Path.GetExtension(file) == ".wma" ||
                Path.GetExtension(file) == ".wav" ||
                Path.GetExtension(file) == ".wmv" ||
                Path.GetExtension(file) == ".avi" ||
                Path.GetExtension(file) == ".DAT")
            {
                var listaitem = new ListBoxItem();
                listaitem.Content = Path.GetFileNameWithoutExtension(file);
                listaitem.Tag = file;
                LbLista.Items.Add(listaitem);
                BorderMediaControls.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Tipo de Archivo No Soportado", "Informacion");
            }

    }
    //Termina Metodo Abrir_Archivos

    /// <summary>
    ///     Metodo TipoMedia() Determina que tipo de medio (audio o video) Se esta reproduciendo
    /// </summary>
    private void TipoMedia()
    {
        if (_tipoMedia == ".mp3" || _tipoMedia == ".MP3" || _tipoMedia == ".wma" || _tipoMedia == ".wav")
        {
            var margin = Bordelista.Margin;
            margin.Left = 0;
            margin.Top = 22;
            margin.Right = 0;
            Bordelista.Margin = margin;
            Bordelista.Width = 1077;
            Bordelista.Height = 421;
            _visibleTimer.Stop();
            _contador = 0;
            BorderMediaControls.Visibility = Visibility.Visible;
        }
        else
        {
            Bordelista.Margin = new Thickness(950, 22, 0, 0);
            Bordelista.Width = 127;
            Bordelista.Height = 486;
        }
    }

    //Termina Metodo TIPO DE MEDIA

    /// <summary>
    ///     Metodo PLAY O PAUSA .
    /// </summary>
    private void PlayoPausa()
    {
        //si la lista contiene elementos
        if (LbLista.HasItems)
        {
            if (BtPlay.Visibility == Visibility.Visible)
                Play();

            else if (BtPausa.Visibility == Visibility.Visible) Pausa();
        }
    }
    //Termina metodo Play o Pausa

    /// <summary>
    ///     Metodo PLAY
    /// </summary>
    private void Play()
    {
        if (LbLista.HasItems)
        {

            TipoMedia();
            MediaControl.Play();
            _mediaPausa = false;
            BtPlay.Visibility = Visibility.Collapsed;
            BtPausa.Visibility = Visibility.Visible;
        }
    }

    private void Pausa()
    {
        BtPausa.Visibility = Visibility.Collapsed;
        BtPlay.Visibility = Visibility.Visible;
        MediaControl.Pause();
        _mediaPausa = true;
    }
    //Fin metodo Play

    /// <summary>
    ///     Metodo que realiza el Cambio a la Pista Siguiente Verificando la Lista si hay una Sola Repite la Misma
    ///     Y llama TipoMedia() Para ver si es video o audio que esta en la siguiente Pista
    /// </summary>
    private void Siguiente()
    {
        if (LbLista.HasItems)
        {
            if (Repeat)
            {
                LbLista.SelectedIndex = LbLista.SelectedIndex;
                _mediaPausa = false;
                Play();
            }
            else
            {
            }
        }
        else
        {
            Stop();
        }
    }

    /// <summary>
    ///     Funcion que realiza el cambio a la Pista Anterior Igual que la Funcion Siguiente()
    /// </summary>
    private void Anterior()
    { 
    }

    /// <summary>
    ///     Funcion STOP Detiene la reproduccion siempre y cuando la lista no este vacia
    /// </summary>
    private void Stop()
    {
        if (LbLista.HasItems)
        {
            BtPlay.Visibility = Visibility.Visible;
            BtPausa.Visibility = Visibility.Collapsed;
            MediaControl.Stop();
            _mediaPausa = false;
            MediaControl.Source = null;
        }
    }

    //Termina Funcion STOP

    /// <summary>
    ///     Funcion que habilita la Pantalla completa Siempre y cuando que se haya cargado un Video
    /// </summary>
    private void Fullpantalla()
    {
        if (LbLista.HasItems)
        {
            if (_tipoMedia != ".mp3" && _tipoMedia != ".MP3" && _tipoMedia != ".wma" && _tipoMedia != ".wav")
            {
                if (!_fullscreen)
                {
                    RootGrid.Children.Remove(BorderMediaControls);
                    GridMedia.Children.Remove(MediaControl);
                    RootGrid.Children.Remove(GridMedia);
                    Content = MediaControl;
                }

                else
                {
                    Content = RootGrid;
                    GridMedia.Children.Add(MediaControl);
                    RootGrid.Children.Add(GridMedia);
                    RootGrid.Children.Add(BorderMediaControls);
                }

                _fullscreen = !_fullscreen;
            }
            else
            {
                MessageBox.Show("Modo De Reproducción De Audio", "Media Player", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
        else
        {
            MessageBox.Show("Lista Vacía", "Media Player", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    /// <summary>
    ///     Funcion Mute Silencia el Audio y Carga la imagen Mute al boton Volumen y Viceversa
    /// </summary>
    private void Mute()
    {
        if (!_mute)
        {
            BtVol.Source = new BitmapImage(new Uri(@"../../Resources/btn_mute_P.png", UriKind.Relative));
            MediaControl.IsMuted = true;
        }
        else
        {
            MediaControl.IsMuted = false;
            BtVol.Source = new BitmapImage(new Uri(@"../../Resources/btn_volume_P.png", UriKind.Relative));
        }

        _mute = !_mute;
    }


    private void Repetir()
    {
        Repeat = !Repeat;

        if (Repeat)
        {
            BtRepeat.Visibility = Visibility.Collapsed;
            BtRepeatActivo.Visibility = Visibility.Visible;
        }
        else
        {
            BtRepeatActivo.Visibility = Visibility.Collapsed;
            BtRepeat.Visibility = Visibility.Visible;
        }
    }


    private void mediacontrol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Fullpantalla();
    }


    private void timer_Tick(object sender, EventArgs e)
    {
        Slider.Value = MediaControl.Position.TotalSeconds;
        Lbtime.Content = MediaControl.Position.Minutes + ":" + MediaControl.Position.Seconds;
    }

    private void mediacontrol_MediaOpened(object sender, RoutedEventArgs e)
    {
        var ts = MediaControl.NaturalDuration.TimeSpan;
        Slider.Maximum = ts.TotalSeconds;
        _timer.Start();
    }

    private void slideravance_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        MediaControl.Position =
            TimeSpan.FromSeconds(Slider.Value); //posicion de media se actualiza al valor del slider
    }


    private void lbLista_MouseDoubleClick_1(object sender, MouseEventArgs e)
    {
        if (LbLista.SelectedItem != null)
            if (LbLista.SelectedItem.ToString() != null)
            {
                var index = LbLista.SelectedIndex;
                LbLista.SelectedIndex = index;
                _mediaPausa = false;
                Play();
                // PistaActual = (ListBox)lbLista.SelectedItem;
            }
    }

    private void mediacontrol_MediaEnded(object sender, RoutedEventArgs e)
    {
        if (_intro)
        {
            MediaControl.Source = null;
            _intro = !_intro;
        }

        else
        {
            Siguiente();
        }
    }


    private void RootWindow_Loaded(object sender, RoutedEventArgs e)
    {
        CargarIntro();
        _intro = !_intro;
    }

    private void imarepeat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Repetir();
    }

    private void btStop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Stop();
    }

    private void btBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Anterior();
    }

    private void btPausa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Pausa();
    }

    private void btPlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Play();
    }

    private void btNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Siguiente();
    }

    private void btVol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Mute();
    }

    private void btScreen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Fullpantalla();
    }

    private void MediaControl_MouseMove(object sender, MouseEventArgs e)
    {
        _visibleTimer.Start();
        _contador = 0;
        BorderMediaControls.Visibility = Visibility.Visible;
    }
}
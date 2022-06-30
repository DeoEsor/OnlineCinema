using System.Windows;
using System.Windows.Input;

namespace CinemaDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
				DragMove();
		}

		private void Close_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void Maximaze_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = 
				Application.Current.MainWindow.WindowState != WindowState.Maximized ?
					WindowState.Maximized : WindowState.Normal;
		}

		private void Minimaze_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}
	}
}

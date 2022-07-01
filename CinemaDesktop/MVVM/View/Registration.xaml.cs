using System.Windows;
using System.Windows.Input;

namespace CinemaDesktop;

public partial class Registration : Window
{
    public Registration()
    {
        InitializeComponent();
    }

    private void Registration_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
}
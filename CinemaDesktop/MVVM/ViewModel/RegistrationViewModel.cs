using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DryIoc;

namespace CinemaDesktop.MVVM.ViewModel;

public sealed class RegistrationViewModel : INotifyPropertyChanged
{
    public Window Owner { get; set; }
    //public CryptoClient CryptoClient { get; set; }
    public ICommand LoginCommand { get; set; }
    public ICommand RegisterCommand { get; set; }
    public string UserName { get; set; } = "Username";
    public RegistrationViewModel(Window owner)
    {
        Owner = owner;
        //CryptoClient = App.Container.Resolve<CryptoClient>();
    }

    private async void Register(object obj)
    {
        if (obj is not PasswordBox passwordBox) return;
        var a =App.Container.Resolve<MainWindow>();
        /*var reply = await CryptoClient.Register(UserName, passwordBox.Password);
        
        if (reply.StatusCode != StatusCode.Accepted)
        {
            bool? messageBox;
            switch (reply.StatusCode)
            {
                case StatusCode.NotAccepted:
                    messageBox = new MessageBoxCustom("Something with server, try later", MessageType.Error, MessageButtons.Ok)
                        .ShowDialog();
                    break;
                case StatusCode.WrongLogin:
                    messageBox = new MessageBoxCustom("This username is defined try another", MessageType.Error, MessageButtons.Ok)
                        .ShowDialog();
                    break;
            }
                
            return;
        }

        var chat = App.Container.Resolve<ChatViewModel>();
        chat.User = new ContactModel
        {
            Username = reply.User.Name,
            Password = reply.User.Password,
            ImageSource = reply.User.ImageSource,
            Id = (int)reply.User.Id,
            Color = reply.User.Color,
            Status = "Online"
        };
        a.DataContext = chat;
        a.Show();
        Owner.Close();
        */
    }

    private async Task Login(object obj)
    {
        /*
        if (obj is not PasswordBox passwordBox) return;
        var a =App.Container.Resolve<MainWindow>();
        var reply = await CryptoClient.AuthAsync(UserName, passwordBox.Password);
        
        if (reply.StatusCode != StatusCode.Accepted)
        {
                
        }

        var chat = App.Container.Resolve<ChatViewModel>();
        chat.User = new ContactModel
        {
            Username = reply.User.Name,
            Password = reply.User.Password,
            ImageSource = reply.User.ImageSource,
            Id = (int)reply.User.Id,
            Color = reply.User.Color,
            Status = "Online"
        };
        a.DataContext = chat;
        a.Show();
        Owner.Close();
        */
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
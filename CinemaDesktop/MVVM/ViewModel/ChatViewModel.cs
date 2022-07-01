using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using CinemaDesktop.MVVM.Commands;
using CinemaDesktop.MVVM.Model;
using Microsoft.Win32;

namespace CinemaDesktop.MVVM.ViewModel;

public class ChatViewModel : INotifyPropertyChanged
{
    private ContactModel _selectedContact;
    private ContactModel _user;
    private string _message;
    public ObservableCollection<ContactModel> Contacts { get; set; } = new ObservableCollection<ContactModel>();
    
    public RelayCommand SendCommand { get; set; }
    public RelayCommand OpenFileCommand { get; set; }
    public RelayCommand ChoiceParamsCommand { get; set; }
    //public CryptoClient CryptoClient { get; set; }

    public ContactModel SelectedContact
    {
        get => _selectedContact;
        set
        {
            _selectedContact = value;
            OnPropertyChanged();
        }
    }

    public ContactModel User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }

    public ChatViewModel()
    {
        //CryptoClient = App.Container.Resolve<CryptoClient>();

        Contacts.Add(new ContactModel
        {
            Username = "Yourseld",
            Messages = new ObservableCollection<MessageModel>()
        });
        SelectedContact = Contacts.First();
        SendCommand = new RelayCommand(o =>
        {
            if (Message == string.Empty) return;
            SelectedContact.Messages.Add( new MessageModel
            {
                Owner = User,
                Message = new Message
                {
                    MessageData = Encoding.UTF8.GetBytes(Message),
                    Type = Model.Message.MessageType.String
                },
            });
            Message = string.Empty;
        });

        OpenFileCommand = new RelayCommand(o =>
        { 
            var dlg = new OpenFileDialog();
            dlg.Filter = "Documents (*.*)|*.*";

            var msg = new Message();
            if (dlg.ShowDialog() == true)
                msg = new Message
                {
                    FileName = dlg.FileName.Split('\\').Last(),
                    Type = Model.Message.MessageType.File,
                    MessageData = File.ReadAllBytes(dlg.FileName)
                };
            
            SelectedContact.Messages.Add(
                new MessageModel
                {
                    Owner = User,
                    Message = msg,
                }
                );
        });
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
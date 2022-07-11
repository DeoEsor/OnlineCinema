using System;
using System.Globalization;
using System.Windows.Input;
using CinemaDesktop.Core;
using CinemaDesktop.MVVM.Commands;
using CinemaDesktop.MVVM.Model;

namespace CinemaDesktop.MVVM.ViewModel;

public class UserSettingsViewModel : ObservableObject
{
    private string _imageSource;
    public UserModel Model { get; }
    public ICommand NewProfilePictureCommand { get; set; }
    public ICommand SaveProfileCommand { get; set; }
    public ICommand SetNewPasswordCommand { get; set; }
    public string Nickname { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public string ImageSource 
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged();
        } 
    }

    public UserSettingsViewModel(UserModel model)
    {
        Model = model;
        Nickname = Model.Nickname;
        Nickname = Model.Nickname;
        Country = Model.Country;
        Email = Model.Email;
        Surname = Model.Surname;
        DateOfBirth = Model.DateOfBirth.ToString(CultureInfo.CurrentCulture);
        Name = Model.Name;
        ImageSource = Model.ImageSource;

        NewProfilePictureCommand = new RelayCommand(o =>
        {
            ImageSource =
                "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQXKskA949YDC85-yvsGsHYgkXOh_og1Lt12vyGa5KthpCXnvti";
        });

        SaveProfileCommand = new RelayCommand(o =>
        {
            Model.Nickname = Nickname;
            Model.Country = Country;
            Model.Email = Email;
            Model.Surname = Surname;
            Model.DateOfBirth = DateTime.Parse(DateOfBirth);
            Model.Name = Name;
            Model.ImageSource = ImageSource;
        });

        SetNewPasswordCommand = new RelayCommand(o => { });
    }

    public UserSettingsViewModel()
    {
        Model = new UserModel
        {
            Name = "Скарлетт",
            Surname = "Сорокина",
            Nickname = "Да я кошка",
            DateOfBirth = DateTime.Today,
            Email = "kakoitoemail@gmail.com",
            Country = "Russia",
            Password = "4yf56y56u6j",
            ImageSource =
                "https://sun9-24.userapi.com/impg/69p9XpWkujGND-ODbQAGq94aPcq2EYXn_uiRig/SjWdzeDD5Cg.jpg?size=1344x1792&quality=95&sign=ad9ccf8eb6f4d6eadde27f566aff10f6&type=album"
        };

        Nickname = Model.Nickname;
        Nickname = Model.Nickname;
        Country = Model.Country;
        Email = Model.Email;
        Surname = Model.Surname;
        DateOfBirth = Model.DateOfBirth.ToString(CultureInfo.CurrentCulture);
        Name = Model.Name;

        NewProfilePictureCommand = new RelayCommand(o =>
        {
            Model.ImageSource =
                "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQXKskA949YDC85-yvsGsHYgkXOh_og1Lt12vyGa5KthpCXnvti";
        });
        
        SaveProfileCommand = new RelayCommand(o =>
        {
            Model.Nickname = Nickname;
            Model.Country = Country;
            Model.Email = Email;
            Model.Surname = Surname;
            Model.DateOfBirth = DateTime.Parse(DateOfBirth);
            Model.Name = Name;
        });
    }
}
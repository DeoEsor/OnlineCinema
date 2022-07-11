using System;
using CinemaDesktop.Core;

namespace CinemaDesktop.MVVM.Model;

public class UserModel : ObservableObject
{
    private string _name = "Скарлетт";
    private string _surname = "Сорокина";
    private string _country = "Russia";
    private string _nickName = "Да я кошка";
    private DateTime _dateOfBirth = DateTime.Today;
    private string _email = "kakoitoemail@gmail.com";
    private string _password;

    private string _imageSource =
        "https://sun9-24.userapi.com/impg/69p9XpWkujGND-ODbQAGq94aPcq2EYXn_uiRig/SjWdzeDD5Cg.jpg?size=1344x1792&quality=95&sign=ad9ccf8eb6f4d6eadde27f566aff10f6&type=album";

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public string Surname
    {
        get => _surname;
        set
        {
            _surname = value;
            OnPropertyChanged();
        }
    }

    public string Country
    {
        get => _country;
        set
        {
            _country = value;
            OnPropertyChanged();
        }
    }

    public string Nickname
    {
        get => _nickName;
        set
        {
            _nickName = value;
            OnPropertyChanged();
        }
    }

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            _dateOfBirth = value;
            OnPropertyChanged();
        }
    }

    public string ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }
}
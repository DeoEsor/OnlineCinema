using System;
using CinemaDesktop.Core;

namespace CinemaDesktop.MVVM.Model;

public class UserModel : ObservableObject
{
    private string _name;
    private string _surname;
    private string _country;
    private string _nickName;
    private DateTime _dateOfBirth;
    private string _email;
    private string _password;
    private string _imageSource;

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
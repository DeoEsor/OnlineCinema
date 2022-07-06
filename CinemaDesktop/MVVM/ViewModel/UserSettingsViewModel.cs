using System;
using CinemaDesktop.MVVM.Model;

namespace CinemaDesktop.MVVM.ViewModel;

public class UserSettingsViewModel
{
    public UserModel Model { get; set; }

    public UserSettingsViewModel(UserModel model)
    {
        Model = model;
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
    }
}
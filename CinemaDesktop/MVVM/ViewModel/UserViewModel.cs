using System.Windows.Input;
using CinemaDesktop.MVVM.Commands;
using CinemaDesktop.MVVM.Model;
using DryIoc;

namespace CinemaDesktop.MVVM.ViewModel;

public class UserViewModel
{
    public UserModel Model { get; set; }
    public ICommand OnClick { get; set; }

    public UserViewModel(UserModel model)
    {
        Model = model;
        OnClick = new RelayCommand(o =>
        {
            var mainViewModel = App.Container.Resolve<MainViewModel>();
            mainViewModel.CurrentView = new UserSettingsViewModel(model);
        });
    }
    public UserViewModel()
    {
        Model = new UserModel
        {
            Name = "Скарлетт",
            Surname = "Сорокина",
            Nickname = "Да я кошка",
            DateOfBirth = "123",
            Email = "kakoitoemail@gmail.com",
            Country = "Russia",
            ImageSource =
                "https://sun9-24.userapi.com/impg/69p9XpWkujGND-ODbQAGq94aPcq2EYXn_uiRig/SjWdzeDD5Cg.jpg?size=1344x1792&quality=95&sign=ad9ccf8eb6f4d6eadde27f566aff10f6&type=album"
        };
        OnClick = new RelayCommand(o =>
        {
            var mainViewModel = App.Container.Resolve<MainViewModel>();
            mainViewModel.CurrentView = new UserSettingsViewModel(Model);
            mainViewModel.VM.Push(mainViewModel.CurrentView);

        });
    }
}
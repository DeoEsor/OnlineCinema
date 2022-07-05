using UserService.Messages;

namespace CinemaDesktop.MVVM.Model;

public class UserModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Country { get; set; }
    public string Nickname { get; set; }
    
    public string DateOfBirth { get; set; }
    
    public string ImageSource { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}
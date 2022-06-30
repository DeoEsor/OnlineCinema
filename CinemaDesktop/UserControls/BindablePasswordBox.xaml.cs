using System.Windows.Controls;
using CryptoDesktop.Core.Interfaces;

namespace CryptoDesktop;

public partial class BindablePasswordBox : UserControl, IPasswordSupplier
{
    public BindablePasswordBox()
    {
        InitializeComponent();
    }
    public string GetPassword()
    {
        return pwdBox.Password;
    }
}
using System.Security.Cryptography;
using System.Text;

namespace OnlineCinema.Domain.Extensions;

public static class PasswordExtensions
{
    /// <summary>
    ///     Required that username is unique for all users
    /// </summary>
    /// <param name="password">string representation of pass</param>
    /// <param name="username">username of user</param>
    /// <returns>salted hash by pass</returns>
    public static byte[] ToSaltedHash(this string password, string username)
    {
        var pwdBytes = Encoding.UTF8.GetBytes(password);
        var salt = Encoding.UTF8.GetBytes(username);
        var saltedPassword = new byte[pwdBytes.Length + salt.Length];

        Buffer.BlockCopy(pwdBytes, 0, saltedPassword, 0, pwdBytes.Length);
        Buffer.BlockCopy(salt, 0, saltedPassword, pwdBytes.Length, salt.Length);

        using var sha = SHA256.Create();

        return sha.ComputeHash(saltedPassword);
    }
}
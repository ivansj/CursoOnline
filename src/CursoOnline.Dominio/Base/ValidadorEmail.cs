using System.Text.RegularExpressions;

namespace CursoOnline.Dominio.Base
{
    /// <summary>
    /// Validar email
    /// </summary>
    /// <remarks>https://www.aspsnippets.com/Articles/Email-Validation-using-Regular-Expression-in-C-and-VBNet.aspx</remarks>
    public static class ValidadorEmail
    {
        public static bool Validar(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            var regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return regex.IsMatch(email?.Trim());
        }


    }
}

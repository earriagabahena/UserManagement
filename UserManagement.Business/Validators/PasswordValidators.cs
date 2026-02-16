using System.Text.RegularExpressions;

namespace UserManagement.Business.Validators
{
    public static class PasswordValidator
    {
        public static (bool IsValid, string ErrorMessage) Validate(string password)
        {
            if (string.IsNullOrEmpty(password))
                return (false, "La contraseña es requerida.");

            if (password.Length < 14)
                return (false, "La contraseña debe tener mínimo 14 caracteres.");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return (false, "La contraseña debe contener al menos una mayúscula.");

            if (!Regex.IsMatch(password, @"[a-z]"))
                return (false, "La contraseña debe contener al menos una minúscula.");

            if (!Regex.IsMatch(password, @"[0-9]"))
                return (false, "La contraseña debe contener al menos un número.");

            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
                return (false, "La contraseña debe contener al menos un carácter especial.");

            return (true, string.Empty);
        }
    }
}
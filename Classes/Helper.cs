using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MVC.Classes
{
    public static class Helper
    {

        //Este método es el encargado de cifrar la contraseña
        public static string Encrypt(string password)
        {
            SHA256 sha = new SHA256Managed();
            byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString().Substring(0, 20);
        }

        //Con este método validamos el formato de la contraseña
        public static string ValidatePassword(String password)
        {
            Regex pattern = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,15}$");
            if (!pattern.IsMatch(password))
            {
                throw new ValidatorException("No se cumple con el formato establecido");
            }
            return password.Trim();
        }

    }
}
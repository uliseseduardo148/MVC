using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Classes
{
    public class ValidatorException:Exception
    {
        //Se obtiene una excepción, la cual recibe un mensaje que luego se muestra al usuario
        public ValidatorException(string message) : base(message)
        {

        }
    }
}
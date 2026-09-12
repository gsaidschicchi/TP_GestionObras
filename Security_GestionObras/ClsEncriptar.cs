using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Security_GestionObras
{
    /*
    Password escrita por usuario
                ↓
           ClsEncriptar
                ↓
             Hash SHA
                ↓
      Password transformada 
    */
    public class ClsEncriptar
    {
        public static string EncriptarSHA(string texto) // // static permite usar el método directamente desde la clase sin crear una instancia.
        {
            SHA256 sha = SHA256.Create();

            byte[] bytesTexto = Encoding.UTF8.GetBytes(texto); // convierte el texto a secuencia de bytes

            byte[] bytesHash = sha.ComputeHash(bytesTexto); // calcula el hash SHA-256.

            StringBuilder resultado = new StringBuilder(); // crea una cadena que vamos a ir armando.

            foreach (byte b in bytesHash)
            {
                resultado.Append(b.ToString("x2")); // convierte cada byte a texto hexadecimal.
            }

            return resultado.ToString();
        }
    }
}

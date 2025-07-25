using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fergeda_2023.Clases
{
    class Transformacion
    {
        public string Contraseña(string texto, string key)
        {
            try
            {
                byte[] keyArray;
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
                tdes.Clear();
                texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return texto;
        }
        public string Mostrar(string textoEncriptado, string key)
        {
            try
            {
                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
                tdes.Clear();
                textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return textoEncriptado;
        }

        public string Formato => "F798e$#R12$43G055eD1ñ656Aqwe_%";
        public string Formato2 => "L@$1Pi4as5?¡tWQW415Qop(/o";

    }
}

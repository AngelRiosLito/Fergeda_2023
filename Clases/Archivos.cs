using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fergeda_2023.Clases
{
    class Archivos
    {
        public void abirArchivo(string ruta)
        {
            if (File.Exists(ruta))
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(ruta); Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }

        }

        public void crearDirectorio(string rt)
        {
            if (!Directory.Exists(rt))
            {
                DirectoryInfo di = Directory.CreateDirectory(rt);
            }
        }

        public void copiarArchivo(string origen, string ruta)
        {

            if (File.Exists(origen))
            {
                System.IO.File.Copy(origen, ruta, true);
            }

        }
        public void borrarArchivo(string ruta)
        {
            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }
        }

        public void abrirWeb(string ruta)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(ruta); Process.Start(startInfo);
        }
    }
}

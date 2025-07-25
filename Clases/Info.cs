using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fergeda_2023.Clases
{
    class Info
    {
        private Consultas X = new Consultas();
        List<string> app = new List<string>() { "Álmacen","Sistemas","Recursos Humanos","Gerencia General","Finanzas","Direccion","Compras", "Mantenimiento" };

        public string departamento(int apli) => app[apli];

        public void accesos(object dato, int i)
        {

            string dat = (string)dato;
            char[] sp = { '/' };
            string[] arr = dat.Split(sp);

            String strHostName = string.Empty;
            strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            string op = "fk_empleado";
            if (i.Equals(0))
                op = "fk_vendedor";

            X.insertar("insert into fergeda.s_access (hots_name,ip_adress0,ip_adress1,date1,application," + op + ",estatus)values('" + strHostName +
                "','" + addr[0].ToString() + "','" + addr[1].ToString() + "',NOW(),'" + app[i] + "','" + Convert.ToInt32(arr[0]) +
                "','" + arr[1] + "');");
        }
        public string estatus(object dato, int i)
        {
            string dat = (string)dato;
            string op = "fk_empleado";
            if (i.Equals(0))
                op = "fk_vendedor";
            int veces = Convert.ToInt32(X.elem3("SELECT count(*) FROM fergeda.s_access where " + op + "='" + Convert.ToInt32(dat) + "' and estatus='Activo' and application='" + app[i] + "';"));
            if (veces > 1)
                return "En Uso";
            else
                return "Libre";
        }


        public void cerrarConex(object dato, int i)
        {
            string dat = (string)dato;
            int id = Convert.ToInt32(X.elem3("SELECT max(idsis_access) FROM fergeda.s_access where hots_name='" + Dns.GetHostName() + "' and fk_empleado='" + Convert.ToInt32(dat) + "' and application='" + app[i] + "' and estatus='Activo';"));
            X.insertar("update fergeda.s_access set estatus='Cerrada',fecha_cierre=now()  where idsis_access='" + id + "';");

        }


        #region EMPLEADOS

        public bool tienePermisos(int empleado)
        {
            int veces = Convert.ToInt32(X.elem3("SELECT count(*) FROM fergeda.s_pass where empleado=" + empleado + ";"));
            return (veces > 0 ? true : false);

        }
        public int PermisoApp(int empleado)
        {
            int dep = Convert.ToInt32(X.elem3("SELECT fk_Departamento FROM fergeda.r_puestos where idr_puestos=(SELECT fk_Puesto FROM fergeda.r_empleado where idr_empleado='"+empleado+"');"));           
            int apli = 1;
            switch (dep)
            {
                case 1:
                    apli = 5;
                    break;
                case 2:
                    apli = 3;
                    break;
                case 3:
                    apli = 6;
                    break;
                case 4:
                    apli = 4;
                    break;
                case 5:
                    apli = 2;
                    break;
                case 6:
                    apli = 0;
                    break;
                case 7:
                    apli = 1;
                    break;
                case 8:
                    apli = 7;
                    break;
            }

            return apli;

        }

        #endregion
    }
}

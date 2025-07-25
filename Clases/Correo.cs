using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Fergeda_2023.Clases
{
    class Correo
    { public void compras(string destinatario, string correo, string asunto, string body, string contraseña)
        {
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress(destinatario));
            email.From = new MailAddress(correo);
            email.CC.Add(new MailAddress("ymelendez@litopolis.com"));
            email.CC.Add(new MailAddress("compras@litopolis.com"));
            email.Bcc.Add(new MailAddress("arios@litopolis.com"));
            email.CC.Add(new MailAddress("gjimenez@litopolis.com"));
            email.CC.Add(new MailAddress("m.edificio@litoplis.com"));
            email.Subject = asunto;
            email.Body = body;           /*funcional las etiquetas web para la elaboracion del correo*/
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "coyote.litopolis.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;      /*sifrado de la cuenta*/
            smtp.UseDefaultCredentials = correo.Equals("chalco@litopolis.com") ? true : false;/*false para gmail*/
            smtp.Credentials = new NetworkCredential(correo.Equals("chalco@litopolis.com") ? "chalco" : correo, contraseña);
            /*para mandar el mensage*/
            try
            {
                smtp.Send(email);
                email.Dispose();
                Console.WriteLine("Mensage enviado Correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine("error al enviar correo electronico" + ex.Message);
            }

        }
    }
}

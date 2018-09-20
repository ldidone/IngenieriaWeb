using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilidades;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class AspNetUsersServicio
    {
        public static string obtenerIdUsuarioPorUserName(string UserName)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.AspNetUsers.Where(x => x.UserName == UserName).FirstOrDefault().Id;
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }
    }
}

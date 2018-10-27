using Datos;
using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.AccesoDatos
{
    public class DenunciasServicio
    {
        public static bool Crear(Denuncia denuncia)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    Denuncias val = new Denuncias
                    {
                        IdDenuncia = denuncia.IdDenuncia,
                        IdPedido = denuncia.IdPedido,
                        DescripcionPedido = denuncia.DescripcionPedido,
                        Motivo = denuncia.Motivo
                    };
                    db.Denuncias.Add(val);
                    db.SaveChanges();

                    return true;

                }
                catch
                {
                    return false;
                }

            }
        }
    }
}

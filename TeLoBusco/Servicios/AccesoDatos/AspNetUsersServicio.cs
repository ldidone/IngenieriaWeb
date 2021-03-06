﻿using Datos;
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
        public static List<AspNetUsers> obtenerTodos()
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.AspNetUsers.ToList(); //Ver includes necesarios
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool existeUserName(string UserName)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.AspNetUsers.Any(x => x.UserName == UserName);
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool existeEmail(string Email)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.AspNetUsers.Any(x => x.Email == Email);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static AspNetUsers obtener(string id)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

        public static AspNetUsers obtenerPorEmail(string email)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.AspNetUsers.Where(x => x.Email == email).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static string obtenerIdPorEmail(string email)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    var user = db.AspNetUsers.Where(x => x.Email == email).FirstOrDefault();
                    if (user != null)
                    {
                        return user.Id;
                    }
                    return "";
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

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

        public static bool editar(string Id, string NombreApellido, string Email, string UserName)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    var usuario = db.AspNetUsers.Where(x => x.Id == Id).FirstOrDefault();
                    if (usuario != null)
                    {
                        usuario.NombreApellido = NombreApellido;
                        usuario.Email = Email;
                        usuario.UserName = UserName;
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool Eliminar(string id)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    var usuario = db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
                    db.AspNetUsers.Remove(usuario);
                    db.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public static string ObtenerNombrePorId(string id)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    return db.AspNetUsers.Where(x => x.Id == id).FirstOrDefault().NombreApellido;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public static bool AlmacenarJWT(string email, string JWT)
        {
            using (TeloBuscoEntities db = new TeloBuscoEntities())
            {
                try
                {
                    var user = db.AspNetUsers.Where(x => x.Email == email).FirstOrDefault();
                    if (user != null)
                    {
                        user.JWT = JWT;
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}

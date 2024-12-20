﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> listar()
        {
            return objCapaDato.listar();
        }
        public int registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El Apellidos del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El Correo del usuario no puede ser vacio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                string clave = CN_Recursos.GenerarClave();

                string asunto = "Creacion De Cuenta";
                string mensaje_correo = "<h3>Su cuenta Fue Creada Correctamente </h3></br><p>Su Clave es: !clave! </p>";
                mensaje_correo = mensaje_correo.Replace("!clave!",clave);

                bool respuesta = CN_Recursos.enviarCorreo(obj.Correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    obj.Clave = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
                
            }
            else
            {
                return 0;
            }

        }


        public bool Editar(Usuario obj, out string Mensaje)
        {
            {
                Mensaje = string.Empty;

                if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
                {
                    Mensaje = "El nombre del usuario no puede ser vacio";
                }
                else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
                {
                    Mensaje = "El Apellidos del usuario no puede ser vacio";
                }
                else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
                {
                    Mensaje = "El Correo del usuario no puede ser vacio";
                }
                if (string.IsNullOrEmpty(Mensaje))
                {

                    return objCapaDato.Editar(obj, Mensaje);
                }
                else
                {
                    return false;
                }
            }

        }
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}

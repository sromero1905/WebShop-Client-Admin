using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;




namespace CapaDatos
{
    public class CD_Usuarios
    {

        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select IdUsuario, Nombres, Apellidos, Correo, Restablecer, Activo, Clave from USUARIO";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader() )
                    {
                        while (dr.Read())
                        {
                            lista.Add(
                             new Usuario()
                             {
                                 IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                 Nombres = dr["Nombres"].ToString(),
                                 Apellidos = dr["Apellidos"].ToString(),
                                 Correo = dr["Correo"].ToString(),
                                 Clave = dr["Clave"].ToString(),
                                 Restablecer = Convert.ToBoolean(dr["Restablecer"]),
                                 Activo = Convert.ToBoolean(dr["Activo"])

                             });


                        }
                    }

                }

            }
            catch (Exception)
            {
                lista = new List<Usuario>();
            }


            return lista;
        }



    }
}

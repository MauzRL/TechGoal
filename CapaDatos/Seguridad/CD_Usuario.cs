﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Reflection;
using System.Collections;

namespace CapaDatos.Seguridad
{
    public class CD_Usuario
    {
        // ---------------------- OBTENER TODOS LOS USUARIOS --------------------
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection conection = new SqlConnection(Conection.cadena))
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT u.id_usuario, u.email, u.clave, u.nombre, u.apellido, u.dni, u.telefono, u.estado, u.fechaRegistro, r.id_rol, r.descripcion FROM usuarios u");
                    query.AppendLine("INNER JOIN rol r ON r.id_rol = u.id_rol");

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), conection))
                    {

                        conection.Open();
                        Console.WriteLine(cmd);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                lista.Add(new Usuario()
                                {
                                    id_usuario = Convert.ToInt32(reader["id_usuario"]),
                                    email = reader["email"].ToString(),
                                    clave = reader["clave"].ToString(),
                                    nombre = reader["nombre"].ToString(),
                                    apellido = reader["apellido"].ToString(),
                                    dni = reader["dni"].ToString(),
                                    telefono = reader["telefono"].ToString(),
                                    estado = Convert.ToBoolean(reader["estado"]),
                                    fechaRegistro = reader["fechaRegistro"].ToString(),
                                    o_rol = new Rol()
                                    {
                                        id_rol = Convert.ToInt32(reader["id_rol"]),
                                        descripcion = reader["descripcion"].ToString()
                                    }

                                });

                            }
                        }

                    }




                }

            } catch (Exception ex) { 
                lista = new List<Usuario>();
                Console.WriteLine(ex.Message);
            }

            

            return lista;
        }

        public static bool AgregarUsuario(Usuario nuevoUsuario)
        {
            bool agregar = false;
            try
            {
                using (SqlConnection conection = new SqlConnection(Conection.cadena))
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("INSERT INTO usuarios (email,clave,nombre,apellido,dni,telefono,id_rol,estado)");
                    query.AppendLine("VALUES (@email,@clave,@nombre,@apellido,@dni,@telefono,@id_rol,@estado)");

                    using (SqlCommand cmd = new SqlCommand(query.ToString(), conection))
                    {

                        cmd.Parameters.AddWithValue("@email", nuevoUsuario.email);
                        cmd.Parameters.AddWithValue("@clave", nuevoUsuario.clave);
                        cmd.Parameters.AddWithValue("@nombre", nuevoUsuario.nombre);
                        cmd.Parameters.AddWithValue("@apellido", nuevoUsuario.apellido);
                        cmd.Parameters.AddWithValue("@dni", nuevoUsuario.dni);
                        cmd.Parameters.AddWithValue("@telefono", nuevoUsuario.telefono);
                        cmd.Parameters.AddWithValue("@id_rol", nuevoUsuario.o_rol.id_rol);
                        cmd.Parameters.AddWithValue("@estado", nuevoUsuario.estado);

                        conection.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        agregar = filasAfectadas > 0;


                    }
                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return agregar;
        }

        public DataTable CargarTablaUsuarios(DataTable tablaUsuarios)
        {
            try
            {
                using (SqlConnection conection = new SqlConnection(Conection.cadena))
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT u.id_usuario, u.email, u.clave, u.nombre, u.apellido, u.dni, r.descripcion, u.telefono, u.estado, u.fechaRegistro, r.id_rol FROM usuarios u");
                    query.AppendLine("INNER JOIN rol r ON r.id_rol = u.id_rol");

                    SqlDataAdapter adapter;

                    using (adapter = new SqlDataAdapter(query.ToString(), conection))
                    {
                        
                        adapter.Fill(tablaUsuarios);
                        return tablaUsuarios;
                        
                    }


                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                    return tablaUsuarios;

            }
        }

        

    }
}

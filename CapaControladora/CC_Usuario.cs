﻿using CapaDatos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;
using System.Security.Cryptography;
using System.Data;

namespace CapaControladora
{
    public class CC_Usuario
    {
        public static CC_Usuario instance = null;


        // -------------------- SINGLETON ---------------------

        public static CC_Usuario getInstance
        {
            get
            {
                if (instance == null)
                    instance = new CC_Usuario();
                return instance;
            }
        }

        // ---------------------- OBTENER TODOS LOS USUARIOS --------------------

        public List<Usuario> Listar()
        {
            List<Usuario> listaUsuarios = new CD_Usuario().Listar();

            return listaUsuarios;
            
        }

        // ---------------------- ENCONTRAR USUARIO EN LA LISTA POR CREDENCIALES OBTENIDAS--------------------

        public Usuario EncontrarUsuarioLogin (string txtCorreo, string txtClave)
        {
            //Filtrar usuarios por email y clave proporcionados
            Usuario usuarioIniciando = new CC_Usuario().Listar().Where(u => u.email == txtCorreo && u.clave == txtClave).FirstOrDefault();

            if (usuarioIniciando != null)
            {
                return usuarioIniciando;
            }
            else
            {
                return usuarioIniciando;
            }


        }

        public Usuario EncontrarUsuarioCorreo (string txtCorreo)
        {

            Usuario buscandoUsuario = new CC_Usuario().Listar().Where(u => u.email == txtCorreo).FirstOrDefault();

            if (buscandoUsuario != null)
            {
                return buscandoUsuario;
            }
            else
            {
                return buscandoUsuario;
            }


        }

        public Usuario EncontrarUsuarioDNI(string txtDNI)
        {

            Usuario buscandoUsuario = new CC_Usuario().Listar().Where(u => u.dni == txtDNI).FirstOrDefault();

            if (buscandoUsuario != null)
            {
                return buscandoUsuario;
            }
            else
            {
                return buscandoUsuario;
            }


        }


        public string EncriptarClave(string clave)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(clave));
            string hashString = BitConverter.ToString(hashValue).Replace("-", "");
            return hashString;
        }

        public DataTable CargarTablaUsuarios(DataTable tablaUsuarios)
        {
            tablaUsuarios = new CD_Usuario().CargarTablaUsuarios(tablaUsuarios);
            return tablaUsuarios;

        }

        public bool AgregarUsuario(Usuario nuevoUsuario)
        {
            if (nuevoUsuario != null)
            {
                if (CD_Usuario.AgregarUsuario(nuevoUsuario))
                {
                    return true;

                }
                else return false;

            } else
            {
                return false;
            }

            
        }

    }

}

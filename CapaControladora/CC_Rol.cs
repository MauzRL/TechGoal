﻿using CapaDatos;
using CapaDatos.Seguridad;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaControladora
{
    public class CC_Rol
    {
        public static CC_Rol instance = null;


        // -------------------- SINGLETON ---------------------

        public static CC_Rol getInstance
        {
            get
            {
                if (instance == null)
                    instance = new CC_Rol();
                return instance;
            }
        }
        public List<Rol> Listar()
        {
            List<Rol> listaRoles = new CD_Rol().Listar();

            return listaRoles;

        }

        public Rol BuscarRol(string descripcion)
        {

            Rol buscandoRol = new CC_Rol().Listar().Where(r => r.descripcion == descripcion).FirstOrDefault();

            if (buscandoRol != null)
            {
                return buscandoRol;
            }
            else
            {
                return buscandoRol;
            }
        }

        public bool AgregarRol(Rol nuevoRol)
        {
            if (nuevoRol != null)
            {
                if (CD_Rol.AgregarRol(nuevoRol))
                {
                    return true;

                }
                else return false;

            }
            else
            {
                return false;
            }


        }

    }
}

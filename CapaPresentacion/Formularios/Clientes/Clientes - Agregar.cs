﻿using CapaControladora;
using CapaEntidad;
using CapaPresentacion.Formularios.Reservas;
using CapaPresentacion.Personalizacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios.Clientes
{
    public partial class formClientesAgregar : Form
    {
        CC_Cliente ClienteControladora = CC_Cliente.getInstance;
        formClientes formClientesC;
        formReservaNueva formReservaNuevaC;
        Funcionalidades Funcionalidades = Funcionalidades.getInstance;
        public formClientesAgregar()
        {
            InitializeComponent();
        }

        public formClientesAgregar(formClientes formClientes) : this()
        {
            formClientesC = formClientes;
            
        }

        public formClientesAgregar(formReservaNueva formReservaNueva) : this()
        {
            
            formReservaNuevaC = formReservaNueva;
        }

        private void formClientesAgregar_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // ---------------------------- VALIDACION DE CAMPOS VACIOS ----------------------------

                foreach (Control control in Controls)
                {
                    if (control is TextBox)
                    {
                        if (string.IsNullOrEmpty(control.Text))
                        {

                            MessageBox.Show("Por favor complete todos los campos", "Oops! Hubo un error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;

                        }
                    }

                }

                Cliente buscarCliente = ClienteControladora.EncontrarClienteDNI(txtDocumento.Text);

                if (buscarCliente != null)
                {
                    MessageBox.Show("Ya existe un cliente con ese DNI.", "Oops! Hubo un error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ---------------------------- AGREGAR CLIENTE ----------------------------

                Cliente nuevoCliente = new Cliente()
                {
                    nombre = txtNombre.Text,
                    apellido = txtApellido.Text,
                    dni = txtDocumento.Text,
                    telefono = txtTelefono.Text,
                    estado = true
                };

                bool agregarCliente = ClienteControladora.AgregarCliente(nuevoCliente);

                if (agregarCliente == false)
                {
                    MessageBox.Show("Hubo un error al agregar usuario. Por favor consulte con un administrador.", "Oops! Hubo un error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Cliente agregado con exito!", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();


            } catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void formClientesAgregar_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (formClientesC == null)
            {
                formReservaNuevaC.llenarTabla();
            } else
            {
                 formClientesC.llenarTabla();

            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funcionalidades.soloLetras(sender, e);
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funcionalidades.soloLetras(sender, e);

        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funcionalidades.soloNumeros(sender, e);

        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funcionalidades.soloNumeros(sender, e);

        }
    }
}

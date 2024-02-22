﻿using CapaPresentacion.Personalizacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class formLoginRecuperar : Form
    {
        Funcionalidades funcionalidades = Funcionalidades.getInstance;
        public formLoginRecuperar()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Application.OpenForms["formLogin"].Show();
            Close();

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            funcionalidades.EnviarCorreo(txtCorreo.Text);
            Application.OpenForms["formLogin"].Show();

            Close();

        }
    }
}
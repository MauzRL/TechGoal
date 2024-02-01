﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace CapaPresentacion.Personalizacion
{
    public class MSButton : Button
    {

        private int borderSize = 0;
        private int borderRadius = 60;
        private Color borderColor = Color.FromArgb(31, 201, 236);
        [Category("Avanzado")]


        public int BorderSize { get => borderSize; set { borderSize = value; this.Invalidate(); } }
        [Category("Avanzado")]

        public int BorderRadius { get => borderRadius; set { borderRadius = value; this.Invalidate(); } }
        [Category("Avanzado")]

        public Color BorderColor { get => borderColor; set {borderColor = value; this.Invalidate(); } }
        [Category("Avanzado")]

        public Color BackgroundColor
        {
            get => this.BackColor;
            set => this.BackColor = value;
        }

        [Category("Avanzado")]
        public Color TextColor
        {
            get => this.ForeColor;
            set => this.ForeColor = value;
        }

        public MSButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.FromArgb(31, 201, 236);
            this.ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
            {
                borderRadius = this.Height;
            }
        }

        private GraphicsPath GetGraphicsPath(RectangleF rect, float radius) { 

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width-radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width-radius, rect.Height-radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height-radius, radius, radius, 90, 90);

            path.CloseFigure();

            return path;



        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);

            if (borderRadius > 2)
            {
                using (GraphicsPath pathSurface = GetGraphicsPath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetGraphicsPath(rectBorder, borderRadius -1F))
                using (Pen penSurface = new Pen(Parent.BackColor, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize))

                {
                    penBorder.Alignment = PenAlignment.Inset;
                    this.Region = new Region(pathSurface);

                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    if (borderSize >= 1)
                    {
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                    }
                }

                    
            }

            
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                Invalidate();
            }
        }
    }

    }


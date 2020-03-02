using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto_BDA
{
    public partial class Form1 : Form
    {
        private BaseDeDatos BaseDeDatos { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                BaseDeDatos = new BaseDeDatos(open.FileName);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(BaseDeDatos != null && BaseDeDatos.NombreBaseDeDatos != null)
            {
                string s = BaseDeDatos.NombreBaseDeDatos;
                textbox_nombre_bd.Text = s.Substring(s.LastIndexOf("\\") + 1);

                list_tablas.Items.Clear();
                foreach (var item in BaseDeDatos.Tablas)
                {
                    string[] subitems = new string[] {item.Key, item.Value };
                    ListViewItem list = new ListViewItem(subitems);
                    list_tablas.Items.Add(list);
                }
            }
            else
            {
                textbox_nombre_bd.Text = "";
                list_tablas.Items.Clear();
            }
        }

        private void abrirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                BaseDeDatos = new BaseDeDatos(open.SelectedPath);
            }
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseDeDatos = null;
            Invalidate();
        }

        private void boton_agregar_tabla_Click(object sender, EventArgs e)
        {
            if (BaseDeDatos != null)
            {
                if (textbox_agregar_tabla.Text != "")
                {
                    if(BaseDeDatos.AgregaTabla(textbox_agregar_tabla.Text))
                    {
                        textbox_agregar_tabla.Text = "";
                        Invalidate();
                    }
                }
            }
        }

        private void boton_modificar_tabla_Click(object sender, EventArgs e)
        {
            if(textbox_actualizar_tabla.Text != "" && list_tablas.SelectedItems.Count > 0)
            {
                string nombreAnterior = list_tablas.SelectedItems[0].Text;
                string nombreNuevo = textbox_actualizar_tabla.Text;

                BaseDeDatos.ModificaNombreTabla(nombreAnterior, nombreNuevo);

                textbox_actualizar_tabla.Text = "";
                Invalidate();
            }
        }

        private void cambiar_name_BD_Click(object sender, EventArgs e)
        {

        }

        private void list_tablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_tablas.SelectedItems.Count > 0)
            {
                string nombre = list_tablas.SelectedItems[0].Text;
                label_eliminar_tabla.Text = textbox_actualizar_tabla.Text = nombre;
            }
        }
    }
}
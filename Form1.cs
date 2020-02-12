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
                textBox1.Text = s.Substring(s.LastIndexOf("\\") + 1);
                textBox2.Text = BaseDeDatos.NombreBaseDeDatos;

                listView1.Items.Clear();
                foreach (var item in BaseDeDatos.Tablas)
                {
                    string[] subitems = new string[] {item.Key, item.Value };
                    ListViewItem list = new ListViewItem(subitems);
                    listView1.Items.Add(list);
                }
            }
            else
            {
                textBox1.Text = textBox2.Text = "";
                listView1.Items.Clear();
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
    }
}
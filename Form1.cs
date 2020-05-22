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
            ModificaPantallas(false);          
        }

        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                BaseDeDatos = new BaseDeDatos(open.FileName);
                ModificaPantallas(true);
            }
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(BaseDeDatos != null && BaseDeDatos.NombreBaseDeDatos != null)
            {
                string s = BaseDeDatos.NombreBaseDeDatos;
                label_bd.Text = s.Substring(s.LastIndexOf("\\") + 1);

                list_tablas.Items.Clear();

                foreach (var item in BaseDeDatos.Set.Tables)
                {
                    string ruta = BaseDeDatos.NombreBaseDeDatos + "\\" + item.ToString() + ".dat";
                    string[] subitems = new string[] { item.ToString(), ruta };
                    ListViewItem list = new ListViewItem(subitems);
                    list_tablas.Items.Add(list);
                }
            }
            else
            {
                label_bd.Text = "";
                list_tablas.Items.Clear();
            }
        }

        private void abrirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                BaseDeDatos = new BaseDeDatos(open.SelectedPath);
                ModificaPantallas(true);
                combobox_tipo_llave.SelectedIndex = 0;
                combobox_tipo_dato.SelectedIndex = 0;
                Invalidate();
            }
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseDeDatos = null;
            diccionario_atributos.Columns.Clear();
            grid_insertar_datos.Columns.Clear();
            registros_datos.Columns.Clear();
            ResetFrontAtributos();
            ModificaPantallas(false);
            Invalidate();
        }

        private void boton_agregar_tabla_Click(object sender, EventArgs e)
        {
            if (BaseDeDatos != null && !string.IsNullOrEmpty(textbox_agregar_tabla.Text))
            {
                try 
                { 
                    BaseDeDatos.AgregaTabla(textbox_agregar_tabla.Text);
                    textbox_agregar_tabla.Text = "";
                }
                catch (DuplicateNameException)
                {
                    MessageBox.Show("Ya existe una tabla con ese nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { Invalidate(); }
            }
        }

        private void boton_modificar_tabla_Click(object sender, EventArgs e)
        {
            if(textbox_agregar_tabla.Text != "" && list_tablas.SelectedItems.Count > 0)
            {
                string nombreAnterior = list_tablas.SelectedItems[0].Text;
                string nombreNuevo = textbox_agregar_tabla.Text;

                try { BaseDeDatos.ModificaNombreTabla(nombreAnterior, nombreNuevo); }
                catch (DuplicateNameException)
                {
                    MessageBox.Show("Ya existe una tabla con ese nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally 
                {
                    textbox_agregar_tabla.Text = "";
                    label_eliminar_tabla.Text = "-";
                    Invalidate();
                }
            }
        }

        private void list_tablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_tablas.SelectedItems.Count > 0)
            {
                string nombre = list_tablas.SelectedItems[0].Text;
                label_eliminar_tabla.Text = textbox_agregar_tabla.Text = nombre;
            }
        }

        private void modificar_bd_Click(object sender, EventArgs e)
        {
            if (textbox_modificar_bd.Text != "")
            {
                
                int index_path = BaseDeDatos.NombreBaseDeDatos.LastIndexOf("\\");
                string nombreNuevo = BaseDeDatos.NombreBaseDeDatos.Substring(0, index_path+1) + textbox_modificar_bd.Text;

                BaseDeDatos.NombreBaseDeDatos = nombreNuevo;

                textbox_modificar_bd.Text = "";
                Invalidate();
            }
        }

        private void boton_eliminar_bd_Click(object sender, EventArgs e)
        {
            if (BaseDeDatos != null && 
                MessageBox.Show("¿Estás seguro de eliminar la Base de Datos?", 
                "Confirmar eliminación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Directory.Delete(BaseDeDatos.NombreBaseDeDatos, true);
                BaseDeDatos = null;
                Invalidate();
            }
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e) => Close();

        private void boton_eliminar_tabla_Click(object sender, EventArgs e)
        {
            if (list_tablas.SelectedItems.Count > 0)
            {
                string tabla = list_tablas.SelectedItems[0].Text;
                BaseDeDatos.EliminaTabla(tabla);
                textbox_agregar_tabla.Text = "";
                label_eliminar_tabla.Text = "-";
                Invalidate();
            }
        }

        private void combobox_tablas_atributos_SelectedIndexChanged(object sender, EventArgs e)
        {
            diccionario_atributos.Columns.Clear();
            diccionario_atributos.DataSource = BaseDeDatos.ObtenAtributos(combobox_tablas_atributos.SelectedItem.ToString());
            ResetFrontAtributos();
        }

        private void combobox_tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            textbox_longitud.Enabled = combobox_tipo_dato.SelectedIndex == 2;
            textbox_longitud.Text = !textbox_longitud.Enabled ? sizeof(int).ToString() : "";
        }

        private void diccionario_atributos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1 || e.ColumnIndex != 0)
                return;

            label_atributo_actual.Text = textbox_agregar_atributo.Text = diccionario_atributos.Rows[e.RowIndex].Cells[0].Value.ToString();

            switch (diccionario_atributos.Rows[e.RowIndex].Cells[1].Value)
            {
                case "System.Int32": combobox_tipo_dato.SelectedIndex = 0; break;
                case "System.Single": combobox_tipo_dato.SelectedIndex = 1; break;
                case "System.String": combobox_tipo_dato.SelectedIndex = 2; break;
            }

            textbox_longitud.Text = diccionario_atributos.Rows[e.RowIndex].Cells[2].Value.ToString();

            switch (diccionario_atributos.Rows[e.RowIndex].Cells[3].Value)
            {
                case "Sin Llave": combobox_tipo_llave.SelectedIndex = 0; break;
                case "Primaria": combobox_tipo_llave.SelectedIndex = 1; break;
                case "Foranea": combobox_tipo_llave.SelectedIndex = 2; break;
            }
        }
        /**
         * Habilita/deshabilita los botones/controles del programa. 
         * Este método es preventivo ;v
         */
        private void ModificaPantallas(bool valor)
        {
            boton_agregar_tabla.Enabled = valor;
            boton_modificar_tabla.Enabled = valor;
            boton_eliminar_tabla.Enabled = valor;
            modificar_bd.Enabled = valor;
            boton_eliminar_bd.Enabled = valor;

            combobox_tablas_atributos.Enabled = valor;
            textbox_agregar_atributo.Enabled = valor;
            combobox_tipo_dato.Enabled = valor;
            combobox_tipo_llave.Enabled = valor;
            boton_agregar_atributo.Enabled = valor;
            boton_modificar_atributo.Enabled = valor;
            boton_eliminar_atributo.Enabled = valor;
            diccionario_atributos.ReadOnly = true;

            combobox_tablas_datos.Enabled = valor;
            grid_insertar_datos.Enabled = valor;
            registros_datos.Enabled = valor;
            boton_agregar_registro.Enabled = valor;
            boton_modificar_registro.Enabled = valor;
            boton_eliminar_registro.Enabled = valor;
        }

        private void ResetFrontAtributos()
        {
            textbox_agregar_atributo.Text = "";
            combobox_tipo_dato.SelectedIndex = 0;
            combobox_tipo_llave.SelectedIndex = 0;
            textbox_longitud.Text = "4";
            label_atributo_actual.Text = "-";
        }

        private void boton_agregar_atributo_Click(object sender, EventArgs e)
        {
            if (!ModificaAtributos())
                return;

            DataColumn atributo = CreaAtributo();

            try
            {
                string nomTabla = combobox_tablas_atributos.SelectedItem.ToString();
                BaseDeDatos.AgregaAtributo(nomTabla, atributo);

                CreaRelacion(atributo);
            }
            catch (DuplicateNameException)
            {
                MessageBox.Show("Ya existe un atributo con ese nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(DuplicatePrimaryKeyException)
            {
                MessageBox.Show("Ya existe una llave primaria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception error)
            {

                MessageBox.Show(error.Message, "Error");
            }
            finally
            {
                diccionario_atributos.DataSource = BaseDeDatos.ObtenAtributos(combobox_tablas_atributos.SelectedItem.ToString());
            }
            ResetFrontAtributos();
        }
        private void boton_modificar_atributo_Click(object sender, EventArgs e)
        {
            int numTupla = diccionario_atributos.CurrentCell.RowIndex;
            bool res = ModificaAtributos() && numTupla >= 0;

            if (!res)
                return;

            DataColumn atributo = CreaAtributo();

            try
            {
                string nomTabla = combobox_tablas_atributos.SelectedItem.ToString();
                string nomAtributo = diccionario_atributos.Rows[numTupla].Cells["Nombre"].Value.ToString();
                BaseDeDatos.ModificaAtributo(nomTabla, nomAtributo, atributo);
                CreaRelacion(atributo);
            }
            catch (DuplicateNameException)
            {
                MessageBox.Show("Ya existe un atributo con ese nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DuplicatePrimaryKeyException)
            {
                MessageBox.Show("Ya existe una llave primaria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }
            finally
            {
                diccionario_atributos.DataSource = BaseDeDatos.ObtenAtributos(combobox_tablas_atributos.SelectedItem.ToString());
            }
            ResetFrontAtributos();
        }
        private void boton_eliminar_atributo_Click(object sender, EventArgs e)
        {
            int numTupla = diccionario_atributos.CurrentCell.RowIndex;
            bool res = combobox_tablas_atributos.SelectedIndex >= 0 && combobox_tipo_llave.SelectedIndex >= 0;
            res &= numTupla >= 0;

            if (!res)
            {
                MessageBox.Show("Por favor escoge un atributo a eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nomTabla = combobox_tablas_atributos.SelectedItem.ToString();
            string nomAtributo = diccionario_atributos.Rows[numTupla].Cells["Nombre"].Value.ToString();

            try
            {
                BaseDeDatos.EliminaAtributo(nomTabla, nomAtributo);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }
            diccionario_atributos.DataSource = BaseDeDatos.ObtenAtributos(combobox_tablas_atributos.SelectedItem.ToString());
            ResetFrontAtributos();
        }

        private DataColumn CreaAtributo()
        {
            string texto = combobox_tipo_dato.SelectedItem.ToString();
            DataColumn atributo = new DataColumn(textbox_agregar_atributo.Text)
            {
                DataType = Type.GetType("System." + (texto.Equals("Cadena") ? "String" :
                                                     texto.Equals("Entero") ? "Int32" :
                                                     "Single")),
                Unique = combobox_tipo_llave.SelectedIndex == 1,
            };

            if (texto.Equals("Cadena"))
                atributo.MaxLength = int.Parse(textbox_longitud.Text);

            return atributo;
        }

        private void CreaRelacion(DataColumn child)
        {
            if (combobox_tipo_llave.SelectedIndex != 2)
                return;

            string table = combobox_foranea.SelectedItem.ToString();
            var father = BaseDeDatos.Set.Tables[table];

            BaseDeDatos.AgregaLlaveForanea(father.PrimaryKey[0], child);
        }

        private bool ModificaAtributos()
        {
            bool res = true;

            if (string.IsNullOrEmpty(textbox_agregar_atributo.Text))
            {
                MessageBox.Show("No puedes insertar un atributo sin nombre. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                res = false;
            }

            if (res && string.IsNullOrEmpty(textbox_longitud.Text))
            {
                MessageBox.Show("No puedes insertar un atributo sin longitud. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                res = false;
            }

            if (res && combobox_tablas_atributos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor selecciona una tabla", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                res = false;
            }

            if (res && combobox_tipo_llave.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor selecciona un tipo de llave", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                res = false;
            }

            if(res && combobox_tipo_llave.SelectedIndex == 2 && combobox_foranea.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor selecciona un atributo de llave foránea", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                res = false;
            }

            return res;
        }

        private void combobox_tipo_llave_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(combobox_tipo_llave.SelectedIndex == 2)
            {

                combobox_foranea.Enabled = true;
                combobox_foranea.Items.Clear();
                foreach (DataTable item in BaseDeDatos.Set.Tables)
                {
                    if (item.ToString() != combobox_tablas_atributos.SelectedItem.ToString() &&
                        item.PrimaryKey.Length > 0) 
                        combobox_foranea.Items.Add(item.ToString());
                }
            }
            else
            {
                combobox_foranea.Items.Clear();
                combobox_foranea.Enabled = false;
            }
        }

        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabs.SelectedIndex == 1)
            {
                combobox_tablas_atributos.Items.Clear();
                if (BaseDeDatos != null)
                {
                    foreach (var item in BaseDeDatos.Set.Tables)
                    {
                        combobox_tablas_atributos.Items.Add(item.ToString());
                    }
                }
                ResetFrontAtributos();
                diccionario_atributos.Columns.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            foreach (DataTable item in BaseDeDatos.Set.Tables)
            {
                Console.WriteLine("Tabla: " + item.TableName);
                foreach (Constraint c in item.Constraints)
                {
                    Console.WriteLine("\tRestriccion: " + c.ConstraintName);
                    if(c is ForeignKeyConstraint)
                    {
                        ForeignKeyConstraint f = (ForeignKeyConstraint)c;
                        Console.WriteLine("\t\tLlave primaria: " + f.Columns[0]);
                        Console.WriteLine("\t\tLlave foránea: " + f.RelatedColumns[0]);
                    }

                }
            }
        }

        private void vistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BaseDeDatos == null)
                return;
            if (vistas.SelectedIndex == 1)
            {
                combobox_tablas_datos.Items.Clear();
                foreach (DataTable item in BaseDeDatos.Set.Tables)
                {
                    combobox_tablas_datos.Items.Add(item.TableName);
                }

                grid_insertar_datos.Columns.Clear();
                registros_datos.Columns.Clear();
            }
        }

        private void combobox_tablas_datos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(combobox_tablas_datos.SelectedIndex != -1)
            {
                string table_name = combobox_tablas_datos.SelectedItem.ToString();
                DataTable table = BaseDeDatos.Set.Tables[table_name];

                grid_insertar_datos.Columns.Clear();
                registros_datos.Columns.Clear();

                foreach (DataColumn item in table.Columns)
                {
                    grid_insertar_datos.Columns.Add(item.ColumnName, item.ColumnName);
                    registros_datos.Columns.Add(item.ColumnName, item.ColumnName);
                }

                ImprimeRegistrosDeDatos();
            }
        }

        private string[] GetDataOfDGV()
        {
            List<string> temp = new List<string>();

            foreach (DataGridViewCell item in grid_insertar_datos.Rows[0].Cells)
            {
                if (item.Value != null)
                    temp.Add(item.Value.ToString());
            }
            return temp.ToArray();
        }

        private void ImprimeRegistrosDeDatos()
        {
            string table_name = combobox_tablas_datos.Text;
            DataTable table = BaseDeDatos.Set.Tables[table_name];

            if (table.Columns.Count > 0)
            {
                registros_datos.Rows.Clear();
                foreach (DataRow item in table.Rows)
                {
                    registros_datos.Rows.Add(item.ItemArray);
                }
            }
        }

        private void boton_agregar_registro_Click(object sender, EventArgs e)
        {
            string [] data = GetDataOfDGV();
            string table_name = combobox_tablas_datos.Text;
            DataTable table = BaseDeDatos.Set.Tables[table_name];

            if(table.Columns.Count == data.Length)
            {
                try
                {
                    BaseDeDatos.AgregaRegistro(table.TableName, data);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            grid_insertar_datos.Rows.Clear();
            ImprimeRegistrosDeDatos();
        }

        private void boton_modificar_registro_Click(object sender, EventArgs e)
        {
            if (registros_datos.SelectedCells.Count == 0)
                return;

            int index = registros_datos.SelectedCells[0].RowIndex;
            string[] data = GetDataOfDGV();
            string table_name = combobox_tablas_datos.Text;

            try
            {
                BaseDeDatos.ModificaRegistro(table_name, data, index);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }

            grid_insertar_datos.Rows.Clear();
            ImprimeRegistrosDeDatos();
        }

        private void boton_eliminar_registro_Click(object sender, EventArgs e)
        {
            if (registros_datos.SelectedCells.Count == 0)
                return;

            int index = registros_datos.SelectedCells[0].RowIndex;
            string table_name = combobox_tablas_datos.Text;

            try
            {
                BaseDeDatos.EliminaRegistro(table_name, index);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }

            grid_insertar_datos.Rows.Clear();
            ImprimeRegistrosDeDatos();
        }

        private void registros_datos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grid_insertar_datos.Rows.Clear();
            if (registros_datos.SelectedCells.Count == 0)
                return;
            
            var row = grid_insertar_datos.Rows[0];

            int idx = registros_datos.SelectedCells[0].RowIndex;
            DataGridViewRow newRow = registros_datos.Rows[idx];

            for (Int32 index = 0; index < row.Cells.Count; index++)
            {
                row.Cells[index].Value = newRow.Cells[index].Value;
            }
        }
    }
}
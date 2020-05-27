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
        /* *************************
         * Métodos de Form1
         * ************************/
        private BaseDeDatos BaseDeDatos { get; set; }
        public Form1()
        {
            InitializeComponent();
            ModificaPantallas(false);          
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (BaseDeDatos != null && BaseDeDatos.NombreBaseDeDatos != null)
            {
                // Copiar nombre de la Base de Datos
                string s = BaseDeDatos.NombreBaseDeDatos;
                label_bd.Text = s.Substring(s.LastIndexOf("\\") + 1);

                // Agrega las tablas de la BD al componente list_tablas
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
                // Si no hay BD entonces se limpia la pantalla
                label_bd.Text = "";
                list_tablas.Items.Clear();
            }
        }
        private void ModificaPantallas(bool valor)
        {
            boton_agregar_tabla.Enabled = valor;
            boton_modificar_tabla.Enabled = valor;
            boton_eliminar_tabla.Enabled = valor;
            boton_modificar_bd.Enabled = valor;
            boton_eliminar_bd.Enabled = valor;

            combobox_tablas_atributos.Enabled = valor;
            textbox_agregar_atributo.Enabled = valor;
            combobox_tipo_dato.Enabled = valor;
            combobox_tipo_llave.Enabled = valor;
            boton_agregar_atributo.Enabled = valor;
            boton_modificar_atributo.Enabled = valor;
            boton_eliminar_atributo.Enabled = valor;
            list_atributos.ReadOnly = true;

            combobox_tablas_datos.Enabled = valor;
            grid_insertar_datos.Enabled = valor;
            registros_datos.Enabled = valor;
            boton_agregar_registro.Enabled = valor;
            boton_modificar_registro.Enabled = valor;
            boton_eliminar_registro.Enabled = valor;
        }


        /* *************************
         * Barra del menú Archivo
         * ************************/

        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                // Se crea una BD con la ruta específica del directorio.
                BaseDeDatos = new BaseDeDatos(open.FileName);

                // Se habilitan las pantallas de la aplicación
                ModificaPantallas(true);

                // Se resetean los valores de los componentes para agregar atributos
                ResetFrontAtributos();
            }
            Invalidate();
        }

        private void abrirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                // Instancia se crea con la ruta establecida
                BaseDeDatos = new BaseDeDatos(open.SelectedPath);

                // Se habilita la pantalla
                ModificaPantallas(true);

                // Se resetean los valores de los componentes para agregar atributos
                ResetFrontAtributos();
            }
            Invalidate();
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Instancia de la BD se vuelve null
            BaseDeDatos = null; 

            // Se limpian componentes
            list_atributos.Columns.Clear();
            grid_insertar_datos.Columns.Clear();
            registros_datos.Columns.Clear();
            ResetFrontAtributos();

            //Se deshabilitan las pantallas
            ModificaPantallas(false);
            Invalidate();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e) => Close();


        /* ********************************
         * Componentes para Bases De Datos
         * *******************************/

        private void boton_modificar_bd_Click(object sender, EventArgs e)
        {
            // Si el textbox que almacena el nuevo nombre de la Base de Datos
            if (textbox_modificar_bd.Text != "")
            {
                // Escribimos el nombre nuevo de la Base de Datos en el último índice de '\'
                int index_path = BaseDeDatos.NombreBaseDeDatos.LastIndexOf("\\");
                string nombreNuevo = BaseDeDatos.NombreBaseDeDatos.Substring(0, index_path + 1) 
                    + textbox_modificar_bd.Text;

                BaseDeDatos.NombreBaseDeDatos = nombreNuevo;

                textbox_modificar_bd.Text = "";
                Invalidate();
            }
        }
        private void boton_eliminar_bd_Click(object sender, EventArgs e)
        {
            // Primero preguntamos si de verdad desea eliminar la Base de Datos
            if (BaseDeDatos != null &&
                MessageBox.Show("¿Estás seguro de eliminar la Base de Datos?",
                "Confirmar eliminación", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Si sí desea borrarla utilizamos la clase Directory para eliminar todo.
                Directory.Delete(BaseDeDatos.NombreBaseDeDatos, true);
                BaseDeDatos = null;
                Invalidate();
            }
        }

      
        /* ********************************
         * Componentes para Tablas
         * *******************************/

        private void boton_agregar_tabla_Click(object sender, EventArgs e)
        {
            // Primero validamos que exista una BD abierta y que el textbox tenga contenido
            if (BaseDeDatos != null && !string.IsNullOrEmpty(textbox_agregar_tabla.Text))
            {
                try 
                { 
                    // Agregamos el nombre de la Tabla
                    BaseDeDatos.AgregaTabla(textbox_agregar_tabla.Text);
                    textbox_agregar_tabla.Text = "";
                }
                catch (DuplicateNameException)
                {
                    // Si nos devuelve una excepción de nombre duplicado, mandamos error.
                    MessageBox.Show("Ya existe una tabla con ese nombre", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { Invalidate(); }
            }
        }
        private void boton_modificar_tabla_Click(object sender, EventArgs e)
        {
            // Primero validamos que tengamos contenido en el TextBox
            // y que tengamos seleccionada una Tabla
            if(textbox_agregar_tabla.Text != "" && list_tablas.SelectedItems.Count > 0)
            {
                // Obtenemos la Tabla anterior y la nueva
                string nombreAnterior = list_tablas.SelectedItems[0].Text;
                string nombreNuevo = textbox_agregar_tabla.Text;

                // Mandamos a llamar al método si hay excepción mandamos el error
                try { BaseDeDatos.ModificaNombreTabla(nombreAnterior, nombreNuevo); }
                catch (DuplicateNameException)
                {
                    MessageBox.Show("Ya existe una tabla con ese nombre", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally 
                {
                    textbox_agregar_tabla.Text = "";
                    label_eliminar_tabla.Text = "-";
                    Invalidate();
                }
            }
        }

        private void boton_eliminar_tabla_Click(object sender, EventArgs e)
        {
            // Validamos que esté seleccionada una tabla
            if (list_tablas.SelectedItems.Count > 0)
            {
                // Llamamos el método y limpiamos componentes
                string tabla = list_tablas.SelectedItems[0].Text;
                BaseDeDatos.EliminaTabla(tabla);
                textbox_agregar_tabla.Text = "";
                label_eliminar_tabla.Text = "-";
                Invalidate();
            }
        }
        private void list_tablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (list_tablas.SelectedItems.Count > 0)
            {
                // Al seleccionar una tabla nos apoyamos de un Label para mostrar
                // el nombre de la tabla actualmente seleccionada
                string nombre = list_tablas.SelectedItems[0].Text;
                label_eliminar_tabla.Text = textbox_agregar_tabla.Text = nombre;
            }
        }


        /* ********************************
         * Componentes para Atributos
         * *******************************/

        // MÉTODO: Seleccionar pestaña de Atributos
        private void tabs_edicion_tablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Validamos que estamos en la pestaña de atributos
            if (tabs_edicion_tablas.SelectedIndex == 1)
            {
                // Eliminamos las tablas que contenga el combobox por si se agregaron más
                combobox_tablas_atributos.Items.Clear();
                if (BaseDeDatos != null)
                {
                    // Si existe la BaseDeDatos entonces vamos a agregar 
                    // los nombres de las tablas que contenga esta
                    foreach (var item in BaseDeDatos.Set.Tables)
                    {
                        combobox_tablas_atributos.Items.Add(item.ToString());
                    }
                }
                ResetFrontAtributos();
                list_atributos.Columns.Clear();
            }
        }

        // MÉTODO: Seleccionar una tabla en pestaña de Atributos
        private void combobox_tablas_atributos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando cambia el índice del combobox entonces vamos a vaciar los atributos de la
            // tabla seleccionada
            list_atributos.Columns.Clear();

            string tabla_seleccionada = combobox_tablas_atributos.SelectedItem.ToString();
            list_atributos.DataSource = BaseDeDatos.ObtenAtributos(tabla_seleccionada);

            ResetFrontAtributos();
        }

        private void boton_agregar_atributo_Click(object sender, EventArgs e)
        {
            // Validamos que los componentes para agregar los atributos sean correctos
            if (!ValidaComponentesAtributos())
                return;
            // Creamos la columna con los datos previamente validados
            DataColumn atributo = CreaAtributo();
            try
            {
                string nomTabla = combobox_tablas_atributos.SelectedItem.ToString();
                // Agregamos el atributo e intentamos hacer la relación
                BaseDeDatos.AgregaAtributo(nomTabla, atributo);
                CreaRelacion(atributo);
            }
            // Si el nombre está duplicado
            catch (DuplicateNameException)
            {
                MessageBox.Show("Ya existe un atributo con ese nombre.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Si ya existe una llave primaria
            catch (DuplicatePrimaryKeyException)
            {
                MessageBox.Show("Ya existe una llave primaria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Cualquier otro tipo de Error
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }
            finally
            {
                string tabla_seleccionada = combobox_tablas_atributos.SelectedItem.ToString();
                // Terminando vamos a actualizar el DataGridView con nuestros atributos
                list_atributos.DataSource = BaseDeDatos.ObtenAtributos(tabla_seleccionada);
            }
            ResetFrontAtributos();
        }
        private void boton_modificar_atributo_Click(object sender, EventArgs e)
        {
            int atributo_seleccionado = list_atributos.CurrentCell.RowIndex;
            // Validamos que estén bien los componentes y exista un atributo seleccionado
            if (!(ValidaComponentesAtributos() && atributo_seleccionado >= 0))
                return;

            // Creamos el atributo
            DataColumn atributo = CreaAtributo();
            try
            {
                // Modificamos el atributo y creamos la relación
                string nomTabla = combobox_tablas_atributos.SelectedItem.ToString();
                string nomAtributo = list_atributos.Rows[atributo_seleccionado].Cells["Nombre"].Value.ToString();
                BaseDeDatos.ModificaAtributo(nomTabla, nomAtributo, atributo);
                CreaRelacion(atributo);
            }
            // Mismas excepciones que al agregar Atributo
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
                // Se actualizan las tablas
                string tabla_seleccionada = combobox_tablas_atributos.SelectedItem.ToString();
                list_atributos.DataSource = BaseDeDatos.ObtenAtributos(tabla_seleccionada);
            }
            ResetFrontAtributos();
        }
        private void boton_eliminar_atributo_Click(object sender, EventArgs e)
        {
            int atributo_seleccionado = list_atributos.CurrentCell.RowIndex;
            bool validacion = combobox_tablas_atributos.SelectedIndex >= 0 &&
                                combobox_tipo_llave.SelectedIndex >= 0;

            validacion &= atributo_seleccionado >= 0;
            // Validamos componentes y que al atributo a eliminar esté seleccionado.
            if (!validacion)
            {
                MessageBox.Show("Por favor escoge un atributo a eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string nomTabla = combobox_tablas_atributos.SelectedItem.ToString();
            string nomAtributo = list_atributos.Rows[atributo_seleccionado].Cells["Nombre"].Value.ToString();
            try
            {
                // Se elimina el atributo de la tabla seleccionada.
                BaseDeDatos.EliminaAtributo(nomTabla, nomAtributo);
            }
            // Escuchamos cualquier error
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }
            // Actualizamos información y limpiamos
            list_atributos.DataSource = BaseDeDatos.ObtenAtributos(combobox_tablas_atributos.SelectedItem.ToString());
            ResetFrontAtributos();
        }

        // MÉTODO: Seleccionamos un tipo de Dato
        private void combobox_tipo_dato_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si seleccionamos que el tipo es una cadena Cadena, 
            // entonces el campo de longitud se habilita.
            textbox_longitud.Enabled = combobox_tipo_dato.SelectedIndex == 2;
            // Si no está habilitado el campo longitud tiene que ser de 4 bytes.
            textbox_longitud.Text = !textbox_longitud.Enabled ? sizeof(int).ToString() : "";
        }

        // MÉTODO: Seleccionamos un tipo de llave
        private void combobox_tipo_llave_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si seleccionamos la llave foránea
            if (combobox_tipo_llave.SelectedIndex == 2)
            {
                string tabla_seleccionada = combobox_tablas_atributos.SelectedItem.ToString();
                // Habilitamos el combobox con las opciones para escoger llave foránea
                combobox_foranea.Enabled = true;
                combobox_foranea.Items.Clear();
                foreach (DataTable item in BaseDeDatos.Set.Tables)
                {
                    // Agregamos solo las tablas que no sean la tabla actual y que tengan llave primaria
                    if (item.ToString() != tabla_seleccionada &&
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

        // MÉTODO: Seleccionamos un atributo
        private void list_atributos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validamos que esté bien seleccionada nuestra celda
            if (e.RowIndex == -1 || e.ColumnIndex == -1 || e.ColumnIndex != 0)
                return;

            // Actualizamos el label que tiene nuestro atributo actual
            label_atributo_actual.Text = textbox_agregar_atributo.Text = list_atributos.Rows[e.RowIndex].Cells[0].Value.ToString();

            // Actualizamos el combobox de tipo de dato
            switch (list_atributos.Rows[e.RowIndex].Cells[1].Value)
            {
                case "System.Int32": combobox_tipo_dato.SelectedIndex = 0; break;
                case "System.Single": combobox_tipo_dato.SelectedIndex = 1; break;
                case "System.String": combobox_tipo_dato.SelectedIndex = 2; break;
            }

            // Actualizamos el textbox con la longitud del atributo seleccionado
            textbox_longitud.Text = list_atributos.Rows[e.RowIndex].Cells[2].Value.ToString();

            // Actualizamos el tipo de llave del atributo seleccionado
            switch (list_atributos.Rows[e.RowIndex].Cells[3].Value)
            {
                case "Sin Llave": combobox_tipo_llave.SelectedIndex = 0; break;
                case "Primaria": combobox_tipo_llave.SelectedIndex = 1; break;
                case "Foranea": combobox_tipo_llave.SelectedIndex = 2; break;
            }
        }

        private bool ValidaComponentesAtributos()
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

            if (res && combobox_tipo_llave.SelectedIndex == 2 && combobox_foranea.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor selecciona un atributo de llave foránea", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                res = false;
            }

            return res;
        }
        private DataColumn CreaAtributo()
        {
            // Obtemos el tipo de dato 
            string tipo_dato = combobox_tipo_dato.SelectedItem.ToString();
            string nombre_atributo = textbox_agregar_atributo.Text;

            // Creamos el atributo con el nombre indicado
            DataColumn atributo = new DataColumn(nombre_atributo)
            {
                // Obtemos el tipo de dato que seleccionamos
                DataType = Type.GetType("System." + (tipo_dato.Equals("Cadena") ? "String" :
                                                     tipo_dato.Equals("Entero") ? "Int32" :
                                                     "Single")),
                // Indicamos si es de tipo llave primaria
                Unique = combobox_tipo_llave.SelectedIndex == 1,
            };

            // Si es cadena tenemos que actualizar su longitud
            if (tipo_dato.Equals("Cadena"))
                atributo.MaxLength = int.Parse(textbox_longitud.Text);

            return atributo;
        }
      
        private void CreaRelacion(DataColumn llave_foranea)
        {
            // Validamos que esté seleccionada el tipo de llave foránea
            if (combobox_tipo_llave.SelectedIndex != 2)
                return;

            string tabla_seleccionada = combobox_foranea.SelectedItem.ToString();
            var llave_primaria = BaseDeDatos.Set.Tables[tabla_seleccionada];

            BaseDeDatos.AgregaLlaveForanea(llave_primaria.PrimaryKey[0], llave_foranea);
        }

        private void ResetFrontAtributos()
        {
            textbox_agregar_atributo.Text = "";
            combobox_tipo_dato.SelectedIndex = 0;
            combobox_tipo_llave.SelectedIndex = 0;
            textbox_longitud.Text = "4";
            label_atributo_actual.Text = "-";
        }


        /* ************************************
         * Componentes para Registros de Datos
         * ************************************/

         // Seleccionamos pestaña de Registros de Datos
        private void vistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Validamos que exista una BD
            if (BaseDeDatos == null)
                return;
            if (vistas.SelectedIndex == 1)
            {
                // En el combobox de registros de datos agregamos las tablas actuales
                combobox_tablas_datos.Items.Clear();
                foreach (DataTable item in BaseDeDatos.Set.Tables)
                {
                    combobox_tablas_datos.Items.Add(item.TableName);
                }

                // Limpiamos pestaña
                grid_insertar_datos.Columns.Clear();
                registros_datos.Columns.Clear();
            }
        }

        // MÉTODO: Seleccionar una Tabla
        private void combobox_tablas_datos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combobox_tablas_datos.SelectedIndex != -1)
            {
                string table_name = combobox_tablas_datos.SelectedItem.ToString();
                DataTable table = BaseDeDatos.Set.Tables[table_name];

                grid_insertar_datos.Columns.Clear();
                registros_datos.Columns.Clear();

                // Lo que hacemos es poner los encabezados del registro de Datos
                foreach (DataColumn item in table.Columns)
                {
                    grid_insertar_datos.Columns.Add(item.ColumnName, item.ColumnName);
                    registros_datos.Columns.Add(item.ColumnName, item.ColumnName);
                }

                // Después mandamos a imprimir los registros
                ImprimeRegistrosDeDatos();
            }
        }
        private void boton_agregar_registro_Click(object sender, EventArgs e)
        {
            // Obtenemos los datos a insertar en la Tabla
            string[] data = GetDataRegisterOfDGV();

            string table_name = combobox_tablas_datos.Text;
            DataTable table = BaseDeDatos.Set.Tables[table_name];

            // Validamos que tengan el mismop número de datos insertados
            if (table.Columns.Count == data.Length)
            {
                try
                {
                    // Agregamos el registro
                    BaseDeDatos.AgregaRegistro(table.TableName, data);
                }
                catch (Exception error)
                {
                    // Cualquier error será captado en este momento
                    MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            grid_insertar_datos.Rows.Clear();
            ImprimeRegistrosDeDatos();
        }
        private void boton_modificar_registro_Click(object sender, EventArgs e)
        {
            // Validamos que esté seleccionado un registro.
            if (registros_datos.SelectedCells.Count == 0)
                return;
            // Obtenemos la nueva información
            int index = registros_datos.SelectedCells[0].RowIndex;
            string[] data = GetDataRegisterOfDGV();
            string table_name = combobox_tablas_datos.Text;

            try
            {
                // Modificamos el registro
                BaseDeDatos.ModificaRegistro(table_name, data, index);
            }
            catch (Exception error)
            {
                // Escuchamos cualquier excepcion
                MessageBox.Show(error.Message, "Error");
            }

            grid_insertar_datos.Rows.Clear();
            ImprimeRegistrosDeDatos();
        }
        private void boton_eliminar_registro_Click(object sender, EventArgs e)
        {
            // Validamos que esté seleccionado
            if (registros_datos.SelectedCells.Count == 0)
                return;

            int index = registros_datos.SelectedCells[0].RowIndex;
            string table_name = combobox_tablas_datos.Text;

            try
            {
                // Eliminamos
                BaseDeDatos.EliminaRegistro(table_name, index);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error");
            }

            grid_insertar_datos.Rows.Clear();
            ImprimeRegistrosDeDatos();
        }

        //MÉTODO: Seleccionar un registro
        private void registros_datos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validamos selección
            grid_insertar_datos.Rows.Clear();
            if (registros_datos.SelectedCells.Count == 0)
                return;

            // Obtenemos el registro
            var row = grid_insertar_datos.Rows[0];
            // Obtenemos el índice del registro
            int idx = registros_datos.SelectedCells[0].RowIndex;
            DataGridViewRow newRow = registros_datos.Rows[idx];
            // Insertamos
            for (Int32 index = 0; index < row.Cells.Count; index++)
            {
                row.Cells[index].Value = newRow.Cells[index].Value;
            }
        }

       
        private string[] GetDataRegisterOfDGV()
        {
            // Obtenemos los datos que se insertaron en el registro de Datos
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
            // Imprimimos todos los r3egistros de la tabla seleccionada
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

        /* ************************************
         * Componentes para Consultas SQL
         * ************************************/
        private void boton_consulta_Click(object sender, EventArgs e)
        {
            // Validamos
            if (textbox_consultas.Text == "")
                return;

            string consulta = textbox_consultas.Text;

            try
            {
                // Obtenemos la tabla
                DataTable tabla = BaseDeDatos.CompilaSentencia(consulta);
                grid_consultas.Columns.Clear();

                // Insertamos la tabla nueva

                foreach (DataColumn item in tabla.Columns)
                    grid_consultas.Columns.Add(item.ColumnName, item.ColumnName);

                foreach (DataRow item in tabla.Rows)
                    grid_consultas.Rows.Add(item.ItemArray);
            }
            catch (Exception error)
            {
                // Escuchamos excepciones
                grid_consultas.Columns.Clear();
                MessageBox.Show(error.Message, "Error al ejecutar consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
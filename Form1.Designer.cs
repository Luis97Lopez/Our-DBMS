namespace proyecto_BDA
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarcomoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.vista_datos = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.registros_datos = new System.Windows.Forms.DataGridView();
            this.boton_modificar_registro = new System.Windows.Forms.Button();
            this.boton_eliminar_registro = new System.Windows.Forms.Button();
            this.boton_agregar_registro = new System.Windows.Forms.Button();
            this.combobox_entidades2 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.grid_insertar_datos = new System.Windows.Forms.DataGridView();
            this.vista_dd = new System.Windows.Forms.TabPage();
            this.label_bd = new System.Windows.Forms.Label();
            this.boton_eliminar_bd = new System.Windows.Forms.Button();
            this.modificar_bd = new System.Windows.Forms.Button();
            this.textbox_modificar_bd = new System.Windows.Forms.TextBox();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tab_entidades = new System.Windows.Forms.TabPage();
            this.label_eliminar_tabla = new System.Windows.Forms.Label();
            this.list_tablas = new System.Windows.Forms.ListView();
            this.fileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.boton_eliminar_tabla = new System.Windows.Forms.Button();
            this.textbox_actualizar_tabla = new System.Windows.Forms.TextBox();
            this.boton_modificar_tabla = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textbox_agregar_tabla = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.boton_agregar_tabla = new System.Windows.Forms.Button();
            this.tab_atributos = new System.Windows.Forms.TabPage();
            this.boton_modificar_atributo = new System.Windows.Forms.Button();
            this.label_atributo_actual = new System.Windows.Forms.Label();
            this.boton_eliminar_atributo = new System.Windows.Forms.Button();
            this.combobox_indice = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textbox_longitud = new System.Windows.Forms.TextBox();
            this.combobox_tipo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.combobox_tablas_atributos = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textbox_agregar_atributo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.boton_agregar_atributo = new System.Windows.Forms.Button();
            this.diccionario_atributos = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Longitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoInd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.vistas = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.menu.SuspendLayout();
            this.vista_datos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registros_datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_insertar_datos)).BeginInit();
            this.vista_dd.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tab_entidades.SuspendLayout();
            this.tab_atributos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diccionario_atributos)).BeginInit();
            this.vistas.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem1});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(801, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem1
            // 
            this.archivoToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem1,
            this.abrirToolStripMenuItem1,
            this.toolStripSeparator,
            this.guardarToolStripMenuItem,
            this.guardarcomoToolStripMenuItem,
            this.toolStripSeparator1,
            this.cerrarToolStripMenuItem,
            this.toolStripSeparator2,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem1.Name = "archivoToolStripMenuItem1";
            this.archivoToolStripMenuItem1.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem1.Text = "&Archivo";
            // 
            // nuevoToolStripMenuItem1
            // 
            this.nuevoToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("nuevoToolStripMenuItem1.Image")));
            this.nuevoToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nuevoToolStripMenuItem1.Name = "nuevoToolStripMenuItem1";
            this.nuevoToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.nuevoToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.nuevoToolStripMenuItem1.Text = "&Nuevo";
            this.nuevoToolStripMenuItem1.Click += new System.EventHandler(this.nuevoToolStripMenuItem1_Click);
            // 
            // abrirToolStripMenuItem1
            // 
            this.abrirToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("abrirToolStripMenuItem1.Image")));
            this.abrirToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.abrirToolStripMenuItem1.Name = "abrirToolStripMenuItem1";
            this.abrirToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.abrirToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.abrirToolStripMenuItem1.Text = "&Abrir";
            this.abrirToolStripMenuItem1.Click += new System.EventHandler(this.abrirToolStripMenuItem1_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(177, 6);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("guardarToolStripMenuItem.Image")));
            this.guardarToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.guardarToolStripMenuItem.Text = "&Guardar";
            // 
            // guardarcomoToolStripMenuItem
            // 
            this.guardarcomoToolStripMenuItem.Name = "guardarcomoToolStripMenuItem";
            this.guardarcomoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.guardarcomoToolStripMenuItem.Text = "G&uardar como";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            this.cerrarToolStripMenuItem.Click += new System.EventHandler(this.cerrarToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.salirToolStripMenuItem.Text = "&Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // vista_datos
            // 
            this.vista_datos.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.vista_datos.Controls.Add(this.label15);
            this.vista_datos.Controls.Add(this.registros_datos);
            this.vista_datos.Controls.Add(this.boton_modificar_registro);
            this.vista_datos.Controls.Add(this.boton_eliminar_registro);
            this.vista_datos.Controls.Add(this.boton_agregar_registro);
            this.vista_datos.Controls.Add(this.combobox_entidades2);
            this.vista_datos.Controls.Add(this.label16);
            this.vista_datos.Controls.Add(this.grid_insertar_datos);
            this.vista_datos.Location = new System.Drawing.Point(4, 22);
            this.vista_datos.Name = "vista_datos";
            this.vista_datos.Padding = new System.Windows.Forms.Padding(3);
            this.vista_datos.Size = new System.Drawing.Size(786, 443);
            this.vista_datos.TabIndex = 1;
            this.vista_datos.Text = "Registros de Datos";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(22, 137);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(119, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "Registros de Datos:";
            // 
            // registros_datos
            // 
            this.registros_datos.AllowUserToAddRows = false;
            this.registros_datos.AllowUserToDeleteRows = false;
            this.registros_datos.AllowUserToResizeColumns = false;
            this.registros_datos.AllowUserToResizeRows = false;
            this.registros_datos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.registros_datos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.registros_datos.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.registros_datos.Location = new System.Drawing.Point(25, 153);
            this.registros_datos.MultiSelect = false;
            this.registros_datos.Name = "registros_datos";
            this.registros_datos.ReadOnly = true;
            this.registros_datos.Size = new System.Drawing.Size(739, 266);
            this.registros_datos.TabIndex = 32;
            // 
            // boton_modificar_registro
            // 
            this.boton_modificar_registro.Location = new System.Drawing.Point(641, 75);
            this.boton_modificar_registro.Name = "boton_modificar_registro";
            this.boton_modificar_registro.Size = new System.Drawing.Size(123, 23);
            this.boton_modificar_registro.TabIndex = 31;
            this.boton_modificar_registro.Text = "Modificar Registro";
            this.boton_modificar_registro.UseVisualStyleBackColor = true;
            // 
            // boton_eliminar_registro
            // 
            this.boton_eliminar_registro.Location = new System.Drawing.Point(641, 104);
            this.boton_eliminar_registro.Name = "boton_eliminar_registro";
            this.boton_eliminar_registro.Size = new System.Drawing.Size(123, 23);
            this.boton_eliminar_registro.TabIndex = 30;
            this.boton_eliminar_registro.Text = "Eliminar Registro";
            this.boton_eliminar_registro.UseVisualStyleBackColor = true;
            // 
            // boton_agregar_registro
            // 
            this.boton_agregar_registro.Location = new System.Drawing.Point(641, 46);
            this.boton_agregar_registro.Name = "boton_agregar_registro";
            this.boton_agregar_registro.Size = new System.Drawing.Size(123, 23);
            this.boton_agregar_registro.TabIndex = 29;
            this.boton_agregar_registro.Text = "Agregar Registro";
            this.boton_agregar_registro.UseVisualStyleBackColor = true;
            // 
            // combobox_entidades2
            // 
            this.combobox_entidades2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_entidades2.FormattingEnabled = true;
            this.combobox_entidades2.Location = new System.Drawing.Point(25, 32);
            this.combobox_entidades2.Name = "combobox_entidades2";
            this.combobox_entidades2.Size = new System.Drawing.Size(121, 21);
            this.combobox_entidades2.TabIndex = 20;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(22, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 13);
            this.label16.TabIndex = 19;
            this.label16.Text = "Tabla actual:";
            // 
            // grid_insertar_datos
            // 
            this.grid_insertar_datos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.grid_insertar_datos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid_insertar_datos.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.grid_insertar_datos.Location = new System.Drawing.Point(25, 59);
            this.grid_insertar_datos.Name = "grid_insertar_datos";
            this.grid_insertar_datos.Size = new System.Drawing.Size(586, 68);
            this.grid_insertar_datos.TabIndex = 0;
            // 
            // vista_dd
            // 
            this.vista_dd.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.vista_dd.Controls.Add(this.label_bd);
            this.vista_dd.Controls.Add(this.boton_eliminar_bd);
            this.vista_dd.Controls.Add(this.modificar_bd);
            this.vista_dd.Controls.Add(this.textbox_modificar_bd);
            this.vista_dd.Controls.Add(this.tabs);
            this.vista_dd.Controls.Add(this.label1);
            this.vista_dd.Location = new System.Drawing.Point(4, 22);
            this.vista_dd.Name = "vista_dd";
            this.vista_dd.Padding = new System.Windows.Forms.Padding(3);
            this.vista_dd.Size = new System.Drawing.Size(786, 443);
            this.vista_dd.TabIndex = 0;
            this.vista_dd.Text = "Diccionario de Datos";
            // 
            // label_bd
            // 
            this.label_bd.AutoSize = true;
            this.label_bd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_bd.Location = new System.Drawing.Point(149, 8);
            this.label_bd.Name = "label_bd";
            this.label_bd.Size = new System.Drawing.Size(11, 13);
            this.label_bd.TabIndex = 18;
            this.label_bd.Text = "-";
            this.label_bd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boton_eliminar_bd
            // 
            this.boton_eliminar_bd.Location = new System.Drawing.Point(637, 4);
            this.boton_eliminar_bd.Name = "boton_eliminar_bd";
            this.boton_eliminar_bd.Size = new System.Drawing.Size(136, 23);
            this.boton_eliminar_bd.TabIndex = 18;
            this.boton_eliminar_bd.Text = "Eliminar BD";
            this.boton_eliminar_bd.UseVisualStyleBackColor = true;
            this.boton_eliminar_bd.Click += new System.EventHandler(this.boton_eliminar_bd_Click);
            // 
            // modificar_bd
            // 
            this.modificar_bd.Location = new System.Drawing.Point(489, 4);
            this.modificar_bd.Name = "modificar_bd";
            this.modificar_bd.Size = new System.Drawing.Size(137, 23);
            this.modificar_bd.TabIndex = 19;
            this.modificar_bd.Text = "Modificar BD";
            this.modificar_bd.UseVisualStyleBackColor = true;
            this.modificar_bd.Click += new System.EventHandler(this.modificar_bd_Click);
            // 
            // textbox_modificar_bd
            // 
            this.textbox_modificar_bd.Location = new System.Drawing.Point(330, 5);
            this.textbox_modificar_bd.Name = "textbox_modificar_bd";
            this.textbox_modificar_bd.Size = new System.Drawing.Size(137, 20);
            this.textbox_modificar_bd.TabIndex = 18;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tab_entidades);
            this.tabs.Controls.Add(this.tab_atributos);
            this.tabs.Location = new System.Drawing.Point(6, 27);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(774, 410);
            this.tabs.TabIndex = 11;
            // 
            // tab_entidades
            // 
            this.tab_entidades.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tab_entidades.Controls.Add(this.label_eliminar_tabla);
            this.tab_entidades.Controls.Add(this.list_tablas);
            this.tab_entidades.Controls.Add(this.boton_eliminar_tabla);
            this.tab_entidades.Controls.Add(this.textbox_actualizar_tabla);
            this.tab_entidades.Controls.Add(this.boton_modificar_tabla);
            this.tab_entidades.Controls.Add(this.label3);
            this.tab_entidades.Controls.Add(this.textbox_agregar_tabla);
            this.tab_entidades.Controls.Add(this.label6);
            this.tab_entidades.Controls.Add(this.boton_agregar_tabla);
            this.tab_entidades.Location = new System.Drawing.Point(4, 22);
            this.tab_entidades.Name = "tab_entidades";
            this.tab_entidades.Padding = new System.Windows.Forms.Padding(3);
            this.tab_entidades.Size = new System.Drawing.Size(766, 384);
            this.tab_entidades.TabIndex = 0;
            this.tab_entidades.Text = "Entidades";
            // 
            // label_eliminar_tabla
            // 
            this.label_eliminar_tabla.AutoSize = true;
            this.label_eliminar_tabla.Location = new System.Drawing.Point(548, 39);
            this.label_eliminar_tabla.Name = "label_eliminar_tabla";
            this.label_eliminar_tabla.Size = new System.Drawing.Size(10, 13);
            this.label_eliminar_tabla.TabIndex = 17;
            this.label_eliminar_tabla.Text = "-";
            this.label_eliminar_tabla.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // list_tablas
            // 
            this.list_tablas.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.list_tablas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileName,
            this.Path});
            this.list_tablas.HideSelection = false;
            this.list_tablas.Location = new System.Drawing.Point(18, 113);
            this.list_tablas.Name = "list_tablas";
            this.list_tablas.Size = new System.Drawing.Size(711, 245);
            this.list_tablas.TabIndex = 7;
            this.list_tablas.UseCompatibleStateImageBehavior = false;
            this.list_tablas.View = System.Windows.Forms.View.Details;
            this.list_tablas.SelectedIndexChanged += new System.EventHandler(this.list_tablas_SelectedIndexChanged);
            // 
            // fileName
            // 
            this.fileName.Text = "File Name";
            this.fileName.Width = 100;
            // 
            // Path
            // 
            this.Path.Text = "Path";
            this.Path.Width = 300;
            // 
            // boton_eliminar_tabla
            // 
            this.boton_eliminar_tabla.Location = new System.Drawing.Point(551, 65);
            this.boton_eliminar_tabla.Name = "boton_eliminar_tabla";
            this.boton_eliminar_tabla.Size = new System.Drawing.Size(174, 23);
            this.boton_eliminar_tabla.TabIndex = 16;
            this.boton_eliminar_tabla.Text = "Eliminar Tabla";
            this.boton_eliminar_tabla.UseVisualStyleBackColor = true;
            this.boton_eliminar_tabla.Click += new System.EventHandler(this.boton_eliminar_tabla_Click);
            // 
            // textbox_actualizar_tabla
            // 
            this.textbox_actualizar_tabla.Location = new System.Drawing.Point(283, 36);
            this.textbox_actualizar_tabla.Name = "textbox_actualizar_tabla";
            this.textbox_actualizar_tabla.Size = new System.Drawing.Size(174, 20);
            this.textbox_actualizar_tabla.TabIndex = 15;
            // 
            // boton_modificar_tabla
            // 
            this.boton_modificar_tabla.Location = new System.Drawing.Point(283, 65);
            this.boton_modificar_tabla.Name = "boton_modificar_tabla";
            this.boton_modificar_tabla.Size = new System.Drawing.Size(174, 23);
            this.boton_modificar_tabla.TabIndex = 14;
            this.boton_modificar_tabla.Text = "Actualizar Tabla";
            this.boton_modificar_tabla.UseVisualStyleBackColor = true;
            this.boton_modificar_tabla.Click += new System.EventHandler(this.boton_modificar_tabla_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nombre:";
            // 
            // textbox_agregar_tabla
            // 
            this.textbox_agregar_tabla.Location = new System.Drawing.Point(65, 36);
            this.textbox_agregar_tabla.Name = "textbox_agregar_tabla";
            this.textbox_agregar_tabla.Size = new System.Drawing.Size(127, 20);
            this.textbox_agregar_tabla.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Tabla:";
            // 
            // boton_agregar_tabla
            // 
            this.boton_agregar_tabla.Location = new System.Drawing.Point(18, 65);
            this.boton_agregar_tabla.Name = "boton_agregar_tabla";
            this.boton_agregar_tabla.Size = new System.Drawing.Size(174, 23);
            this.boton_agregar_tabla.TabIndex = 9;
            this.boton_agregar_tabla.Text = "Agregar Tabla";
            this.boton_agregar_tabla.UseVisualStyleBackColor = true;
            this.boton_agregar_tabla.Click += new System.EventHandler(this.boton_agregar_tabla_Click);
            // 
            // tab_atributos
            // 
            this.tab_atributos.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tab_atributos.Controls.Add(this.boton_modificar_atributo);
            this.tab_atributos.Controls.Add(this.label_atributo_actual);
            this.tab_atributos.Controls.Add(this.boton_eliminar_atributo);
            this.tab_atributos.Controls.Add(this.combobox_indice);
            this.tab_atributos.Controls.Add(this.label11);
            this.tab_atributos.Controls.Add(this.label10);
            this.tab_atributos.Controls.Add(this.textbox_longitud);
            this.tab_atributos.Controls.Add(this.combobox_tipo);
            this.tab_atributos.Controls.Add(this.label9);
            this.tab_atributos.Controls.Add(this.combobox_tablas_atributos);
            this.tab_atributos.Controls.Add(this.label8);
            this.tab_atributos.Controls.Add(this.label7);
            this.tab_atributos.Controls.Add(this.textbox_agregar_atributo);
            this.tab_atributos.Controls.Add(this.label12);
            this.tab_atributos.Controls.Add(this.boton_agregar_atributo);
            this.tab_atributos.Controls.Add(this.diccionario_atributos);
            this.tab_atributos.Location = new System.Drawing.Point(4, 22);
            this.tab_atributos.Name = "tab_atributos";
            this.tab_atributos.Padding = new System.Windows.Forms.Padding(3);
            this.tab_atributos.Size = new System.Drawing.Size(766, 384);
            this.tab_atributos.TabIndex = 1;
            this.tab_atributos.Text = "Atributos";
            // 
            // boton_modificar_atributo
            // 
            this.boton_modificar_atributo.Location = new System.Drawing.Point(469, 76);
            this.boton_modificar_atributo.Name = "boton_modificar_atributo";
            this.boton_modificar_atributo.Size = new System.Drawing.Size(123, 23);
            this.boton_modificar_atributo.TabIndex = 28;
            this.boton_modificar_atributo.Text = "Modificar Atributo";
            this.boton_modificar_atributo.UseVisualStyleBackColor = true;
            // 
            // label_atributo_actual
            // 
            this.label_atributo_actual.AutoSize = true;
            this.label_atributo_actual.Location = new System.Drawing.Point(634, 30);
            this.label_atributo_actual.Name = "label_atributo_actual";
            this.label_atributo_actual.Size = new System.Drawing.Size(10, 13);
            this.label_atributo_actual.TabIndex = 27;
            this.label_atributo_actual.Text = "-";
            this.label_atributo_actual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // boton_eliminar_atributo
            // 
            this.boton_eliminar_atributo.Location = new System.Drawing.Point(637, 54);
            this.boton_eliminar_atributo.Name = "boton_eliminar_atributo";
            this.boton_eliminar_atributo.Size = new System.Drawing.Size(123, 23);
            this.boton_eliminar_atributo.TabIndex = 26;
            this.boton_eliminar_atributo.Text = "Eliminar Atributo";
            this.boton_eliminar_atributo.UseVisualStyleBackColor = true;
            // 
            // combobox_indice
            // 
            this.combobox_indice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_indice.FormattingEnabled = true;
            this.combobox_indice.Items.AddRange(new object[] {
            "0 - Sin Índice",
            "1 - Clave Búsqueda",
            "2 - Índice Primario",
            "3 - Índice Secundario",
            "4 - Índice Primario de Árbol B+",
            "5 - Índice Primario de Árbol B+"});
            this.combobox_indice.Location = new System.Drawing.Point(469, 20);
            this.combobox_indice.Name = "combobox_indice";
            this.combobox_indice.Size = new System.Drawing.Size(123, 21);
            this.combobox_indice.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(385, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Tipo de Índice:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(195, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Longitud:";
            // 
            // textbox_longitud
            // 
            this.textbox_longitud.Location = new System.Drawing.Point(252, 76);
            this.textbox_longitud.Name = "textbox_longitud";
            this.textbox_longitud.Size = new System.Drawing.Size(123, 20);
            this.textbox_longitud.TabIndex = 22;
            // 
            // combobox_tipo
            // 
            this.combobox_tipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_tipo.FormattingEnabled = true;
            this.combobox_tipo.Items.AddRange(new object[] {
            "Entero",
            "Cadena"});
            this.combobox_tipo.Location = new System.Drawing.Point(252, 51);
            this.combobox_tipo.Name = "combobox_tipo";
            this.combobox_tipo.Size = new System.Drawing.Size(123, 21);
            this.combobox_tipo.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(215, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Tipo:";
            // 
            // combobox_tablas_atributos
            // 
            this.combobox_tablas_atributos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_tablas_atributos.FormattingEnabled = true;
            this.combobox_tablas_atributos.Location = new System.Drawing.Point(18, 46);
            this.combobox_tablas_atributos.Name = "combobox_tablas_atributos";
            this.combobox_tablas_atributos.Size = new System.Drawing.Size(121, 21);
            this.combobox_tablas_atributos.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Tabla actual:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(202, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Nombre:";
            // 
            // textbox_agregar_atributo
            // 
            this.textbox_agregar_atributo.Location = new System.Drawing.Point(252, 25);
            this.textbox_agregar_atributo.Name = "textbox_agregar_atributo";
            this.textbox_agregar_atributo.Size = new System.Drawing.Size(123, 20);
            this.textbox_agregar_atributo.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(202, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Atributo:";
            // 
            // boton_agregar_atributo
            // 
            this.boton_agregar_atributo.Location = new System.Drawing.Point(469, 47);
            this.boton_agregar_atributo.Name = "boton_agregar_atributo";
            this.boton_agregar_atributo.Size = new System.Drawing.Size(123, 23);
            this.boton_agregar_atributo.TabIndex = 15;
            this.boton_agregar_atributo.Text = "Agregar Atributo";
            this.boton_agregar_atributo.UseVisualStyleBackColor = true;
            // 
            // diccionario_atributos
            // 
            this.diccionario_atributos.AllowUserToAddRows = false;
            this.diccionario_atributos.AllowUserToDeleteRows = false;
            this.diccionario_atributos.AllowUserToResizeColumns = false;
            this.diccionario_atributos.AllowUserToResizeRows = false;
            this.diccionario_atributos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.diccionario_atributos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.diccionario_atributos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Longitud,
            this.dataGridViewTextBoxColumn4,
            this.TipoInd,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.diccionario_atributos.Location = new System.Drawing.Point(8, 105);
            this.diccionario_atributos.Name = "diccionario_atributos";
            this.diccionario_atributos.RowHeadersWidth = 5;
            this.diccionario_atributos.Size = new System.Drawing.Size(752, 270);
            this.diccionario_atributos.TabIndex = 11;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 21;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Nombre";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 105;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tipo de Dato";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 105;
            // 
            // Longitud
            // 
            this.Longitud.HeaderText = "Longitud";
            this.Longitud.Name = "Longitud";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Dirección";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 105;
            // 
            // TipoInd
            // 
            this.TipoInd.HeaderText = "Tipo de Índice";
            this.TipoInd.Name = "TipoInd";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Dirección Índice";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 105;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Siguiente Atributo";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 105;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre de Base de Datos:";
            // 
            // vistas
            // 
            this.vistas.CausesValidation = false;
            this.vistas.Controls.Add(this.vista_dd);
            this.vistas.Controls.Add(this.vista_datos);
            this.vistas.Location = new System.Drawing.Point(0, 27);
            this.vistas.Name = "vistas";
            this.vistas.SelectedIndex = 0;
            this.vistas.Size = new System.Drawing.Size(794, 469);
            this.vistas.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 502);
            this.Controls.Add(this.vistas);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menu;
            this.Name = "Form1";
            this.Text = "Bases de Datos A - Manejador de Base de Datos";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.vista_datos.ResumeLayout(false);
            this.vista_datos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registros_datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid_insertar_datos)).EndInit();
            this.vista_dd.ResumeLayout(false);
            this.vista_dd.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tab_entidades.ResumeLayout(false);
            this.tab_entidades.PerformLayout();
            this.tab_atributos.ResumeLayout(false);
            this.tab_atributos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diccionario_atributos)).EndInit();
            this.vistas.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarcomoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabControl vistas;
        private System.Windows.Forms.TabPage vista_dd;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tab_entidades;
        private System.Windows.Forms.Label label_eliminar_tabla;
        private System.Windows.Forms.ListView list_tablas;
        private System.Windows.Forms.ColumnHeader fileName;
        private System.Windows.Forms.ColumnHeader Path;
        private System.Windows.Forms.Button boton_eliminar_tabla;
        private System.Windows.Forms.TextBox textbox_actualizar_tabla;
        private System.Windows.Forms.Button boton_modificar_tabla;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textbox_agregar_tabla;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button boton_agregar_tabla;
        private System.Windows.Forms.TabPage tab_atributos;
        private System.Windows.Forms.Button boton_modificar_atributo;
        private System.Windows.Forms.Label label_atributo_actual;
        private System.Windows.Forms.Button boton_eliminar_atributo;
        private System.Windows.Forms.ComboBox combobox_indice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textbox_longitud;
        private System.Windows.Forms.ComboBox combobox_tipo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox combobox_tablas_atributos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textbox_agregar_atributo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button boton_agregar_atributo;
        private System.Windows.Forms.DataGridView diccionario_atributos;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Longitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoInd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage vista_datos;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView registros_datos;
        private System.Windows.Forms.Button boton_modificar_registro;
        private System.Windows.Forms.Button boton_eliminar_registro;
        private System.Windows.Forms.Button boton_agregar_registro;
        private System.Windows.Forms.ComboBox combobox_entidades2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridView grid_insertar_datos;
        private System.Windows.Forms.Label label_bd;
        private System.Windows.Forms.Button boton_eliminar_bd;
        private System.Windows.Forms.Button modificar_bd;
        private System.Windows.Forms.TextBox textbox_modificar_bd;
    }
}


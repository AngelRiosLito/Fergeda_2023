namespace Fergeda_2023.Finanzas
{
    partial class SolicitudCheque
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolicitudCheque));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_CalcularTotales = new Bunifu.Framework.UI.BunifuImageButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_Total = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_Retencion = new System.Windows.Forms.TextBox();
            this.txt_iva = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Subtotal = new System.Windows.Forms.TextBox();
            this.txt_Folio = new System.Windows.Forms.TextBox();
            this.btn_Generar = new Bunifu.Framework.UI.BunifuImageButton();
            this.Excepciones = new System.Windows.Forms.GroupBox();
            this.com_moneda = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.chec_honorarios = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.chec_retencion = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.op_Iva_No = new System.Windows.Forms.RadioButton();
            this.op_Iva_Si = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.rich_NC_ND = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.com_Proveedor = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_OC = new System.Windows.Forms.TextBox();
            this.txt_Solicitante = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_Departamento = new System.Windows.Forms.TextBox();
            this.txt_OI = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_Importe = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.date_Fecha = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bunifuGradientPanel1 = new Bunifu.Framework.UI.BunifuGradientPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.label19 = new System.Windows.Forms.Label();
            this.date_Pago = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.btn_CalcularTotales)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Generar)).BeginInit();
            this.Excepciones.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.bunifuGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_CalcularTotales
            // 
            this.btn_CalcularTotales.BackColor = System.Drawing.Color.Transparent;
            this.btn_CalcularTotales.Image = ((System.Drawing.Image)(resources.GetObject("btn_CalcularTotales.Image")));
            this.btn_CalcularTotales.ImageActive = null;
            this.btn_CalcularTotales.Location = new System.Drawing.Point(838, 556);
            this.btn_CalcularTotales.Name = "btn_CalcularTotales";
            this.btn_CalcularTotales.Size = new System.Drawing.Size(68, 56);
            this.btn_CalcularTotales.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_CalcularTotales.TabIndex = 38;
            this.btn_CalcularTotales.TabStop = false;
            this.btn_CalcularTotales.Zoom = 10;
            this.btn_CalcularTotales.Click += new System.EventHandler(this.btn_CalcularTotales_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.txt_Total);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.txt_Retencion);
            this.groupBox3.Controls.Add(this.txt_iva);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txt_Subtotal);
            this.groupBox3.Location = new System.Drawing.Point(819, 287);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(178, 263);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Costos:";
            // 
            // txt_Total
            // 
            this.txt_Total.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Total.ForeColor = System.Drawing.Color.Crimson;
            this.txt_Total.Location = new System.Drawing.Point(6, 210);
            this.txt_Total.Name = "txt_Total";
            this.txt_Total.Size = new System.Drawing.Size(166, 26);
            this.txt_Total.TabIndex = 29;
            this.txt_Total.Text = "0";
            this.txt_Total.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 187);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 20);
            this.label14.TabIndex = 28;
            this.label14.Text = "Total:";
            // 
            // txt_Retencion
            // 
            this.txt_Retencion.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Retencion.ForeColor = System.Drawing.Color.Crimson;
            this.txt_Retencion.Location = new System.Drawing.Point(6, 158);
            this.txt_Retencion.Name = "txt_Retencion";
            this.txt_Retencion.Size = new System.Drawing.Size(166, 26);
            this.txt_Retencion.TabIndex = 27;
            this.txt_Retencion.Text = "0";
            this.txt_Retencion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_iva
            // 
            this.txt_iva.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_iva.ForeColor = System.Drawing.Color.Crimson;
            this.txt_iva.Location = new System.Drawing.Point(6, 106);
            this.txt_iva.MaxLength = 30;
            this.txt_iva.Name = "txt_iva";
            this.txt_iva.Size = new System.Drawing.Size(166, 26);
            this.txt_iva.TabIndex = 26;
            this.txt_iva.Text = "0";
            this.txt_iva.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 135);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 20);
            this.label13.TabIndex = 25;
            this.label13.Text = "Retencion";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 20);
            this.label12.TabIndex = 24;
            this.label12.Text = "Iva";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 20);
            this.label11.TabIndex = 23;
            this.label11.Text = "Subtotal";
            // 
            // txt_Subtotal
            // 
            this.txt_Subtotal.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Subtotal.ForeColor = System.Drawing.Color.Crimson;
            this.txt_Subtotal.Location = new System.Drawing.Point(6, 54);
            this.txt_Subtotal.MaxLength = 30;
            this.txt_Subtotal.Name = "txt_Subtotal";
            this.txt_Subtotal.Size = new System.Drawing.Size(166, 26);
            this.txt_Subtotal.TabIndex = 22;
            this.txt_Subtotal.Text = "0";
            this.txt_Subtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_Folio
            // 
            this.txt_Folio.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Folio.ForeColor = System.Drawing.Color.Crimson;
            this.txt_Folio.Location = new System.Drawing.Point(76, 35);
            this.txt_Folio.MaxLength = 100;
            this.txt_Folio.Name = "txt_Folio";
            this.txt_Folio.ReadOnly = true;
            this.txt_Folio.Size = new System.Drawing.Size(108, 26);
            this.txt_Folio.TabIndex = 37;
            this.txt_Folio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_Generar
            // 
            this.btn_Generar.BackColor = System.Drawing.Color.Transparent;
            this.btn_Generar.Image = ((System.Drawing.Image)(resources.GetObject("btn_Generar.Image")));
            this.btn_Generar.ImageActive = null;
            this.btn_Generar.Location = new System.Drawing.Point(923, 556);
            this.btn_Generar.Name = "btn_Generar";
            this.btn_Generar.Size = new System.Drawing.Size(68, 56);
            this.btn_Generar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Generar.TabIndex = 36;
            this.btn_Generar.TabStop = false;
            this.btn_Generar.Zoom = 10;
            this.btn_Generar.Click += new System.EventHandler(this.btn_Generar_Click);
            // 
            // Excepciones
            // 
            this.Excepciones.BackColor = System.Drawing.Color.Transparent;
            this.Excepciones.Controls.Add(this.com_moneda);
            this.Excepciones.Controls.Add(this.label3);
            this.Excepciones.Controls.Add(this.comboBox1);
            this.Excepciones.Controls.Add(this.chec_honorarios);
            this.Excepciones.Controls.Add(this.label18);
            this.Excepciones.Controls.Add(this.chec_retencion);
            this.Excepciones.Controls.Add(this.label17);
            this.Excepciones.Controls.Add(this.label16);
            this.Excepciones.Controls.Add(this.op_Iva_No);
            this.Excepciones.Controls.Add(this.op_Iva_Si);
            this.Excepciones.Location = new System.Drawing.Point(785, 43);
            this.Excepciones.Name = "Excepciones";
            this.Excepciones.Size = new System.Drawing.Size(212, 238);
            this.Excepciones.TabIndex = 34;
            this.Excepciones.TabStop = false;
            this.Excepciones.Text = "Excepciones";
            // 
            // com_moneda
            // 
            this.com_moneda.BackColor = System.Drawing.Color.LightBlue;
            this.com_moneda.Cursor = System.Windows.Forms.Cursors.Default;
            this.com_moneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_moneda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.com_moneda.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.com_moneda.FormattingEnabled = true;
            this.com_moneda.Items.AddRange(new object[] {
            "MXN",
            "USD",
            "EUR"});
            this.com_moneda.Location = new System.Drawing.Point(74, 195);
            this.com_moneda.Name = "com_moneda";
            this.com_moneda.Size = new System.Drawing.Size(58, 28);
            this.com_moneda.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Moneda:";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.LightBlue;
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            ".04",
            ".08",
            ".25"});
            this.comboBox1.Location = new System.Drawing.Point(96, 95);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(58, 28);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.Visible = false;
            // 
            // chec_honorarios
            // 
            this.chec_honorarios.AutoSize = true;
            this.chec_honorarios.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chec_honorarios.Location = new System.Drawing.Point(29, 159);
            this.chec_honorarios.Name = "chec_honorarios";
            this.chec_honorarios.Size = new System.Drawing.Size(38, 20);
            this.chec_honorarios.TabIndex = 7;
            this.chec_honorarios.Text = "Si";
            this.chec_honorarios.UseVisualStyleBackColor = true;
            this.chec_honorarios.CheckedChanged += new System.EventHandler(this.op_Honorarios_CheckedChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(6, 136);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 20);
            this.label18.TabIndex = 6;
            this.label18.Text = "Honorarios:";
            // 
            // chec_retencion
            // 
            this.chec_retencion.AutoSize = true;
            this.chec_retencion.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chec_retencion.Location = new System.Drawing.Point(29, 103);
            this.chec_retencion.Name = "chec_retencion";
            this.chec_retencion.Size = new System.Drawing.Size(38, 20);
            this.chec_retencion.TabIndex = 5;
            this.chec_retencion.Text = "Si";
            this.chec_retencion.UseVisualStyleBackColor = true;
            this.chec_retencion.CheckedChanged += new System.EventHandler(this.chec_retencion_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(6, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 20);
            this.label17.TabIndex = 4;
            this.label17.Text = "Retencion:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(6, 29);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 20);
            this.label16.TabIndex = 3;
            this.label16.Text = "Iva:";
            // 
            // op_Iva_No
            // 
            this.op_Iva_No.AutoSize = true;
            this.op_Iva_No.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.op_Iva_No.Location = new System.Drawing.Point(96, 52);
            this.op_Iva_No.Name = "op_Iva_No";
            this.op_Iva_No.Size = new System.Drawing.Size(41, 20);
            this.op_Iva_No.TabIndex = 1;
            this.op_Iva_No.Text = "No";
            this.op_Iva_No.UseVisualStyleBackColor = true;
            // 
            // op_Iva_Si
            // 
            this.op_Iva_Si.AutoSize = true;
            this.op_Iva_Si.Checked = true;
            this.op_Iva_Si.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.op_Iva_Si.Location = new System.Drawing.Point(30, 52);
            this.op_Iva_Si.Name = "op_Iva_Si";
            this.op_Iva_Si.Size = new System.Drawing.Size(37, 20);
            this.op_Iva_Si.TabIndex = 0;
            this.op_Iva_Si.TabStop = true;
            this.op_Iva_Si.Text = "Si";
            this.op_Iva_Si.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.rich_NC_ND);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.com_Proveedor);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_OC);
            this.groupBox1.Controls.Add(this.txt_Solicitante);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_Departamento);
            this.groupBox1.Controls.Add(this.txt_OI);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_Importe);
            this.groupBox1.Location = new System.Drawing.Point(17, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(762, 177);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(567, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(109, 20);
            this.label15.TabIndex = 19;
            this.label15.Text = "Notas: (Dev, NC)";
            // 
            // rich_NC_ND
            // 
            this.rich_NC_ND.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rich_NC_ND.Location = new System.Drawing.Point(567, 77);
            this.rich_NC_ND.MaxLength = 300;
            this.rich_NC_ND.Name = "rich_NC_ND";
            this.rich_NC_ND.Size = new System.Drawing.Size(184, 61);
            this.rich_NC_ND.TabIndex = 18;
            this.rich_NC_ND.Text = "N/A";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(167, 20);
            this.label10.TabIndex = 17;
            this.label10.Text = "No. de Orden de Compra:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Solicitante:";
            // 
            // com_Proveedor
            // 
            this.com_Proveedor.BackColor = System.Drawing.Color.LightBlue;
            this.com_Proveedor.DropDownHeight = 200;
            this.com_Proveedor.DropDownWidth = 550;
            this.com_Proveedor.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.com_Proveedor.FormattingEnabled = true;
            this.com_Proveedor.IntegralHeight = false;
            this.com_Proveedor.Location = new System.Drawing.Point(196, 111);
            this.com_Proveedor.Name = "com_Proveedor";
            this.com_Proveedor.Size = new System.Drawing.Size(333, 28);
            this.com_Proveedor.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(402, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Departamento:";
            // 
            // txt_OC
            // 
            this.txt_OC.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_OC.Location = new System.Drawing.Point(196, 148);
            this.txt_OC.MaxLength = 150;
            this.txt_OC.Name = "txt_OC";
            this.txt_OC.Size = new System.Drawing.Size(546, 26);
            this.txt_OC.TabIndex = 15;
            // 
            // txt_Solicitante
            // 
            this.txt_Solicitante.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Solicitante.Location = new System.Drawing.Point(117, 13);
            this.txt_Solicitante.MaxLength = 100;
            this.txt_Solicitante.Name = "txt_Solicitante";
            this.txt_Solicitante.ReadOnly = true;
            this.txt_Solicitante.Size = new System.Drawing.Size(279, 26);
            this.txt_Solicitante.TabIndex = 8;
            this.txt_Solicitante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(5, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 20);
            this.label9.TabIndex = 14;
            this.label9.Text = "Proveedor:";
            // 
            // txt_Departamento
            // 
            this.txt_Departamento.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Departamento.Location = new System.Drawing.Point(508, 13);
            this.txt_Departamento.MaxLength = 50;
            this.txt_Departamento.Name = "txt_Departamento";
            this.txt_Departamento.ReadOnly = true;
            this.txt_Departamento.Size = new System.Drawing.Size(244, 26);
            this.txt_Departamento.TabIndex = 9;
            this.txt_Departamento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_OI
            // 
            this.txt_OI.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_OI.Location = new System.Drawing.Point(196, 79);
            this.txt_OI.MaxLength = 150;
            this.txt_OI.Name = "txt_OI";
            this.txt_OI.Size = new System.Drawing.Size(361, 26);
            this.txt_OI.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Importe:  $";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(181, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = "No. de Orden de Impresion:";
            // 
            // txt_Importe
            // 
            this.txt_Importe.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Importe.Location = new System.Drawing.Point(117, 48);
            this.txt_Importe.MaxLength = 20;
            this.txt_Importe.Name = "txt_Importe";
            this.txt_Importe.ReadOnly = true;
            this.txt_Importe.Size = new System.Drawing.Size(131, 26);
            this.txt_Importe.TabIndex = 11;
            this.txt_Importe.Text = "0";
            this.txt_Importe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 20);
            this.label5.TabIndex = 31;
            this.label5.Text = "Fecha de Pago:";
            // 
            // date_Fecha
            // 
            this.date_Fecha.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_Fecha.Location = new System.Drawing.Point(552, 71);
            this.date_Fecha.MinDate = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.date_Fecha.Name = "date_Fecha";
            this.date_Fecha.Size = new System.Drawing.Size(196, 26);
            this.date_Fecha.TabIndex = 30;
            this.date_Fecha.Value = new System.DateTime(2023, 4, 5, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(496, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 29;
            this.label2.Text = "Fecha:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Factura,
            this.Pago,
            this.imp});
            this.dataGridView1.Location = new System.Drawing.Point(8, 287);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(804, 325);
            this.dataGridView1.TabIndex = 28;
            // 
            // Factura
            // 
            this.Factura.FillWeight = 200F;
            this.Factura.HeaderText = "No. Factura";
            this.Factura.MaxInputLength = 18;
            this.Factura.Name = "Factura";
            this.Factura.Width = 200;
            // 
            // Pago
            // 
            this.Pago.HeaderText = "Concepto de Pago";
            this.Pago.MaxInputLength = 65;
            this.Pago.Name = "Pago";
            this.Pago.Width = 500;
            // 
            // imp
            // 
            dataGridViewCellStyle2.Format = "n2";
            this.imp.DefaultCellStyle = dataGridViewCellStyle2;
            this.imp.HeaderText = "Importe";
            this.imp.MaxInputLength = 15;
            this.imp.Name = "imp";
            this.imp.ToolTipText = "0";
            // 
            // bunifuGradientPanel1
            // 
            this.bunifuGradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuGradientPanel1.BackgroundImage")));
            this.bunifuGradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuGradientPanel1.Controls.Add(this.label1);
            this.bunifuGradientPanel1.Controls.Add(this.panel1);
            this.bunifuGradientPanel1.Controls.Add(this.bunifuImageButton1);
            this.bunifuGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuGradientPanel1.GradientBottomLeft = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(148)))), ((int)(((byte)(178)))));
            this.bunifuGradientPanel1.GradientBottomRight = System.Drawing.Color.MidnightBlue;
            this.bunifuGradientPanel1.GradientTopLeft = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(169)))), ((int)(((byte)(199)))));
            this.bunifuGradientPanel1.GradientTopRight = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(80)))), ((int)(((byte)(166)))));
            this.bunifuGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.bunifuGradientPanel1.Name = "bunifuGradientPanel1";
            this.bunifuGradientPanel1.Quality = 10;
            this.bunifuGradientPanel1.Size = new System.Drawing.Size(1004, 25);
            this.bunifuGradientPanel1.TabIndex = 42;
            this.bunifuGradientPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bunifuGradientPanel1_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(432, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Solicitud de Cheque";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 20);
            this.panel1.TabIndex = 11;
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(978, 1);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(23, 23);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 10;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(25, 38);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(45, 20);
            this.label19.TabIndex = 43;
            this.label19.Text = "Folio:";
            // 
            // date_Pago
            // 
            this.date_Pago.CalendarFont = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_Pago.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_Pago.Location = new System.Drawing.Point(134, 72);
            this.date_Pago.Name = "date_Pago";
            this.date_Pago.Size = new System.Drawing.Size(214, 26);
            this.date_Pago.TabIndex = 44;
            // 
            // SolicitudCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1004, 623);
            this.Controls.Add(this.date_Pago);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.bunifuGradientPanel1);
            this.Controls.Add(this.btn_CalcularTotales);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txt_Folio);
            this.Controls.Add(this.btn_Generar);
            this.Controls.Add(this.Excepciones);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.date_Fecha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SolicitudCheque";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SolicitudCheque";
            ((System.ComponentModel.ISupportInitialize)(this.btn_CalcularTotales)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Generar)).EndInit();
            this.Excepciones.ResumeLayout(false);
            this.Excepciones.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.bunifuGradientPanel1.ResumeLayout(false);
            this.bunifuGradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Bunifu.Framework.UI.BunifuImageButton btn_CalcularTotales;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_Total;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_Retencion;
        private System.Windows.Forms.TextBox txt_iva;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Subtotal;
        private System.Windows.Forms.TextBox txt_Folio;
        private Bunifu.Framework.UI.BunifuImageButton btn_Generar;
        private System.Windows.Forms.GroupBox Excepciones;
        private System.Windows.Forms.ComboBox com_moneda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox chec_honorarios;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox chec_retencion;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RadioButton op_Iva_No;
        private System.Windows.Forms.RadioButton op_Iva_Si;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RichTextBox rich_NC_ND;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox com_Proveedor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_OC;
        private System.Windows.Forms.TextBox txt_Solicitante;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_Departamento;
        private System.Windows.Forms.TextBox txt_OI;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_Importe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker date_Fecha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Bunifu.Framework.UI.BunifuGradientPanel bunifuGradientPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker date_Pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn imp;
    }
}
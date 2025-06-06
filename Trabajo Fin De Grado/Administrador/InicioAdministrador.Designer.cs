namespace Trabajo_Fin_De_Grado.Administrador
{
    partial class InicioAdministrador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InicioAdministrador));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.informesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informeRecetasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.porUsuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todasLasRecetasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informeUsuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnEliminar);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1344, 712);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(545, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Listado de usuarios";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(570, 605);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(197, 36);
            this.btnEliminar.TabIndex = 1;
            this.btnEliminar.Text = "Eliminar Usuario";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 130);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1320, 442);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1344, 33);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // informesToolStripMenuItem
            // 
            this.informesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informeRecetasToolStripMenuItem,
            this.informeUsuariosToolStripMenuItem});
            this.informesToolStripMenuItem.Name = "informesToolStripMenuItem";
            this.informesToolStripMenuItem.Size = new System.Drawing.Size(99, 29);
            this.informesToolStripMenuItem.Text = "Informes";
            // 
            // informeRecetasToolStripMenuItem
            // 
            this.informeRecetasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.porUsuarioToolStripMenuItem,
            this.todasLasRecetasToolStripMenuItem});
            this.informeRecetasToolStripMenuItem.Name = "informeRecetasToolStripMenuItem";
            this.informeRecetasToolStripMenuItem.Size = new System.Drawing.Size(250, 34);
            this.informeRecetasToolStripMenuItem.Text = "Informe Recetas";
            // 
            // porUsuarioToolStripMenuItem
            // 
            this.porUsuarioToolStripMenuItem.Name = "porUsuarioToolStripMenuItem";
            this.porUsuarioToolStripMenuItem.Size = new System.Drawing.Size(250, 34);
            this.porUsuarioToolStripMenuItem.Text = "Por Usuario";
            this.porUsuarioToolStripMenuItem.Click += new System.EventHandler(this.porUsuarioToolStripMenuItem_Click);
            // 
            // todasLasRecetasToolStripMenuItem
            // 
            this.todasLasRecetasToolStripMenuItem.Name = "todasLasRecetasToolStripMenuItem";
            this.todasLasRecetasToolStripMenuItem.Size = new System.Drawing.Size(250, 34);
            this.todasLasRecetasToolStripMenuItem.Text = "Todas las Recetas";
            this.todasLasRecetasToolStripMenuItem.Click += new System.EventHandler(this.todasLasRecetasToolStripMenuItem_Click);
            // 
            // informeUsuariosToolStripMenuItem
            // 
            this.informeUsuariosToolStripMenuItem.Name = "informeUsuariosToolStripMenuItem";
            this.informeUsuariosToolStripMenuItem.Size = new System.Drawing.Size(250, 34);
            this.informeUsuariosToolStripMenuItem.Text = "Informe Usuarios";
            this.informeUsuariosToolStripMenuItem.Click += new System.EventHandler(this.informeUsuariosToolStripMenuItem_Click);
            // 
            // InicioAdministrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 712);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "InicioAdministrador";
            this.Text = "InicioAdministrador";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem informesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informeRecetasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informeUsuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem porUsuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem todasLasRecetasToolStripMenuItem;
    }
}
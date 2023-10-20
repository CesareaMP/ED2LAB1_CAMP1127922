namespace ED2LAB1_CAMP1127922
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.showmslbl = new System.Windows.Forms.Label();
            this.edTabControl = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dpibtn = new System.Windows.Forms.Button();
            this.dpitxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.nombrebtn = new System.Windows.Forms.Button();
            this.nombretxt = new System.Windows.Forms.TextBox();
            this.nombrelbl = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buscartasbtn = new System.Windows.Forms.Button();
            this.buscartastxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCartas = new System.Windows.Forms.Button();
            this.edTabControl.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cargar Archivos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // showmslbl
            // 
            this.showmslbl.AutoSize = true;
            this.showmslbl.Location = new System.Drawing.Point(170, 45);
            this.showmslbl.Name = "showmslbl";
            this.showmslbl.Size = new System.Drawing.Size(0, 13);
            this.showmslbl.TabIndex = 1;
            // 
            // edTabControl
            // 
            this.edTabControl.AllowDrop = true;
            this.edTabControl.Controls.Add(this.tabPage3);
            this.edTabControl.Controls.Add(this.tabPage1);
            this.edTabControl.Controls.Add(this.tabPage2);
            this.edTabControl.Enabled = false;
            this.edTabControl.Location = new System.Drawing.Point(12, 96);
            this.edTabControl.Multiline = true;
            this.edTabControl.Name = "edTabControl";
            this.edTabControl.SelectedIndex = 0;
            this.edTabControl.Size = new System.Drawing.Size(284, 209);
            this.edTabControl.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dpibtn);
            this.tabPage3.Controls.Add(this.dpitxt);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(276, 183);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Buscar Dpi";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dpibtn
            // 
            this.dpibtn.Location = new System.Drawing.Point(25, 64);
            this.dpibtn.Name = "dpibtn";
            this.dpibtn.Size = new System.Drawing.Size(219, 100);
            this.dpibtn.TabIndex = 2;
            this.dpibtn.Text = "Buscar";
            this.dpibtn.UseVisualStyleBackColor = true;
            this.dpibtn.Click += new System.EventHandler(this.dpibtn_Click);
            // 
            // dpitxt
            // 
            this.dpitxt.Location = new System.Drawing.Point(53, 27);
            this.dpitxt.Name = "dpitxt";
            this.dpitxt.Size = new System.Drawing.Size(191, 20);
            this.dpitxt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DPI";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.nombrebtn);
            this.tabPage1.Controls.Add(this.nombretxt);
            this.tabPage1.Controls.Add(this.nombrelbl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(276, 183);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Buscar Nombre";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // nombrebtn
            // 
            this.nombrebtn.Location = new System.Drawing.Point(25, 65);
            this.nombrebtn.Name = "nombrebtn";
            this.nombrebtn.Size = new System.Drawing.Size(219, 100);
            this.nombrebtn.TabIndex = 5;
            this.nombrebtn.Text = "Buscar";
            this.nombrebtn.UseVisualStyleBackColor = true;
            this.nombrebtn.Click += new System.EventHandler(this.nombrebtn_Click);
            // 
            // nombretxt
            // 
            this.nombretxt.Location = new System.Drawing.Point(72, 26);
            this.nombretxt.Name = "nombretxt";
            this.nombretxt.Size = new System.Drawing.Size(173, 20);
            this.nombretxt.TabIndex = 4;
            // 
            // nombrelbl
            // 
            this.nombrelbl.AutoSize = true;
            this.nombrelbl.Location = new System.Drawing.Point(22, 29);
            this.nombrelbl.Name = "nombrelbl";
            this.nombrelbl.Size = new System.Drawing.Size(44, 13);
            this.nombrelbl.TabIndex = 3;
            this.nombrelbl.Text = "Nombre";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buscartasbtn);
            this.tabPage2.Controls.Add(this.buscartastxt);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(276, 183);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "Buscar Cartas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buscartasbtn
            // 
            this.buscartasbtn.Location = new System.Drawing.Point(35, 66);
            this.buscartasbtn.Name = "buscartasbtn";
            this.buscartasbtn.Size = new System.Drawing.Size(208, 96);
            this.buscartasbtn.TabIndex = 2;
            this.buscartasbtn.Text = "Buscar";
            this.buscartasbtn.UseVisualStyleBackColor = true;
            this.buscartasbtn.Click += new System.EventHandler(this.buscartasbtn_Click);
            // 
            // buscartastxt
            // 
            this.buscartastxt.Location = new System.Drawing.Point(101, 29);
            this.buscartastxt.Name = "buscartastxt";
            this.buscartastxt.Size = new System.Drawing.Size(142, 20);
            this.buscartastxt.TabIndex = 1;
            this.buscartastxt.TextChanged += new System.EventHandler(this.buscartastxt_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ingrese DPI";
            // 
            // btnCartas
            // 
            this.btnCartas.Enabled = false;
            this.btnCartas.Location = new System.Drawing.Point(12, 54);
            this.btnCartas.Name = "btnCartas";
            this.btnCartas.Size = new System.Drawing.Size(132, 36);
            this.btnCartas.TabIndex = 3;
            this.btnCartas.Text = "Cargar Cartas";
            this.btnCartas.UseVisualStyleBackColor = true;
            this.btnCartas.Click += new System.EventHandler(this.btnCartas_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 317);
            this.Controls.Add(this.btnCartas);
            this.Controls.Add(this.edTabControl);
            this.Controls.Add(this.showmslbl);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Talent Hub";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.edTabControl.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label showmslbl;
        private System.Windows.Forms.TabControl edTabControl;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox dpitxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox nombretxt;
        private System.Windows.Forms.Label nombrelbl;
        private System.Windows.Forms.Button dpibtn;
        private System.Windows.Forms.Button nombrebtn;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buscartasbtn;
        private System.Windows.Forms.TextBox buscartastxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCartas;
    }
}


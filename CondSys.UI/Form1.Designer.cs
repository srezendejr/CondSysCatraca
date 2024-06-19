namespace CondSys.UI
{
    partial class frmConfiguracao
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
            this.btnConectar = new System.Windows.Forms.Button();
            this.lblIp = new System.Windows.Forms.Label();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.lblPorta = new System.Windows.Forms.Label();
            this.gbVisitantes = new System.Windows.Forms.GroupBox();
            this.btnStatus = new System.Windows.Forms.Button();
            this.btnDesconcetar = new System.Windows.Forms.Button();
            this.mtxtBoxIp = new System.Windows.Forms.MaskedTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.gbVisitantes.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(17, 71);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(75, 23);
            this.btnConectar.TabIndex = 2;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(14, 19);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(57, 13);
            this.lblIp.TabIndex = 1;
            this.lblIp.Text = "Número IP";
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(77, 45);
            this.txtPorta.MaxLength = 6;
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(100, 20);
            this.txtPorta.TabIndex = 1;
            this.txtPorta.Text = "3000";
            // 
            // lblPorta
            // 
            this.lblPorta.AutoSize = true;
            this.lblPorta.Location = new System.Drawing.Point(14, 45);
            this.lblPorta.Name = "lblPorta";
            this.lblPorta.Size = new System.Drawing.Size(32, 13);
            this.lblPorta.TabIndex = 3;
            this.lblPorta.Text = "Porta";
            // 
            // gbVisitantes
            // 
            this.gbVisitantes.Controls.Add(this.btnStatus);
            this.gbVisitantes.Controls.Add(this.btnDesconcetar);
            this.gbVisitantes.Controls.Add(this.mtxtBoxIp);
            this.gbVisitantes.Controls.Add(this.btnConectar);
            this.gbVisitantes.Controls.Add(this.lblIp);
            this.gbVisitantes.Controls.Add(this.txtPorta);
            this.gbVisitantes.Controls.Add(this.lblPorta);
            this.gbVisitantes.Location = new System.Drawing.Point(12, 12);
            this.gbVisitantes.Name = "gbVisitantes";
            this.gbVisitantes.Size = new System.Drawing.Size(210, 103);
            this.gbVisitantes.TabIndex = 6;
            this.gbVisitantes.TabStop = false;
            this.gbVisitantes.Text = "Visitantes";
            // 
            // btnStatus
            // 
            this.btnStatus.BackColor = System.Drawing.Color.Red;
            this.btnStatus.FlatAppearance.BorderSize = 0;
            this.btnStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatus.Location = new System.Drawing.Point(184, 30);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(20, 19);
            this.btnStatus.TabIndex = 7;
            this.btnStatus.UseVisualStyleBackColor = false;
            // 
            // btnDesconcetar
            // 
            this.btnDesconcetar.Location = new System.Drawing.Point(102, 71);
            this.btnDesconcetar.Name = "btnDesconcetar";
            this.btnDesconcetar.Size = new System.Drawing.Size(75, 23);
            this.btnDesconcetar.TabIndex = 3;
            this.btnDesconcetar.Text = "Desconectar";
            this.btnDesconcetar.UseVisualStyleBackColor = true;
            this.btnDesconcetar.Click += new System.EventHandler(this.btnDesconcetar_Click);
            // 
            // mtxtBoxIp
            // 
            this.mtxtBoxIp.Location = new System.Drawing.Point(77, 12);
            this.mtxtBoxIp.Name = "mtxtBoxIp";
            this.mtxtBoxIp.Size = new System.Drawing.Size(100, 20);
            this.mtxtBoxIp.TabIndex = 0;
            this.mtxtBoxIp.Text = "192.168.0.104";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(52, 165);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(317, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmConfiguracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 366);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gbVisitantes);
            this.Name = "frmConfiguracao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfiguracao_FormClosing);
            this.gbVisitantes.ResumeLayout(false);
            this.gbVisitantes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.TextBox txtPorta;
        private System.Windows.Forms.Label lblPorta;
        private System.Windows.Forms.GroupBox gbVisitantes;
        private System.Windows.Forms.MaskedTextBox mtxtBoxIp;
        private System.Windows.Forms.Button btnDesconcetar;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}


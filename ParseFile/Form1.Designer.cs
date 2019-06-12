namespace ParseFile
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.tbPosx = new System.Windows.Forms.TextBox();
            this.labelposx = new System.Windows.Forms.Label();
            this.labelposy = new System.Windows.Forms.Label();
            this.tbPosy = new System.Windows.Forms.TextBox();
            this.cbSelectedMeasure = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(56, 64);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(98, 39);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Открыть фаил";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 109);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(368, 381);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // zedGraphControl
            // 
            this.zedGraphControl.IsShowPointValues = false;
            this.zedGraphControl.Location = new System.Drawing.Point(521, 64);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.PointValueFormat = "G";
            this.zedGraphControl.Size = new System.Drawing.Size(588, 443);
            this.zedGraphControl.TabIndex = 3;
            this.zedGraphControl.Load += new System.EventHandler(this.zedGraphControl1_Load);
            this.zedGraphControl.Click += new System.EventHandler(this.zedGraphControl_Click);
            // 
            // tbPosx
            // 
            this.tbPosx.Location = new System.Drawing.Point(54, 12);
            this.tbPosx.Name = "tbPosx";
            this.tbPosx.Size = new System.Drawing.Size(100, 20);
            this.tbPosx.TabIndex = 4;
            // 
            // labelposx
            // 
            this.labelposx.AutoSize = true;
            this.labelposx.Location = new System.Drawing.Point(7, 15);
            this.labelposx.Name = "labelposx";
            this.labelposx.Size = new System.Drawing.Size(33, 13);
            this.labelposx.TabIndex = 5;
            this.labelposx.Text = "Pos x";
            this.labelposx.Click += new System.EventHandler(this.label2_Click);
            // 
            // labelposy
            // 
            this.labelposy.AutoSize = true;
            this.labelposy.Location = new System.Drawing.Point(7, 41);
            this.labelposy.Name = "labelposy";
            this.labelposy.Size = new System.Drawing.Size(33, 13);
            this.labelposy.TabIndex = 7;
            this.labelposy.Text = "Pos y";
            // 
            // tbPosy
            // 
            this.tbPosy.Location = new System.Drawing.Point(54, 38);
            this.tbPosy.Name = "tbPosy";
            this.tbPosy.Size = new System.Drawing.Size(100, 20);
            this.tbPosy.TabIndex = 6;
            // 
            // cbSelectedMeasure
            // 
            this.cbSelectedMeasure.FormattingEnabled = true;
            this.cbSelectedMeasure.Location = new System.Drawing.Point(196, 33);
            this.cbSelectedMeasure.Name = "cbSelectedMeasure";
            this.cbSelectedMeasure.Size = new System.Drawing.Size(184, 21);
            this.cbSelectedMeasure.TabIndex = 8;
            this.cbSelectedMeasure.SelectedIndexChanged += new System.EventHandler(this.cbSelectedMeasure_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select measure";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 600);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSelectedMeasure);
            this.Controls.Add(this.labelposy);
            this.Controls.Add(this.tbPosy);
            this.Controls.Add(this.labelposx);
            this.Controls.Add(this.tbPosx);
            this.Controls.Add(this.zedGraphControl);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOpenFile);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ZedGraph.ZedGraphControl zedGraphControl;
        private System.Windows.Forms.TextBox tbPosx;
        private System.Windows.Forms.Label labelposx;
        private System.Windows.Forms.Label labelposy;
        private System.Windows.Forms.TextBox tbPosy;
        private System.Windows.Forms.ComboBox cbSelectedMeasure;
        private System.Windows.Forms.Label label1;
    }
}


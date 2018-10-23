namespace PRT_z_csv
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
            this.wczytaj_csv = new System.Windows.Forms.Button();
            this.table = new System.Windows.Forms.DataGridView();
            this.familyList = new System.Windows.Forms.ListBox();
            this.failList = new System.Windows.Forms.ListBox();
            this.steptextList = new System.Windows.Forms.CheckedListBox();
            this.Top5 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Results = new System.Windows.Forms.TabPage();
            this.Analyse = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.Report = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Top5)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.Results.SuspendLayout();
            this.Analyse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // wczytaj_csv
            // 
            this.wczytaj_csv.Location = new System.Drawing.Point(6, 6);
            this.wczytaj_csv.Name = "wczytaj_csv";
            this.wczytaj_csv.Size = new System.Drawing.Size(75, 23);
            this.wczytaj_csv.TabIndex = 0;
            this.wczytaj_csv.Text = "Wczytaj plik csv";
            this.wczytaj_csv.UseVisualStyleBackColor = true;
            this.wczytaj_csv.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // table
            // 
            this.table.AllowUserToAddRows = false;
            this.table.AllowUserToDeleteRows = false;
            this.table.AllowUserToOrderColumns = true;
            this.table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.table.Location = new System.Drawing.Point(6, 117);
            this.table.Name = "table";
            this.table.ReadOnly = true;
            this.table.Size = new System.Drawing.Size(1085, 385);
            this.table.TabIndex = 1;
            // 
            // familyList
            // 
            this.familyList.FormattingEnabled = true;
            this.familyList.Location = new System.Drawing.Point(87, 3);
            this.familyList.Name = "familyList";
            this.familyList.Size = new System.Drawing.Size(179, 108);
            this.familyList.TabIndex = 4;
            this.familyList.SelectedIndexChanged += new System.EventHandler(this.chooseFamily_ClickList);
            // 
            // failList
            // 
            this.failList.FormattingEnabled = true;
            this.failList.Location = new System.Drawing.Point(453, 3);
            this.failList.Name = "failList";
            this.failList.Size = new System.Drawing.Size(275, 108);
            this.failList.TabIndex = 5;
            this.failList.SelectedIndexChanged += new System.EventHandler(this.chooseFail_ClickList);
            // 
            // steptextList
            // 
            this.steptextList.CheckOnClick = true;
            this.steptextList.FormattingEnabled = true;
            this.steptextList.Location = new System.Drawing.Point(272, 2);
            this.steptextList.Name = "steptextList";
            this.steptextList.Size = new System.Drawing.Size(175, 109);
            this.steptextList.Sorted = true;
            this.steptextList.TabIndex = 8;
            this.steptextList.SelectedIndexChanged += new System.EventHandler(this.chooseSteptext_ClickList);
            // 
            // Top5
            // 
            this.Top5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Top5.Location = new System.Drawing.Point(734, 4);
            this.Top5.Name = "Top5";
            this.Top5.Size = new System.Drawing.Size(357, 107);
            this.Top5.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Results);
            this.tabControl1.Controls.Add(this.Analyse);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1570, 545);
            this.tabControl1.TabIndex = 10;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // Results
            // 
            this.Results.Controls.Add(this.wczytaj_csv);
            this.Results.Controls.Add(this.table);
            this.Results.Controls.Add(this.dataGridView1);
            this.Results.Controls.Add(this.Top5);
            this.Results.Controls.Add(this.familyList);
            this.Results.Controls.Add(this.failList);
            this.Results.Controls.Add(this.steptextList);
            this.Results.Location = new System.Drawing.Point(4, 22);
            this.Results.Name = "Results";
            this.Results.Padding = new System.Windows.Forms.Padding(3);
            this.Results.Size = new System.Drawing.Size(1562, 519);
            this.Results.TabIndex = 0;
            this.Results.Text = "Wyniki";
            this.Results.UseVisualStyleBackColor = true;
            // 
            // Analyse
            // 
            this.Analyse.Controls.Add(this.button1);
            this.Analyse.Controls.Add(this.Report);
            this.Analyse.Location = new System.Drawing.Point(4, 22);
            this.Analyse.Name = "Analyse";
            this.Analyse.Size = new System.Drawing.Size(1319, 615);
            this.Analyse.TabIndex = 1;
            this.Analyse.Text = "Analiza";
            this.Analyse.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(579, 546);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Report
            // 
            this.Report.FormattingEnabled = true;
            this.Report.Location = new System.Drawing.Point(0, 0);
            this.Report.Name = "Report";
            this.Report.Size = new System.Drawing.Size(1319, 615);
            this.Report.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1107, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(438, 221);
            this.dataGridView1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1570, 545);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Top5)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.Results.ResumeLayout(false);
            this.Analyse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button wczytaj_csv;
        private System.Windows.Forms.DataGridView table;
        private System.Windows.Forms.ListBox familyList;
        private System.Windows.Forms.ListBox failList;
        private System.Windows.Forms.CheckedListBox steptextList;
        private System.Windows.Forms.DataGridView Top5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Results;
        private System.Windows.Forms.TabPage Analyse;
        private System.Windows.Forms.ListBox Report;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}


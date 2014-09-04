namespace TelerikMovieDatabase.UIClient
{
    partial class frmTMDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTMDB));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cmbImport = new System.Windows.Forms.ComboBox();
            this.cmbExport = new System.Windows.Forms.ComboBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.cmbExportInfo = new System.Windows.Forms.ComboBox();
            this.btnOpenReport = new System.Windows.Forms.Button();
            this.btnMovies = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.groupCategories = new System.Windows.Forms.GroupBox();
            this.chkAllInfo = new System.Windows.Forms.CheckBox();
            this.chkAwards = new System.Windows.Forms.CheckBox();
            this.chkWriters = new System.Windows.Forms.CheckBox();
            this.chkActors = new System.Windows.Forms.CheckBox();
            this.chkDirector = new System.Windows.Forms.CheckBox();
            this.chkGenre = new System.Windows.Forms.CheckBox();
            this.listInfo = new System.Windows.Forms.ListBox();
            this.groupCategories.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1230, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cmbImport
            // 
            this.cmbImport.FormattingEnabled = true;
            this.cmbImport.ItemHeight = 13;
            this.cmbImport.Location = new System.Drawing.Point(12, 407);
            this.cmbImport.Name = "cmbImport";
            this.cmbImport.Size = new System.Drawing.Size(121, 21);
            this.cmbImport.TabIndex = 1;
            this.cmbImport.TabStop = false;
            this.cmbImport.Text = "Import to...";
            // 
            // cmbExport
            // 
            this.cmbExport.FormattingEnabled = true;
            this.cmbExport.Items.AddRange(new object[] {
            "MongoDB",
            "MySQL",
            "SQLite",
            "XLS",
            "XML"});
            this.cmbExport.Location = new System.Drawing.Point(12, 366);
            this.cmbExport.Name = "cmbExport";
            this.cmbExport.Size = new System.Drawing.Size(121, 21);
            this.cmbExport.TabIndex = 2;
            this.cmbExport.TabStop = false;
            this.cmbExport.Text = "Export from..";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(12, 319);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 25);
            this.btnImport.TabIndex = 3;
            this.btnImport.TabStop = false;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // cmbExportInfo
            // 
            this.cmbExportInfo.FormattingEnabled = true;
            this.cmbExportInfo.Items.AddRange(new object[] {
            "JSON",
            "PDF",
            "XML",
            "XLS"});
            this.cmbExportInfo.Location = new System.Drawing.Point(12, 487);
            this.cmbExportInfo.Name = "cmbExportInfo";
            this.cmbExportInfo.Size = new System.Drawing.Size(121, 21);
            this.cmbExportInfo.TabIndex = 4;
            this.cmbExportInfo.TabStop = false;
            this.cmbExportInfo.Text = "Export from...";
            // 
            // btnOpenReport
            // 
            this.btnOpenReport.Location = new System.Drawing.Point(12, 449);
            this.btnOpenReport.Name = "btnOpenReport";
            this.btnOpenReport.Size = new System.Drawing.Size(75, 25);
            this.btnOpenReport.TabIndex = 5;
            this.btnOpenReport.TabStop = false;
            this.btnOpenReport.Text = "Open report";
            this.btnOpenReport.UseVisualStyleBackColor = true;
            // 
            // btnMovies
            // 
            this.btnMovies.Location = new System.Drawing.Point(12, 24);
            this.btnMovies.Name = "btnMovies";
            this.btnMovies.Size = new System.Drawing.Size(75, 25);
            this.btnMovies.TabIndex = 7;
            this.btnMovies.Text = "All movies";
            this.btnMovies.UseVisualStyleBackColor = true;
            this.btnMovies.Click += new System.EventHandler(this.btnMovies_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(11, 66);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(121, 25);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search for movie";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 108);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(120, 20);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // groupCategories
            // 
            this.groupCategories.Controls.Add(this.chkAllInfo);
            this.groupCategories.Controls.Add(this.chkAwards);
            this.groupCategories.Controls.Add(this.chkWriters);
            this.groupCategories.Controls.Add(this.chkActors);
            this.groupCategories.Controls.Add(this.chkDirector);
            this.groupCategories.Controls.Add(this.chkGenre);
            this.groupCategories.Location = new System.Drawing.Point(12, 146);
            this.groupCategories.Name = "groupCategories";
            this.groupCategories.Size = new System.Drawing.Size(121, 157);
            this.groupCategories.TabIndex = 10;
            this.groupCategories.TabStop = false;
            this.groupCategories.Text = "Categories";
            // 
            // chkAllInfo
            // 
            this.chkAllInfo.AutoSize = true;
            this.chkAllInfo.Location = new System.Drawing.Point(6, 136);
            this.chkAllInfo.Name = "chkAllInfo";
            this.chkAllInfo.Size = new System.Drawing.Size(92, 17);
            this.chkAllInfo.TabIndex = 15;
            this.chkAllInfo.Text = "All Information";
            this.chkAllInfo.UseVisualStyleBackColor = true;
            // 
            // chkAwards
            // 
            this.chkAwards.AutoSize = true;
            this.chkAwards.Location = new System.Drawing.Point(6, 113);
            this.chkAwards.Name = "chkAwards";
            this.chkAwards.Size = new System.Drawing.Size(61, 17);
            this.chkAwards.TabIndex = 14;
            this.chkAwards.Text = "Awards";
            this.chkAwards.UseVisualStyleBackColor = true;
            // 
            // chkWriters
            // 
            this.chkWriters.AutoSize = true;
            this.chkWriters.Location = new System.Drawing.Point(6, 89);
            this.chkWriters.Name = "chkWriters";
            this.chkWriters.Size = new System.Drawing.Size(59, 17);
            this.chkWriters.TabIndex = 13;
            this.chkWriters.Text = "Writers";
            this.chkWriters.UseVisualStyleBackColor = true;
            // 
            // chkActors
            // 
            this.chkActors.AutoSize = true;
            this.chkActors.Location = new System.Drawing.Point(7, 66);
            this.chkActors.Name = "chkActors";
            this.chkActors.Size = new System.Drawing.Size(56, 17);
            this.chkActors.TabIndex = 12;
            this.chkActors.Text = "Actors";
            this.chkActors.UseVisualStyleBackColor = true;
            // 
            // chkDirector
            // 
            this.chkDirector.AutoSize = true;
            this.chkDirector.Location = new System.Drawing.Point(7, 43);
            this.chkDirector.Name = "chkDirector";
            this.chkDirector.Size = new System.Drawing.Size(63, 17);
            this.chkDirector.TabIndex = 11;
            this.chkDirector.Text = "Director";
            this.chkDirector.UseVisualStyleBackColor = true;
            // 
            // chkGenre
            // 
            this.chkGenre.AutoSize = true;
            this.chkGenre.Location = new System.Drawing.Point(7, 20);
            this.chkGenre.Name = "chkGenre";
            this.chkGenre.Size = new System.Drawing.Size(55, 17);
            this.chkGenre.TabIndex = 0;
            this.chkGenre.Text = "Genre";
            this.chkGenre.UseVisualStyleBackColor = true;
            // 
            // listInfo
            // 
            this.listInfo.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listInfo.FormattingEnabled = true;
            this.listInfo.Location = new System.Drawing.Point(147, 24);
            this.listInfo.Name = "listInfo";
            this.listInfo.Size = new System.Drawing.Size(1071, 485);
            this.listInfo.TabIndex = 11;
            // 
            // frmTMDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 529);
            this.Controls.Add(this.listInfo);
            this.Controls.Add(this.groupCategories);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnMovies);
            this.Controls.Add(this.btnOpenReport);
            this.Controls.Add(this.cmbExportInfo);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.cmbExport);
            this.Controls.Add(this.cmbImport);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmTMDB";
            this.Text = "Telerik Movie Database";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupCategories.ResumeLayout(false);
            this.groupCategories.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ComboBox cmbImport;
        private System.Windows.Forms.ComboBox cmbExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ComboBox cmbExportInfo;
        private System.Windows.Forms.Button btnOpenReport;
        private System.Windows.Forms.Button btnMovies;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.GroupBox groupCategories;
        private System.Windows.Forms.CheckBox chkAllInfo;
        private System.Windows.Forms.CheckBox chkAwards;
        private System.Windows.Forms.CheckBox chkWriters;
        private System.Windows.Forms.CheckBox chkActors;
        private System.Windows.Forms.CheckBox chkDirector;
        private System.Windows.Forms.CheckBox chkGenre;
        private System.Windows.Forms.ListBox listInfo;
    }
}


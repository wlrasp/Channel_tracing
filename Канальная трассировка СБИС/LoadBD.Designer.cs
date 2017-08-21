namespace Канальная_трассировка_СБИС
{
    partial class LoadBD
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.сБИСBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kanalDataSet = new Канальная_трассировка_СБИС.kanalDataSet();
            this.сБИСTableAdapter = new Канальная_трассировка_СБИС.kanalDataSetTableAdapters.СБИСTableAdapter();
            this.kanalDataSet1 = new Канальная_трассировка_СБИС.kanalDataSet();
            this.tableAdapterManager1 = new Канальная_трассировка_СБИС.kanalDataSetTableAdapters.TableAdapterManager();
            this.каналыTableAdapter1 = new Канальная_трассировка_СБИС.kanalDataSetTableAdapters.КаналыTableAdapter();
            this.перечни_электрических_соединенийTableAdapter1 = new Канальная_трассировка_СБИС.kanalDataSetTableAdapters.Перечни_электрических_соединенийTableAdapter();
            this.сбисTableAdapter1 = new Канальная_трассировка_СБИС.kanalDataSetTableAdapters.СБИСTableAdapter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.сБИСBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kanalDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kanalDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 90);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(147, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Выбор № СБИС";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 58);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(370, 26);
            this.button3.TabIndex = 14;
            this.button3.Text = "Загрузить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.сБИСBindingSource;
            this.comboBox1.DisplayMember = "№ СБИС";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(9, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(370, 21);
            this.comboBox1.TabIndex = 15;
            // 
            // сБИСBindingSource
            // 
            this.сБИСBindingSource.DataMember = "СБИС";
            this.сБИСBindingSource.DataSource = this.kanalDataSet;
            // 
            // kanalDataSet
            // 
            this.kanalDataSet.DataSetName = "kanalDataSet";
            this.kanalDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // сБИСTableAdapter
            // 
            this.сБИСTableAdapter.ClearBeforeFill = true;
            // 
            // kanalDataSet1
            // 
            this.kanalDataSet1.DataSetName = "kanalDataSet";
            this.kanalDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tableAdapterManager1
            // 
            this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager1.UpdateOrder = Канальная_трассировка_СБИС.kanalDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager1.КаналыTableAdapter = this.каналыTableAdapter1;
            this.tableAdapterManager1.Перечни_электрических_соединенийTableAdapter = this.перечни_электрических_соединенийTableAdapter1;
            this.tableAdapterManager1.СБИСTableAdapter = this.сБИСTableAdapter;
            // 
            // каналыTableAdapter1
            // 
            this.каналыTableAdapter1.ClearBeforeFill = true;
            // 
            // перечни_электрических_соединенийTableAdapter1
            // 
            this.перечни_электрических_соединенийTableAdapter1.ClearBeforeFill = true;
            // 
            // сбисTableAdapter1
            // 
            this.сбисTableAdapter1.ClearBeforeFill = true;
            // 
            // LoadBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(409, 114);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadBD";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Загрузить из базы данных";
            this.Load += new System.EventHandler(this.LoadBD_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.сБИСBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kanalDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kanalDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBox1;
        private kanalDataSet kanalDataSet;
        private System.Windows.Forms.BindingSource сБИСBindingSource;
        private kanalDataSetTableAdapters.СБИСTableAdapter сБИСTableAdapter;
        private kanalDataSet kanalDataSet1;
        private kanalDataSetTableAdapters.TableAdapterManager tableAdapterManager1;
        private kanalDataSetTableAdapters.КаналыTableAdapter каналыTableAdapter1;
        private kanalDataSetTableAdapters.Перечни_электрических_соединенийTableAdapter перечни_электрических_соединенийTableAdapter1;
        private kanalDataSetTableAdapters.СБИСTableAdapter сбисTableAdapter1;
    }
}
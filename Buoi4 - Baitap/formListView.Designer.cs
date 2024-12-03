namespace Buoi4___Baitap
{
    partial class formListView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dtg_ListView = new DataGridView();
            btn_Them = new Button();
            btn_Sua = new Button();
            btn_Xoa = new Button();
            btn_Dong = new Button();
            ((System.ComponentModel.ISupportInitialize)dtg_ListView).BeginInit();
            SuspendLayout();
            // 
            // dtg_ListView
            // 
            dtg_ListView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_ListView.Location = new Point(12, 12);
            dtg_ListView.Name = "dtg_ListView";
            dtg_ListView.RowHeadersWidth = 51;
            dtg_ListView.Size = new Size(640, 426);
            dtg_ListView.TabIndex = 0;
            // 
            // btn_Them
            // 
            btn_Them.Location = new Point(676, 12);
            btn_Them.Name = "btn_Them";
            btn_Them.Size = new Size(94, 29);
            btn_Them.TabIndex = 1;
            btn_Them.Text = "Thêm";
            btn_Them.UseVisualStyleBackColor = true;
            btn_Them.Click += btn_Them_Click;
            // 
            // btn_Sua
            // 
            btn_Sua.Location = new Point(676, 59);
            btn_Sua.Name = "btn_Sua";
            btn_Sua.Size = new Size(94, 29);
            btn_Sua.TabIndex = 2;
            btn_Sua.Text = "Sửa";
            btn_Sua.UseVisualStyleBackColor = true;
            btn_Sua.Click += btn_Sua_Click;
            // 
            // btn_Xoa
            // 
            btn_Xoa.Location = new Point(676, 106);
            btn_Xoa.Name = "btn_Xoa";
            btn_Xoa.Size = new Size(94, 29);
            btn_Xoa.TabIndex = 3;
            btn_Xoa.Text = "Xóa";
            btn_Xoa.UseVisualStyleBackColor = true;
            btn_Xoa.Click += btn_Xoa_Click;
            // 
            // btn_Dong
            // 
            btn_Dong.Location = new Point(676, 154);
            btn_Dong.Name = "btn_Dong";
            btn_Dong.Size = new Size(94, 29);
            btn_Dong.TabIndex = 4;
            btn_Dong.Text = "Đóng";
            btn_Dong.UseVisualStyleBackColor = true;
            btn_Dong.Click += btn_Dong_Click;
            // 
            // formListView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_Dong);
            Controls.Add(btn_Xoa);
            Controls.Add(btn_Sua);
            Controls.Add(btn_Them);
            Controls.Add(dtg_ListView);
            Name = "formListView";
            Text = "List View";
            ((System.ComponentModel.ISupportInitialize)dtg_ListView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dtg_ListView;
        private Button btn_Them;
        private Button btn_Sua;
        private Button btn_Xoa;
        private Button btn_Dong;
    }
}

namespace Buoi4___Baitap
{
    partial class formNhanVien
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txt_MSSV = new TextBox();
            txt_TenNV = new TextBox();
            txt_LuongCB = new TextBox();
            btn_DongY = new Button();
            btn_BoQua = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "MSSV:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 53);
            label2.Name = "label2";
            label2.Size = new Size(102, 20);
            label2.TabIndex = 1;
            label2.Text = "Tên nhân viên:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 100);
            label3.Name = "label3";
            label3.Size = new Size(110, 20);
            label3.TabIndex = 2;
            label3.Text = "Lương căn bản:";
            // 
            // txt_MSSV
            // 
            txt_MSSV.Location = new Point(130, 6);
            txt_MSSV.Name = "txt_MSSV";
            txt_MSSV.Size = new Size(141, 27);
            txt_MSSV.TabIndex = 3;
            // 
            // txt_TenNV
            // 
            txt_TenNV.Location = new Point(130, 50);
            txt_TenNV.Name = "txt_TenNV";
            txt_TenNV.Size = new Size(231, 27);
            txt_TenNV.TabIndex = 4;
            // 
            // txt_LuongCB
            // 
            txt_LuongCB.Location = new Point(130, 97);
            txt_LuongCB.Name = "txt_LuongCB";
            txt_LuongCB.Size = new Size(125, 27);
            txt_LuongCB.TabIndex = 5;
            // 
            // btn_DongY
            // 
            btn_DongY.Location = new Point(41, 152);
            btn_DongY.Name = "btn_DongY";
            btn_DongY.Size = new Size(94, 29);
            btn_DongY.TabIndex = 6;
            btn_DongY.Text = "Đồng ý";
            btn_DongY.UseVisualStyleBackColor = true;
            btn_DongY.Click += btn_DongY_Click;
            // 
            // btn_BoQua
            // 
            btn_BoQua.Location = new Point(191, 152);
            btn_BoQua.Name = "btn_BoQua";
            btn_BoQua.Size = new Size(94, 29);
            btn_BoQua.TabIndex = 7;
            btn_BoQua.Text = "Bỏ qua";
            btn_BoQua.UseVisualStyleBackColor = true;
            // 
            // formNhanVien
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(418, 203);
            Controls.Add(btn_BoQua);
            Controls.Add(btn_DongY);
            Controls.Add(txt_LuongCB);
            Controls.Add(txt_TenNV);
            Controls.Add(txt_MSSV);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "formNhanVien";
            Text = "Nhân viên";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txt_MSSV;
        private TextBox txt_TenNV;
        private TextBox txt_LuongCB;
        private Button btn_DongY;
        private Button btn_BoQua;
    }
}
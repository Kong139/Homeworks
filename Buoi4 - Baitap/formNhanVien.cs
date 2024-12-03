using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buoi4___Baitap
{
    public partial class formNhanVien : Form
    {
        public formNhanVien()
        {
            InitializeComponent();
        }

        public Nhanvien NhanVienMoi {  get; set; }
        private void btn_DongY_Click(object sender, EventArgs e)
        {
            NhanVienMoi = new Nhanvien
            {
                MSNV = txt_MSSV.Text,
                TenNV = txt_TenNV.Text,
                LuongCB = int.Parse(txt_LuongCB.Text)
            };
            DialogResult = DialogResult.OK;
        }
    }
}

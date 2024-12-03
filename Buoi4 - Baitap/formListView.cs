using System.Windows.Forms;

namespace Buoi4___Baitap
{
    public partial class formListView : Form
    {
        public formListView()
        {
            InitializeComponent();
        }

        private List<Nhanvien> nhanviens = new List<Nhanvien>();
        private void LoadData()
        {
            dtg_ListView.DataSource = null;
            dtg_ListView.DataSource = nhanviens;
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            formNhanVien form = new formNhanVien();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (nhanviens.Any(n => n.MSNV == form.NhanVienMoi.MSNV))
                {
                    MessageBox.Show("Mã số nhân viên đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    nhanviens.Add(form.NhanVienMoi);
                    LoadData();
                }
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (dtg_ListView.SelectedRows.Count > 0)
            {
                formNhanVien form = new formNhanVien();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    int index = dtg_ListView.SelectedRows[0].Index;
                    string manv = nhanviens[index].MSNV;
                    nhanviens[index].MSNV = "";
                    if (nhanviens.Any(n => n.MSNV == form.NhanVienMoi.MSNV))
                    {
                        MessageBox.Show("Mã số nhân viên đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        nhanviens[index].MSNV = manv;
                    }
                    else
                    {
                        nhanviens[index] = form.NhanVienMoi;
                        LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (dtg_ListView.SelectedRows.Count > 0)
            {
                int index = dtg_ListView.SelectedRows[0].Index;
                DialogResult result = MessageBox.Show("Xác nhận muốn xóa nhân viên này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    nhanviens.RemoveAt(index);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

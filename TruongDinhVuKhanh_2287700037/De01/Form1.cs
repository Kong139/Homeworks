using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using De01.Models;

namespace De01
{
    public partial class frmSinhvien : Form
    {
        StudentContextDB context = new StudentContextDB();
        private List<Sinhvien> backupList = new List<Sinhvien>();

        public frmSinhvien()
        {
            InitializeComponent();
        }

        private void frmSinhvien_Load(object sender, EventArgs e)
        {
            try
            {
                List<Lop> listLop = context.Lops.ToList(); //lấy các lớp 
                List<Sinhvien> listSinhvien = context.Sinhviens.Include("Lop").ToList(); //lấy sinh viên
                FillLopCombobox(listLop);
                BindGrid(listSinhvien);
                backupList = new List<Sinhvien>(listSinhvien);
                btLuu.Enabled = false;
                btKhong.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Hàm binding list có tên hiển thị là tên lớp, giá trị là Mã lớp
        private void FillLopCombobox(List<Lop> listLop)
        {
            this.cboLop.DataSource = listLop;
            this.cboLop.DisplayMember = "TenLop";
            this.cboLop.ValueMember = "MaLop";
        }

        //Hàm binding gridView
        private void BindGrid(List<Sinhvien> studentList)
        {
            dtgSinhvien.Rows.Clear();
            using (var db = new StudentContextDB())
            {
                foreach (var item in studentList)
                {
                    int index = dtgSinhvien.Rows.Add();
                    dtgSinhvien.Rows[index].Cells[0].Value = item.MaSV;
                    dtgSinhvien.Rows[index].Cells[1].Value = item.HotenSV;
                    dtgSinhvien.Rows[index].Cells[2].Value = item.NgaySinh;
                    dtgSinhvien.Rows[index].Cells[3].Value = item.Lop.TenLop;
                }
            }
        }

        private void dtgSinhvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra nếu dòng hợp lệ
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dtgSinhvien.Rows[e.RowIndex];

                // Gán dữ liệu từ dòng vào các ô nhập liệu
                txtMaSV.Text = selectedRow.Cells[0].Value?.ToString() ?? "";
                txtHotenSV.Text = selectedRow.Cells[1].Value?.ToString() ?? "";
                dtNgaysinh.Text = selectedRow.Cells[2].Value?.ToString() ?? "";
                cboLop.Text = selectedRow.Cells[3].Value?.ToString() ?? "";
            }
        }

        private void dtgSinhvien_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            btLuu.Enabled = true;
            btKhong.Enabled = true;
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            try
            {
                List<Sinhvien> studentList = context.Sinhviens.ToList();
                if (studentList.Any(s => s.MaSV == txtMaSV.Text))
                {
                    MessageBox.Show("Mã SV đã tồn tại. Vui lòng nhập một mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtMaSV.Text.Length != 6)
                {
                    MessageBox.Show("Mã sinh viên phải có đúng 6 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newStudent = new Sinhvien
                {
                    MaSV = txtMaSV.Text,
                    HotenSV = txtHotenSV.Text,
                    NgaySinh = dtNgaysinh.Value,
                    MaLop = cboLop.SelectedValue.ToString()
                };

                // Thêm sinh viên vào CSDL
                context.Sinhviens.Add(newStudent);
                context.SaveChanges();
                // Thêm sinh viên vào datagridview
                BindGrid(context.Sinhviens.Include("Lop").ToList());

                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (dtgSinhvien.SelectedRows.Count > 0)
            {
                int rowIndex = dtgSinhvien.SelectedRows[0].Index;
                string ID = dtgSinhvien.Rows[rowIndex].Cells[0].Value.ToString();
                Sinhvien dbUpdate = context.Sinhviens.FirstOrDefault(p => p.MaSV == ID);
                if (dbUpdate != null)
                {
                    dbUpdate.MaSV = txtMaSV.Text;
                    dbUpdate.HotenSV = txtHotenSV.Text;
                    dbUpdate.NgaySinh = dtNgaysinh.Value;
                    string newID = cboLop.SelectedValue.ToString();
                    dbUpdate.MaLop = newID;

                    try
                    {
                        context.SaveChanges();
                        List<Sinhvien> listStudent = context.Sinhviens.ToList();
                        BindGrid(context.Sinhviens.Include("Lop").ToList());
                    }
                    catch
                    {
                        MessageBox.Show("Mã số đã tồn tại hoặc thông tin nhập không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa.", "Thông báo", MessageBoxButtons.OK);
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                List<Sinhvien> studentList = context.Sinhviens.ToList();

                // Tìm kiếm sinh viên có tồn tại trong CSDL hay không
                var student = studentList.FirstOrDefault(s => s.MaSV == txtMaSV.Text);

                if (student != null)
                {
                    // Xóa sinh viên khỏi CSDL
                    context.Sinhviens.Remove(student);
                    context.SaveChanges();

                    BindGrid(context.Sinhviens.Include("Lop").ToList());

                    MessageBox.Show("Sinh viên đã được xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sinh viên không tìm thấy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            backupList = new List<Sinhvien>(context.Sinhviens.ToList());
            btLuu.Enabled = false;
            btKhong.Enabled = false;
        }

        private void btKhong_Click(object sender, EventArgs e)
        {
            context.Sinhviens.RemoveRange(context.Sinhviens);
            context.SaveChanges();
            foreach (var item in backupList)
            {
                context.Sinhviens.Add(item);
            }
            context.SaveChanges();
            BindGrid(context.Sinhviens.Include("Lop").ToList());
            btLuu.Enabled = false;
            btKhong.Enabled = false;
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Kiểm tra lựa chọn của người dùng
            if (result == DialogResult.Yes)
            {
                // Nếu chọn Yes, đóng form
                this.Close();
            }
        }
    }
}

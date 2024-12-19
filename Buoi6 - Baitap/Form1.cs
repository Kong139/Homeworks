using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Buoi6___Baitap.Models;

namespace Buoi6___Baitap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StudentContextDB context = new StudentContextDB();
                List<Faculty> listFalcultys = context.Faculties.ToList(); //lấy các khoa 
                List<Student> listStudent = context.Students.ToList(); //lấy sinh viên 
                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Hàm binding list có tên hiển thị là tên khoa, giá trị là Mã khoa 
        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            this.cbb_faculty.DataSource = listFalcultys;
            this.cbb_faculty.DisplayMember = "FacultyName";
            this.cbb_faculty.ValueMember = "FacultyID";
        }

        //Hàm binding gridView từ list sinh viên 
        private void BindGrid(List<Student> listStudent)
        {
            dgv_student.Rows.Clear();
            using (var db = new StudentContextDB())
            {
                var studentList = db.Students.Include("Faculty").ToList();  // Tải dữ liệu liên quan
                foreach (var item in studentList)
                {
                    int index = dgv_student.Rows.Add();
                    dgv_student.Rows[index].Cells[0].Value = item.StudentID;
                    dgv_student.Rows[index].Cells[1].Value = item.FullName;
                    dgv_student.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                    dgv_student.Rows[index].Cells[3].Value = item.AverageScore;
                }
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                StudentContextDB db = new StudentContextDB();
                List<Student> studentList = db.Students.ToList();
                if (studentList.Any(s => s.StudentID == txt_studentID.Text))
                {
                    MessageBox.Show("Mã SV đã tồn tại. Vui lòng nhập một mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbb_faculty.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn khoa hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newStudent = new Student
                {
                    StudentID = txt_studentID.Text,
                    FullName = txt_fullName.Text,
                    FacultyID = int.Parse(cbb_faculty.SelectedValue.ToString()),
                    AverageScore = double.Parse(txt_averageScore.Text)
                };

                // Thêm sinh viên vào CSDL
                db.Students.Add(newStudent);
                db.SaveChanges();

                // Hiển thị lại danh sách sinh viên
                BindGrid(db.Students.ToList());

                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                StudentContextDB db = new StudentContextDB();
                List<Student> students = db.Students.ToList();
                var student = students.FirstOrDefault(s => s.StudentID == txt_studentID.Text);
                if (student != null)
                {
                    if (students.Any(s => s.StudentID == txt_studentID.Text && s.StudentID != student.StudentID))
                    {
                        MessageBox.Show("Mã SV đã tồn tại. Vui lòng nhập một mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    student.FullName = txt_fullName.Text;
                    student.FacultyID = int.Parse(cbb_faculty.SelectedValue.ToString());
                    student.AverageScore = double.Parse(txt_averageScore.Text);

                    // Cập nhật sinh viên lưu vào CSDL
                    db.SaveChanges();

                    // Hiển thị lại danh sách sinh viên
                    BindGrid(db.Students.ToList());

                    MessageBox.Show("Chỉnh sửa thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                StudentContextDB db = new StudentContextDB();
                List<Student> studentList = db.Students.ToList();

                // Tìm kiếm sinh viên có tồn tại trong CSDL hay không
                var student = studentList.FirstOrDefault(s => s.StudentID == txt_studentID.Text);

                if (student != null)
                {
                    // Xóa sinh viên khỏi CSDL
                    db.Students.Remove(student);
                    db.SaveChanges();

                    BindGrid(db.Students.ToList());

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

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_student_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra nếu dòng hợp lệ
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dgv_student.Rows[e.RowIndex];

                // Gán dữ liệu từ dòng vào các ô nhập liệu
                txt_studentID.Text = selectedRow.Cells[0].Value?.ToString() ?? "";
                txt_fullName.Text = selectedRow.Cells[1].Value?.ToString() ?? "";
                cbb_faculty.Text = selectedRow.Cells[2].Value?.ToString() ?? ""; // Nếu `cbb_faculty` hiển thị tên khoa
                txt_averageScore.Text = selectedRow.Cells[3].Value?.ToString() ?? "";
            }
        }
    }
}

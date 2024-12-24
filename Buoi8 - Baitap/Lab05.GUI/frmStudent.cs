using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Lab05.BUS;
using Lab05.DAL.Entities;

namespace Lab05.GUI
{
    public partial class frmStudent : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private string avatarFilePath = string.Empty;
        public frmStudent()
        {
            InitializeComponent();
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            try
            {
                setGridViewStyle(dgvStudent);
                var listFacultys = facultyService.GetAll();
                var listStudents = studentService.GetAll();
                FillFalcultyCombobox(listFacultys);
                BindGrid(listStudents);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Hàm binding list dữ liệu khoa vào combobox có tên hiện thị là tên khoa, giá trị là Mã khoa
        private void FillFalcultyCombobox(List<Faculty> listFacultys)
        {
            listFacultys.Insert(0, new Faculty());
            this.cmbFaculty.DataSource = listFacultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }
        //Hàm binding gridView từ list sinh viên
        private void BindGrid(List<Student> listStudent)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;
                if (item.Faculty != null)
                    dgvStudent.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore + "";

                if (item.MajorID != null)
                    dgvStudent.Rows[index].Cells[4].Value = item.Major.Name + "";
            }
        }
        public void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        //Mở tệp để upload hình ảnh
        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    avatarFilePath = openFileDialog.FileName;
                    picAvatar.Image = Image.FromFile(avatarFilePath);
                }
            }
        }
        // Hiển thị hình ảnh đại diện của sinh viên từ thư mục Images
        private void LoadAvatar(string studentID)
        {
            string folderPath = Path.Combine(Application.StartupPath, "Images");
            var student = studentService.FindById(studentID);
            if (student != null && !string.IsNullOrEmpty(student.Avatar))
            {
                string avatarFilePath = Path.Combine(folderPath, student.Avatar);
                if (File.Exists(avatarFilePath))
                {
                    picAvatar.Image = Image.FromFile(avatarFilePath);
                }
                else
                {
                    picAvatar.Image = null;
                }
            }
            else
            {
                picAvatar.Image = null;
            }
        }
        //Lưu hình ảnh đại diện vào thư mục Images trên máy tính
        private string SaveAvatar(string sourceFilePath, string studentID)
        {
            try
            {
                string folderPath = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string fileExtension = Path.GetExtension(sourceFilePath);
                string targetFilePath = Path.Combine(folderPath, $"{studentID}{fileExtension}");
                if (!File.Exists(sourceFilePath))
                {
                    throw new FileNotFoundException($"Không tìm thấy file: {sourceFilePath}");
                }
                File.Copy(sourceFilePath, targetFilePath, true);
                return $"{studentID}{fileExtension}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving avatar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                var student = studentService.FindById(txtStudentID.Text) ?? new Student();

                // Cập nhật thông tin sinh viên
                student.StudentID = txtStudentID.Text;
                student.FullName = txtFullName.Text;
                student.AverageScore = double.Parse(txtGPA.Text);
                student.FacultyID = int.Parse(cmbFaculty.SelectedValue.ToString());

                // Kiểm tra xem tệp avatar đã được chọn chưa
                if (!string.IsNullOrEmpty(avatarFilePath))
                {
                    string avatarFileName = SaveAvatar(avatarFilePath, txtStudentID.Text);
                    if (!string.IsNullOrEmpty(avatarFileName))
                    {
                        student.Avatar = avatarFileName;
                    }
                }

                // Lưu sinh viên vào cơ sở dữ liệu
                studentService.InsertUpdate(student);

                // Làm mới giao diện người dùng
                BindGrid(studentService.GetAll());

                // Xóa dữ liệu và đặt lại đường dẫn avatar
                ClearData();
                avatarFilePath = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateInput()
        {
            if (txtStudentID.Text.Length != 10)
            {
                MessageBox.Show("Student ID must be 10 characters", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (double.Parse(txtGPA.Text) < 0 || double.Parse(txtGPA.Text) > 10)
            {
                MessageBox.Show("GPA must be between 0 and 10", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void ClearData()
        {
            txtStudentID.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtGPA.Text = string.Empty;
            cmbFaculty.SelectedIndex = 0;
            picAvatar.Image = null;
        }
        //Gán thông tin sinh viên vào groupbox khi nhấn vào sinh viên đó trong datagridview
        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStudent.SelectedRows.Count > 0)
            {
                var studentID = dgvStudent.SelectedRows[0].Cells[0].Value.ToString();
                var student = studentService.FindById(studentID);
                if (student != null)
                {
                    txtStudentID.Text = student.StudentID;
                    txtFullName.Text = student.FullName;
                    txtGPA.Text = student.AverageScore.ToString();
                    cmbFaculty.SelectedValue = student.FacultyID;
                    LoadAvatar(studentID);
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudent.SelectedRows.Count > 0)
                {
                    var studentID = dgvStudent.SelectedRows[0].Cells[0].Value.ToString();
                    studentService.Delete(studentID);
                    BindGrid(studentService.GetAll());
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Please select a student to delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Checkbox lọc những sinh viên chưa đăng ký chuyên ngành
        private void chkUnregisterMajor_CheckedChanged(object sender, EventArgs e)
        {
            var listStudents = new List<Student>();
            if (this.chkUnregisterMajor.Checked)
                listStudents = studentService.GetAllHasNoMajor();
            else
                listStudents = studentService.GetAll();
            BindGrid(listStudents);
        }
    }
}

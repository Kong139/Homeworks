using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lab03_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string currentFilePath = "";
        private string currentFont = "Tahoma";
        private float currentSize = 14;

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;
            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox1.ForeColor = fontDlg.Color;
                richTextBox1.Font = fontDlg.Font;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Tạo dữ liệu cho ComboBox Font (chứa tất cả các Font chữ hệ thống)
            foreach (FontFamily fontFamily in FontFamily.Families)
            {
                cbb_font.Items.Add(fontFamily.Name);
            }

            // Tạo dữ liệu cho ComboBox Size (chứa các giá trị từ 8 → 72)
            int[] sizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (int size in sizes)
            {
                cbb_size.Items.Add(size);
            }

            // Đặt giá trị mặc định cho ComboBox
            if (cbb_font.Items.Count > 0)
                cbb_font.SelectedItem = currentFont;

            if (cbb_size.Items.Count > 0)
                cbb_size.SelectedIndex = cbb_size.FindStringExact(currentSize.ToString());
        }

        private void cbb_font_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFont = cbb_font.SelectedItem.ToString();

            // Kiểm tra nếu font hợp lệ, áp dụng cho RichTextBox
            if (!string.IsNullOrEmpty(selectedFont))
            {
                try
                {
                    // Áp dụng font đã chọn vào RichTextBox
                    richTextBox1.SelectionFont = new Font(selectedFont, currentSize);
                    currentFont = selectedFont;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể áp dụng font: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbb_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu giá trị được chọn là hợp lệ
            if (cbb_size.SelectedItem != null && int.TryParse(cbb_size.SelectedItem.ToString(), out int fontSize))
            {
                // Áp dụng font mới với kích thước được chọn
                richTextBox1.SelectionFont = new Font(currentFont, fontSize);
                currentSize = fontSize;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Xóa nội dung hiện có trong RichTextBox
            richTextBox1.Clear();

            // Đặt lại font chữ mặc định
            cbb_font.SelectedItem = "Tahoma"; // Font mặc định
            cbb_size.SelectedItem = 14; // Kích thước mặc định
        }

        private void tạoMớiVănBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Xóa nội dung hiện có trong RichTextBox
            richTextBox1.Clear();

            // Đặt lại font chữ mặc định
            cbb_font.SelectedItem = "Tahoma"; // Font mặc định
            cbb_size.SelectedItem = 14; // Kích thước mặc định
        }

        private void mởTậpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|RichText files (*.rtf)|*.rtf";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;
                string savedFilePath = openFileDialog.FileName;
                try
                {
                    if (Path.GetExtension(selectedFileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        richTextBox1.LoadFile(selectedFileName, RichTextBoxStreamType.PlainText);
                    }
                    else
                    {
                        richTextBox1.LoadFile(selectedFileName, RichTextBoxStreamType.RichText);
                    }
                    MessageBox.Show("Tập tin đã được mở thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình mở tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                FontStyle style = richTextBox1.SelectionFont.Style;
                if (richTextBox1.SelectionFont.Bold)
                {
                    // Nếu văn bản đã đậm, xóa thuộc tính Bold ra khỏi FontStyle hiện tại
                    style &= ~FontStyle.Bold;
                }
                else
                {
                    // Nếu văn bản chưa đậm, thêm thuộc tính Bold vào FontStyle hiện tại
                    style |= FontStyle.Bold;
                }
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                FontStyle style = richTextBox1.SelectionFont.Style;
                if (richTextBox1.SelectionFont.Italic)
                {
                    // Nếu văn bản đã in nghiêng, xóa thuộc tính Italic
                    style &= ~FontStyle.Italic;
                }
                else
                {
                    // Nếu văn bản chưa in nghiêng, thêm thuộc tính Italic
                    style |= FontStyle.Italic;
                }
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                FontStyle style = richTextBox1.SelectionFont.Style;
                if (richTextBox1.SelectionFont.Underline)
                {
                    // Nếu văn bản đã gạch chân, xóa thuộc tính Underline
                    style &= ~FontStyle.Underline;
                }
                else
                {
                    // Nếu văn bản chưa gạch chân, thêm thuộc tính Underline
                    style |= FontStyle.Underline;
                }
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
            }
        }

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath)) // Nếu là văn bản mới (chưa lưu lần nào)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Rich Text Format (*.txt)|*.txt";
                saveFileDialog.DefaultExt = "rtf";
                saveFileDialog.Title = "Lưu tập tin";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        currentFilePath = saveFileDialog.FileName;
                        richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                        MessageBox.Show("Tập tin đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi lưu tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else // Nếu là văn bản đã được mở trước đó
            {
                try
                {
                    richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Tập tin đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi lưu tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath)) // Nếu là văn bản mới (chưa lưu lần nào)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf";
                saveFileDialog.DefaultExt = "rtf";
                saveFileDialog.Title = "Lưu tập tin";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        currentFilePath = saveFileDialog.FileName;
                        richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                        MessageBox.Show("Tập tin đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi khi lưu tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else // Nếu là văn bản đã được mở trước đó
            {
                try
                {
                    richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Tập tin đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi lưu tập tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

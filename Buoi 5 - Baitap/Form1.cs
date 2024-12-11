using System.Windows.Forms;

namespace Buoi_5___Baitap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tạoVănBảnMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;
            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richText.ForeColor = fontDlg.Color;
                richText.Font = fontDlg.Font;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (FontFamily fontFamily in FontFamily.Families)
            {
                // Thêm mỗi Font vào ComboBox
                cbb_font.Items.Add(fontFamily.Name);
            }

            // Đặt mặc định cho ComboBox (chọn font đầu tiên)
            if (cbb_font.Items.Count > 0)
            {
                cbb_font.SelectedItem = "Tahoma";
            }

            int[] fontSizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

            // Thêm kích thước vào ComboBox
            foreach (int size in fontSizes)
            {
                cbb_size.Items.Add(size);
            }
            cbb_size.SelectedItem = 14;
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
                    richText.Font = new Font(selectedFont, richText.Font.Size);
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
                // Lấy font hiện tại
                string currentFontName = richText.Font.FontFamily.Name;

                // Áp dụng font mới với kích thước được chọn
                richText.Font = new Font(currentFontName, fontSize);
            }
        }
    }
}

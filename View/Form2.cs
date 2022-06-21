using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LINQ_17_5_22.BLL;
using LINQ_17_5_22.DTO;

namespace LINQ_17_5_22.View
{
    public partial class Form2 : Form
    {
        public delegate void Trans(int id, string text);
        public Trans onedel { get; set; }
        string mssv;
        int lopshid;
        string search;
        public Form2(int f1lopshid, string f1mssv, string f1search)
        {
            InitializeComponent();
            setCBB();
            mssv = f1mssv;
            lopshid = f1lopshid;
            search = f1search;
            InfoDisplay();
        }
        private void setCBB()
        {
            comboBox1.Items.AddRange(BLLQLSV.Instance.GetCBB().ToArray());
            comboBox1.SelectedIndex = -1;
        }
        private void InfoDisplay()
        {
            if (mssv != "")
            {
                SV s = BLLQLSV.Instance.GetSVByMSSV(mssv);
                textBox1.Text = s.mssv;
                textBox1.Enabled = false;
                textBox2.Text = s.hoten;
                comboBox1.Text = s.lopsh_name;
                dateTimePicker1.Value = (DateTime)s.ngaysinh;
                textBox3.Text = (s.dtb).ToString();
                radioButton1.Checked = (bool)s.gender ? true : false;
                radioButton2.Checked = !radioButton1.Checked;
                checkBox1.Checked = (bool)s.anh;
                checkBox2.Checked = (bool)s.hocba;
                checkBox3.Checked = (bool)s.cmnd;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Chưa nhập mssv");
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Chưa nhập họ tên");
                return;
            }
            else if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn lớp");
                return;
            }
            else if (dateTimePicker1 == null)
            {
                MessageBox.Show("Chưa chọn ngày sinh");
                return;
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Chưa nhập điểm trung bình");
                return;
            }
            sv s = new sv()
            {
                mssv = textBox1.Text,
                hoten = textBox2.Text,
                lopsh_id = ((CBBItem)comboBox1.SelectedItem).value,
                gender = radioButton1.Checked ? true : false,
                ngaysinh = dateTimePicker1.Value,
                dtb = Convert.ToDouble(textBox3.Text.ToString()),
                anh = checkBox1.Checked ? true : false,
                hocba = checkBox2.Checked ? true : false,
                cmnd = checkBox3.Checked ? true : false
            };
            if (mssv == "")
            {
                if (!BLLQLSV.Instance.AddNewSV(s))
                {
                    MessageBox.Show("Trùng mã sinh viên");
                    return;
                }
                else
                {
                    MessageBox.Show("Thêm thành công");
                }
                onedel(0, "");
            }
            else
            {
                BLLQLSV.Instance.UpdateExistSV(s);
                MessageBox.Show("Sửa thành công");
                onedel(lopshid, search);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

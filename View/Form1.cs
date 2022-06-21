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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            setCBB();
        }
        private void setCBB()
        {
            comboBox1.Items.Add(new CBBItem() { value = 0, text = "ALL"});
            comboBox1.Items.AddRange(BLLQLSV.Instance.GetCBB().ToArray());
            comboBox1.SelectedIndex = 0;
        }
        private void showDGV(int lopshid, string search)
        {
            dssvdgv.DataSource = BLLQLSV.Instance.GetAllSVByIDLop(lopshid, search);
            dssvdgv.Columns[0].Width = 54;
            dssvdgv.Columns[1].Width = 145;
            dssvdgv.Columns[2].Width = 76;
            dssvdgv.Columns[3].Width = 60;
            dssvdgv.Columns[4].Width = 76;
            dssvdgv.Columns[5].Width = 50;
            dssvdgv.Columns[6].Width = 51;
            dssvdgv.Columns[7].Width = 51;
            dssvdgv.Columns[8].Width = 52;

            dssvdgv.Columns[0].HeaderText = "MSSV";
            dssvdgv.Columns[1].HeaderText = "Họ và tên";
            dssvdgv.Columns[2].HeaderText = "Tên Lớp";
            dssvdgv.Columns[3].HeaderText = "Giới tính";
            dssvdgv.Columns[4].HeaderText = "Ngày sinh";
            dssvdgv.Columns[5].HeaderText = "ĐTB";
            dssvdgv.Columns[6].HeaderText = "Ảnh";
            dssvdgv.Columns[7].HeaderText = "Học bạ";
            dssvdgv.Columns[8].HeaderText = "CMND";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            showDGV(((CBBItem)comboBox1.SelectedItem).value, textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showDGV(((CBBItem)comboBox1.SelectedItem).value, textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(0,"","");
            f2.onedel = new Form2.Trans(showDGV);
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dssvdgv.SelectedRows.Count > 1)
                MessageBox.Show("Chỉnh sửa cùng lúc một sinh viên");
            else if (dssvdgv.SelectedRows.Count == 1)
            {
                foreach (DataGridViewRow r in dssvdgv.SelectedRows)
                {
                    Form2 f2 = new Form2(((CBBItem)comboBox1.SelectedItem).value,r.Cells[0].Value.ToString(),textBox1.Text);
                    f2.onedel = new Form2.Trans(showDGV);
                    f2.Show();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dssvdgv.SelectedRows.Count > 0)
            {
                foreach(DataGridViewRow r in dssvdgv.SelectedRows)
                {
                    BLLQLSV.Instance.DeleteSV(r.Cells[0].Value.ToString());
                }
                MessageBox.Show("Xóa thành công");
                showDGV(((CBBItem)comboBox1.SelectedItem).value, textBox1.Text);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                showDGV(((CBBItem)comboBox1.SelectedItem).value, textBox1.Text);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0)
            {
                dssvdgv.DataSource = BLLQLSV.Instance.SortSVWithOption(BLLQLSV.Instance.GetAllSVByIDLop(((CBBItem)comboBox1.SelectedItem).value, textBox1.Text),
                    comboBox2.SelectedIndex);
            }
        }
    }
}

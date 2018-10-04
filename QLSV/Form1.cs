using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic;
namespace QLSV
{   
    
    public partial class Form1 : Form
    {
        string path = "";
        XL_SINHVIEN Bang_SinhVien;
        XL_LOP Bang_Lop;
        BindingManagerBase DSSV;
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bang_SinhVien = new XL_SINHVIEN();
            Bang_Lop = new XL_LOP();

            loadCBOLop();
            loadDGVHocSinh();
            txtMaSV.DataBindings.Add("text", Bang_SinhVien, "MaSV", true);
            txtHoTen.DataBindings.Add("text", Bang_SinhVien, "Hoten", true);
            dtpNgaySinh.DataBindings.Add("text", Bang_SinhVien, "NgaySinh", true);
            rdbNam.DataBindings.Add("checked", Bang_SinhVien, "GioiTinh", true);
            cbLop.DataBindings.Add("SelectedValue", Bang_SinhVien, "MaLop", true);
            txtDiaChi.DataBindings.Add("text", Bang_SinhVien, "DiaChi", true);
            DSSV = this.BindingContext[Bang_SinhVien];
            enabledNutLenh(false);
            rdbNu.Checked = !rdbNam.Checked;

        }
        // cac phuong thuc
        private void enabledNutLenh(bool pCapNhat)
        {
            btThem.Enabled = !pCapNhat;
            btXoa.Enabled = !pCapNhat;
            btSua.Enabled = !pCapNhat;
            btThoat.Enabled = !pCapNhat;
            btLuu.Enabled = !pCapNhat;
            btHuy.Enabled = !pCapNhat;
        }
        private void loadCBOLop()
        {
            cbLop.DataSource = Bang_Lop;
            cbLop.DisplayMember = "TenLop";
            cbLop.ValueMember = "MaLop";
        }
        private void loadDGVHocSinh()
        {
            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = Bang_SinhVien;
        }

        private void rdbNam_CheckedChanged(object sender, EventArgs e)
        {
            rdbNu.Checked = !rdbNam.Checked;
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            DSSV.AddNew();
            //enabledNutLenh(true);
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            try
            {
                DSSV.EndCurrentEdit();
                Bang_SinhVien.Ghi();
               // enabledNutLenh(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btHuy_Click(object sender, EventArgs e)
        {
            DSSV.CancelCurrentEdit();
            Bang_SinhVien.RejectChanges();
            //enabledNutLenh(false);
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            DSSV.RemoveAt(DSSV.Position);
            if (!Bang_SinhVien.Ghi())
            {
                MessageBox.Show("Failed");
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            try
            {
                DSSV.EndCurrentEdit();
                Bang_SinhVien.Ghi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow r = Bang_SinhVien.Select("MaSv ='" + txtTimKiem.Text + "'")[0];
                DSSV.Position = Bang_SinhVien.Rows.IndexOf(r);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong tim thay");
            }
        }

        private void txtTimKiem_MouseDown(object sender, MouseEventArgs e)
        {
            txtTimKiem.Text = "";
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btThemhinh_Click(object sender, EventArgs e)
        {

        }
        
    }
}

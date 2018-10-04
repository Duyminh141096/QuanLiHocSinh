using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


using System.Data.SqlClient;
namespace DataAccess
{
    public class XL_BANG: DataTable
    {
        public static String Chuoi_lien_ket ="Data Source=DESKTOP-U9AJAJR\\SQLEXPRESS;Initial Catalog=QLSINHVIEN4;Integrated Security=True";
        private SqlDataAdapter mBo_doc_ghi = new SqlDataAdapter();
        private SqlConnection mKet_noi;
        private String mChuoi_SQL;

public String MChuoi_SQL
{
  get { return mChuoi_SQL; }
  set { mChuoi_SQL = value; }
}
        private String mTen_bang;

public String MTen_bang
{
  get { return mTen_bang; }
  set { mTen_bang = value; }
}
        //cac phuong thuc khoi tao
        public XL_BANG():base(){}
        //tao moi bang voi ten pTen_bang
        public XL_BANG(String pTen_bang){
            mTen_bang = pTen_bang;
            Doc_bang();
        }
        //tao moi bang voi cai truy van
        public XL_BANG(String pTen_bang,String pChuoi_SQL){
            mTen_bang = pTen_bang;
            mChuoi_SQL = pChuoi_SQL;
            Doc_bang();
        }

        //doc du lieu
        public void Doc_bang(){
            if(mChuoi_SQL==null){
                mChuoi_SQL ="SELECT * FROM "+ mTen_bang;
            }
            if(mKet_noi ==null){
                mKet_noi = new SqlConnection(Chuoi_lien_ket);
            }
            try{
                mBo_doc_ghi = new SqlDataAdapter(mChuoi_SQL,mKet_noi);
                mBo_doc_ghi.FillSchema(this,SchemaType.Mapped);
                mBo_doc_ghi.Fill(this);
                mBo_doc_ghi.RowUpdated+= new SqlRowUpdatedEventHandler(mBo_doc_ghi_RowUpdated);
                SqlCommandBuilder Bo_phat_sinh = new SqlCommandBuilder(mBo_doc_ghi);
            }
            catch(SqlException ex){
                throw ex;
            }
        }
        //ghi du lieu
        public Boolean Ghi(){
            Boolean ket_qua = true;
            try{
                mBo_doc_ghi.Update(this);
                this.AcceptChanges();
            }
            catch(SqlException ex){
                this.RejectChanges();
                ket_qua = false;
                throw ex;
            }
            return ket_qua;
        }
        //loc du lieu
        public void Loc_du_lieu(String pDieu_kien){
            try{
                this.DefaultView.RowFilter = pDieu_kien;
            }
            catch  (Exception ex){
                throw ex;
            }
        }
        //thus hien cau truy van cap nhat du lieu(Insert,Update,Delete)
        public int Thuc_hien_lenh(String Lenh){
            try{
                SqlCommand Cau_lenh = new SqlCommand(Lenh,mKet_noi);
                mKet_noi.Open();
                int ket_qua = Cau_lenh.ExecuteNonQuery();
                mKet_noi.Close();
                return ket_qua;
            }
            catch{
                return -1;
            }
        }
        //thuc hien cau truy van tra ve 1 gia tri
        public Object Thuc_hien_len_tinh_toan(String Lenh){
            try{
                SqlCommand Cau_lenh = new SqlCommand(Lenh ,mKet_noi);
                mKet_noi.Open();
                Object ket_qua = Cau_lenh.ExecuteScalar();
                mKet_noi.Close();
                return ket_qua;
            }
            catch{
                return null;
            }
        }
        //xu ly su kien RowUpdate doi voi truong khoa chinh co kieu Autonumber
        private void mBo_doc_ghi_RowUpdated(Object sender,SqlRowUpdatedEventArgs e){
            if(this.PrimaryKey[0].AutoIncrement){
                if((e.Status == UpdateStatus.Continue)&& (e.StatementType== StatementType.Insert)){
                    SqlCommand cmd = new SqlCommand("SELECT @INDENTITY", mKet_noi);
                    e.Row.ItemArray[0] =cmd.ExecuteScalar();
                    e.Row.AcceptChanges();
                }
            }
        }
    }
}

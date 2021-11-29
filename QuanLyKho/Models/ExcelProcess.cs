using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace QuanLyKho.Models
{
    public class ExcelProcess
    {
        public DataTable ReadDataFromExcelFile(string filepath, bool removeRow0)
        {
            string connectionString = "";
            string fileExtention = filepath.Substring(filepath.Length - 4).ToLower();
            if (fileExtention.IndexOf("xlsx") < 0)
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + "; Extended Properties=Excel 8.0";

            }
            else
            {
                connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + filepath + "; Extended Properties =\"Excel 12.0 Xml;HDR=NO\"";
            }
            //tạo kết nối
            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            DataTable data = null;
            try
            {
                //mở kết nối
                oledbConn.Open();

                //tạo đối tượng Oledbcomman và query data từ sheet có tên "sheet1"
                OleDbCommand cmd = new OleDbCommand("SELECT*FROM[Sheet1$]", oledbConn);

                //tạo đối tượng oledbDataAdapter để thực thi việc query lấy dữ liệu từ tập tin
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;

                //tạo đối tượng Dataset để hứng dữ liệu từ tập tin
                DataSet ds = new DataSet();

                //đổ dữ liệu từ excel vào dataset
                oleda.Fill(ds);

                data = ds.Tables[0];
                if (removeRow0 == true)
                {
                    data.Rows.RemoveAt(0);
                }
            }
            catch
            {

            }
            finally
            {
                //đóng chuỗi kết nối
                oledbConn.Close();
            }
            return data;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace ImageSearchProgram
{
    /// <summary>
    /// Interaction logic for SearchControl.xaml
    /// </summary>
    public partial class SearchControl : UserControl
    {

        public SearchControl()
        {
            InitializeComponent();
        }

        public void InsertDB(string paramName, DateTime paramTime)
        {
            int rowCnt = 0;

            string dbName = "Data Source = database.sdf;";
            SqlCeConnection con = new SqlCeConnection(dbName);
            con.Open();

            SqlCeCommand cmd = new SqlCeCommand();
            cmd.Connection = con;

            SqlCeTransaction tran = con.BeginTransaction();
            cmd.Transaction = tran;

            cmd.CommandText = "SELECT COUNT(*) FROM Logs WHERE searchName = '" + paramName + "'";
            rowCnt = (Int32)cmd.ExecuteScalar();
            tran.Commit();

            try
            {
                cmd = new SqlCeCommand();
                cmd.Connection = con;

                tran = con.BeginTransaction();
                cmd.Transaction = tran;

                if (rowCnt != 0)
                {
                    cmd.CommandText = "DELETE FROM Logs WHERE searchName = '" + paramName + "'";
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                }

                cmd = new SqlCeCommand();
                cmd.Connection = con;

                tran = con.BeginTransaction();
                cmd.Transaction = tran;

                cmd.CommandText = "INSERT INTO Logs VALUES('" + paramName + "','" + paramTime + "')";
                cmd.ExecuteNonQuery();
                tran.Commit();
                

            }
            catch (SystemException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

    }
}

/* 네이버 api 이용, key값은 각자 발급 받은 키를 사용하거나 또는 그대로 사용해도 무방
         XmlDocument doc = new XmlDocument();
         doc.Load("http://openapi.naver.com/search?key=4432e614518baff96f7dcc60d0fe5c88&query=제주도&target=image&start=1&display=10");
         XmlNodeList imgList = doc.GetElementsByTagName("thumbnail");
        */

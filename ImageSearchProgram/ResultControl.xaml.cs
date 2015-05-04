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
using TimeTable.Useful_Functions;

namespace ImageSearchProgram
{
    /// <summary>
    /// Interaction logic for ResultControl.xaml
    /// </summary>
    public partial class ResultControl : UserControl
    {
        JKAppExceptions err = JKAppExceptions.GetInstance();
       
        public ResultControl()
        {
            InitializeComponent();
        }

        public void ShowResultOfSearch()
        {
            Dictionary<string, string> resultDic = SelectDB();
            TextBlock txtblock = null;


            foreach (KeyValuePair<string, string> pair in resultDic)
            {
                txtblock = new TextBlock
                {
                    Width = 450,
                    Height = 18,
                    //Text = err.BytesPadRight(pair.Key, 30, ' ') + pair.Value
                    Text = pair.Key.PadRight(20, ' ') + pair.Value
                };
                wp.Children.Add(txtblock);
            }
        }

        public void DeleteDB()
        {
            try
            {
                string dbName = "Data Source = database.sdf;";
                SqlCeConnection con = new SqlCeConnection(dbName);
                con.Open();

                SqlCeCommand cmd = new SqlCeCommand();
                cmd.Connection = con;

                SqlCeTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;

                cmd.CommandText = "DELETE FROM Logs";
                cmd.ExecuteNonQuery();
                tran.Commit();

            }
            catch(SystemException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        public Dictionary<string, string> SelectDB()
        {
            Dictionary<string, string> resultDic = new Dictionary<string, string>();

            try
            {
                string dbName = "Data Source = database.sdf;";
                SqlCeConnection con = new SqlCeConnection(dbName);
                con.Open();

                SqlCeCommand cmd = new SqlCeCommand();
                cmd.Connection = con;

                SqlCeTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;

                cmd.CommandText = "SELECT * FROM Logs";

                SqlCeDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    resultDic.Add(reader["searchName"].ToString(), reader["searchTime"].ToString());
                }
            }
            catch (SystemException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            return resultDic;
        }
    }
}

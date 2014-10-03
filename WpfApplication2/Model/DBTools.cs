using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;  

namespace WpfApplication2.Model
{
    public class DBTools
    {
        private string Database = "Data.db";

        public SQLiteConnection OpenConn()
        {
            string cnstr = string.Format("Data Source={0} ;Version=3;New=False;Compress=True;", Database);
            SQLiteConnection icn = new SQLiteConnection();
            icn.ConnectionString = cnstr;
            if (icn.State == ConnectionState.Open) icn.Close();
            icn.Open();
            return icn;
        }

        public void CreateSQLiteDatabase()
        {
            string cnstr = string.Format("Data Source={0};Version=3;New=True;Compress=True;", Database);
            SQLiteConnection icn = new SQLiteConnection();
            icn.ConnectionString = cnstr;
            icn.Open();
            icn.Close();
        }

        public void CreateSQLiteTable(string CreateTableString)
        {
            SQLiteConnection icn = OpenConn();
            SQLiteCommand cmd = new SQLiteCommand(CreateTableString, icn);
            SQLiteTransaction mySqlTransaction = icn.BeginTransaction();
            try
            {
                cmd.Transaction = mySqlTransaction;
                cmd.ExecuteNonQuery();
                mySqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                mySqlTransaction.Rollback();
                throw (ex);
            }
            if (icn.State == ConnectionState.Open) icn.Close();
        }

        public void SQLiteInsertUpdateDelete(string SqlSelectString)
        {
            SQLiteConnection icn = OpenConn();
            SQLiteCommand cmd = new SQLiteCommand(SqlSelectString, icn);
            SQLiteTransaction mySqlTransaction = icn.BeginTransaction();
            try
            {
                cmd.Transaction = mySqlTransaction;
                cmd.ExecuteNonQuery();
                mySqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                mySqlTransaction.Rollback();
                throw (ex);
            }
            if (icn.State == ConnectionState.Open) icn.Close();
        }

        public DataTable GetDataTable(string SQLiteString)
        {
            DataTable myDataTable = new DataTable();
            SQLiteConnection icn = OpenConn();
            SQLiteDataAdapter da = new SQLiteDataAdapter(SQLiteString, icn);
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            myDataTable = ds.Tables[0];
            if (icn.State == ConnectionState.Open) icn.Close();
            return myDataTable;
        }
 
    }
}

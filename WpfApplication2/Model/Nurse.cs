using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication2.View;

namespace WpfApplication2.Model
{
    class Nurse
    {
        private DBTools DB = new DBTools();

        public void CreateTable()
        {
            string createtablestring = "create table if not exists Nurse (NurseNo NUMERIC, Name String, StationNo NUMERIC);";
            DB.CreateSQLiteTable(createtablestring);
        }

        public DataTable Query()
        {
            //讀取資料
            string SQLStr = "SELECT NurseNo, Name, StationNo FROM Nurse";
            DataTable dt = DB.GetDataTable(SQLStr);
            return dt; 
        }

        public bool Insert(NurseView Data)
        {
            string insertstring =  string.Format("insert into Nurse (NurseNo,Name,StationNo) values ('{0}','{1}','{2}');", Data.NurseNo,Data.Name,Data.StationNo);   
            DB.SQLiteInsertUpdateDelete(insertstring);
            return true;
        }

        public bool Update(NurseView Data)
        {
            string insertstring = string.Format("Update Nurse SET Name='{1}', StationNo='{2}' where NurseNo='{0}'", Data.NurseNo, Data.Name, Data.StationNo);
            DB.SQLiteInsertUpdateDelete(insertstring);
            return true;
        }

        public bool Del(int NurseNo)
        {
            string deletestring = string.Format("DELETE FROM Nurse WHERE NurseNo = {0}", NurseNo);
            DB.SQLiteInsertUpdateDelete(deletestring);
            return true;
        }
    }
}

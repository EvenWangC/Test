using System;
using System.Collections.Generic;
using System.Data;
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
using WpfApplication2.Model;
using WpfApplication2.View;

namespace WpfApplication2
{
    /// <summary>
    /// PageNurse.xaml 的互動邏輯
    /// </summary>
    public partial class PageNurse : Page
    {
        private Nurse NurseData = new Nurse();
        private DataTable NurseDataTable;
        private int DataStatus;   //0:Edit, 1:Add 
        private List<NurseView> AddNurse = new List<NurseView>();   //新增列
        private List<NurseView> EditNurse = new List<NurseView>();  //修改列
        private List<int> SelectNurseNo = new List<int>();          //勾選列表         

        public PageNurse()
        {
            InitializeComponent();

            DataStatus = 0;
            NurseData.CreateTable();
            QueryData();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QueryData();
        }

        public void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataStatus = 1;
            NurseGrid.CanUserAddRows = true;
            ModiStatus(true);
            btnAdd.IsEnabled = false;
        }

        private void dataGrid1_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            NurseView Info = new NurseView();
            DataRowView Rows = e.Row.Item as DataRowView;

            Info.NurseNo = Rows.Row[0].ToString();
            Info.Name = Rows.Row[1].ToString();
            Info.StationNo = Rows.Row[2].ToString();

            if (DataStatus == 1)                            //新增
                AddNurse.Add(Info);
            else
            {
                EditNurse.Add(Info);                        //修改 
                ModiStatus(true);
            }
        }


        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (NurseView info in AddNurse)
                NurseData.Insert(info);

            AddNurse.Clear();

            foreach (NurseView info in EditNurse)
                NurseData.Update(info);

            EditNurse.Clear();
            
            QueryData(); 
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox Check = sender as CheckBox;
            int NurseNo = 0;
            var SelectNurse = Check.IsChecked;

            if (Check.Tag != null && int.TryParse(Check.Tag.ToString(), out NurseNo))
            {
                if (SelectNurse == true)
                    SelectNurseNo.Add(NurseNo);      
                else
                    SelectNurseNo.Remove(NurseNo); 
            }
        }

        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            QueryData();
            DataStatus = 0;
        }

        public void btnDel_Click(object sender, RoutedEventArgs e)
        {
            foreach (int NurseNo in SelectNurseNo)
                NurseData.Del(NurseNo);

            QueryData();

        }

        private void QueryData()
        {
            NurseDataTable = NurseData.Query();
            NurseGrid.DataContext = NurseDataTable;
            ModiStatus(false);
            NurseGrid.CanUserAddRows = false;  
        }

        private void ModiStatus(bool Status)
        {
            if (Status)
            {
                btnDel.IsEnabled = false;
                btnSave.IsEnabled = true;
                btnCancel.IsEnabled = true;
            }
            else
            {
                btnDel.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                btnAdd.IsEnabled = true;
            }
        }   
    }
}

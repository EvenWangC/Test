using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using WpfApplication2.Model;
using System.Data;
using WpfApplication2.View;

namespace WpfApplication2
{
    /// <summary>
    /// MainWindows.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindows : Window
    {
        public MainWindows()
        {
            InitializeComponent();
          
        }

        private void NurseClick(object sender, RoutedEventArgs e)
        {
            SubFrame.Source = new Uri("pack://application:,,,/PageNurse.xaml");
        }   
    }
}

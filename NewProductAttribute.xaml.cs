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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace BillDemo1
{
    /// <summary>
    /// Interaction logic for NewProductAttribute.xaml
    /// </summary>
    public partial class NewProductAttribute : Window
    {
        public NewProductAttribute()
        {
            InitializeComponent();
            string connetionString;
            connetionString = @"Data Source=DESKTOP-D0V3DV0;Initial Catalog=billing;User ID=sa;Password=chellakutty";

            SqlConnection cnn;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string Sql = "SELECT NAME FROM CATEGORY";            
            SqlCommand cmd = new SqlCommand(Sql, cnn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                category.ItemsSource = ds.Tables[0].Rows[0].ToString();                  
            }
            else
            {
                MessageBox.Show("No data to Show!");
            }

        }

        private void Submit(object sender, RoutedEventArgs e)
        {

        }
    }
}

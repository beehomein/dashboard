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
using System.IO;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for SearchCustomer.xaml
    /// </summary>
    public partial class SearchCustomer : Window
    {
        public string customerNumber="";
        public string customerEmail="";
        public SearchCustomer()
        {
            try
            {
                string file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                //MessageBox.Show(file);
                string cnn = File.ReadAllText(file);

                //Decryption

                //string ou = new string(cnn);
                string ou;
                char[] output = new char[cnn.Length];
                var i = 0;
                foreach (var character in cnn)
                {
                    output[i++] = (char)(character - '~');
                }
                ou = new string(output);

                String ConnString;
                //Decrypting Text File
                ConnString = ou.Substring(0, ou.Length - 2);
                //MessageBox.Show(ConnString);
                SqlConnection scn = new SqlConnection(ConnString);
                scn.Open();
                scn.Close();
                InitializeComponent();
            }
            catch
            {
                new Connection().Show();
                MessageBox.Show("SQL Connection Required!", "Alert");
                this.Close();
            }
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            var Number = number.Text;
            var Email = email.Text;
            if (Number != "" || Email != "")
            {
                try
                {
                    string connetionString;
                    DataSet ds = new DataSet();

                    string file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                    //MessageBox.Show(file);
                    string cnn = File.ReadAllText(file);

                    //Decryption

                    //string ou = new string(cnn);
                    string ou;
                    char[] output = new char[cnn.Length];
                    var i = 0;
                    foreach (var character in cnn)
                    {
                        output[i++] = (char)(character - '~');
                    }
                    ou = new string(output);

                    String ConnString;
                    //Decrypting Text File
                    ConnString = ou.Substring(0, ou.Length - 2);

                    //connetionString = @"Data Source=DESKTOP-D0V3DV0;Initial Catalog=billing;User ID=sa;Password=chellakutty";
                    connetionString = ConnString;
                    SqlConnection cns;
                    cns = new SqlConnection(connetionString);
                    cns.Open();
                    SqlCommand sql_cmnd = new SqlCommand("SP_SEARCH_CUSTOMER_DETAIL", cns);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@NUMBER", SqlDbType.VarChar).Value = Number;
                    sql_cmnd.Parameters.AddWithValue("@EMAIL", SqlDbType.VarChar).Value = Email;
                    sql_cmnd.ExecuteNonQuery();
                    SqlDataAdapter sda = new SqlDataAdapter(sql_cmnd);

                    sda.Fill(ds);
                    name.Content = ds.Tables[0].Rows[0]["F_NAME"].ToString();
                    age.Content = ds.Tables[0].Rows[0]["AGE_GROUP"].ToString();
                    communication.Content = ds.Tables[0].Rows[0]["COMMUNICATION_TYPE"].ToString();
                    city.Content = ds.Tables[0].Rows[0]["CITY"].ToString();
                    this.customerEmail = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                    this.customerNumber= ds.Tables[0].Rows[0]["NUMBER"].ToString();
                    //MessageBox.Show(customerEmail);
                    //MessageBox.Show(customerNumber);
                    cns.Close();
                }
                catch
                {
                    MessageBox.Show("Customer Not Found","Warning");
                    name.Content = "";
                    age.Content = "";
                    communication.Content = "";
                    city.Content = "";
                    customerNumber = "";
                    customerEmail = "";
                }
            }
            else
            {
                MessageBox.Show("Fill All Fields", "Alert");
                customerNumber = "";
                customerEmail = "";
            }
        }

        private void openNewCustomer(object sender, RoutedEventArgs e)
        {
            var newCustomer = new NewCustomerRegistration();
            newCustomer.Show();
        }

        private void clear(object sender, RoutedEventArgs e)
        {
            number.Text="";
            email.Text="";
            name.Content = "";
            age.Content = "";
            communication.Content = "";
            city.Content = "";
            customerNumber = "";
            customerEmail = "";
        }

        private void openDashBoardWithCustomer(object sender, RoutedEventArgs e)
        {
            var Number = this.customerNumber;
            var Email = this.customerEmail;
            //MessageBox.Show("-"+customerEmail+"-");
            //MessageBox.Show("-" + customerNumber + "-");
            if (Number.Length > 0 && Email.Length > 0)
            {
                var dashBoardWithCustomer =new Dashboard(Number, Email);
                //dashBoardWithCustomer.setCustomerEmail( Email);
                //dashBoardWithCustomer.setCustomerNumber(Number);
                //MessageBox.Show(customerEmail);
                //MessageBox.Show(customerNumber);
                dashBoardWithCustomer.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Select valid Customer!","Warning");
            }
        }
    }
}


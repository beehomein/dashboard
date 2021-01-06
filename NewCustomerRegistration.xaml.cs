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
    /// Interaction logic for NewCustomerRegistration.xaml
    /// </summary>
    public partial class NewCustomerRegistration : Window
    {
        public NewCustomerRegistration()
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
                //dob.SetValue = System.DateTime.Now();
            }
            catch
            {
                new Connection().Show();
                MessageBox.Show("SQL Connection Required!", "Alert");
                this.Close();
            }
        }

        private void submit(object sender, RoutedEventArgs e)
        {
           
            var FName = fname.Text;
            var LName = lname.Text;
            var Number = number.Text;
            var Gender = gender.Text;
            var Email = email.Text;
            var Occupation = occupation.Text;
            var Age = age.Text;
            var Communication = communication.Text;
            var DOB = dob.SelectedDate;
            var City = city.Text;
            if (FName != "" && LName != "" && Number != "" && Gender != "" && Email != "" && Occupation != "" && Age != "" && Communication != "" && DOB.ToString() != "" && City != "")
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

                string connetionString;
                //connetionString = @"Data Source=DESKTOP-D0V3DV0;Initial Catalog=billing;User ID=sa;Password=chellakutty";
                connetionString = ConnString;
                SqlConnection cns;
                cns = new SqlConnection(connetionString);
                cns.Open();
                try
                {
                    SqlCommand sql_cmnd = new SqlCommand("SP_INSERT_CUSTOMER_DETAIL", cns);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@F_NAME", SqlDbType.VarChar).Value = FName;
                    sql_cmnd.Parameters.AddWithValue("@L_NAME", SqlDbType.VarChar).Value = LName;
                    sql_cmnd.Parameters.AddWithValue("@NUMBER", SqlDbType.VarChar).Value = Number;
                    sql_cmnd.Parameters.AddWithValue("@EMAIL", SqlDbType.VarChar).Value = Email;
                    sql_cmnd.Parameters.AddWithValue("@OCCUPATION", SqlDbType.VarChar).Value = Occupation;
                    sql_cmnd.Parameters.AddWithValue("@GENDER", SqlDbType.VarChar).Value = Gender;
                    sql_cmnd.Parameters.AddWithValue("@AGE_GROUP", SqlDbType.VarChar).Value = Age;
                    sql_cmnd.Parameters.AddWithValue("@COMMUNICATION_TYPE", SqlDbType.VarChar).Value = Communication;
                    sql_cmnd.Parameters.AddWithValue("@CITY", SqlDbType.VarChar).Value = City;
                    sql_cmnd.Parameters.AddWithValue("@DOB", SqlDbType.Date).Value = DOB;
                    sql_cmnd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added Successfully!");
                    fname.Text = "";
                    lname.Text = "";
                    number.Text = "";
                    gender.Text = "";
                    email.Text = "";
                    occupation.Text = "";
                    age.Text = "";
                    communication.Text = "";
                    city.Text = "";
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Error Occur,Can not Add Customer!");
                }

                cns.Close();
            }
            else
            {
                MessageBox.Show("Fill All Fields", "Alert");
            }
        }

        private void clear(object sender, RoutedEventArgs e)
        {
            fname.Text = "";
            lname.Text = "";
            number.Text = "";
            gender.Text = "";
            email.Text = "";
            occupation.Text = "";
            age.Text = "";
            communication.Text = "";
            city.Text = "";
            dob.SelectedDate = new DateTime(1998, 1, 1);
        }
    }
}

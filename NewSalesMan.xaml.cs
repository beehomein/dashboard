using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.IO;
namespace DashBoard
{
    /// <summary>
    /// Interaction logic for NewSalesMan.xaml
    /// </summary>
    public partial class NewSalesMan : Window
    {
        public NewSalesMan()
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

        private void clearAllFields(object sender, RoutedEventArgs e)
        {
            //firstName.Text = "";
            //lastName.Text = "";
            //mobileNumber.Text = "";
            new NewSalesMan().Show();
            this.Close();
        }

        private void addNewSalesMan(object sender, RoutedEventArgs e)
        {
            var FirstName = firstName.Text;
            var LastName = lastName.Text;
            var MobileNumber = mobileNumber.Text;
            var DateOfJoining = dateOfJoining.SelectedDate;
            var Gender = gender.Text;
            var Email = email.Text;
            var DateOfBirth = dateOfBirth.Text;
            var IDProffType = IdProffType.Text;
            var IDProffNumber = IdProffNumber.Text;
            var Address = address.Text;
            if (FirstName != "" && LastName != "" && MobileNumber != "" && Gender != "" && Email != "" && IDProffNumber != "" && IDProffType != "" && Address != "")
            {
                try
                {
                    String ConnString;
                    string file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                    string cnn = File.ReadAllText(file);
                    //Decryption

                    string ou;
                    char[] output = new char[cnn.Length];
                    var i = 0;
                    foreach (var character in cnn)
                    {
                        output[i++] = (char)(character - '~');
                    }
                    ou = new string(output);
                    //Decrypting Text File

                    ConnString = ou.Substring(0, ou.Length - 2);
                    SqlConnection scn = new SqlConnection(ConnString);
                    SqlCommand cmd = new SqlCommand("SP_INSERT_SALES_MAN_REGISTER", scn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("F_NAME", FirstName);
                    cmd.Parameters.AddWithValue("L_NAME", LastName);
                    cmd.Parameters.AddWithValue("NUMBER", MobileNumber);
                    cmd.Parameters.AddWithValue("DOJ", DateOfJoining);
                    cmd.Parameters.AddWithValue("GENDER", Gender);
                    cmd.Parameters.AddWithValue("EMAIL", Email);
                    cmd.Parameters.AddWithValue("DOB", DateOfBirth);
                    cmd.Parameters.AddWithValue("ID_TYPE", IDProffType);
                    cmd.Parameters.AddWithValue("ID_NUMBER", IDProffNumber);
                    cmd.Parameters.AddWithValue("ADDRESS", Address);

                    scn.Open();
                    //MessageBox.Show("Connection Opened!");
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        MessageBox.Show("New Sales Man Added Sucessfully!");
                        scn.Close();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("New Sales Man Addition Failed!");
                    }
                    scn.Close();
                }
                catch
                {
                    MessageBox.Show("Connection Failed!");
                }
            }
            else
            {
                MessageBox.Show("Fill all fileds", "Alert");
            }

        }
    }
}

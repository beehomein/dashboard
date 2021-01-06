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
    /// Interaction logic for CompanyRegistrationxaml.xaml
    /// </summary>
    public partial class CompanyRegistration : Window
    {
        public CompanyRegistration()
        {
            try
            {
                string file1 = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                //MessageBox.Show(file);
                string cnn1 = File.ReadAllText(file1);

                //Decryption

                //string ou = new string(cnn);
                string ou1;
                char[] output1 = new char[cnn1.Length];
                var i1 = 0;
                foreach (var character in cnn1)
                {
                    output1[i1++] = (char)(character - '~');
                }
                ou1 = new string(output1);

                String ConnString1;
                //Decrypting Text File
                ConnString1 = ou1.Substring(0, ou1.Length - 2);
                //MessageBox.Show(ConnString);
                SqlConnection scn1 = new SqlConnection(ConnString1);
                scn1.Open();
                scn1.Close();
                InitializeComponent();
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
            var name = Name.Text;
            var type = Type.Text;
            var number = Number.Text;
            var email = Email.Text;
            var address = Address.Text;
            var companyId = CompanyId.Text;
            if( name!="" && type!= "Select Type" && number!="" && email!="" && address!="" && companyId!="")
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
                SqlCommand cmd = new SqlCommand("SP_INSERT_COMPANY_DETAILS", scn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("NAME", name);
                cmd.Parameters.AddWithValue("TYPE", type);
                cmd.Parameters.AddWithValue("NUMBER", number);
                cmd.Parameters.AddWithValue("EMAIL", email);
                cmd.Parameters.AddWithValue("ADDRESS", address);
                cmd.Parameters.AddWithValue("COMPANY_ID", companyId);

                scn.Open();
                //MessageBox.Show("Connection Opened!");
                int k = cmd.ExecuteNonQuery();
                if (k != 0)
                {
                    MessageBox.Show("Company Details Sucessfully!");
                    Name.Text = "";
                    Type.Text = "Select Type";
                    Number.Text = "";
                    Email.Text = "";
                    Address.Text = "";
                    CompanyId.Text = "";
                }
                else
                {
                    MessageBox.Show("Company Details Addition Failed!");
                    Name.Text = "";
                    Type.Text = "Select Type";
                    Number.Text = "";
                    Email.Text = "";
                    Address.Text = "";
                    CompanyId.Text = "";
                }
                scn.Close();
            }
            else
            {
                MessageBox.Show("Fill All Fields", "Alert");
            }
        }
        private void clear(object sender, RoutedEventArgs e)
        {
            Name.Text = "";
            Type.Text = "Select Type";
            Number.Text = "";
            Email.Text = "";
            Address.Text = "";
            CompanyId.Text = "";
        }
    }
}

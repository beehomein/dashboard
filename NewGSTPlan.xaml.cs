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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.IO;
namespace DashBoard
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class NewGSTPlan : Window
    {
        public NewGSTPlan()
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
                display();
            }
            catch
            {
                new Connection().Show();
                MessageBox.Show("SQL Connection Required!", "Alert");
                this.Close();
            }
        }
        private void reset(object sender, RoutedEventArgs e)
        {
            above.Text = "";
            gst.Text = "";
        }
        private void addgst(object sender, RoutedEventArgs e)
        {
            var Above = above.Text;
            var GST = gst.Text;
            if (Above != "" && GST != "")
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
                    SqlCommand cmd = new SqlCommand("SP_INSERT_GST_PLAN", scn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ABOVE", Above);
                    cmd.Parameters.AddWithValue("GST", GST);

                    scn.Open();
                    //MessageBox.Show("Connection Opened!");
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        MessageBox.Show("New GST plan Added Sucessfully!");
                    }
                    else
                    {
                        MessageBox.Show("New GST plan Addition Failed!");
                    }
                    scn.Close();
                    display();
                    above.Text = "";
                    gst.Text = "";
                }
                catch
                {
                    MessageBox.Show("Connection Failed!");
                }
            }
            else
            {
                MessageBox.Show("Fill All Fields!");
            }
        }
        public void display()
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
            SqlCommand cmd = new SqlCommand("SP_LIST_GST_PLAN", scn);
            cmd.CommandType = CommandType.StoredProcedure;
            scn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            resArea.Children.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string fs = "18";
                StackPanel s1 = new StackPanel();
                TextBlock textBlock1 = new TextBlock();
                textBlock1.Margin = new Thickness(5, 10, 5, 10);
                textBlock1.Inlines.Add("Above : " + row[1].ToString() + " Rs ");
                textBlock1.FontSize = int.Parse(fs);
                TextBlock textBlock2 = new TextBlock();
                textBlock2.Margin = new Thickness(5, 10, 5, 10);
                textBlock2.Inlines.Add("GGT : " + row[2].ToString() + " % ");
                textBlock2.FontSize = int.Parse(fs);
                s1.Orientation = Orientation.Horizontal;
                s1.HorizontalAlignment = HorizontalAlignment.Center;
                s1.Margin = new Thickness(5, 10, 5, 10);
                s1.Children.Add(textBlock1);
                s1.Children.Add(textBlock2);
                resArea.Children.Add(s1);
            }
            scn.Close();
        }
    }
}


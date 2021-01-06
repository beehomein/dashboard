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
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Linq;

namespace DashBoard
{

    public partial class Connection : Window
    {
        public Connection()
        {
            InitializeComponent();
            //Current Directory
            //MessageBox.Show(System.IO.Directory.GetCurrentDirectory());
        }
        private void connect(object sender, RoutedEventArgs e)
        {
            //User Defined Values
            var ServerName = serverName.Text;
            var DatabaseName = databaseName.Text;
            var UserId = userId.Text;
            var Password = password.Password;
            if (ServerName != "" && DatabaseName != "" && UserId != "" && Password != "")
            {
                try
                {
                    String ConnString;
                    //Actual Connection String
                    //ConnString = @"Data Source=DESKTOP-CP8V18I\BEE007470;Initial Catalog=billing;User ID=sa;Password=Crazycoders@2020";
                    ConnString = @"Data Source=" + ServerName + ";Initial Catalog=" + DatabaseName + ";User ID=" + UserId + ";Password=" + Password;
                    SqlConnection scn = new SqlConnection(ConnString);
                    scn.Open();
                    scn.Close();
                    //MessageBox.Show(ConnString);
                    //Create text file
                    string file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                    var myFile = File.Create(file);
                    myFile.Close();
                    string text = ConnString;
                    try
                    {
                        StreamWriter writer = new StreamWriter(file, true);

                        //Encryption 
                        string input = text;
                        char[] output = new char[input.Length];
                        int i = 0;
                        foreach (var character in input)
                        {
                            output[i++] = (char)(character + '~');
                        }
                        string ou = new string(output);

                        text = ou;
                        //Writing Connection String in Text file
                        writer.WriteLine(text);
                        writer.Close();
                        //MessageBox.Show("text writed");
                        serverName.Text = "";
                        databaseName.Text = "";
                        userId.Text = "";
                        password.Password = "";
                        MessageBox.Show("Connection Verified", "SQL Connection");
                        new Dashboard().Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        //Connection String Write Exception
                        MessageBox.Show(ex.ToString());
                    }
                }
                catch (Exception ex)
                {
                    //Database Connection Exception
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Fill All Fields!", "Alert");
            }
        }

        private void resetAll(object sender, RoutedEventArgs e)
        {
            //Clear all input fields
            serverName.Text = "";
            databaseName.Text = "";
            userId.Text = "";
            password.Password = "";
        }
    }
}

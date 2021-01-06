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
using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace DashBoard
{
    /// <summary>
    /// Interaction logic for NewGiftCard.xaml
    /// </summary>
    public partial class NewGiftCard : Window
    {
        public NewGiftCard()
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

        private void addGiftCard(object sender, RoutedEventArgs e)
        {
            var CardNumber = cardNumber.Text;
            var Amount = amount.Text;
            var PIN = pin.Text;
            var Validity = validity.SelectedDate;
            if(CardNumber!="" && Amount != "" && PIN != "")
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
                    SqlCommand cmd = new SqlCommand("SP_INSERT_GIFT_CARDS", scn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("CARD_NUMBER", CardNumber);
                    cmd.Parameters.AddWithValue("AMOUNT", Amount);
                    cmd.Parameters.AddWithValue("PIN", PIN);
                    cmd.Parameters.AddWithValue("VALIDITY", Validity);

                    scn.Open();
                    //MessageBox.Show("Connection Opened!");
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        MessageBox.Show("New Gift Card Added Sucessfully!");
                        scn.Close();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("New Gift Card Addition Failed!");
                    }
                    scn.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                    MessageBox.Show("Connection Failed", "Alert");
                }
            }
            else
            {
                MessageBox.Show("Fill All Fields", "Alert");
            }
        }

        private void clearAllItems(object sender, RoutedEventArgs e)
        {

        }
    }
}

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
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for ProductAttributes.xaml
    /// </summary>
    public partial class NewProductAttributes : Window
    {
        public NewProductAttributes()
        {
            try
            {

                InitializeComponent();
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

                //Category Combo Box
                SqlCommand cmd = new SqlCommand("SP_LIST_CATEGORY", scn);
                cmd.CommandType = CommandType.StoredProcedure;
                scn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);

                //Adding Empty Messages for Category Drop Down
                ComboBoxItem ci = new ComboBoxItem();
                ci.Content = "Select Category";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                CategoryItems1.Items.Add(ci);

                ci = new ComboBoxItem();
                ci.Content = "Select Category";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                CategoryItems2.Items.Add(ci);

                ci = new ComboBoxItem();
                ci.Content = "Select Category";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                CategoryItems3.Items.Add(ci);

                //Adding dynamic values for Category Drop Down
                foreach (DataRow row in dt.Rows)
                {

                    ci = new ComboBoxItem();
                    ci.Content = row[0].ToString();
                    ci.FontSize = 15;
                    CategoryItems1.Items.Add(ci);

                    ci = new ComboBoxItem();
                    ci.Content = row[0].ToString();
                    ci.FontSize = 15;
                    CategoryItems2.Items.Add(ci);

                    ci = new ComboBoxItem();
                    ci.Content = row[0].ToString();
                    ci.FontSize = 15;
                    CategoryItems3.Items.Add(ci);

                }
                scn.Close();

            }
            catch
            {
                new Connection().Show();
                MessageBox.Show("SQL Connection Required!", "Alert");
                this.Close();
            }
        }
        public void hideAll()
        {
            group.Height = 0;
            division.Height = 0;
            brand.Height = 0;
        }
        private void Button1(object sender, RoutedEventArgs e)
        {
            hideAll();
            group.Height = 250;
        }

        private void Hide1(object sender, RoutedEventArgs e)
        {
            group.Height = 0;
        }

        private void Button2(object sender, RoutedEventArgs e)
        {
            hideAll();
            division.Height = 350;
        }

        private void Hide2(object sender, RoutedEventArgs e)
        {
            division.Height = 0;
        }

        private void Button4(object sender, RoutedEventArgs e)
        {
            hideAll();
            brand.Height = 550;
        }

        private void Hide4(object sender, RoutedEventArgs e)
        {
            brand.Height = 0;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addGroup(object sender, RoutedEventArgs e)
        {
            var category = CategoryItems1.Text;
            var group = GGroup.Text;

            if (group == "" && category == "Select Category")
            {
                MessageBox.Show("Select Category \n Enter Group", "Alert");
            }

            else if (category == "Select Category")
            {
                MessageBox.Show("Select Category", "Alert");
            }

            else if (group == "")
            {
                MessageBox.Show("Enter Group", "Alert");
            }
            else
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

                    SqlCommand cmd = new SqlCommand("SP_LIST_DYNAMIC_GROUP_TABLE", scn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NAME", category);
                    scn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Employee");
                    sda.Fill(dt);
                    var flag = true;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (group == row[0].ToString())
                        {
                            flag = false;
                        }
                    }
                    scn.Close();
                    if (flag)
                    {
                        scn = new SqlConnection(ConnString);
                        cmd = new SqlCommand("SP_INSERT_CAETEGORY_INTO_DYNAMIC_GROUP_TABLE", scn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("NAME", category);
                        cmd.Parameters.AddWithValue("CAETEGORY", group);

                        scn.Open();
                        //MessageBox.Show("Connection Opened!");
                        int k = cmd.ExecuteNonQuery();
                        if (k != 0)
                        {
                            MessageBox.Show("New Group : " + group + "\nunder Category : " + category + "\nAdded Sucessfully!", "Success");
                        }
                        else
                        {
                            MessageBox.Show("New Group : " + group + "\nunder Category : " + category + "\nAddition Failed!", "Failed");
                        }
                        scn.Close();
                    }
                    else
                    {
                        MessageBox.Show("New Group : " + group + "\nunder Category : " + category + "\nAddition Failed!", "Group " + group + " already exists");
                    }
                    CategoryItems1.Text = "Select Category";
                    GGroup.Text = "";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void DcategoryChangeEvent(object sender, EventArgs e)
        {

            var Dcategory = (ComboBox)sender;
            var Category= Dcategory.Text;
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

                //Category Combo Box
                SqlCommand cmd = new SqlCommand("SP_LIST_DYNAMIC_GROUP_TABLE", scn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("NAME", Category);
                scn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                GroupItems1.Items.Clear();
                //Adding Empty Messages for Category Drop Down
                ComboBoxItem ci = new ComboBoxItem();
                ci.Content = "Select Group";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                GroupItems1.Items.Add(ci);


                //Adding dynamic values for Category Drop Down
                foreach (DataRow row in dt.Rows)
                {

                    ci = new ComboBoxItem();
                    ci.Content = row[0].ToString();
                    ci.FontSize = 15;
                    GroupItems1.Items.Add(ci);

                }
                scn.Close();

            }
            catch
            {
                GroupItems1.Items.Clear();
                ComboBoxItem ci = new ComboBoxItem();
                ci.Content = "No Data Found Add New Group";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                GroupItems1.Items.Add(ci);
            }

        }
        private void BcategoryChangeEvent(object sender, EventArgs e)
        {

            var Dcategory = (ComboBox)sender;
            var Category = Dcategory.Text;
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

                //Category Combo Box
                SqlCommand cmd = new SqlCommand("SP_LIST_DYNAMIC_GROUP_TABLE", scn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("NAME", Category);
                scn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                GroupItems2.Items.Clear();
                //Adding Empty Messages for Category Drop Down
                ComboBoxItem ci = new ComboBoxItem();
                ci.Content = "Select Group";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                GroupItems2.Items.Add(ci);


                //Adding dynamic values for Category Drop Down
                foreach (DataRow row in dt.Rows)
                {

                    ci = new ComboBoxItem();
                    ci.Content = row[0].ToString();
                    ci.FontSize = 15;
                    GroupItems2.Items.Add(ci);

                }
                scn.Close();

            }
            catch
            {
                GroupItems2.Items.Clear();
                ComboBoxItem ci = new ComboBoxItem();
                ci.Content = "No Data Found Add New Group";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                GroupItems2.Items.Add(ci);
            }

        }

        private void BgroupChangeEvent(object sender, EventArgs e)
        {
            var Category = CategoryItems3.Text;
            var Group = GroupItems2.Text;
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

                //Category Combo Box
                SqlCommand cmd = new SqlCommand("SP_LIST_DYNAMIC_GROUP_TABLE", scn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("NAME", Category+"_"+Group);
                scn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                DivisionItems.Items.Clear();
                //Adding Empty Messages for Category Drop Down
                ComboBoxItem ci = new ComboBoxItem();
                ci.Content = "Select Division";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                DivisionItems.Items.Add(ci);


                //Adding dynamic values for Category Drop Down
                foreach (DataRow row in dt.Rows)
                {

                    ci = new ComboBoxItem();
                    ci.Content = row[0].ToString();
                    ci.FontSize = 15;
                    DivisionItems.Items.Add(ci);

                }
                scn.Close();

            }
            catch
            {
                DivisionItems.Items.Clear();
                ComboBoxItem ci = new ComboBoxItem();
                ci.Content = "No Data Found Add New Group";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                DivisionItems.Items.Add(ci);
            }
        }

        private void addDivision(object sender, RoutedEventArgs e)
        {
            var Category = CategoryItems2.Text;
            var Group = GroupItems1.Text;
            var Division = DivisionText.Text;
            if (Category== "Select Category" && Group== "No Data Found Select Category" && Division=="")
            {
                MessageBox.Show("Select Category,\nGroup and Division", "Alert");
            }
            else if (Category == "Select Category")
            {
                MessageBox.Show("Select Category", "Alert");
            }
            else if ( Group == "No Data Found Select Category" || Group== "Select Group")
            {
                MessageBox.Show("Select Group", "Alert");
            }
            else if ( Division == "")
            {
                MessageBox.Show("Enter Division", "Alert");
            }
            else if(Category != "Select Category" && Group != "No Data Found Select Category" && Group != "Select Group" && Division != "")
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

                SqlCommand cmd = new SqlCommand("SP_LIST_DYNAMIC_GROUP_TABLE", scn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("NAME", Category+"_"+ Group);
                scn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                var flag = true;
                foreach (DataRow row in dt.Rows)
                {
                    if (Division == row[0].ToString())
                    {
                        flag = false;
                    }
                }
                scn.Close();
                if (flag)
                {
                    scn = new SqlConnection(ConnString);
                    cmd = new SqlCommand("SP_INSERT_DIVISION_INTO_DYNAMIC_GROUP_TABLE", scn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NAME", Category);
                    cmd.Parameters.AddWithValue("CAETEGORY", Group);
                    cmd.Parameters.AddWithValue("DIVISION", Division);

                    scn.Open();
                    //MessageBox.Show("Connection Opened!");
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        MessageBox.Show("New Division : " + Division + "\nunder Group : " + Group + "\nunder Category : " + Category + "\nAdded Sucessfully!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("New Division : " + Division + "\nunder Group : " + Group + "\nunder Category : " + Category + "\nAddition Failed!", "Failed");
                    }
                    scn.Close();
                }
                else
                {
                    MessageBox.Show("New Division : " + Division + "\nunder Group : " + Group + "\nunder Category : " + Category + "\nAddition Failed!", "Group : " + Group + " already exists");
                }

                //
                CategoryItems2.Text = "Select Category";
                GroupItems1.Items.Clear();
                ComboBoxItem ci = new ComboBoxItem();
                ci.Content = "No Data Found Select Category";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                GroupItems1.Items.Add(ci);
                DivisionText.Text = "";
            }
        }

        private void addBrand(object sender, RoutedEventArgs e)
        {

            var Category = CategoryItems3.Text;
            var Group = GroupItems2.Text;
            var Division = DivisionItems.Text;
            var Brand = BrandText.Text;
            if (Category == "Select Category" && Group == "No Data Found Select Category" && Division == "No Data Found Select Group" && Brand =="")
            {
                MessageBox.Show("Select Category, Group, Division and Enter Brand", "Alert");
            }
            else if (Category == "Select Category")
            {
                MessageBox.Show("Select Category", "Alert");
            }
            else if (Group == "No Data Found Select Category" || Group == "Select Group")
            {
                MessageBox.Show("Select Group", "Alert");
            }
            else if (Division == "No Data Found Select Group" || Division == "Select Division")
            {
                MessageBox.Show("Select Division", "Alert");
            }
            else if (Brand == "")
            {
                MessageBox.Show("Enter Brand", "Alert");
            }
            else if (Category != "Select Category" && Group != "No Data Found Select Category" && Group != "Select Group" && Division != "No Data Found Select Group" && Division!= "Select Division" && Brand != "")
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

                SqlCommand cmd = new SqlCommand("SP_LIST_DYNAMIC_GROUP_TABLE", scn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("NAME", Category + "_" + Group + "_" + Division);
                scn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                var flag = true;
                foreach (DataRow row in dt.Rows)
                {
                    if (Brand == row[0].ToString())
                    {
                        flag = false;
                    }
                }
                scn.Close();
                if (flag)
                {
                    scn = new SqlConnection(ConnString);
                    cmd = new SqlCommand("SP_INSERT_BRAND_INTO_DYNAMIC_GROUP_TABLE", scn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NAME", Category);
                    cmd.Parameters.AddWithValue("CAETEGORY", Group);
                    cmd.Parameters.AddWithValue("DIVISION", Division);
                    cmd.Parameters.AddWithValue("BRAND", Brand);

                    scn.Open();
                    //MessageBox.Show("Connection Opened!");
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        MessageBox.Show("New Brand : "+Brand+" \nunder Division : " + Division + "\nunder Group : " + Group + "\nunder Category : " + Category + "\nAdded Sucessfully!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("New Brand : " + Brand + " \nunder Division : " + Division + "\nunder Group : " + Group + "\nunder Category : " + Category + "\nAddition Failed!", "Failed");
                    }
                    scn.Close();
                }
                else
                {
                    MessageBox.Show("New Brand : " + Brand + " \nunder Division : " + Division + "\nunder Group : " + Group + "\nunder Category : " + Category + "\nAddition Failed!", "Group : " + Group + " already exists");
                }

                //
                CategoryItems3.Text = "Select Category";
                GroupItems2.Items.Clear();
                ComboBoxItem ci = new ComboBoxItem();
                ci.Content = "No Data Found Select Category";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                GroupItems2.Items.Add(ci);
                DivisionItems.Items.Clear();
                ci = new ComboBoxItem();
                ci.Content = "No Data Found Select Group";
                ci.IsEnabled = false;
                ci.IsSelected = true;
                ci.FontSize = 15;
                DivisionItems.Items.Add(ci);
                BrandText.Text = "";
            }
        }
    }
}
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
    /// Interaction logic for NewProduct.xaml
    /// </summary>
    public partial class NewProduct : Window
    {
        public int preCount = 0;
        public ComboBox tempList;
        public ComboBox tempList1;
        public ComboBox tempList2;
        public NewProduct()
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
            Fill();
        }
        public void Fill()
        {
            root.Children.Clear();
            preCount = 0;
            //Row 1
            var Wrap = new WrapPanel();
            Wrap.Height = 100;
            Wrap.HorizontalAlignment = HorizontalAlignment.Center;
            Wrap.VerticalAlignment = VerticalAlignment.Center;
            Wrap.Margin = new Thickness(0, 50, 0, 0);
            Wrap.Name = "ParentA" + (preCount + 1);

            //Category
            var Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            var Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Category";

            var Combo = new ComboBox();
            Combo.Width = 170;
            Combo.Margin = new Thickness(10);
            Combo.Margin = new Thickness(5);
            Combo.FontSize = 20;
            Combo.Name = "Category" + (preCount + 1);
            //MessageBox.Show(Combo.Name);
            var CategoryListObj = Combo;
            //Dynamic Category

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
            SqlCommand cmd = new SqlCommand("SP_LIST_CATEGORY", scn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            scn.Open();

            var Item = new ComboBoxItem();
            Item.Content = "Select Category";
            Item.IsSelected = true;
            Item.IsEnabled = false;
            Combo.Items.Add(Item);
            foreach (DataRow row in dt.Rows)
            {
                Item = new ComboBoxItem();
                Item.Content = row[0].ToString();
                Combo.Items.Add(Item);
            }
            scn.Close();

            Stack.Children.Add(Text);
            Stack.Children.Add(Combo);
            Wrap.Children.Add(Stack);

            //Group
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Group";


            this.tempList = new ComboBox();
            Combo = this.tempList;
            Combo.Width = 170;
            Combo.Margin = new Thickness(10);
            Combo.Margin = new Thickness(5);
            Combo.FontSize = 20;
            Combo.Name = "Group" + (preCount + 1);
            var GroupObj = Combo;


            Item = new ComboBoxItem();
            Item.Content = "Select Group";
            Item.IsSelected = true;
            Item.IsEnabled = false;
            this.tempList.Items.Add(Item);
            //calling handler for caterory change
            CategoryListObj.DropDownClosed += new EventHandler(Category_DropDownClosed);

            //Combo.Items.Add(Item);
            Stack.Children.Add(Text);
            Stack.Children.Add(this.tempList);
            Wrap.Children.Add(Stack);

            //Division
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Division";

            this.tempList1 = new ComboBox();
            Combo = this.tempList1;
            Combo.Width = 170;
            Combo.Margin = new Thickness(10);
            Combo.Margin = new Thickness(5);
            Combo.FontSize = 20;
            Combo.Name = "Division" + (preCount + 1);
            var DivisionObj = Combo;

            Item = new ComboBoxItem();
            Item.Content = "Select Division";
            Item.IsSelected = true;
            Item.IsEnabled = false;
            this.tempList1.Items.Add(Item);
            //calling handler for Group change
            GroupObj.DropDownClosed += new EventHandler((sender, e) => Group_DropDownClosed(sender, e, CategoryListObj));

            //Combo.Items.Add(Item);
            Stack.Children.Add(Text);
            Stack.Children.Add(this.tempList1);
            Wrap.Children.Add(Stack);

            //Size
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Size";

            Combo = new ComboBox();
            Combo.Width = 170;
            Combo.Margin = new Thickness(10);
            Combo.Margin = new Thickness(5);
            Combo.FontSize = 20;
            Combo.Name = "Size" + (preCount + 1);

            //Dynamic Category

            file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
            //MessageBox.Show(file);
            cnn = File.ReadAllText(file);

            //Decryption

            //string ou = new string(cnn);
            output = new char[cnn.Length];
            i = 0;
            foreach (var character in cnn)
            {
                output[i++] = (char)(character - '~');
            }
            ou = new string(output);

            //Decrypting Text File
            ConnString = ou.Substring(0, ou.Length - 2);
            //MessageBox.Show(ConnString);
            scn = new SqlConnection(ConnString);
            cmd = new SqlCommand("SP_LIST_SIZE", scn);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable("Employee");
            sda.Fill(dt);
            scn.Open();

            Item = new ComboBoxItem();
            Item.Content = "Select Size";
            Item.IsSelected = true;
            Item.IsEnabled = false;
            Combo.Items.Add(Item);
            foreach (DataRow row in dt.Rows)
            {
                Item = new ComboBoxItem();
                Item.Content = row[1].ToString();
                Combo.Items.Add(Item);
            }
            scn.Close();

            //Combo.Items.Add(Item);
            Stack.Children.Add(Text);
            Stack.Children.Add(Combo);
            Wrap.Children.Add(Stack);

            //Brand
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Brand";

            this.tempList2 = new ComboBox();
            Combo = this.tempList2;
            Combo.Width = 170;
            Combo.Margin = new Thickness(10);
            Combo.Margin = new Thickness(5);
            Combo.FontSize = 20;
            Combo.Name = "Brand" + (preCount + 1);

            Item = new ComboBoxItem();
            Item.Content = "Select Brand";
            Item.IsSelected = true;
            Item.IsEnabled = false;
            this.tempList2.Items.Add(Item);
            //calling handler for Group change
            DivisionObj.DropDownClosed += new EventHandler((sender, e) => Division_DropDownClosed(sender, e, CategoryListObj, GroupObj));

            //Combo.Items.Add(Item);
            Stack.Children.Add(Text);
            Stack.Children.Add(this.tempList2);
            Wrap.Children.Add(Stack);

            root.Children.Add(Wrap);
            preCount++;
            //Row 2
            Wrap = new WrapPanel();
            Wrap.Height = 100;
            Wrap.HorizontalAlignment = HorizontalAlignment.Center;
            Wrap.VerticalAlignment = VerticalAlignment.Center;
            Wrap.Name = "ParentB" + (preCount + 1);

            //Style Code
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Style Code";

            var Input = new TextBox();
            Input.Margin = new Thickness(10);
            Input.FontSize = 20;
            Input.Name = "StyleCode" + (preCount + 1);

            Stack.Children.Add(Text);
            Stack.Children.Add(Input);
            Wrap.Children.Add(Stack);

            //Borcode Id
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Borcode Id";

            Input = new TextBox();
            Input.Margin = new Thickness(10);
            Input.FontSize = 20;
            Input.Name = "textboxes" + (preCount + 1);

            Stack.Children.Add(Text);
            Stack.Children.Add(Input);
            Wrap.Children.Add(Stack);

            //Length
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Length";

            Input = new TextBox();
            Input.Margin = new Thickness(10);
            Input.FontSize = 20;
            Input.Name = "Length" + (preCount + 1);
            Stack.Children.Add(Text);
            Stack.Children.Add(Input);
            Wrap.Children.Add(Stack);

            //MRP
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "MRP";

            Input = new TextBox();
            Input.Margin = new Thickness(10);
            Input.FontSize = 20;
            Input.Name = "MRP" + (preCount + 1);
            Stack.Children.Add(Text);
            Stack.Children.Add(Input);
            Wrap.Children.Add(Stack);
            //Manufactured Date
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Manufactured Date";

            var Date = new DatePicker();
            Date.FontSize = 20;
            Date.Name = "ManufacturedDate" + (preCount + 1);
            Date.Margin = new Thickness(10);
            Stack.Children.Add(Text);
            Stack.Children.Add(Date);
            Wrap.Children.Add(Stack);

            root.Children.Add(Wrap);
            preCount++;
            if (root.Children.Count < 3)
            {
                btnRemove.Width = 0;
            }
            else
            {
                btnRemove.Width = 100;
            }
        }
        private void add(object sender, RoutedEventArgs e)
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
            //Row 1
            var Wrap = new WrapPanel();
            Wrap.Height = 100;
            Wrap.HorizontalAlignment = HorizontalAlignment.Center;
            Wrap.VerticalAlignment = VerticalAlignment.Center;
            Wrap.Margin = new Thickness(0, 50, 0, 0);
            Wrap.Name = "ParentA" + (preCount + 1);

            //Category
            var Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            var Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Category";

            var Combo = new ComboBox();
            Combo.Width = 170;
            Combo.Margin = new Thickness(10);
            Combo.Margin = new Thickness(5);
            Combo.FontSize = 20;
            Combo.Name = "Category" + (preCount + 1);
            //MessageBox.Show(Combo.Name);
            var CategoryListObj = Combo;
            //Dynamic Category

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
            SqlCommand cmd = new SqlCommand("SP_LIST_CATEGORY", scn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            scn.Open();

            var Item = new ComboBoxItem();
            Item.Content = "Select Category";
            Item.IsSelected = true;
            Item.IsEnabled = false;
            Combo.Items.Add(Item);
            foreach (DataRow row in dt.Rows)
            {
                Item = new ComboBoxItem();
                Item.Content = row[0].ToString();
                Combo.Items.Add(Item);
            }
            scn.Close();

            Stack.Children.Add(Text);
            Stack.Children.Add(Combo);
            Wrap.Children.Add(Stack);

            //Group
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Group";


            this.tempList = new ComboBox();
            Combo = this.tempList;
            Combo.Width = 170;
            Combo.Margin = new Thickness(10);
            Combo.Margin = new Thickness(5);
            Combo.FontSize = 20;
            Combo.Name = "Group" + (preCount + 1);
            var GroupObj = Combo;


            Item = new ComboBoxItem();
            Item.Content = "Select Group";
            Item.IsSelected = true;
            Item.IsEnabled = false;
            this.tempList.Items.Add(Item);
            //calling handler for caterory change
            CategoryListObj.DropDownClosed += new EventHandler(Category_DropDownClosed);

            //Combo.Items.Add(Item);
            Stack.Children.Add(Text);
            Stack.Children.Add(this.tempList);
            Wrap.Children.Add(Stack);

            //Division
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Division";

            this.tempList1 = new ComboBox();
            Combo = this.tempList1;
            Combo.Width = 170;
            Combo.Margin = new Thickness(10);
            Combo.Margin = new Thickness(5);
            Combo.FontSize = 20;
            Combo.Name = "Division" + (preCount + 1);
            var DivisionObj = Combo;

            Item = new ComboBoxItem();
            Item.Content = "Select Division";
            Item.IsSelected = true;
            Item.IsEnabled = false;
            this.tempList1.Items.Add(Item);
            //calling handler for Group change
            GroupObj.DropDownClosed += new EventHandler((sender1, e1) => Group_DropDownClosed(sender1, e1, CategoryListObj));

            //Combo.Items.Add(Item);
            Stack.Children.Add(Text);
            Stack.Children.Add(this.tempList1);
            Wrap.Children.Add(Stack);

            //Size
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Size";

            Combo = new ComboBox();
            Combo.Width = 170;
            Combo.Margin = new Thickness(10);
            Combo.Margin = new Thickness(5);
            Combo.FontSize = 20;
            Combo.Name = "Size" + (preCount + 1);

            //Dynamic Category

            file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
            //MessageBox.Show(file);
            cnn = File.ReadAllText(file);

            //Decryption

            //string ou = new string(cnn);
            output = new char[cnn.Length];
            i = 0;
            foreach (var character in cnn)
            {
                output[i++] = (char)(character - '~');
            }
            ou = new string(output);

            //Decrypting Text File
            ConnString = ou.Substring(0, ou.Length - 2);
            //MessageBox.Show(ConnString);
            scn = new SqlConnection(ConnString);
            cmd = new SqlCommand("SP_LIST_SIZE", scn);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable("Employee");
            sda.Fill(dt);
            scn.Open();

            Item = new ComboBoxItem();
            Item.Content = "Select Size";
            Item.IsSelected = true;
            Item.IsEnabled = false;
            Combo.Items.Add(Item);
            foreach (DataRow row in dt.Rows)
            {
                Item = new ComboBoxItem();
                Item.Content = row[1].ToString();
                Combo.Items.Add(Item);
            }
            scn.Close();

            //Combo.Items.Add(Item);
            Stack.Children.Add(Text);
            Stack.Children.Add(Combo);
            Wrap.Children.Add(Stack);

            //Brand
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Brand";

            this.tempList2 = new ComboBox();
            Combo = this.tempList2;
            Combo.Width = 170;
            Combo.Margin = new Thickness(10);
            Combo.Margin = new Thickness(5);
            Combo.FontSize = 20;
            Combo.Name = "Brand" + (preCount + 1);

            Item = new ComboBoxItem();
            Item.Content = "Select Brand";
            Item.IsSelected = true;
            Item.IsEnabled = false;
            this.tempList2.Items.Add(Item);
            //calling handler for Group change
            DivisionObj.DropDownClosed += new EventHandler((sender2, e2) => Division_DropDownClosed(sender2, e2, CategoryListObj, GroupObj));

            //Combo.Items.Add(Item);
            Stack.Children.Add(Text);
            Stack.Children.Add(this.tempList2);
            Wrap.Children.Add(Stack);

            root.Children.Add(Wrap);
            preCount++;
            //Row 2
            Wrap = new WrapPanel();
            Wrap.Height = 100;
            Wrap.HorizontalAlignment = HorizontalAlignment.Center;
            Wrap.VerticalAlignment = VerticalAlignment.Center;
            Wrap.Name = "ParentB" + (preCount + 1);

            //Style Code
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Style Code";

            var Input = new TextBox();
            Input.Margin = new Thickness(10);
            Input.FontSize = 20;
            Input.Name = "StyleCode" + (preCount + 1);

            Stack.Children.Add(Text);
            Stack.Children.Add(Input);
            Wrap.Children.Add(Stack);

            //Borcode Id
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Borcode Id";

            Input = new TextBox();
            Input.Margin = new Thickness(10);
            Input.FontSize = 20;
            Input.Name = "textboxes" + (preCount + 1);

            Stack.Children.Add(Text);
            Stack.Children.Add(Input);
            Wrap.Children.Add(Stack);

            //Length
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Length";

            Input = new TextBox();
            Input.Margin = new Thickness(10);
            Input.FontSize = 20;
            Input.Name = "Length" + (preCount + 1);
            Stack.Children.Add(Text);
            Stack.Children.Add(Input);
            Wrap.Children.Add(Stack);

            //MRP
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "MRP";

            Input = new TextBox();
            Input.Margin = new Thickness(10);
            Input.FontSize = 20;
            Input.Name = "MRP" + (preCount + 1);
            Stack.Children.Add(Text);
            Stack.Children.Add(Input);
            Wrap.Children.Add(Stack);
            //Manufactured Date
            Stack = new StackPanel();
            Stack.Width = 200;
            Stack.HorizontalAlignment = HorizontalAlignment.Center;
            Stack.VerticalAlignment = VerticalAlignment.Center;

            Text = new TextBlock();
            Text.Margin = new Thickness(10);
            Text.FontSize = 20;
            Text.Text = "Manufactured Date";

            var Date = new DatePicker();
            Date.FontSize = 20;
            Date.Name = "ManufacturedDate" + (preCount + 1);
            Date.Margin = new Thickness(10);
            Stack.Children.Add(Text);
            Stack.Children.Add(Date);
            Wrap.Children.Add(Stack);

            root.Children.Add(Wrap);
            preCount++;
            if (root.Children.Count < 3)
            {
                btnRemove.Width = 0;
            }
            else
            {
                btnRemove.Width = 100;
            }

        }

        private void remove(object sender, RoutedEventArgs e)
        {
            if (root.Children.Count > 2)
            {
                root.Children.RemoveAt(root.Children.Count - 1);
                preCount--;
                root.Children.RemoveAt(root.Children.Count - 1);
                preCount--;
            }
            if (root.Children.Count < 3)
            {
                btnRemove.Width = 0;
            }
            else
            {
                btnRemove.Width = 100;
            }
        }
        public void Category_DropDownClosed(object sender, EventArgs e)
        {
            var Category = (ComboBox)sender;
            var CategoryText = Category.Text;
            //MessageBox.Show("Hello "+CategoryText);
            if (CategoryText != "Select Category")
            {
                //Dynamic Category

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
                SqlCommand cmd = new SqlCommand("SP_LIST_DYNAMIC_GROUP_TABLE", scn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("NAME", CategoryText);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                scn.Open();
                //var cb = new ComboBox();
                var Item = new ComboBoxItem();
                this.tempList.Items.Clear();
                Item = new ComboBoxItem();
                Item.Content = "Select Group";
                Item.IsSelected = true;
                Item.IsEnabled = false;
                this.tempList.Items.Add(Item);
                foreach (DataRow row in dt.Rows)
                {
                    Item = new ComboBoxItem();
                    Item.Content = row[0].ToString();
                    //MessageBox.Show(row[0].ToString());
                    this.tempList.Items.Add(Item);
                }
                //this.tempList = destination;
                scn.Close();
            }
        }
        public void Group_DropDownClosed(object sender, EventArgs e,ComboBox category)
        {

            var Category = (ComboBox)category;
            var CategoryText = Category.Text;
            var Group = (ComboBox)sender;
            var GroupText = Group.Text;
            //MessageBox.Show("Hello "+CategoryText);
            if(GroupText!="Select Group")
            {
                //Dynamic Category

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
                SqlCommand cmd = new SqlCommand("SP_LIST_DYNAMIC_GROUP_TABLE", scn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("NAME", CategoryText + "_" + GroupText);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                scn.Open();
                //var cb = new ComboBox();
                var Item = new ComboBoxItem();
                this.tempList1.Items.Clear();
                Item = new ComboBoxItem();
                Item.Content = "Select Division";
                Item.IsSelected = true;
                Item.IsEnabled = false;
                this.tempList1.Items.Add(Item);
                foreach (DataRow row in dt.Rows)
                {
                    Item = new ComboBoxItem();
                    Item.Content = row[0].ToString();
                    //MessageBox.Show(row[0].ToString());
                    this.tempList1.Items.Add(Item);
                }
                //this.tempList = destination;
                scn.Close();
            }
        }
        public void Division_DropDownClosed(object sender, EventArgs e, ComboBox category,ComboBox group)
        {

            var Category = (ComboBox)category;
            var CategoryText = Category.Text;
            var Group = (ComboBox)group;
            var GroupText = Group.Text;
            var Division = (ComboBox)sender;
            var DivisionText = Division.Text;
            //MessageBox.Show("Hello "+CategoryText);
            if (DivisionText != "Select Division")
            {
                //Dynamic Category

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
                SqlCommand cmd = new SqlCommand("SP_LIST_DYNAMIC_GROUP_TABLE", scn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("NAME", CategoryText + "_" + GroupText + "_" + DivisionText);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                scn.Open();
                //var cb = new ComboBox();
                var Item = new ComboBoxItem();
                this.tempList2.Items.Clear();
                Item = new ComboBoxItem();
                Item.Content = "Select Brand";
                Item.IsSelected = true;
                Item.IsEnabled = false;
                this.tempList2.Items.Add(Item);
                foreach (DataRow row in dt.Rows)
                {
                    Item = new ComboBoxItem();
                    Item.Content = row[0].ToString();
                    //MessageBox.Show(row[0].ToString());
                    this.tempList2.Items.Add(Item);
                }
                //this.tempList = destination;
                scn.Close();
            }
        }

        private void submit(object sender, RoutedEventArgs e)
        {
            List<string> Category = new List<string>();
            List<string> Group = new List<string>();
            List<string> Division = new List<string>();
            List<string> Size = new List<string>();
            List<string> Brand = new List<string>();

            List<string> StyleCode = new List<string>();
            List<string> BorcodeId = new List<string>();
            List<string> Length = new List<string>();
            List<string> MRP = new List<string>();
            List<string> ManufacturedDate = new List<string>();
            var wrappanels = root.Children
                  .OfType<WrapPanel>()
                  .Where(x => x.Name.StartsWith("Parent"));
            //MessageBox.Show(wrappanels.Count().ToString());
            foreach (var wrappanel in wrappanels)
            {
                var stackpanels = wrappanel.Children.OfType<StackPanel>();
                foreach(var stackpanel in stackpanels)
                {
                    //Category
                    var comboxes=stackpanel.Children.OfType<ComboBox>().Where(x => x.Name.StartsWith("Category"));
                    foreach ( var combox in comboxes)
                    {
                        //MessageBox.Show(combox.Text.ToString());
                        Category.Add(combox.Text.ToString());
                    }
                    //Group
                    comboxes = stackpanel.Children.OfType<ComboBox>().Where(x => x.Name.StartsWith("Group"));
                    foreach (var combox in comboxes)
                    {
                        //MessageBox.Show(combox.Text.ToString());
                        Group.Add(combox.Text.ToString());
                    }
                    //Division
                    comboxes = stackpanel.Children.OfType<ComboBox>().Where(x => x.Name.StartsWith("Division"));
                    foreach (var combox in comboxes)
                    {
                        //MessageBox.Show(combox.Text.ToString());
                        Division.Add(combox.Text.ToString());
                    }
                    //Division
                    comboxes = stackpanel.Children.OfType<ComboBox>().Where(x => x.Name.StartsWith("Size"));
                    foreach (var combox in comboxes)
                    {
                        //MessageBox.Show(combox.Text.ToString());
                        Size.Add(combox.Text.ToString());
                    }
                    //Division
                    comboxes = stackpanel.Children.OfType<ComboBox>().Where(x => x.Name.StartsWith("Brand"));
                    foreach (var combox in comboxes)
                    {
                        //MessageBox.Show(combox.Text.ToString());
                        Brand.Add(combox.Text.ToString());
                    }
                    //StyleCode
                    var textboxes = stackpanel.Children.OfType<TextBox>().Where(x => x.Name.StartsWith("StyleCode"));
                    foreach (var textbox in textboxes)
                    {
                        //MessageBox.Show(combox.Text.ToString());
                        StyleCode.Add(textbox.Text.ToString());
                    }
                    //BorcodeId
                    textboxes = stackpanel.Children.OfType<TextBox>().Where(x => x.Name.StartsWith("textboxes"));
                    foreach (var textbox in textboxes)
                    {
                        //MessageBox.Show(combox.Text.ToString());
                        BorcodeId.Add(textbox.Text.ToString());
                    }
                    //Length
                    textboxes = stackpanel.Children.OfType<TextBox>().Where(x => x.Name.StartsWith("Length"));
                    foreach (var textbox in textboxes)
                    {
                        //MessageBox.Show(combox.Text.ToString());
                        Length.Add(textbox.Text.ToString());
                    }
                    //MRP
                    textboxes = stackpanel.Children.OfType<TextBox>().Where(x => x.Name.StartsWith("MRP"));
                    foreach (var textbox in textboxes)
                    {
                        //MessageBox.Show(combox.Text.ToString());
                        MRP.Add(textbox.Text.ToString());
                    }
                    //ManufacturedDate
                    var dates = stackpanel.Children.OfType<DatePicker>().Where(x => x.Name.StartsWith("ManufacturedDate"));
                    foreach (var date in dates)
                    {
                        //MessageBox.Show(combox.Text.ToString());
                        ManufacturedDate.Add(date.SelectedDate.ToString());
                    }
                }
            }

            int count = 0;
            int CategoryFlag = 0;
            //Category Validation
            foreach( var value in Category)
            {
                if(value!="" && value!= "Select Category")
                {
                    count++;
                }
            }
            if ( count<Category.Count)
            {
                //MessageBox.Show("Fill All Categories");
            }
            else
            {
                CategoryFlag = 1;
            }

            count = 0;
            int GroupFlag = 0;
            //Group Validation
            foreach (var value in Group)
            {
                if (value != "" && value != "Select Group")
                {
                    count++;
                }
            }
            if (count < Group.Count)
            {
                //MessageBox.Show("Fill All Groups");
            }
            else
            {
                GroupFlag = 1;
            }

            count = 0;
            int DivisionFlag = 0;
            //Division Validation
            foreach (var value in Division)
            {
                if (value != "" && value != "Select Division")
                {
                    count++;
                }
            }
            if (count < Division.Count)
            {
                //MessageBox.Show("Fill All Divisions");
            }
            else
            {
                DivisionFlag = 1;
            }

            count = 0;
            int SizeFlag = 0;
            //Size Validation
            foreach (var value in Size)
            {
                if (value != "" && value != "Select Size")
                {
                    count++;
                }
            }
            if (count < Size.Count)
            {
                //MessageBox.Show("Fill All Sizes");
            }
            else
            {
                SizeFlag = 1;
            }
            
            count = 0;
            int BrandFlag = 0;
            //Brand Validation
            foreach (var value in Brand)
            {
                if (value != "" && value != "Select Brand")
                {
                    count++;
                }
            }
            if (count < Brand.Count)
            {
                //MessageBox.Show("Fill All Brands");
            }
            else
            {
                BrandFlag = 1;
            }
            
            count = 0;
            int StyleCodeFlag = 0;
            //StyleCode Validation
            foreach (var value in StyleCode)
            {
                if (value != "")
                {
                    count++;
                }
            }
            if (count < StyleCode.Count)
            {
                //MessageBox.Show("Fill All Style Codes");
            }
            else
            {
                StyleCodeFlag = 1;
            }
            
            count = 0;
            int BorcodeIdFlag = 0;
            //BorcodeId Validation
            foreach (var value in BorcodeId)
            {
                if (value != "")
                {
                    count++;
                }
            }
            if (count < BorcodeId.Count)
            {
                //MessageBox.Show("Fill All BarCodes");
            }
            else
            {
                BorcodeIdFlag = 1;
            }
            
            count = 0;
            int LengthFlag = 0;
            //Length Validation
            foreach (var value in Length)
            {
                if (value != "")
                {
                    count++;
                }
            }
            if (count < Length.Count)
            {
                //MessageBox.Show("Fill All Lengths");
            }
            else
            {
                LengthFlag = 1;
            }
            
            count = 0;
            int MRPFlag = 0;
            //MRP Validation
            foreach (var value in MRP)
            {
                if (value != "")
                {
                    count++;
                }
            }
            if (count < MRP.Count)
            {
                //MessageBox.Show("Fill All MRP");
            }
            else
            {
                MRPFlag = 1;
            }
            
            count = 0;
            int ManufacturedDateFlag = 0;
            //ManufacturedDate Validation
            foreach (var value in ManufacturedDate)
            {
                if (value != "")
                {
                    count++;
                }
            }
            if (count < ManufacturedDate.Count)
            {
                //MessageBox.Show("Fill All Manufactured Dates");
            }
            else
            {
                ManufacturedDateFlag = 1;
            }

            string alert="";
            if(ManufacturedDateFlag==0)
            {
                alert = "Fill All Manufactured Dates";
            }
            if (MRPFlag == 0)
            {
                alert = "Fill All MRP";
            }
            if (LengthFlag == 0)
            {
                alert = "Fill All Length";
            }
            if (BorcodeIdFlag == 0)
            {
                alert = "Fill All Borcodes";
            }
            if (StyleCodeFlag == 0)
            {
                alert = "Fill All StyleCodes";
            }
            if (BrandFlag == 0)
            {
                alert = "Select All Brands";
            }
            if (SizeFlag == 0)
            {
                alert = "Select All Sizes";
            }
            if (DivisionFlag == 0)
            {
                alert = "Select All Divisions";
            }
            if (GroupFlag == 0)
            {
                alert = "Select All Groups";
            }
            if (CategoryFlag == 0)
            {
                alert = "Select All Categories";
            }
            //MessageBox.Show(BorcodeId[0]);
            if (alert!="")
            {
                MessageBox.Show(alert);
            }
            else
            {
                //MessageBox.Show(Category[0]);
                for(int ia=0;ia<Category.Count;ia++)
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

                        SqlCommand cmd = new SqlCommand("SP_LIST_BARCODES", scn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        scn.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable("Employee");
                        sda.Fill(dt);
                        var flag = true;
                        foreach (DataRow row in dt.Rows)
                        {
                            if (BorcodeId[ia] == row[0].ToString())
                            {
                                flag = false;
                            }
                        }
                        if(flag)
                        {
                            file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                            cnn = File.ReadAllText(file);
                            //Decryption
                            output = new char[cnn.Length];
                            i = 0;
                            foreach (var character in cnn)
                            {
                                output[i++] = (char)(character - '~');
                            }
                            ou = new string(output);
                            //Decrypting Text File

                            ConnString = ou.Substring(0, ou.Length - 2);
                            scn = new SqlConnection(ConnString);
                            cmd = new SqlCommand("SP_INSERT_NEW_PRODUCT", scn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("CATEGORY", Category[ia]);
                            cmd.Parameters.AddWithValue("GROUP_NAME", Group[ia]);
                            cmd.Parameters.AddWithValue("DIVISION", Division[ia]);
                            cmd.Parameters.AddWithValue("SIZE", Size[ia]);
                            cmd.Parameters.AddWithValue("BRAND", Brand[ia]);

                            cmd.Parameters.AddWithValue("STYLE_CODE", StyleCode[ia]);
                            cmd.Parameters.AddWithValue("BARCODE", BorcodeId[ia]);
                            cmd.Parameters.AddWithValue("LENGTH", Length[ia]);
                            cmd.Parameters.AddWithValue("MRP", MRP[ia]);
                            cmd.Parameters.AddWithValue("MANUFACTURED_DATE", ManufacturedDate[ia]);

                            scn.Open();
                            //MessageBox.Show("Connection Opened!");
                            int k = cmd.ExecuteNonQuery();
                            if (k != 0)
                            {
                                MessageBox.Show("Product " + BorcodeId[ia] + " Added Sucessfully!");
                            }
                            else
                            {
                                MessageBox.Show("Product " + BorcodeId[ia] + " Addition Failed!");
                            }
                            scn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Product " + BorcodeId[ia] + " Addition Failed!" ,"Barcode Already Exists!");
                        }
                        //CLEAR CONTENTS
                        Fill();
                    }
                    catch//(Exception ex)
                    {
                        //MessageBox.Show(ex.ToString());
                        MessageBox.Show("Connection Failed!","Alert");
                    }
                }
            }
        }
    }
}

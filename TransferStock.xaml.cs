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
    /// Interaction logic for AddingStock.xaml
    /// </summary>
    public partial class TransferStock : Window
    {
        private int preCount = 0;
        public TransferStock()
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
                SqlCommand cmd = new SqlCommand("USP_LIST_TRANSFERS_LIST", scn1);
                cmd.CommandType = CommandType.StoredProcedure;
                scn1.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    preCount = Convert.ToInt32(row[0]);
                }
                scn1.Close();
                InitializeComponent();
                if (preCount != 0)
                {
                    previous.Text = previous.Text + " TO_" + (preCount - 1);
                }
                else
                {
                    previous.Text = previous.Text + " None ";
                }
                Fill();
            }
            catch
            {
                new Connection().Show();
                MessageBox.Show("SQL Connection Required!", "Alert");
                this.Close();
            }
        }
        public void Fill()
        {
            root.Children.Clear();
            //Single row
            var rowDef = new RowDefinition();
            root.RowDefinitions.Add(rowDef);
            //Nine Columns
            var colDef = new ColumnDefinition();
            root.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            root.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            root.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            root.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            root.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            root.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            root.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            root.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            root.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            root.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            root.ColumnDefinitions.Add(colDef);

            int rowCount = root.RowDefinitions.Count();
            //MessageBox.Show(rowCount.ToString());

            //S No
            var border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Background = Brushes.LightGray;
            border.BorderThickness = new Thickness(1, 1, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 0);
            var text = new TextBlock();
            text.Text = "S NO";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Barcode
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Background = Brushes.LightGray;
            border.BorderThickness = new Thickness(1, 1, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 1);
            text = new TextBlock();
            text.Text = "Barcode";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Brand Name
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Background = Brushes.LightGray;
            border.BorderThickness = new Thickness(1, 1, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 2);
            text = new TextBlock();
            text.Text = "Brand Name";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Group
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Background = Brushes.LightGray;
            border.BorderThickness = new Thickness(1, 1, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 3);
            text = new TextBlock();
            text.Text = "Group";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Division
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Background = Brushes.LightGray;
            border.BorderThickness = new Thickness(1, 1, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 4);
            text = new TextBlock();
            text.Text = "Division";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Size
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Background = Brushes.LightGray;
            border.BorderThickness = new Thickness(1, 1, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 5);
            text = new TextBlock();
            text.Text = "Size";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //MRP
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Background = Brushes.LightGray;
            border.BorderThickness = new Thickness(1, 1, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 6);
            text = new TextBlock();
            text.Text = "MRP";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Quantity
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Background = Brushes.LightGray;
            border.BorderThickness = new Thickness(1, 1, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 7);
            text = new TextBlock();
            text.Text = "Quantity";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Add
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Background = Brushes.LightGray;
            border.BorderThickness = new Thickness(1, 1, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 8);
            text = new TextBlock();
            text.Text = "Add";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Reduce
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Background = Brushes.LightGray;
            border.BorderThickness = new Thickness(1, 1, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 9);
            text = new TextBlock();
            text.Text = "Reduce";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Delete
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.Background = Brushes.LightGray;
            border.BorderThickness = new Thickness(1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 10);
            text = new TextBlock();
            text.Text = "Delete";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);
        }

        public void addRow(string barcode, string brandName, string group, string division, string size, string mrp, string quantity)
        {
            //root.Children.Clear();
            //Single row
            var rowDef = new RowDefinition();
            root.RowDefinitions.Add(rowDef);
            int rowCount = root.RowDefinitions.Count();
            //MessageBox.Show(rowCount.ToString());
            //S No
            var border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 0);
            var text = new TextBlock();
            text.Text = (rowCount - 1).ToString();
            text.Name = "sno" + (rowCount - 1);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Barcode
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 1);
            text = new TextBlock();
            text.Text = barcode;
            text.Name = "borcode" + (rowCount - 1);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Brand Name
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 2);
            text = new TextBlock();
            text.Text = brandName;
            text.Name = "brand" + (rowCount - 1);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Group
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 3);
            text = new TextBlock();
            text.Text = group;
            text.Name = "group" + (rowCount - 1);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Division
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 4);
            text = new TextBlock();
            text.Text = division;
            text.Name = "division" + (rowCount - 1);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Size
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 5);
            text = new TextBlock();
            text.Text = size;
            text.Name = "size" + (rowCount - 1);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //MRP
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 6);
            text = new TextBlock();
            text.Text = mrp;
            text.Name = "mrp" + (rowCount - 1);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Quantity
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 7);
            text = new TextBlock();
            text.Text = quantity;
            text.Name = "quantity" + (rowCount - 1);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Add
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 8);
            text = new TextBlock();
            text.Text = " + ";
            text.Name = "add" + (rowCount - 1);
            text.Cursor = Cursors.Hand;
            text.Foreground = Brushes.Green;
            text.FontWeight = FontWeights.Bold;
            text.PreviewMouseDown += new MouseButtonEventHandler((sender, e) => addQuantity(sender, e, rowCount));
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Reduce
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 9);
            text = new TextBlock();
            text.Text = " - ";
            text.Name = "reduce" + (rowCount - 1);
            text.Cursor = Cursors.Hand;
            text.Foreground = Brushes.Blue;
            text.FontWeight = FontWeights.Bold;
            text.PreviewMouseDown += new MouseButtonEventHandler((sender, e) => reduceQuantity(sender, e, rowCount));
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //Delete
            border = new Border();
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 1, 1);
            border.Padding = new Thickness(20, 5, 20, 5);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 10);
            text = new TextBlock();
            text.Text = " x ";
            text.Name = "delete" + (rowCount - 1);
            text.Cursor = Cursors.Hand;
            text.Foreground = Brushes.Red;
            text.FontWeight = FontWeights.Bold;
            text.PreviewMouseDown += new MouseButtonEventHandler((sender, e) => deleteRow(sender, e, rowCount));
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.FontSize = 20;
            border.Child = text;
            root.Children.Add(border);

            //refix s no
            var i = 0;
            var borders = root.Children.OfType<Border>();
            foreach (var bord in borders)
            {
                var texts = bord.Child as TextBlock;
                if (texts.Name.StartsWith("sno"))
                {
                    i++;
                    texts.Text = i.ToString();
                }
            }
        }

        private void addQuantity(object sender, RoutedEventArgs e, int row)
        {
            var borders = root.Children.OfType<Border>();
            var i = 0;
            var th = (TextBlock)sender;
            foreach (var border in borders)
            {
                var texts = border.Child as TextBlock;
                if (texts.Name == th.Name)
                {
                    break;
                }
                i++;
            }
            updateQuantity(1, i);
        }
        private void reduceQuantity(object sender, RoutedEventArgs e, int row)
        {
            var borders = root.Children.OfType<Border>();
            var i = -1;
            var th = (TextBlock)sender;
            foreach (var border in borders)
            {
                var texts = border.Child as TextBlock;
                if (texts.Name == th.Name)
                {
                    break;
                }
                i++;
            }
            updateQuantity(-1, i);
        }
        private void deleteRow(object sender, RoutedEventArgs e, int row)
        {
            var borders = root.Children.OfType<Border>();
            var i = 1;
            var th = (TextBlock)sender;
            foreach (var border in borders)
            {
                var texts = border.Child as TextBlock;
                if (texts.Name == th.Name)
                {
                    break;
                }
                i++;
            }
            //MessageBox.Show((i / 11).ToString());
            removeRow((i / 11));
        }
        private void updateQuantity(int val, int index)
        {

            var j = 0;
            var borders = root.Children.OfType<Border>();
            foreach (var border in borders)
            {
                var texts = border.Child as TextBlock;
                j++;
                if (j == index)
                {
                    if (texts.Name.StartsWith("quantity"))
                    {
                        texts.Text = (Convert.ToInt32(texts.Text) + val).ToString();
                    }
                    break;
                }
            }
        }
        private void removeRow(int index)
        {
            var j = 0;
            index = index - 1;
            var borders = root.Children.OfType<Border>();
            var from = index * 11;
            var to = from + 11;
            //MessageBox.Show(from + " " + to);
            //root.Children.RemoveAt(to-1);
            for (j = from; j < to; j++)
            {
                root.Children.RemoveAt(from);
            }
            //root.RowDefinitions.RemoveAt(index);

            //refix s no
            var i = 0;
            borders = root.Children.OfType<Border>();
            foreach (var border in borders)
            {
                var texts = border.Child as TextBlock;
                if (texts.Name.StartsWith("sno"))
                {
                    i++;
                    texts.Text = i.ToString();
                }
            }
        }

        private void addProduct(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var barcodeID = BarcodeID.Text;
                //MessageBox.Show(barcodeID);
                var category = "";
                var group = "";
                var division = "";
                var size = "";
                var brand = "";
                var styleCode = "";
                var length = "";
                var mrp = "";
                var manufacturedDate = "";
                List<string> Borcode = new List<string>();
                List<int> Quantity = new List<int>();
                var borders = root.Children.OfType<Border>();
                var flag = true;
                var i = 1;
                var index = 0;
                foreach (var border in borders)
                {
                    var texts = border.Child as TextBlock;
                    if (texts.Name.StartsWith("borcode"))
                    {
                        Borcode.Add(texts.Text);
                        //MessageBox.Show(texts.Text);
                        if (texts.Text == barcodeID)
                        {
                            flag = false;
                            index = i + 6;
                        }
                    }
                    if (texts.Name.StartsWith("quantity"))
                    {
                        Quantity.Add(Convert.ToInt32(texts.Text));
                    }
                    i++;
                }
                //MessageBox.Show(index.ToString());
                if (flag)
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
                        SqlCommand cmd = new SqlCommand("SP_SELECT_NEW_PRODUCT_BY_BARCODE", scn1);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("BARCODE", barcodeID);
                        scn1.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable("Employee");
                        sda.Fill(dt);
                        var k = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            category = row[1].ToString();
                            group = row[2].ToString();
                            division = row[3].ToString();
                            size = row[4].ToString();
                            brand = row[5].ToString();
                            styleCode = row[6].ToString();
                            length = row[8].ToString();
                            mrp = row[9].ToString();
                            manufacturedDate = row[10].ToString();
                            if (category != "")
                            {
                                addRow(barcodeID, brand, group, division, size, mrp, "1");
                            }
                            else
                            {
                                MessageBox.Show("Invalid Barcode", "Alert");
                            }
                            k++;
                        }
                        if (k == 0)
                        {
                            MessageBox.Show("Invalid Barcode", "Alert");
                        }
                        scn1.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Barcode", "Alert");
                    }
                }
                else
                {
                    //MessageBox.Show(index.ToString());
                    updateQuantity(1, index);
                }

            }
        }

        private void genarateTO(object sender, RoutedEventArgs e)
        {
            var borders = root.Children.OfType<Border>();
            List<string> Barcode = new List<string>();
            List<string> Brand = new List<string>();
            List<string> Group = new List<string>();
            List<string> Division = new List<string>();
            List<string> Size = new List<string>();
            List<int> MRP = new List<int>();
            List<int> Quantity = new List<int>();
            foreach (var border in borders)
            {
                var texts = border.Child as TextBlock;
                if (texts.Name.StartsWith("borcode"))
                {
                    Barcode.Add(texts.Text);
                }
                else if (texts.Name.StartsWith("brand"))
                {
                    Brand.Add(texts.Text);
                }
                else if (texts.Name.StartsWith("group"))
                {
                    Group.Add(texts.Text);
                }
                else if (texts.Name.StartsWith("division"))
                {
                    Division.Add(texts.Text);
                }
                else if (texts.Name.StartsWith("size"))
                {
                    Size.Add(texts.Text);
                }
                else if (texts.Name.StartsWith("mrp"))
                {
                    MRP.Add(Convert.ToInt32(texts.Text));
                }
                else if (texts.Name.StartsWith("quantity"))
                {
                    Quantity.Add(Convert.ToInt32(texts.Text));
                }
            }
            if (Barcode.Count() != 0)
            {
                var sucess = 0;
                for (int i = 0; i < Barcode.Count(); i++)
                {
                    String ConnString;
                    string file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                    string cnn = File.ReadAllText(file);
                    //Decryption

                    string ou;
                    char[] output = new char[cnn.Length];
                    var ia = 0;
                    foreach (var character in cnn)
                    {
                        output[ia++] = (char)(character - '~');
                    }
                    ou = new string(output);
                    //Decrypting Text File
                    var tableName = "TO_" + preCount;
                    ConnString = ou.Substring(0, ou.Length - 2);
                    SqlConnection scn = new SqlConnection(ConnString);
                    SqlCommand cmd = new SqlCommand("USP_INSERT_PRODUCTS_INTO_TRANSFERS_LIST", scn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("TABLE_NAME", tableName);
                    cmd.Parameters.AddWithValue("BARCODE", Barcode[i]);
                    cmd.Parameters.AddWithValue("BRAND", Brand[i]);
                    cmd.Parameters.AddWithValue("CAETEGORY", "Textiles");
                    cmd.Parameters.AddWithValue("GROUP_NAME", Group[i]);
                    cmd.Parameters.AddWithValue("DIVISION", Division[i]);
                    cmd.Parameters.AddWithValue("SIZE", Size[i]);
                    cmd.Parameters.AddWithValue("QUANTITY", Quantity[i]);

                    scn.Open();
                    //MessageBox.Show("Connection Opened!");
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        sucess++;
                    }
                    scn.Close();
                }
                if (sucess != 0)
                {
                    String ConnString;
                    string file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                    string cnn = File.ReadAllText(file);
                    //Decryption

                    string ou;
                    char[] output = new char[cnn.Length];
                    var ia = 0;
                    foreach (var character in cnn)
                    {
                        output[ia++] = (char)(character - '~');
                    }
                    ou = new string(output);
                    //Decrypting Text File
                    var tableName = "TO_" + preCount;
                    ConnString = ou.Substring(0, ou.Length - 2);
                    SqlConnection scn = new SqlConnection(ConnString);
                    SqlCommand cmd = new SqlCommand("USP_INSERT_TRANSFERS_LIST", scn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("PURCHASE_ID", tableName);
                    cmd.Parameters.AddWithValue("NO_OF_PRODUCTS", Quantity.Sum());
                    cmd.Parameters.AddWithValue("SOURCE_FROM", "This");
                    cmd.Parameters.AddWithValue("SOURCE_TO", "This");
                    cmd.Parameters.AddWithValue("STATUS", "Pending");

                    scn.Open();
                    //MessageBox.Show("Connection Opened!");
                    int k = cmd.ExecuteNonQuery();
                    if (k != 0)
                    {
                        MessageBox.Show("Transfer Order Genarated Sucessfully!", tableName);
                        preCount++;
                        var thisWindow = new TransferStock();
                        thisWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Transfer Order Genaration Failed!", "Warning");
                    }
                }
                else
                {
                    MessageBox.Show("Transfer Order Genaration Failed!", "Warning");
                }
            }
            else
            {
                MessageBox.Show("Add atleast one product", "Alert");
            }
        }

        private void clear(object sender, RoutedEventArgs e)
        {
            var thisWindow = new TransferStock();
            thisWindow.Show();
            this.Close();
        }
    }
}


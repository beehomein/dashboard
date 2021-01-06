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
    /// Interaction logic for StockInwardTransit.xaml
    /// </summary>
    public partial class StockInwardTransit : Window
    {
        private string TableName = "";
        private string Source = "";
        public StockInwardTransit()
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
                MessageBox.Show("Transit Parameters Are Missing","Warning");
            }
            catch
            {
                new Connection().Show();
                MessageBox.Show("SQL Connection Required!", "Alert");
                this.Close();
            }
        }
        public StockInwardTransit(string tableName,string source)
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
                SqlCommand cmd = new SqlCommand("USP_LIST_TRANSIT_ORDERS", scn1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("TABLE_NAME", tableName);

                scn1.Open();

                SqlCommand cmd1 = new SqlCommand("USP_UPDATE_TRANSIT_ORDERS", scn1);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("TABLE_NAME", tableName);
                cmd1.ExecuteNonQuery();

                InitializeComponent();
                this.TableName = tableName;
                this.Source = source;
                transitId.Text +=" "+ TableName;
                sourceName.Text += " " + Source;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Employee");
                sda.Fill(dt);
                var i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    i++;
                    root.RowDefinitions.Add(new RowDefinition());
                    var rowCount = root.RowDefinitions.Count();

                    //S No
                    var border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1, 0, 0, 1);
                    border.Padding = new Thickness(20);
                    Grid.SetRow(border, rowCount-1);
                    Grid.SetColumn(border, 0);
                    var text = new TextBlock();
                    text.FontSize = 20;
                    text.HorizontalAlignment = HorizontalAlignment.Center;
                    text.Text = i.ToString();
                    text.Name = "sno" + rowCount;
                    border.Child = text;
                    root.Children.Add(border);
                    
                    //Barcode
                    border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1, 0, 0, 1);
                    border.Padding = new Thickness(20);
                    Grid.SetRow(border, rowCount-1);
                    Grid.SetColumn(border, 1);
                    text = new TextBlock();
                    text.FontSize = 20;
                    text.HorizontalAlignment = HorizontalAlignment.Center;
                    text.Text = row[1].ToString();
                    text.Name = "barcode" + rowCount;
                    border.Child = text;
                    root.Children.Add(border);

                    //Brand
                    border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1, 0, 0, 1);
                    border.Padding = new Thickness(20);
                    Grid.SetRow(border, rowCount-1);
                    Grid.SetColumn(border, 2);
                    text = new TextBlock();
                    text.FontSize = 20;
                    text.HorizontalAlignment = HorizontalAlignment.Center;
                    text.Text = row[2].ToString();
                    text.Name = "brand" + rowCount;
                    border.Child = text;
                    root.Children.Add(border);

                    //No of Products
                    border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1, 0, 0, 1);
                    border.Padding = new Thickness(20);
                    Grid.SetRow(border, rowCount-1);
                    Grid.SetColumn(border, 3);
                    text = new TextBlock();
                    text.FontSize = 20;
                    text.HorizontalAlignment = HorizontalAlignment.Center;
                    text.Text = row[7].ToString();
                    text.Name = "nop" + rowCount;
                    border.Child = text;
                    root.Children.Add(border);
                    
                    //Current Quantity
                    border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1, 0, 0, 1);
                    border.Padding = new Thickness(20);
                    Grid.SetRow(border, rowCount-1);
                    Grid.SetColumn(border, 4);
                    text = new TextBlock();
                    text.FontSize = 20;
                    text.HorizontalAlignment = HorizontalAlignment.Center;
                    if(row[9].ToString()!="")
                    {
                        text.Text = row[9].ToString();
                    }
                    else
                    {
                        text.Text = "0";
                    }
                    text.Name = "cq" + rowCount;
                    border.Child = text;
                    root.Children.Add(border);

                    //Pending Quantity
                    border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1, 0, 0, 1);
                    border.Padding = new Thickness(20);
                    Grid.SetRow(border, rowCount - 1);
                    Grid.SetColumn(border, 5);
                    text = new TextBlock();
                    text.FontSize = 20;
                    text.HorizontalAlignment = HorizontalAlignment.Center;
                    if (row[10].ToString() != "")
                    {
                        text.Text = row[10].ToString();
                    }
                    else
                    {
                        text.Text = row[7].ToString();
                    }
                    text.Name = "pq" + rowCount;
                    border.Child = text;
                    root.Children.Add(border);

                    //Status
                    border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1, 0, 0, 1);
                    border.Padding = new Thickness(20);
                    Grid.SetRow(border, rowCount - 1);
                    Grid.SetColumn(border, 6);
                    text = new TextBlock();
                    text.FontSize = 20;
                    text.HorizontalAlignment = HorizontalAlignment.Center;
                    if (row[11].ToString() != "")
                    {
                        text.Text = row[11].ToString();
                    }
                    else
                    {
                        text.Text = "Pending";
                    }
                    text.Name = "status" + rowCount;
                    border.Child = text;
                    root.Children.Add(border);

                    //Date
                    border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1, 0, 1, 1);
                    border.Padding = new Thickness(20);
                    Grid.SetRow(border, rowCount - 1);
                    Grid.SetColumn(border, 7);
                    text = new TextBlock();
                    text.FontSize = 20;
                    text.HorizontalAlignment = HorizontalAlignment.Center;
                    text.Text = row[8].ToString();
                    text.Name = "date" + rowCount;
                    border.Child = text;
                    root.Children.Add(border);

                }
                scn1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert");
                //new Connection().Show();
                //MessageBox.Show("SQL Connection Required!", "Alert");
                //this.Close();
            }
        }

        private void addBarcode(object sender, KeyEventArgs e)
        {
            var BarcodeField = (TextBox)sender;
            var BarcodeText = BarcodeField.Text;
            var dataList = root.Children.OfType<Border>();

            List<string> Barcode = new List<string>();
            List<int> Nop = new List<int>();
            List<int> Cq = new List<int>();
            List<int> Pq = new List<int>();
            List<string> Status = new List<string>(); 

            var index = -1;
            var i = 0;
            if (e.Key==Key.Enter)
            {
                // Get all Values
                foreach (var data in dataList)
                {
                    var textBlock = (TextBlock)data.Child;
                    var name = textBlock.Name;
                    var text = textBlock.Text;
                    //Barcode Id
                    var barcode = "";
                    if (name.StartsWith("barcode"))
                    {
                        barcode = text;
                        Barcode.Add(barcode);
                    }
                    //Number Of Products
                    var nop = "";
                    if (name.StartsWith("nop"))
                    {
                        nop = text;
                        Nop.Add(Convert.ToInt32( nop ));
                    }
                    //Current Quantity
                    var cq = "";
                    if (name.StartsWith("cq"))
                    {
                        cq = text;
                        Cq.Add(Convert.ToInt32(cq));
                    }
                    //Pending Quantity
                    var pq = "";
                    if (name.StartsWith("pq"))
                    {
                        pq = text;
                        Pq.Add(Convert.ToInt32(pq));
                    }
                    //Status
                    var status = "";
                    if (name.StartsWith("status"))
                    {
                        status = text;
                        Status.Add(status);
                    }
                    //Date
                    var date = "";
                    if (name.StartsWith("date"))
                    {
                        date = text;
                    }

                    if(barcode!="" && barcode==BarcodeText)
                    {
                        index = i;
                    }
                    i++;
                }
                /*
                 * Barcode index => index
                 * Current Quantity(cq) index => index+3
                 * Pending Quantity(pq) index => index+4
                 * Status index => index+5
                 */
                var row = -1;
                if (index!=-1)
                {
                    row = Convert.ToInt32(index / 8);
                    row--;
                    var ActualQuantity = Nop[row];
                    var CurrentQuantity = Cq[row];
                    var PendingQuantity = Pq[row];
                    var StatusText = "Pending";
                    if(PendingQuantity!=0)
                    {
                        CurrentQuantity++;
                        PendingQuantity--;
                        if (PendingQuantity == 0)
                        {
                            StatusText = "Completed";
                        }
                        i=0;
                        foreach (var data in dataList)
                        {
                            var textBlock = (TextBlock)data.Child;
                            var name = textBlock.Name;
                            var text = textBlock.Text;
                            //Update Current Quantity
                            if(i==(index + 3))
                            {
                                textBlock.Text = CurrentQuantity.ToString();
                            }
                            //Update Pending Quantity
                            if(i==(index + 4))
                            {
                                textBlock.Text = PendingQuantity.ToString();
                            }
                            //Update Status
                            if(i==(index + 5))
                            {
                                textBlock.Text = StatusText;
                            }
                            //Update Status
                            if (i == (index + 6))
                            {
                                textBlock.Text = DateTime.Today.ToString();
                            }
                            i++;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Product limit already Filled", "Alert");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Barcode", "Alert");
                }
            }
        }

        private void moveToInventory(object sender, RoutedEventArgs e)
        {
            List<string> Barcode = new List<string>();
            List<int> Nop = new List<int>();
            List<int> Cq = new List<int>();
            List<int> Pq = new List<int>();
            List<string> Status = new List<string>();

            var dataList = root.Children.OfType<Border>();
            foreach (var data in dataList)
            {
                var textBlock = (TextBlock)data.Child;
                var name = textBlock.Name;
                var text = textBlock.Text;
                //Barcode Id
                var barcode = "";
                if (name.StartsWith("barcode"))
                {
                    barcode = text;
                    Barcode.Add(barcode);
                }
                //Number Of Products
                var nop = "";
                if (name.StartsWith("nop"))
                {
                    nop = text;
                    Nop.Add(Convert.ToInt32(nop));
                }
                //Current Quantity
                var cq = "";
                if (name.StartsWith("cq"))
                {
                    cq = text;
                    Cq.Add(Convert.ToInt32(cq));
                }
                //Pending Quantity
                var pq = "";
                if (name.StartsWith("pq"))
                {
                    pq = text;
                    Pq.Add(Convert.ToInt32(pq));
                }
                //Status
                var status = "";
                if (name.StartsWith("status"))
                {
                    status = text;
                    Status.Add(status);
                }
            }
            var noOfProducts = Cq.Count().ToString();
            var res = MessageBox.Show("Are you sure you want to move "+noOfProducts+" Products into inventory?", "Confirm", MessageBoxButton.YesNo);
            if(res==MessageBoxResult.Yes)
            {
                string file1 = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                string cnn1 = File.ReadAllText(file1);
                string ou1;
                char[] output1 = new char[cnn1.Length];
                var i1 = 0;
                foreach (var character in cnn1)
                {
                    output1[i1++] = (char)(character - '~');
                }
                ou1 = new string(output1);
                String ConnString1;
                ConnString1 = ou1.Substring(0, ou1.Length - 2);
                SqlConnection scn1 = new SqlConnection(ConnString1);
                scn1.Open();
                var flag = true;
                for (var i=0;i< Barcode.Count(); i++)
                {
                    var productName = "";
                    var barcodeId = "";
                    var mrp = 0;
                    var gst =0.0f;
                    var price = 0.0f;
                    var percentage = 0;
                    var quantity = 0;
                    var transactionId = "";
                    var category = "";
                    var division = "";
                    var groupName = "";
                    var size = "";
                    var brandName = "";
                    //Fetch Product Details
                    SqlCommand cmd = new SqlCommand("USP_SELECT_NEW_PRODUCT_BY_BARCODE", scn1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("BARCODE", Barcode[i]);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("NewProduct");
                    sda.Fill(dt);
                    category = dt.Rows[0][1].ToString();
                    groupName= dt.Rows[0][2].ToString();
                    division = dt.Rows[0][3].ToString();
                    size = dt.Rows[0][4].ToString();
                    brandName = dt.Rows[0][5].ToString();
                    productName = dt.Rows[0][5].ToString();
                    var temp = dt.Rows[0][9].ToString();
                    mrp =Convert.ToInt32(temp);
                    barcodeId = Barcode[i];
                    transactionId = this.TableName;
                    quantity = Cq[i];
                    //Fetch GST
                    cmd = new SqlCommand("USP_FETCH_GST", scn1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MRP", mrp);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("GST");
                    sda.Fill(dt);
                    temp = dt.Rows[0][0].ToString();
                    percentage = Convert.ToInt32(temp);
                    price = ((int)((float)mrp / (100 + percentage) ) * 100);
                    gst = mrp-price;
                    //Update Current Table
                    cmd = new SqlCommand("USP_UPDATE_TO_PO", scn1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("TABLE_NAME", transactionId);
                    cmd.Parameters.AddWithValue("BARCODE", barcodeId);
                    cmd.Parameters.AddWithValue("CURRENT_QUANTITY", Cq[i]);
                    cmd.Parameters.AddWithValue("PENDING_QUANTITY", Pq[i]);
                    cmd.Parameters.AddWithValue("STATUS", Status[i]);
                    var k1=cmd.ExecuteNonQuery();
                    var k2=-1;
                    if (k1!=0)
                    {
                        //Update Inventory 
                        cmd = new SqlCommand("USP_INSERT_PRODUCT_INTO_INVENTORY", scn1);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("PRODUCT_NAME", productName);
                        cmd.Parameters.AddWithValue("BARCODE_ID", barcodeId);
                        cmd.Parameters.AddWithValue("MRP", mrp);
                        cmd.Parameters.AddWithValue("GST", gst);
                        cmd.Parameters.AddWithValue("PRICE", price);
                        cmd.Parameters.AddWithValue("QUANTITY", quantity);
                        cmd.Parameters.AddWithValue("TRANSACTION_ID", transactionId);
                        cmd.Parameters.AddWithValue("CATEGORY", category);
                        cmd.Parameters.AddWithValue("DIVISION", division);
                        cmd.Parameters.AddWithValue("GROUP_NAME", groupName);
                        cmd.Parameters.AddWithValue("SIZE", size);
                        cmd.Parameters.AddWithValue("BRAND_NAME", productName);
                        k2 = cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if(flag)
                {
                    var tableNameFor = "TRANSFERS_LIST";
                    if(this.TableName.StartsWith("PO"))
                    {
                        tableNameFor = "PURCHASES_LIST";
                    }
                    var lisId = this.TableName;
                    var status = "Pending";
                    if(Pq.Sum()==0)
                    {
                        status = "Completed";
                    }
                    //Update Inventory  
                    SqlCommand cmd = new SqlCommand("USP_UPDATE_TO_PO_STATUS", scn1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("TABLE_NAME", tableNameFor);
                    cmd.Parameters.AddWithValue("LIST_ID", lisId);
                    cmd.Parameters.AddWithValue("STATUS", status);
                    var k=cmd.ExecuteNonQuery(); 

                    //Insert Tansactions list 
                    cmd = new SqlCommand("USP_INSERT_TRANSACTIONS_LIST", scn1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("PURCHASE_ID", tableNameFor);
                    cmd.Parameters.AddWithValue("NO_OF_PRODUCTS", Cq.Sum());
                    cmd.Parameters.AddWithValue("SOURCE_FROM", "this");
                    cmd.Parameters.AddWithValue("SOURCE_TO", "this");
                    cmd.Parameters.AddWithValue("STATUS", status);
                    cmd.ExecuteNonQuery();

                    if (k!=0)
                    {
                        MessageBox.Show("Products moved to inventory", "Sucsess");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Operation Failed", "Failed");
                    }
                }
                else
                {
                    MessageBox.Show("Operation Failed", "Failed");
                }
                scn1.Close();
            }
        }
    }
}

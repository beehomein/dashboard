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
    /// Interaction logic for Dashboard.xaml
    /// </summary>

    public partial class Dashboard : Window
    {
        //customer Details Parameters, getter & setter
        private string customerNumber;
        private string customerEmail;
        public void setCustomerNumber(string number)
        {
            this.customerNumber = number;
        }
        public string getCustomerNumber()
        {
            return this.customerNumber;
        }
        public void setCustomerEmail(string name)
        {
            this.customerEmail = name;
        }
        public string getCustomerEmail()
        {
            return this.customerEmail;
        }
        public void displayCustomerDetails()
        {
            try
            {
                string connetionString;
                DataSet ds = new DataSet();
                string file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                string cnn = File.ReadAllText(file);
                string ou;
                char[] output = new char[cnn.Length];
                var i = 0;
                foreach (var character in cnn)
                {
                    output[i++] = (char)(character - '~');
                }
                ou = new string(output);
                String ConnString;
                ConnString = ou.Substring(0, ou.Length - 2);
                connetionString = ConnString;
                SqlConnection cns;
                cns = new SqlConnection(connetionString);
                cns.Open();
                SqlCommand sql_cmnd = new SqlCommand("SP_SEARCH_CUSTOMER_DETAIL", cns);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@NUMBER", SqlDbType.VarChar).Value = customerNumber;
                sql_cmnd.Parameters.AddWithValue("@EMAIL", SqlDbType.VarChar).Value = customerEmail;
                sql_cmnd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(sql_cmnd);
                sda.Fill(ds);
                var customerName = ds.Tables[0].Rows[0]["F_NAME"].ToString();
                customerEmail= ds.Tables[0].Rows[0]["EMAIL"].ToString();
                customerNumber= ds.Tables[0].Rows[0]["NUMBER"].ToString();
                cns.Close();

                var viewBoxEl = new Viewbox();
                viewBoxEl.Height = 25;
                viewBoxEl.HorizontalAlignment = HorizontalAlignment.Left;
                viewBoxEl.VerticalAlignment = VerticalAlignment.Center;
                var textblockEl = new TextBlock();
                
                textblockEl.Text = "Customer Name:";
                textblockEl.FontWeight = FontWeights.Bold;
                viewBoxEl.Child = textblockEl;
                CustomerDetails.Children.Add(viewBoxEl);

                viewBoxEl = new Viewbox();
                viewBoxEl.Height = 25;
                viewBoxEl.HorizontalAlignment = HorizontalAlignment.Left;
                viewBoxEl.VerticalAlignment = VerticalAlignment.Center;
                textblockEl = new TextBlock();
                textblockEl.Text =customerName;
                viewBoxEl.Child = textblockEl;
                CustomerDetails.Children.Add(viewBoxEl);

                viewBoxEl = new Viewbox();
                viewBoxEl.Height = 25;
                viewBoxEl.HorizontalAlignment = HorizontalAlignment.Left;
                viewBoxEl.VerticalAlignment = VerticalAlignment.Center;
                textblockEl = new TextBlock();
                textblockEl.Text = "Contact Number:";
                textblockEl.FontWeight = FontWeights.Bold;
                viewBoxEl.Child = textblockEl;
                CustomerDetails.Children.Add(viewBoxEl);


                viewBoxEl = new Viewbox();
                viewBoxEl.Height = 25;
                viewBoxEl.HorizontalAlignment = HorizontalAlignment.Left;
                viewBoxEl.VerticalAlignment = VerticalAlignment.Center;
                textblockEl = new TextBlock();
                textblockEl.Text = customerNumber;
                viewBoxEl.Child = textblockEl;
                CustomerDetails.Children.Add(viewBoxEl);
            }
            catch
            {
                var viewBoxEl = new Viewbox();
                viewBoxEl.Height = 25;
                viewBoxEl.HorizontalAlignment = HorizontalAlignment.Left;
                viewBoxEl.VerticalAlignment = VerticalAlignment.Center;
                var textblockEl = new TextBlock();
                textblockEl.Text = "Select Customer";
                textblockEl.FontWeight = FontWeights.Bold;
                viewBoxEl.Child = textblockEl;
                CustomerDetails.Children.Add(viewBoxEl);
            }
        }
        public Dashboard(string number,string email )
        {
            this.customerNumber = number;
            this.customerEmail = email;
            try
            {
                string file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                string cnn = File.ReadAllText(file);
                string ou;
                char[] output = new char[cnn.Length];
                var i = 0;
                foreach (var character in cnn)
                {
                    output[i++] = (char)(character - '~');
                }
                ou = new string(output);
                String ConnString;
                ConnString = ou.Substring(0, ou.Length - 2);
                SqlConnection scn = new SqlConnection(ConnString);
                scn.Open();
                scn.Close();
                InitializeComponent();
                //MessageBox.Show("Are you sure you want to lanuch app", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
                var customerNumber = this.getCustomerNumber();
                var customerEmail = this.getCustomerEmail();
                displayCustomerDetails();
            }
            catch
            {
                new Connection().Show();
                MessageBox.Show("SQL Connection Required!", "Alert");
                this.Close();
            }
        }
        public Dashboard()
        {
            try
            {
                string file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                string cnn = File.ReadAllText(file);
                string ou;
                char[] output = new char[cnn.Length];
                var i = 0;
                foreach (var character in cnn)
                {
                    output[i++] = (char)(character - '~');
                }
                ou = new string(output);
                String ConnString;
                ConnString = ou.Substring(0, ou.Length - 2);
                SqlConnection scn = new SqlConnection(ConnString);
                scn.Open();
                scn.Close();
                InitializeComponent();

                var customerNumber = this.getCustomerNumber();
                var customerEmail = this.getCustomerEmail();
                displayCustomerDetails();
            }
            catch
            {
                new Connection().Show();
                MessageBox.Show("SQL Connection Required!", "Alert");
                this.Close();
            }
        }
        private void opendNewSalesMan(object sender, RoutedEventArgs e)
        {
            var newSalesMan=new NewSalesMan();
            newSalesMan.Show();
        }

        private void openNewCustomer(object sender, RoutedEventArgs e)
        {
            var newCustomer = new NewCustomerRegistration();
            newCustomer.Show();
        }

        private void openSearchCustomer(object sender, RoutedEventArgs e)
        {
            var searchCustomer = new SearchCustomer();
            searchCustomer.Show();
            this.Close();
        }

        private void showBuddyStore(object sender, RoutedEventArgs e)
        {
            var width = Home.Width;
            Grid.SetColumn(BuddyStore, 0);
            Grid.SetRow(BuddyStore, 2);
            BuddyStore.Width = width;
        }

        private void hideBuddyStore(object sender, RoutedEventArgs e)
        {
            Grid.SetColumn(BuddyStore, 0);
            Grid.SetRow(BuddyStore, 0);
            BuddyStore.Width = 0;
        }

        private void hideStoreOperations(object sender, RoutedEventArgs e)
        {
            Grid.SetColumn(StoreOperations, 0);
            Grid.SetRow(StoreOperations, 0);
            StoreOperations.Width = 0;
        }

        private void showStoreOperations(object sender, RoutedEventArgs e)
        {
            var width = Home.Width;
            Grid.SetColumn(StoreOperations, 0);
            Grid.SetRow(StoreOperations, 2);
            StoreOperations.Width = width;
        }

        private void hideReports(object sender, RoutedEventArgs e)
        {
            Grid.SetColumn(Reports, 0); 
            Grid.SetRow(Reports, 0);
            Reports.Width = 0;
        }

        private void showReports(object sender, RoutedEventArgs e)
        {
            var width = Home.Width;
            Grid.SetColumn(Reports, 0);
            Grid.SetRow(Reports, 2);
            Reports.Width = width;
        }

        private void showAddNew(object sender, RoutedEventArgs e)
        {
            var width = Home.Width;
            Grid.SetColumn(AddNew, 0);
            Grid.SetRow(AddNew, 2);
            AddNew.Width = width;
        }

        private void hideAddNew(object sender, RoutedEventArgs e)
        {
            Grid.SetColumn(AddNew, 0);
            Grid.SetRow(AddNew, 0);
            AddNew.Width = 0;
        }

        private void showTransactions(object sender, RoutedEventArgs e)
        {
            var width = Home.Width;
            Grid.SetColumn(Transactions, 0);
            Grid.SetRow(Transactions, 2);
            Transactions.Width = width;
        }

        private void hideTransactions(object sender, RoutedEventArgs e)
        {
            Grid.SetColumn(Transactions, 0);
            Grid.SetRow(Transactions, 0);
            Transactions.Width = 0;
        }

        private void showInventoryManagement(object sender, RoutedEventArgs e)
        {
            var width = Home.Width;
            Grid.SetColumn(InventoryManagement, 0);
            Grid.SetRow(InventoryManagement, 2);
            InventoryManagement.Width = width;
        }

        private void hideInventoryManagement(object sender, RoutedEventArgs e)
        {
            Grid.SetColumn(InventoryManagement, 0);
            Grid.SetRow(InventoryManagement, 0);
            InventoryManagement.Width = 0;
        }

        private void showPromotionsAndDiscounts(object sender, RoutedEventArgs e)
        {
            var width = Home.Width;
            Grid.SetColumn(PromotionsAndDiscounts, 0);
            Grid.SetRow(PromotionsAndDiscounts, 2);
            PromotionsAndDiscounts.Width = width;
        }

        private void hidePromotionsAndDiscounts(object sender, RoutedEventArgs e)
        {
            Grid.SetColumn(PromotionsAndDiscounts, 0);
            Grid.SetRow(PromotionsAndDiscounts, 0);
            PromotionsAndDiscounts.Width = 0;
        }

        private void openNewGSTPlan(object sender, RoutedEventArgs e)
        {
            var newGstPlan = new NewGSTPlan();
            newGstPlan.Show();
        }

        private void openNewGiftCard(object sender, RoutedEventArgs e)
        {
            var newGiftCard = new NewGiftCard();
            newGiftCard.Show();
        }

        private void openNewProductAttributes(object sender, RoutedEventArgs e)
        {
            var newProductAttributes = new NewProductAttributes();
            newProductAttributes.Show();
        }

        private void openNewProduct(object sender, RoutedEventArgs e)
        {
            var newProduct = new NewProduct();
            newProduct.Show();
        }

        private void openAddingStock(object sender, RoutedEventArgs e)
        {
            var addingStock = new AddingStock();
            addingStock.Show();
        }

        private void openTransferStock(object sender, RoutedEventArgs e)
        {
            var transferStock = new TransferStock();
            transferStock.Show();
        }

        private void openReprintBarcode(object sender, RoutedEventArgs e)
        {
            var barcode = new Barcode();
            barcode.Show();
        }

        private void openStockInward(object sender, RoutedEventArgs e)
        {
            var stockInward = new StockInward();
            stockInward.Show();
        }

        private void addProduct(object sender, KeyEventArgs e)
        {
            var barcodeObject = (TextBox)sender;
            var barcode = barcodeObject.Text;
            if (e.Key==Key.Enter)
            {
                try
                {
                    string connetionString;
                    DataSet ds = new DataSet();
                    string file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                    string cnn = File.ReadAllText(file);
                    string ou;
                    char[] output = new char[cnn.Length];
                    var i = 0;
                    foreach (var character in cnn)
                    {
                        output[i++] = (char)(character - '~');
                    }
                    ou = new string(output);
                    String ConnString;
                    ConnString = ou.Substring(0, ou.Length - 2);
                    connetionString = ConnString;
                    SqlConnection cns;
                    cns = new SqlConnection(connetionString);
                    cns.Open();
                    SqlCommand sql_cmnd = new SqlCommand("USP_PRODUCT_FROM_INVENTORY", cns);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@BARCODE", barcode);
                    sql_cmnd.ExecuteNonQuery();
                    SqlDataAdapter sda = new SqlDataAdapter(sql_cmnd);
                    sda.Fill(ds);
                    var brand = ds.Tables[0].Rows[0][1].ToString();
                    var mrp = ds.Tables[0].Rows[0][3].ToString();
                    //Fetch GST
                    SqlCommand cmd = new SqlCommand("USP_FETCH_GST", cns);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MRP", mrp);
                    sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("GST");
                    sda.Fill(dt);
                    var temp = dt.Rows[0][0].ToString();
                    var percentage = Convert.ToInt32(temp);
                    addProductList(barcode, brand, mrp,percentage);
                    cns.Close();
                }
                catch
                {
                    MessageBox.Show("Invalid Barcode","Alert");
                }
            }
        }
        private void addProductList(string barcode,string brand,string price,int percentage)
        {

            var quantity = 1;

            var preBillList = billing_list.Children.OfType<Border>();
            var flag = true;
            var index = -1;
            var i = 0;
            foreach(var borderElemnt in preBillList)
            {
                Border borderEl = borderElemnt as Border;
                TextBlock text = borderEl.Child as TextBlock;
                var name = text.Name;
                var value = text.Text;
                if(name.StartsWith("barcode") && value==barcode)
                {
                    flag = false;
                    index = i;
                }
                i++;
            }
            if (flag)
            {
                //billing_list
                var rowCount = billing_list.RowDefinitions.Count();
                var rowDef = new RowDefinition();
                billing_list.RowDefinitions.Add(rowDef);

                // S No
                var border = new Border();
                border.Padding = new Thickness(10, 5, 10, 5);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetColumn(border, 0);
                Grid.SetRow(border, rowCount - 1);
                var textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.FontSize = 18;
                textBlock.Text = rowCount.ToString();
                textBlock.Name = "sno" + (rowCount);
                border.Child = textBlock;
                billing_list.Children.Add(border);

                // Barcode
                border = new Border();
                border.Padding = new Thickness(10, 5, 10, 5);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetColumn(border, 1);
                Grid.SetRow(border, rowCount - 1);
                textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.FontSize = 18;
                textBlock.Text = barcode;
                textBlock.Name = "barcode" + (rowCount);
                border.Child = textBlock;
                billing_list.Children.Add(border);

                // Brand
                border = new Border();
                border.Padding = new Thickness(10, 5, 10, 5);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetColumn(border, 2);
                Grid.SetRow(border, rowCount - 1);
                textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.FontSize = 18;
                textBlock.Text = brand;
                textBlock.Name = "brand" + (rowCount);
                border.Child = textBlock;
                billing_list.Children.Add(border);

                // Quantity
                border = new Border();
                border.Padding = new Thickness(10, 5, 10, 5);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetColumn(border, 3);
                Grid.SetRow(border, rowCount - 1);
                textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.FontSize = 18;
                textBlock.Text = "1";
                textBlock.Name = "quantity" + (rowCount);
                border.Child = textBlock;
                billing_list.Children.Add(border);

                // Price
                border = new Border();
                border.Padding = new Thickness(10, 5, 10, 5);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetColumn(border, 4);
                Grid.SetRow(border, rowCount - 1);
                textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.FontSize = 18;
                textBlock.Text = price;
                textBlock.Name = "price" + (rowCount);
                border.Child = textBlock;
                billing_list.Children.Add(border);

                // Remove
                border = new Border();
                border.Padding = new Thickness(10, 5, 10, 5);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetColumn(border, 5);
                Grid.SetRow(border, rowCount - 1);
                textBlock = new TextBlock();
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.FontSize = 18;
                textBlock.Text = "x";
                textBlock.Cursor = Cursors.Hand;
                textBlock.PreviewMouseDown += new MouseButtonEventHandler(removeRow);
                textBlock.Name = "remove" + (rowCount);
                textBlock.Foreground = Brushes.Red;
                textBlock.FontWeight = FontWeights.Bold;
                border.Child = textBlock;
                billing_list.Children.Add(border);
            }
            else
            {
                i = 0;
                var quantityVal = -1;
                foreach (var borderElemnt in preBillList)
                {
                    var borderEl = borderElemnt as Border;
                    var text = borderEl.Child as TextBlock;
                    var name = text.Name;
                    var value = text.Text;
                    if (name.StartsWith("quantity") && i==(index+2))
                    {
                        var intVal = Convert.ToInt32( value );
                        quantityVal = intVal + 1;
                        text.Text = (intVal + 1).ToString();
                    }
                    if (name.StartsWith("price") && i==(index+3) && quantityVal!=-1)
                    {
                        text.Text = (quantityVal* Convert.ToInt32(price)).ToString();
                    }
                    i++;
                }
                quantity = quantityVal;
            }

            serialNumberBalancing();
        }
        private void removeRow(object sender,MouseButtonEventArgs e)
        {
            var preBillList = billing_list.Children.OfType<Border>();
            var removeButton = (TextBlock)sender;
            var removeButtonName = removeButton.Name;
            var i = 0;
            var index = -1;
            //Find row
            foreach (var borderElemnt in preBillList)
            {
                Border borderEl = borderElemnt as Border;
                TextBlock text = borderEl.Child as TextBlock;
                var name = text.Name;
                var value = text.Text;
                if (name.StartsWith(removeButtonName))
                {
                    index = i;
                }
                i++;
            }
            //Remove Child
            var rowStart = index - 5;
            for(i=0;i<6;i++)
            {
                billing_list.Children.RemoveAt(rowStart);
            }
            billing_list.RowDefinitions.RemoveAt((index/5)-1);

            serialNumberBalancing();
        }
        private void serialNumberBalancing()
        {
            var i = 0;
            var j = 1;
            var preBillList = billing_list.Children.OfType<Border>();
            foreach (var borderElemnt in preBillList)
            {
                Border borderEl = borderElemnt as Border;
                TextBlock text = borderEl.Child as TextBlock;
                var name = text.Name;
                var value = text.Text;
                if (name.StartsWith("sno"))
                {
                    text.Text = j.ToString();
                    j++;
                }
                i++;
            }
            billingAmountList();
        }
        private void billingAmountList()
        {
            List<int> mrp = new List<int>();
            List<int> discount = new List<int>();
            List<int> price = new List<int>();
            List<int> gst = new List<int>();
            //get all MRP
            var preBillList = billing_list.Children.OfType<Border>();
            foreach (var borderElemnt in preBillList)
            {
                Border borderEl = borderElemnt as Border;
                TextBlock text = borderEl.Child as TextBlock;
                var name = text.Name;
                var value = text.Text;
                if (name.StartsWith("price"))
                {
                    mrp.Add(Convert.ToInt32(value));
                }
            }
            //get price and gst
            foreach(var value in mrp)
            {
                discount.Add(0);
                try
                {
                    string connetionString;
                    DataSet ds = new DataSet();
                    string file = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                    string cnn = File.ReadAllText(file);
                    string ou;
                    char[] output = new char[cnn.Length];
                    var i = 0;
                    foreach (var character in cnn)
                    {
                        output[i++] = (char)(character - '~');
                    }
                    ou = new string(output);
                    String ConnString;
                    ConnString = ou.Substring(0, ou.Length - 2);
                    connetionString = ConnString;
                    SqlConnection cns;
                    cns = new SqlConnection(connetionString);
                    cns.Open();
                    SqlCommand cmd = new SqlCommand("USP_FETCH_GST", cns);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("MRP", value);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("GST");
                    sda.Fill(dt);
                    var temp = dt.Rows[0][0].ToString();
                    var percentage = Convert.ToInt32(temp);

                    var PriceAmount =  Math.Ceiling(((float)value / (100 + percentage)) * 100);
                    var GstAmount = value - PriceAmount;

                    price.Add(Convert.ToInt32( PriceAmount));
                    gst.Add(Convert.ToInt32(GstAmount));

                    cns.Close();
                }
                catch
                {
                    MessageBox.Show("Connection Failed!", "Alert");
                }
            }
            //assign values
            netMrp.Text = mrp.Sum().ToString();
            netDiscount.Text = discount.Sum().ToString();
            netPrice.Text = price.Sum().ToString();
            netGst.Text = gst.Sum().ToString();
            netBillAmount.Text = ( mrp.Sum()- discount.Sum() ).ToString();
            balance.Text = "0";
        }
    }
}

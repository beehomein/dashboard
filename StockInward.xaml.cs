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
    /// Interaction logic for StockInward.xaml
    /// </summary>
    public partial class StockInward : Window
    {
        public StockInward()
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
                listFill1();
                listFill2();
            }
            catch
            {
                new Connection().Show();
                MessageBox.Show("SQL Connection Required!", "Alert");
                this.Close();
            }
        }
        public void listFill1()
        {
            var rowDef = new RowDefinition();
            purchasesList.RowDefinitions.Add(rowDef);

            var colDef = new ColumnDefinition();
            purchasesList.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            purchasesList.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            purchasesList.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            purchasesList.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            purchasesList.ColumnDefinitions.Add(colDef);

            //row 0
            var rowCount = purchasesList.RowDefinitions.Count();
            var border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1);
            Grid.SetRow(border, rowCount-1);
            Grid.SetColumn(border, 0);
            Grid.SetColumnSpan(border, 5);

            var text = new TextBlock();
            text.FontSize = 23;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.Text = "PurchasesList";
            border.Child = text;
            purchasesList.Children.Add(border);

            //row 1
            rowDef = new RowDefinition();
            purchasesList.RowDefinitions.Add(rowDef);

            rowCount = purchasesList.RowDefinitions.Count();
            //S No
            border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            Grid.SetRow(border, rowCount-1);
            Grid.SetColumn(border, 0);

            text = new TextBlock();
            text.FontSize = 20;
            text.Text = "S No";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            border.Child = text;
            purchasesList.Children.Add(border);
            //PO
            border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 1);

            text = new TextBlock();
            text.FontSize = 20;
            text.Text = "Purchase Id";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            border.Child = text;
            purchasesList.Children.Add(border);
            //No of Products
            border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 2);

            text = new TextBlock();
            text.FontSize = 20;
            text.Text = "No of Products";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            border.Child = text;
            purchasesList.Children.Add(border);
            //Transaction Date
            border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 3);

            text = new TextBlock();
            text.FontSize = 20;
            text.Text = "Transaction Date";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            border.Child = text;
            purchasesList.Children.Add(border);
            //Status
            border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 1, 1);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 4);

            text = new TextBlock();
            text.FontSize = 20;
            text.Text = "Status";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            border.Child = text;
            purchasesList.Children.Add(border);

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
            SqlCommand cmd = new SqlCommand("USP_LIST_PURCHASES_FULL_LIST", scn);
            cmd.CommandType = CommandType.StoredProcedure;
            scn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            //resArea.Children.Clear();
            i = 0;
            foreach (DataRow row in dt.Rows)
            {
                i++;
                //row 1
                rowDef = new RowDefinition();
                purchasesList.RowDefinitions.Add(rowDef);

                rowCount = purchasesList.RowDefinitions.Count();
                //S No
                border = new Border();
                border.Padding = new Thickness(20);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetRow(border, rowCount - 1);
                Grid.SetColumn(border, 0);

                text = new TextBlock();
                text.FontSize = 16;
                text.Text =i.ToString();
                text.HorizontalAlignment = HorizontalAlignment.Center;
                border.Child = text;
                purchasesList.Children.Add(border);
                //PO
                border = new Border();
                border.Padding = new Thickness(20);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetRow(border, rowCount - 1);
                Grid.SetColumn(border, 1);

                text = new TextBlock();
                text.FontSize = 16;
                text.Text = row[1].ToString();
                text.Foreground = Brushes.Blue;
                text.Cursor = Cursors.Hand;
                text.HorizontalAlignment = HorizontalAlignment.Center;
                text.PreviewMouseDown += new MouseButtonEventHandler((sender,e)=> openTransit(sender, e,"Purchase Order"));
                border.Child = text;
                purchasesList.Children.Add(border);
                //No of Products
                border = new Border();
                border.Padding = new Thickness(20);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetRow(border, rowCount - 1);
                Grid.SetColumn(border, 2);

                text = new TextBlock();
                text.FontSize = 16;
                text.Text = row[2].ToString();
                text.HorizontalAlignment = HorizontalAlignment.Center;
                border.Child = text;
                purchasesList.Children.Add(border);
                //Transaction Date
                border = new Border();
                border.Padding = new Thickness(20);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetRow(border, rowCount - 1);
                Grid.SetColumn(border, 3);

                text = new TextBlock();
                text.FontSize = 16;
                text.Text = row[6].ToString();
                text.HorizontalAlignment = HorizontalAlignment.Center;
                border.Child = text;
                purchasesList.Children.Add(border);
                //Status
                border = new Border();
                border.Padding = new Thickness(20);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 1, 1);
                Grid.SetRow(border, rowCount - 1);
                Grid.SetColumn(border, 4);

                text = new TextBlock();
                text.FontSize = 16;
                text.Text = row[5].ToString();
                text.HorizontalAlignment = HorizontalAlignment.Center;
                border.Child = text;
                purchasesList.Children.Add(border);
            }
            scn.Close();
        }
        public void listFill2()
        {
            var rowDef = new RowDefinition();
            transfersList.RowDefinitions.Add(rowDef);

            var colDef = new ColumnDefinition();
            transfersList.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            transfersList.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            transfersList.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            transfersList.ColumnDefinitions.Add(colDef);
            colDef = new ColumnDefinition();
            transfersList.ColumnDefinitions.Add(colDef);

            //row 0
            var rowCount = transfersList.RowDefinitions.Count();
            var border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 0);
            Grid.SetColumnSpan(border, 5);

            var text = new TextBlock();
            text.FontSize = 23;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.Text = "Transfers List";
            border.Child = text;
            transfersList.Children.Add(border);

            //row 1
            rowDef = new RowDefinition();
            transfersList.RowDefinitions.Add(rowDef);

            rowCount = transfersList.RowDefinitions.Count();
            //S No
            border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 0);

            text = new TextBlock();
            text.FontSize = 20;
            text.Text = "S No";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            border.Child = text;
            transfersList.Children.Add(border);
            //TO
            border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 1);

            text = new TextBlock();
            text.FontSize = 20;
            text.Text = "Transfer Id";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            border.Child = text;
            transfersList.Children.Add(border);
            //No of Products
            border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 2);

            text = new TextBlock();
            text.FontSize = 20;
            text.Text = "No of Products";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            border.Child = text;
            transfersList.Children.Add(border);
            //Transaction Date
            border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 0, 1);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 3);

            text = new TextBlock();
            text.FontSize = 20;
            text.Text = "Transaction Date";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            border.Child = text;
            transfersList.Children.Add(border);
            //Status
            border = new Border();
            border.Padding = new Thickness(20);
            border.Background = Brushes.LightGray;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1, 0, 1, 1);
            Grid.SetRow(border, rowCount - 1);
            Grid.SetColumn(border, 4);

            text = new TextBlock();
            text.FontSize = 20;
            text.Text = "Status";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            border.Child = text;
            transfersList.Children.Add(border);

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
            SqlCommand cmd = new SqlCommand("USP_LIST_TRANSFERS_FULL_LIST", scn);
            cmd.CommandType = CommandType.StoredProcedure;
            scn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            //resArea.Children.Clear();
            i = 0;
            foreach (DataRow row in dt.Rows)
            {
                i++;
                //row 1
                rowDef = new RowDefinition();
                transfersList.RowDefinitions.Add(rowDef);

                rowCount = transfersList.RowDefinitions.Count();
                //S No
                border = new Border();
                border.Padding = new Thickness(20);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetRow(border, rowCount - 1);
                Grid.SetColumn(border, 0);

                text = new TextBlock();
                text.FontSize = 16;
                text.Text = i.ToString();
                text.HorizontalAlignment = HorizontalAlignment.Center;
                border.Child = text;
                transfersList.Children.Add(border);
                //TO
                border = new Border();
                border.Padding = new Thickness(20);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetRow(border, rowCount - 1);
                Grid.SetColumn(border, 1);

                text = new TextBlock();
                text.FontSize = 16;
                text.Text = row[1].ToString();
                text.HorizontalAlignment = HorizontalAlignment.Center;
                text.Foreground = Brushes.Blue;
                text.PreviewMouseDown += new MouseButtonEventHandler((sender, e) => openTransit(sender, e, "Transfer Order"));
                text.Cursor = Cursors.Hand;
                border.Child = text;
                transfersList.Children.Add(border);
                //No of Products
                border = new Border();
                border.Padding = new Thickness(20);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetRow(border, rowCount - 1);
                Grid.SetColumn(border, 2);

                text = new TextBlock();
                text.FontSize = 16;
                text.Text = row[2].ToString();
                text.HorizontalAlignment = HorizontalAlignment.Center;
                border.Child = text;
                transfersList.Children.Add(border);
                //Transaction Date
                border = new Border();
                border.Padding = new Thickness(20);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 0, 1);
                Grid.SetRow(border, rowCount - 1);
                Grid.SetColumn(border, 3);

                text = new TextBlock();
                text.FontSize = 16;
                text.Text = row[6].ToString();
                text.HorizontalAlignment = HorizontalAlignment.Center;
                border.Child = text;
                transfersList.Children.Add(border);
                //Status
                border = new Border();
                border.Padding = new Thickness(20);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 0, 1, 1);
                Grid.SetRow(border, rowCount - 1);
                Grid.SetColumn(border, 4);

                text = new TextBlock();
                text.FontSize = 16;
                text.Text = row[5].ToString();
                text.HorizontalAlignment = HorizontalAlignment.Center;
                border.Child = text;
                transfersList.Children.Add(border);
            }
            scn.Close();
        }

        private void openTransit(object sender,MouseButtonEventArgs e,string source)
        {
            var link = (TextBlock)sender;
            var tableName = link.Text;
            var stockInwardTransit = new StockInwardTransit(tableName, source);
            stockInwardTransit.Show();
            this.Close();
        }

    }
}

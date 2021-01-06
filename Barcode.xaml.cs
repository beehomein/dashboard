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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for Barcode.xaml
    /// </summary>
    public partial class Barcode : Window
    {
        public int precount = 0;
        public Barcode()
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
                Fill();
                BarcodeType.Text = "Price Tag Only";
                listCheckBoxes();
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
            var Wrap = new WrapPanel();
            Wrap.Orientation = Orientation.Horizontal;
            Wrap.HorizontalAlignment = HorizontalAlignment.Center;
            Wrap.VerticalAlignment = VerticalAlignment.Center;

            //Barcode
            var Stack = new StackPanel();
            Stack.VerticalAlignment = VerticalAlignment.Center;
            Stack.HorizontalAlignment = HorizontalAlignment.Left;
            Stack.Orientation = Orientation.Vertical;
            Stack.Width = 300;
            Stack.Margin = new Thickness(20);

            var Text = new TextBlock();
            Text.Text = "Barcode";
            Text.Padding = new Thickness(20);
            Text.FontSize = 20;

            var Combo = new ComboBox();
            Combo.FontSize = 20;
            Combo.Width = 250;
            Combo.Padding = new Thickness(10);
            Combo.Name="Barcode"+(precount + 1);

            //Dynamic Barcode

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
            SqlConnection scn = new SqlConnection(ConnString1);
            SqlCommand cmd = new SqlCommand("SP_LIST_BARCODES", scn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            scn.Open();

            var Item = new ComboBoxItem();
            Item.Content = "Select Barcode";
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

            //No of Copies
            Stack = new StackPanel();
            Stack.VerticalAlignment = VerticalAlignment.Center;
            Stack.HorizontalAlignment = HorizontalAlignment.Left;
            Stack.Orientation = Orientation.Vertical;
            Stack.Width = 300;
            Stack.Margin = new Thickness(20);

            Text = new TextBlock();
            Text.Text = "No of Copies";
            Text.Padding = new Thickness(20);
            Text.FontSize = 20;

            var Input = new TextBox();
            Input.Name = "noOfCopies" + (precount + 1);
            Input.FontSize = 20;
            Input.Width = 250;
            //FontFamily ff = this.Resources["Bcd342"] as FontFamily;
            //Input.FontFamily = ff;
            Input.Padding = new Thickness(10);
            Input.Text = "1";

            Stack.Children.Add(Text);
            Stack.Children.Add(Input);
            Wrap.Children.Add(Stack);
            root.Children.Add(Wrap);
            if(precount==0)
            {
                btnRemove.Width = 0;
            }
            else
            {
                btnRemove.Width = 100;
            }
            precount++;
        }
        public void listCheckBoxes()
        {
            var choice = BarcodeType.Text;
            if(choice!= "Select Barcode Type")
            {
                if(choice== "Price Tag Only")
                {
                    priceTagOnlyCheckList.Height = 200;
                    priceTagWithDetailsCheckList.Height = 0;
                }
                if (choice == "Price With Details")
                {
                    priceTagOnlyCheckList.Height = 0;
                    priceTagWithDetailsCheckList.Height = 500;
                }
            }
        }

        private void BarcodeTypeChage(object sender, EventArgs e)
        {
            listCheckBoxes();
        }

        private void add(object sender, RoutedEventArgs e)
        {
            Fill();
        }

        private void remove(object sender, RoutedEventArgs e)
        {
            if(precount>1)
            {
                root.Children.RemoveAt(precount-1);
                precount--;
                if (precount == 1)
                {
                    btnRemove.Width = 0;
                }
                else
                {
                    btnRemove.Width = 100;
                }
            }
        }

        private void genBarcode(object sender, RoutedEventArgs e)
        {
            //new window
            var printOut = new Window();
            printOut.WindowState = WindowState.Maximized;
            printOut.Title = "Print Preview";

            string companyName = "";
            string companyType = "";
            string companyNumber = "";
            string companyEmail = "";
            string companyId= "";
            string companyAdderess= "";
            //Company Details

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
            SqlConnection scn = new SqlConnection(ConnString1);
            SqlCommand cmd = new SqlCommand("SP_LIST_COMPANY_DETAILS", scn);
            cmd.CommandType = CommandType.StoredProcedure;
            scn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Employee");
            sda.Fill(dt);
            scn.Close();
            foreach (DataRow row in dt.Rows)
            {
                companyName = row[1].ToString();
                companyType = row[2].ToString();
                companyNumber = row[3].ToString();
                companyEmail = row[4].ToString();
                companyAdderess = row[5].ToString();
                companyId = row[6].ToString();

            }
            var wrapPanels= root.Children.OfType<WrapPanel>();
            List<string> Barcode = new List<string>(); 
            List<int> NoOfCopies = new List<int>(); 
            foreach(var wrapPanel in wrapPanels)
            {
                var stackPanels = wrapPanel.Children.OfType<StackPanel>();
                foreach(var stackPanel in stackPanels)
                {
                    var comboBoxes = stackPanel.Children.OfType<ComboBox>();
                    foreach(var comboBox in comboBoxes)
                    {
                        Barcode.Add(comboBox.Text);
                    }
                    var textBoxes = stackPanel.Children.OfType<TextBox>();
                    foreach( var textBox in textBoxes)
                    {
                        NoOfCopies.Add(Convert.ToInt32( textBox.Text));
                    }
                }
            }
            int count = 0;
            var conutFlag = true;
            foreach(var value in Barcode)
            {
                if( value!= "Select Barcode")
                {
                    count++;
                }
            }
            if(count<Barcode.Count())
            {
                conutFlag = false;
            }
            string outString="";
            if(conutFlag)
            {
                var category = "";
                var group = "";
                var division = "";
                var size = "";
                var brand = "";
                var styleCode = "";
                var length = "";
                var mrp = "";
                var manufacturedDate = "";

                var Wrap = new WrapPanel();
                Wrap.Orientation = Orientation.Horizontal;
                Wrap.HorizontalAlignment = HorizontalAlignment.Center;
                Wrap.VerticalAlignment = VerticalAlignment.Center;
                var i = 0;
                for (i = 0; i < Barcode.Count(); i++)
                {
                    outString += Barcode[i] + " " + NoOfCopies[i] + "\n";
                    //Company Details

                    file1 = System.IO.Directory.GetCurrentDirectory().ToString() + @"\conn.txt";
                    //MessageBox.Show(file);
                    cnn1 = File.ReadAllText(file1);

                    //Decryption

                    //string ou = new string(cnn);
                    output1 = new char[cnn1.Length];
                    i1 = 0;
                    foreach (var character in cnn1)
                    {
                        output1[i1++] = (char)(character - '~');
                    }
                    ou1 = new string(output1);

                    //Decrypting Text File
                    ConnString1 = ou1.Substring(0, ou1.Length - 2);
                    //MessageBox.Show(ConnString);
                    SqlConnection scn1 = new SqlConnection(ConnString1);
                    cmd = new SqlCommand("SP_SELECT_NEW_PRODUCT_BY_BARCODE", scn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("BARCODE", Barcode[i]);
                    scn.Open();
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("Employee");
                    sda.Fill(dt);
                    scn.Close();
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

                    }
                    var barcodeType = BarcodeType.Text;
                    if(barcodeType== "Price Tag Only")
                    {
                        for(int j =0;j<NoOfCopies[i];j++)
                        {
                            var Border = new Border();
                            Border.BorderBrush =System.Windows.Media.Brushes.Black;
                            //Border.CornerRadius = new CornerRadius(10);
                            Border.BorderThickness = new Thickness(1);
                            Border.Margin = new Thickness(5);
                            Border.VerticalAlignment = VerticalAlignment.Center;
                            Border.HorizontalAlignment = HorizontalAlignment.Center;

                            var Stack = new StackPanel();
                            Stack.Orientation = Orientation.Vertical;
                            Stack.Width = 300;
                            Stack.HorizontalAlignment = HorizontalAlignment.Center;
                            Stack.VerticalAlignment = VerticalAlignment.Center;
                            Stack.Margin = new Thickness(5);

                            //Barcode Image and Text
                            Stack.Children.Add(barcodeToImage(Barcode[i]));

                            //Barand
                            var Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = brand;
                            Stack.Children.Add(Text);

                            //MRP
                            Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = mrp;
                            Stack.Children.Add(Text);

                            //Company Details
                            Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = companyAdderess + "\n" + companyEmail + "\n" + companyNumber;
                            Stack.Children.Add(Text);

                            Border.Child = Stack;
                            Wrap.Children.Add(Border);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < NoOfCopies[i]; j++)
                        {
                            var Border = new Border();
                            Border.BorderBrush = System.Windows.Media.Brushes.Black;
                            //Border.CornerRadius = new CornerRadius(10);
                            Border.BorderThickness = new Thickness(1);
                            Border.Margin = new Thickness(5);
                            Border.VerticalAlignment = VerticalAlignment.Center;
                            Border.HorizontalAlignment = HorizontalAlignment.Center;

                            var Stack = new StackPanel();
                            Stack.Orientation = Orientation.Vertical;
                            Stack.Width = 300;
                            Stack.HorizontalAlignment = HorizontalAlignment.Center;
                            Stack.VerticalAlignment = VerticalAlignment.Center;
                            Stack.Margin = new Thickness(5);

                            //Barcode Image and Text
                            Stack.Children.Add(barcodeToImage(Barcode[i]));

                            //Brand
                            var Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = "Brand Name: " + brand;
                            Stack.Children.Add(Text);

                            //Category
                            Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = "Category: " + category;
                            Stack.Children.Add(Text);

                            //Group
                            Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = "Group: " + group;
                            Stack.Children.Add(Text);

                            //Division
                            Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = "Division: " + group;
                            Stack.Children.Add(Text);

                            //Size
                            Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = "Size: " + size;
                            Stack.Children.Add(Text);

                            //Length
                            Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = "Length: " + length;
                            Stack.Children.Add(Text);

                            //Style Code
                            Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = "Style Code : " + styleCode;
                            Stack.Children.Add(Text);

                            //MRP
                            Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = "MRP: " + mrp;
                            Stack.Children.Add(Text);

                            //Company Details
                            Text = new TextBlock();
                            Text.FontSize = 16;
                            Text.Padding = new Thickness(20, 5, 20, 5);
                            Text.Text = companyAdderess + "\n" + companyEmail + "\n" + companyNumber;
                            Stack.Children.Add(Text);

                            Border.Child = Stack;
                            Wrap.Children.Add(Border);
                        }
                    }
                }
                var scroll = new ScrollViewer();
                scroll.Margin = new Thickness(50,100,50,100);
                scroll.Content = Wrap;
                printOut.Content = scroll;
                
                /*
                pdf genaration
                using (PdfDocument document = new PdfDocument())
                {
                    //Add a page to the document
                    PdfPage page = document.Pages.Add();

                    //Create PDF graphics for a page
                    PdfGraphics graphics = page.Graphics;

                    //Set the standard font
                    PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                    //Draw the text
                    graphics.DrawString("Wrap", font, PdfBrushes.Black, new PointF(0, 0));
                    //Save the document
                    document.Save("Output.pdf");
                }
                */
                printOut.Show();
                //MessageBox.Show(outString, "Output");
            }
            else
            {
                MessageBox.Show("Select All Barcode","Alert");
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            root.Children.Clear();
            precount = 0;
            Fill();
        }
        private StackPanel barcodeToImage(string barcodeText)
        {
            /* StackPanel barcodeToImage(string)
             * returns barcode with text
             * depencies : Zen.Barcode.Core.dll
             */
            var contatiner = new StackPanel();
            contatiner.VerticalAlignment = VerticalAlignment.Center;
            contatiner.HorizontalAlignment = HorizontalAlignment.Center;

            var text = new TextBlock();
            text.Padding = new Thickness(20, 5, 20, 5);
            text.Text = barcodeText;
            text.FontSize = 16;
            text.HorizontalAlignment = HorizontalAlignment.Center;

            var imageEl = new System.Windows.Controls.Image();
            imageEl.Width = 150;
            imageEl.Height = 50;
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            System.Drawing.Image image;
            image = barcode.Draw(barcodeText, 20);
            var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            stream.Position = 0;
            var bmpImgage = new System.Windows.Media.Imaging.BitmapImage();
            bmpImgage.BeginInit();
            bmpImgage.CacheOption = BitmapCacheOption.OnLoad;
            bmpImgage.StreamSource = stream;
            bmpImgage.EndInit();
            imageEl.Source = bmpImgage;
            contatiner.Children.Add(imageEl);
            contatiner.Children.Add(text);
            return contatiner;
        }
    }
}

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
using System.Drawing;
namespace DashBoard
{
    /// <summary>
    /// Interaction logic for Printer.xaml
    /// </summary>
    public partial class Printer : Window
    {
        public Printer()
        {
            InitializeComponent();
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + "wmic printer get name > printerName.txt");
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + "wmic printer get PrinterStatus > printerStatus.txt");
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                string path = @"printerName.txt";
                int length = 0;
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        length++;
                    }
                }
                string[] printerNames = new string[length];
                string[] printerStatus = new string[length];
                path = @"printerName.txt";
                int i = 0;
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        printerNames[i] = s;
                        i++;
                    }
                }
                path = @"printerStatus.txt";
                i = 0;
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        if(s.Contains('3'))
                        {
                            s = "Connected";
                        }
                        else
                        {
                            s = "Not Connected";
                        }
                        printerStatus[i] = s;
                        i++;
                    }
                }
                //Main Stack panel
                //var mainStack = new StackPanel();
                //mainStack.Orientation = Orientation.Vertical;
                //mainStack.HorizontalAlignment = HorizontalAlignment.Center;
                //mainStack.VerticalAlignment = VerticalAlignment.Center;
                //mainStack.Margin = new Thickness(20);
                //Row
                var stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;
                //Column 1
                var border = new Border();
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = System.Windows.Media.Brushes.Black;
                border.Width = 450;
                border.HorizontalAlignment = HorizontalAlignment.Center;
                var textBlock = new TextBlock();
                textBlock.Text = "Printer Name";
                textBlock.FontSize = 25;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.FontWeight = FontWeights.Bold;
                border.Child = textBlock;
                //Binding Column 1
                stack.Children.Add(border);
                //Column 2
                border = new Border();
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = System.Windows.Media.Brushes.Black;
                border.Width = 450;
                border.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock = new TextBlock();
                textBlock.Text = "Printer Status";
                textBlock.FontSize = 25;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.FontWeight = FontWeights.Bold;
                border.Child = textBlock;
                //Binding Column 2
                stack.Children.Add(border);
                //Binding row
                Container.Children.Add(stack);
                //var border = new Border();
                //border.BorderThickness = new Thickness(1);
                //border.BorderBrush = Brushes.Black;
                //var stack = new StackPanel();
                //stack.Orientation = Orientation.Horizontal;
                //stack.Margin = new Thickness(20,20,20,20);
                //var textBox = new TextBlock();
                //textBox.Text = "Printer Name";
                //textBox.FontSize = 25;
                //textBox.Width = 450;
                //textBox.FontWeight = FontWeights.Bold;
                //stack.Children.Add(textBox);
                //textBox = new TextBlock();
                //textBox.Text = "Printer Status";
                //textBox.FontSize = 25;
                //textBox.Width = 200;
                //stack.Children.Add(textBox);
                //border.Child = stack;
                //Container.Children.Add(border);
                for (i=1;i<length;i++)
                {
                    stack = new StackPanel();
                    stack.Orientation = Orientation.Horizontal;
                    //Column 1
                    border = new Border();
                    border.BorderThickness = new Thickness(1,0,1,1);
                    border.BorderBrush = System.Windows.Media.Brushes.Black;
                    border.Width = 450;
                    border.HorizontalAlignment = HorizontalAlignment.Left;
                    border.Padding = new Thickness(10, 0, 0, 0);
                    textBlock = new TextBlock();
                    textBlock.Text = printerNames[i];
                    textBlock.FontSize = 25;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    //textBlock.FontWeight = FontWeights.Bold;
                    border.Child = textBlock;
                    //Binding Column 1
                    stack.Children.Add(border);
                    //Column 2
                    border = new Border();
                    border.BorderThickness = new Thickness(1, 0, 1, 1);
                    border.BorderBrush = System.Windows.Media.Brushes.Black;
                    border.Width = 450;
                    border.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock = new TextBlock();
                    textBlock.Text = printerStatus[i];
                    textBlock.FontSize = 25;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    //textBlock.FontWeight = FontWeights.Bold;
                    border.Child = textBlock;
                    //Binding Column 2
                    stack.Children.Add(border);
                    //Binding row
                    Container.Children.Add(stack);
                }

            }
            catch (Exception objException)
            {
                // Log the exception
                MessageBox.Show(objException.ToString());
            }
        }
    }
}

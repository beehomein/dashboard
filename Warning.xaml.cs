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

namespace DashBoard
{
    /// <summary>
    /// Interaction logic for Warning.xaml
    /// </summary>
    public class PopUpOptions
    {
        string alert = "Alert";
        string warning = "Waning";
        string confirm = "Confirm";
        string Success = "Success";
    }
    public partial class Warning : Window
    {
        public Warning()
        {
            InitializeComponent();
        }
    }
}

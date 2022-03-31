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

namespace SoftBook
{
    /// <summary>
    /// Логика взаимодействия для Replace.xaml
    /// </summary>
    public partial class Replace : Window
    {
        public RichTextBox rtb;
        public Replace(RichTextBox richTextBox)
        {
            InitializeComponent();
            rtb = richTextBox;
        }
        private void Replacetxt_Click(object sender, RoutedEventArgs e)
        {
            string search = what.Text;
            string search1 = that.Text;
            TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            string rtf = tr.Text.ToString();
            rtf = tr.Text.ToString();
            rtf = rtf.Replace(search, search1);
            rtb.SelectAll();
            tr.Text = rtf;
        }

        private void Cancel1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

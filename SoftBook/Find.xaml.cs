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
    /// Логика взаимодействия для Find.xaml
    /// </summary>
    public partial class Find : Window
    {
        public RichTextBox rtb;
        public Find(RichTextBox richTextBox)
        {
            InitializeComponent();
            rtb = richTextBox;
        }
        private static TextPointer GetTextPointAt(TextPointer from, int pos)
        {
            TextPointer ret = from;
            int i = 0;

            while ((i < pos) && (ret != null))
            {
                if ((ret.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.Text) || (ret.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.None))
                    i++;

                if (ret.GetPositionAtOffset(1, LogicalDirection.Forward) == null)
                    return ret;

                ret = ret.GetPositionAtOffset(1, LogicalDirection.Forward);
            }

            return ret;
        }
        internal string SelectedTxt(RichTextBox rtb, int offset, int length, Color color)
        {

            TextSelection textRange = rtb.Selection;
            TextPointer start = rtb.Document.ContentStart;
            TextPointer startPos = GetTextPointAt(start, offset);
            TextPointer endPos = GetTextPointAt(start, offset + length);
            textRange.Select(startPos, endPos);
            textRange.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(color));
            return rtb.Selection.Text;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string search = what.Text;
            TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            string rtf = tr.Text.ToString();
            int indexOfChar = rtf.IndexOf(search);
            this.SelectedTxt(this.rtb, indexOfChar, what.Text.Length, Colors.White);
        }

        private void Findtxt_Click(object sender, RoutedEventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString("#FF4A78C5");
            string search = what.Text;
            TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            string rtf = tr.Text.ToString();
            int indexOfChar = rtf.IndexOf(search);
            this.SelectedTxt(this.rtb, indexOfChar, what.Text.Length, color);
        }

        private void Cancel1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

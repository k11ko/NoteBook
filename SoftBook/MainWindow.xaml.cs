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
using SoftBook;

namespace SoftBook
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Func softbook;
		public MainWindow()
		{
			InitializeComponent();
			softbook = new Func(fieldEdit);
			fieldEdit.Focus();
		}

		private void Create_Click(object sender, RoutedEventArgs e)
		{
			softbook.Create();
			this.Title = "Безымянный - Блокнот";
		}

		private void Open_Click(object sender, RoutedEventArgs e)
		{
			if (softbook.Modified == true)
			{
				MessageBoxResult rezult;
				rezult = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Блокнот", MessageBoxButton.YesNoCancel);
				if (rezult == MessageBoxResult.Yes)
					if (softbook.ASaveBloknot() == false) return;
				if (rezult == MessageBoxResult.Cancel) return;
			}
			softbook.Open();
			this.Title = softbook.NameFile;
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			softbook.ASaveBloknot();
			this.Title = softbook.NameFile;
		}

		private void SaveHow_Click(object sender, RoutedEventArgs e)
		{
			softbook.ASaveASBloknot();
			this.Title = softbook.NameFile;
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			if (softbook.Modified == true)
			{
				MessageBoxResult rezult;
				rezult = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Выход из программы", MessageBoxButton.YesNoCancel);
				if (rezult == MessageBoxResult.Yes)
					if (softbook.ASaveBloknot() == false) return;
				if (rezult == MessageBoxResult.Cancel) return;
			}
			else Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (softbook.Modified == true)
			{
				MessageBoxResult rezult;
				rezult = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Выход из программы", MessageBoxButton.YesNoCancel);
				if (rezult == MessageBoxResult.Yes)
					if (softbook.ASaveBloknot() == false) return;
				if (rezult == MessageBoxResult.Cancel) e.Cancel = true;
			}
		}

		private void fieldEdit_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (softbook == null) return;
			else softbook.Modified = true;
			this.Title = "*" + softbook.NameFile;
		}
		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			fieldEdit.Selection.Text = "";
		}

		private void Find_Click(object sender, RoutedEventArgs e)
		{
            Find find = new Find(fieldEdit);
            find.Owner = this;
            find.Show();
        }

		private void SelectAll_Click(object sender, RoutedEventArgs e)
		{
			fieldEdit.SelectAll();
		}

		private void Time_Click(object sender, RoutedEventArgs e)
		{
			DateTime d = DateTime.Now;
			fieldEdit.AppendText(d.ToString("HH:mm:ss") + " " + d.ToString("dd:MM:yyyy"));
		}

		private void Info_Click(object sender, RoutedEventArgs e)
		{
			Information info = new Information();
			info.Owner = this;
			info.ShowDialog();
		}

        private void Defoult_Click(object sender, RoutedEventArgs e)
        {
			fieldEdit.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);
			fieldEdit.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);
		}

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
			fieldEdit.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
		}

        private void Fat_Click(object sender, RoutedEventArgs e)
        {
			fieldEdit.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);
		}

        private void Replace_Click(object sender, RoutedEventArgs e)
        {
			Replace replace = new Replace(fieldEdit);
			replace.Owner = this;
			replace.ShowDialog();
        }
    }
}

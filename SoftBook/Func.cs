using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SoftBook
{
	class Func
	{
        string nameFile;
        RichTextBox fieldEdit;
        public bool Modified { get; set; }
        public string NameFile
        {
            get { return nameFile; }
        }

        public Func(RichTextBox fieldEdit)
        {
            nameFile = "Безымянный - Блокнот";
            this.fieldEdit = fieldEdit;
            Modified = false;
        }

        //Модуль "Открыть"
        public void Open()
		{
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = "txt";
            open.Filter = "Текстовый файл (*.txt)|*.txt|Все файлы(*.*)|*.*";
            open.Title = "Открытие файла";

            if (open.ShowDialog() == true)
            {
                nameFile = open.FileName;
                TextRange doc = new TextRange(fieldEdit.Document.ContentStart, fieldEdit.Document.ContentEnd);
                using (FileStream fs = new FileStream(open.FileName, FileMode.Open))
                {
                    if (Path.GetExtension(open.FileName).ToLower() == ".txt")
                        doc.Load(fs, DataFormats.Text);
                }
            }
            
        }
        //Модуль "Сохранить"
        public bool ASaveBloknot()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = "txt";
            save.Filter = "Текстовый файл (*.txt)|*.txt|Все файлы(*.*)|*.*";
            save.Title = "Сохранение файла";
            if (nameFile == "Безымянный - Блокнот")
                if (save.ShowDialog() == true)
                    nameFile = save.FileName;
                else return false;
            TextRange doc = new TextRange(fieldEdit.Document.ContentStart,
                fieldEdit.Document.ContentEnd);
            FileStream fs = File.Create(nameFile);
            doc.Save(fs, DataFormats.Text);
            fs.Close();
            Modified = false;
            return true;
        }
        //Модуль "Сохранить как"
        public bool ASaveASBloknot()
        {
            nameFile = "";
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = "txt";
            save.Filter = "Текстовый файл (*.txt)|*.txt|Все файлы(*.*)|*.*";
            save.Title = "Сохранение файла";
            if (nameFile == "Безымянный - Блокнот")
                if (save.ShowDialog() == true)
                    nameFile = save.FileName;
                else return false;
            TextRange doc = new TextRange(fieldEdit.Document.ContentStart,
                fieldEdit.Document.ContentEnd);
            FileStream fs = File.Create(save.FileName);
            doc.Save(fs, DataFormats.Text);
            fs.Close();
            Modified = false;
            return true;
        }
        //Модуль "Создать"
        public void Create()
        {
            if (Modified == true)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите сохранить изменения в файле?",
                    "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                    if (ASaveBloknot() == false) return;
                if (result == MessageBoxResult.Cancel) return;
            }
            fieldEdit.Document.Blocks.Clear();
            nameFile = "Безымянный - Блокнот";
            Modified = false;
        }
    }
}

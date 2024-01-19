 using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach (UIElement el in MainRoot.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string key = "Password secret";
            string iv = "Secret iv";
            string filename = TextBox.Text;

            string name1 = System.IO.Path.GetFileNameWithoutExtension(TextBox2.Text);
            string extencion1 = System.IO.Path.GetExtension(TextBox2.Text);

            string name2 = System.IO.Path.GetFileNameWithoutExtension(TextBox3.Text);
            string extencion2 = System.IO.Path.GetExtension(TextBox3.Text);


            var extension = System.IO.Path.GetExtension("abc.txt");

            if (CheckBox1.IsChecked == true)
            {
                Encrypt.DecryptFile(filename, name1 + ".zip", key, iv);
                Archive.unzip(name1 + ".zip");
            }
            else if (CheckBox2.IsChecked == true)
            {
                Archive.unzip(filename);
                Encrypt.DecryptFile(name1 + ".enc", name1 + extencion1, key, iv);
            }
            else if (CheckBox3.IsChecked == true)
            {
                Encrypt.DecryptFile(filename, name1 + extencion1, key, iv);
            }
            else if (CheckBox4.IsChecked == true)
            {
                Archive.unzip(filename);
            }

            List<string> operation = new List<string> ();

            if (extencion1 == ".txt")
            {
                StreamReader f = new StreamReader(name1 + ".txt");
                while (!f.EndOfStream)
                {
                    string expression = f.ReadLine();
                    operation.Add(expression);
                }
                f.Close();
            }
            else if (extencion1 == ".xml")
            {
                Xml.read(name1 + ".xml", operation);
            }
            else if (extencion1 == ".json")
            {
                Json.JsonRead(name1 + ".json", operation);
            }
            else
            {
                TextBox2.ToolTip = "Введите коректно формат файла";
                TextBox2.Background = Brushes.Red;
            }



            List<double> result = new List<double> ();

            for(int i = 0; i < operation.Count(); i++)
            {
                string expression = operation[i];
                string postfixExpression = Parser.ConvertToPostfix(expression);
                double res = Parser.EvaluatePostfix(postfixExpression);
                result.Add(res);
            }



            if (extencion2 == ".txt")
            {
                using (StreamWriter sw = new StreamWriter(TextBox3.Text))
                {
                    for (int i = 0; i < result.Count(); i++)
                    {
                        sw.WriteLine(result[i]);
                    }
                }
            }
            else if (extencion2 == ".xml")
            {
                Xml.write(name2 + ".xml", result);
            }
            else if (extencion2 == ".json")
            {
                Json.JsonWrite(name2 + ".json", result);
            }
            else
            {
                TextBox3.ToolTip = "Введите коректно формат файла";
                TextBox3.Background = Brushes.Red;
            }



            if (CheckBox5.IsChecked == true)
            {
                Encrypt.EncryptFile(TextBox3.Text, name2 + ".enc", key, iv);
                Archive.archive(name2 + ".enc", name2 + ".zip");
            }
            else if (CheckBox6.IsChecked == true)
            {
                Archive.archive(TextBox3.Text, name2 + ".zip");
                Encrypt.EncryptFile(name2 + ".zip", name2 + ".enc", key, iv);
            }
            else if (Checkbox7.IsChecked == true)
            {
                Archive.archive(TextBox3.Text, name2 + ".zip");
            }
            else if (Checkbox8.IsChecked == true)
            {
                Encrypt.EncryptFile(TextBox3.Text, name2 + ".enc", key, iv);
            }

        }
    }
}

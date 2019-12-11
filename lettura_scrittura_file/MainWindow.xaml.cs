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
using System.IO;

namespace lettura_scrittura_file
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Random random = new Random();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int n = int.Parse(txtNumero.Text);
                if (n <= 0)
                    throw new Exception("Il numero deve essere maggiore di 0!");
                int[] array = new int[n];
                for (int i = 0; i < array.Length; i++)
                    array[i] = random.Next(1, 101);
                lblArray.Content = "[";
                for (int i = 0; i < array.Length; i++)
                {
                    lblArray.Content += $"{array[i]}";
                    if (i < array.Length - 1)
                        lblArray.Content += ",";
                }
                lblArray.Content += "]";
                string file = "stato.txt";
                StreamWriter MyStreamWriter = new StreamWriter(file);
                for (int i = 0; i < array.Length; i++)
                {
                    MyStreamWriter.WriteLine(array[i] + " ");
                }
                MyStreamWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

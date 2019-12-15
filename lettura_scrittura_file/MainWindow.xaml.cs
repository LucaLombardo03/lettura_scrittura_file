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
        private const int max_array_size = 10000;
        private const int max_array_value = 1000;
        private const string file = "array.txt";
        private readonly Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
            LoadArray();
        }

        private void txtNumero_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = txtNumero.Text;
            try
            {
                int n = int.Parse(text);
                if (n <= 0 || n > max_array_size)
                    throw new Exception();
                txtNumero.Foreground = Brushes.Black;
                btnGenera.IsEnabled = true;
                txtStatus.Text = "";
            }catch
            {
                txtNumero.Foreground = Brushes.Red;
                btnGenera.IsEnabled = false;
                txtStatus.Text = "Numero non valido";
            }
        }
        private void btnGenera_Click(object sender, RoutedEventArgs e)
        {
            int dim = int.Parse(txtNumero.Text);
            int[] array = new int[dim];
            for (int i = 0; i < dim; i++)
                array[i] = random.Next(1, max_array_value);
            ShowArray(array);
            SaveArray(array);
        }
        private void ShowArray(int[] array)
        {
            string s = "[";
            for (int i = 0; i < array.Length; i++)
            {
                s += array[i];
                if (i < array.Length - 1)
                    s = ", ";
            }
            s += "]";
            txtOutput.Text = s;
        }
        private void LoadArray()
        {
            if (File.Exists(file))
                try
                {
                    using (StreamReader r = new StreamReader(file, Encoding.UTF8))
                    {
                        int dim = int.Parse(r.ReadLine());
                        int[] array = new int[dim];
                        string line;
                        int i = 0;
                        while ((line = r.ReadLine()) != null)
                            array[i++] = int.Parse(line);
                        ShowArray(array);
                        txtStatus.Text = $"Caricato array di {dim} elementi da file";
                    }
                }
                catch { }
        }
        private void SaveArray(int[] array)
        {
            try
            {
                using (StreamWriter w = new StreamWriter(file, false, Encoding.UTF8))
                {
                    w.WriteLine(array.Length);
                    for (int i = 0; i < array.Length; i++)
                        w.WriteLine(array[i]);
                    w.Flush();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(file))
                File.Delete(file);
            txtNumero.Text = "";
            txtStatus.Text = "";
            txtOutput.Text = "";
        }
    }
}

using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using SarDecrypt.FileFormat;
//using SarDecrypt.Compression;

namespace SarDecrypt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private SarFile sarFile;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void openSar(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName == "") return;

            try
            {
                sarFile = new SarFile(File.ReadAllBytes(openFileDialog.FileName));
                statusBox.Text = "File opened";
            }
            catch
            {
                statusBox.Text = "Error attempting to open";
                return;
            }
        }

        private void decryptSar(object sender, RoutedEventArgs e)
        {
            if (sarFile != null)
            {
                sarFile.Decrypt();
                statusBox.Text = "Decrypted file";
            }
        }

        private void saveSar(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            Nullable<bool> result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                if (sarFile != null)
                {
                    try
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, sarFile.data);
                        statusBox.Text = "Saved file";
                    }
                    catch
                    {
                        statusBox.Text = "Error attempting to save";
                        return;
                    }
                }
                else
                {
                    statusBox.Text = "No file loaded";
                }

            }
        }
    }
}

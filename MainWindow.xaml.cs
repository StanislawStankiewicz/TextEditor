using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? fileName;

        public MainWindow()
        {
            InitializeComponent();
            UpdateTitle(); // Call this method to set the initial title
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnToggleMaximized_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    fileName = openFileDialog.FileName;
                    string text = File.ReadAllText(fileName);
                    txtBox.Text = text;
                    UpdateTitle(); // Call this method to update the title after opening a file
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading the file: " + ex.Message);
                }
            }
        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    File.WriteAllText(fileName, txtBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving the file: " + ex.Message);
                }
            }
            else
            {
                menuSaveAs_Click(sender, e);
            }
        }

        private void menuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    fileName = saveFileDialog.FileName;
                    File.WriteAllText(fileName, txtBox.Text);
                    UpdateTitle(); // Call this method to update the title after saving a file
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving the file: " + ex.Message);
                }
            }
        }

        private void UpdateTitle()
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string filename = Path.GetFileName(fileName);
                textBlockTitle.Text = filename + " - stahu's TextEditor";
            }
            else
            {
                textBlockTitle.Text = "Untitled - stahu's TextEditor";
            }
        }
    }
}
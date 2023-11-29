using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Word = Microsoft.Office.Interop.Word;

namespace WpfApp1
{
    public partial class MainWindow : System.Windows.Window
    {
        private List<string> successfulPatterns = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private List<string> loadedFilePaths = new List<string>();

        private void ReadTextFromDoc(string filePath)
        {
            Word.Application wordApp = new Word.Application();
            Word.Document doc = wordApp.Documents.Open(filePath);

            string text = doc.Content.Text;

            // Clear existing content in the richTextBoxOutput
            richTextBoxOutput.Document.Blocks.Clear();

            // Add the text to the richTextBoxOutput
            richTextBoxOutput.Document.Blocks.Add(new Paragraph(new Run(text)));

            doc.Close();
            wordApp.Quit();
        }


        private void ReadTextFromFile(string filePath)
        {
            try
            {
                string fileExtension = System.IO.Path.GetExtension(filePath);

                if (fileExtension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                {
                    ReadTextFromDocx(filePath);
                    loadedFilePaths.Add(filePath);
                }
                else if (fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    ReadTextFromTxt(filePath);
                    loadedFilePaths.Add(filePath);
                }
                else if (fileExtension.Equals(".doc", StringComparison.OrdinalIgnoreCase))
                {
                    ReadTextFromDoc(filePath);
                    loadedFilePaths.Add(filePath);
                }
                else
                {
                    MessageBox.Show("Unsupported file format");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void OpenSelectedFile()
        {
            if (comboBoxFiles.SelectedItem != null)
            {
                string selectedFilePath = comboBoxFiles.SelectedItem.ToString();

                // Check if the file is already open
                if (loadedFilePaths.Contains(selectedFilePath))
                {
                    MessageBox.Show("File is already open. Switching not required.");
                    return;
                }

                ReadTextFromFile(selectedFilePath);

                // Add the selected file path to the list
                loadedFilePaths.Add(selectedFilePath);
            }
        }

        private void ButtonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectedFile();
        }

        private void ReadTextFromDocx(string filePath)
        {
            Word.Application wordApp = new Word.Application();
            Word.Document doc = wordApp.Documents.Open(filePath);

            string text = doc.Content.Text;

            // Clear existing content in the richTextBoxOutput
            richTextBoxOutput.Document.Blocks.Clear();

            // Add the text to the richTextBoxOutput
            richTextBoxOutput.Document.Blocks.Add(new Paragraph(new Run(text)));

            doc.Close();
            wordApp.Quit();
        }

        private void ReadTextFromTxt(string filePath)
        {
            string text = File.ReadAllText(filePath);

            // Clear existing content in the richTextBoxOutput
            richTextBoxOutput.Document.Blocks.Clear();

            // Add the text to the richTextBoxOutput
            richTextBoxOutput.Document.Blocks.Add(new Paragraph(new Run(text)));
        }

        private void SearchPatternInText(string pattern)
        {
            string text = new TextRange(richTextBoxOutput.Document.ContentStart, richTextBoxOutput.Document.ContentEnd).Text;

            int index = text.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) + 2;

            if (index != -1)
            {
                // Clear previous formatting
                richTextBoxOutput.Selection.ClearAllProperties();

                // Find and highlight the pattern in richTextBoxOutput
                TextPointer start = richTextBoxOutput.Document.ContentStart.GetPositionAtOffset(index);
                TextPointer end = richTextBoxOutput.Document.ContentStart.GetPositionAtOffset(index + pattern.Length);

                richTextBoxOutput.Selection.Select(start, end);
                richTextBoxOutput.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);

                // Scroll the RichTextBox to the position of the found pattern
                richTextBoxOutput.ScrollToVerticalOffset(start.GetCharacterRect(LogicalDirection.Forward).Top);

                // Add the pattern to the list of successful patterns with date and time
                string patternWithDate = $"{pattern} - {DateTime.Now}";
                successfulPatterns.Add(patternWithDate);

                // Update the list box displaying successful patterns
                UpdatePatternListBox();
            }
            else
            {
                MessageBox.Show($"Pattern '{pattern}' not found in the text.");
            }
        }

        private void UpdatePatternListBox()
        {
            listBoxSuccessfulPatterns.ItemsSource = null; // Clear the existing items
            listBoxSuccessfulPatterns.ItemsSource = successfulPatterns; // Set the new items
        }

        private string currentlyOpenFilePath = "";

        private void ButtonReadFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|Word Documents (*.doc;*.docx)|*.doc;*.docx|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                // Перевірте, чи вже відкритий файл
                if (selectedFilePath.Equals(currentlyOpenFilePath, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Файл вже відкритий. Переключатись не потрібно.");
                    return;
                }

                ReadTextFromFile(selectedFilePath);

                // Збережіть шлях поточного відкритого файлу
                currentlyOpenFilePath = selectedFilePath;

                // Отримайте інформацію про файл
                FileInfo fileInfo = new FileInfo(selectedFilePath);
                string fileDetails = $"{fileInfo.Name} - {fileInfo.LastWriteTime} - {fileInfo.Length / 1024} KB";

                // Додайте деталі відкритого файлу до ComboBox, якщо їх ще немає
                if (!comboBoxFiles.Items.Contains(fileDetails))
                {
                    comboBoxFiles.Items.Add(fileDetails);
                }
            }
        }

        private void ComboBoxFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Очистіть currentlyOpenFilePath, оскільки файл не відкритий через ComboBox
            currentlyOpenFilePath = "";

            // Завантажте вміст вибраного файлу до RichTextBox
            if (comboBoxFiles.SelectedItem != null)
            {
                string selectedFilePath = comboBoxFiles.SelectedItem.ToString();
                ReadTextFromFile(selectedFilePath);

                // Збережіть шлях поточного відкритого файлу
                currentlyOpenFilePath = selectedFilePath;
            }
        }

        private string FormatFileSize(long sizeInBytes)
        {
            string[] sizeSuffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

            int index = 0;
            double size = sizeInBytes;

            while (size >= 1024 && index < sizeSuffixes.Length - 1)
            {
                size /= 1024;
                index++;
            }

            return $"{size:N1} {sizeSuffixes[index]}";
        }

        private void ButtonSearchPattern_Click(object sender, RoutedEventArgs e)
        {
            string pattern = PatternTextBox.Text;
            SearchPatternInText(pattern);
        }

        private void ButtonPickFragment_Click(object sender, RoutedEventArgs e)
        {
            TextRange selectedTextRange = new TextRange(richTextBoxOutput.Selection.Start, richTextBoxOutput.Selection.End);

            if (!string.IsNullOrEmpty(selectedTextRange.Text))
            {
                string selectedFragment = selectedTextRange.Text;

                // Check if the fragment is not already in the successful patterns list
                if (!successfulPatterns.Contains(selectedFragment))
                {
                    // Add the selected fragment to the list of successful patterns with date and time
                    string fragmentWithDate = $"{selectedFragment} - {DateTime.Now}";
                    successfulPatterns.Add(fragmentWithDate);

                    // Update the list box displaying successful patterns
                    UpdatePatternListBox();

                    MessageBox.Show($"Fragment '{selectedFragment}' added to the list of patterns.");
                }
                else
                {
                    MessageBox.Show($"Fragment '{selectedFragment}' is already in the list of patterns.");
                }
            }
            else
            {
                MessageBox.Show("No text selected. Please select a fragment to add to the list of patterns.");
            }
        }

        private void ButtonDeletePattern_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxSuccessfulPatterns.SelectedItem != null)
            {
                string selectedPattern = listBoxSuccessfulPatterns.SelectedItem.ToString();

                MessageBoxResult result = MessageBox.Show($"Do you want to delete the pattern '{selectedPattern}'?", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    // Remove the selected pattern from the list
                    successfulPatterns.Remove(selectedPattern);

                    // Update the list box displaying successful patterns
                    UpdatePatternListBox();

                    MessageBox.Show($"Pattern '{selectedPattern}' deleted from the list.");
                }
            }
            else
            {
                MessageBox.Show("Please select a pattern to delete.");
            }
        }

        private void ListBoxSuccessfulPatterns_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBoxSuccessfulPatterns.SelectedItem != null)
            {
                string selectedPatternWithDate = listBoxSuccessfulPatterns.SelectedItem.ToString();

                // Extract the pattern without the date
                string selectedPattern = selectedPatternWithDate.Split(new[] { " - " }, StringSplitOptions.None)[0];

                // Remove existing highlighting
                richTextBoxOutput.Selection.ClearAllProperties();

                // Find and highlight the selected pattern in the text
                TextPointer textPointer = richTextBoxOutput.Document.ContentStart;

                while (textPointer != null)
                {
                    string textRun = textPointer.GetTextInRun(LogicalDirection.Forward);

                    int index = textRun.IndexOf(selectedPattern, StringComparison.OrdinalIgnoreCase);

                    if (index >= 0)
                    {
                        TextPointer start = textPointer.GetPositionAtOffset(index);
                        TextPointer end = start.GetPositionAtOffset(selectedPattern.Length);

                        richTextBoxOutput.Selection.Select(start, end);
                        richTextBoxOutput.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);

                        // Scroll the RichTextBox to the position of the found pattern
                        richTextBoxOutput.ScrollToVerticalOffset(start.GetCharacterRect(LogicalDirection.Forward).Top);

                        // Stop highlighting after the first occurrence
                        break;
                    }

                    textPointer = textPointer.GetNextContextPosition(LogicalDirection.Forward);
                }
            }
        }
    }
}

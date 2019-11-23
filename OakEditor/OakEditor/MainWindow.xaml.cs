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
using System.Windows.Forms;
using System.IO;

namespace OakEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path = "";
        public MainWindow()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            txtField.Focus();


        }

        private void OpenMI_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtField.Text = File.ReadAllText(dialog.FileName);
                path = dialog.FileName;
            }
        }

        private void SAveMI_Click(object sender, RoutedEventArgs e)
        {
            if(path == "")
            {
                SaveAsMI_Click(this, null);
            }
            else
            {
                File.WriteAllText(path, txtField.Text);
            }
        }

        private void SaveAsMI_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(dialog.FileName, txtField.Text);
                path = dialog.FileName;
            }
        }

        private void ExitMI_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void UndoMI_Click(object sender, RoutedEventArgs e)
        {
            txtField.Undo();
        }

        private void RedoMI_Click(object sender, RoutedEventArgs e)
        {
            txtField.Redo();
        }

        private void CutMI_Click(object sender, RoutedEventArgs e)
        {
            txtField.Cut();
        }

        private void CopyMI_Click(object sender, RoutedEventArgs e)
        {
            txtField.Copy();
        }

        private void PasteMI_Click(object sender, RoutedEventArgs e)
        {
            txtField.Paste();
        }

        private void DeleteMI_Click(object sender, RoutedEventArgs e)
        {
            string text = txtField.Text;
            int start = txtField.SelectionStart;
            int end = start + txtField.SelectionLength;
            txtField.Text = text.Substring(0, start) + text.Substring(end, text.Length - end);
        }

        private void SelectAllMI_Click(object sender, RoutedEventArgs e)
        {
            txtField.SelectAll();
        }

        private void TimeAndDateMI_Click(object sender, RoutedEventArgs e)
        {
            int caret = txtField.CaretIndex;
            string dt = DateTime.Now.ToString();
            txtField.Text = txtField.Text.Insert(caret, dt);
            txtField.CaretIndex = caret + dt.Length;

        }

        private void FontMI_Click(object sender, RoutedEventArgs e)
        {
            FontDialog dialog = new FontDialog();
            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var font = dialog.Font;
                txtField.FontFamily = new FontFamily(font.FontFamily.Name);
                txtField.FontSize = font.Size;
                FontStyleConverter conv = new FontStyleConverter();
                switch(font.Style)
                {
                    case System.Drawing.FontStyle.Regular:
                        txtField.FontStyle = FontStyles.Normal;
                        break;
                    case System.Drawing.FontStyle.Bold:
                        txtField.FontStyle = FontStyles.Oblique;
                        break;
                    case System.Drawing.FontStyle.Italic:
                        txtField.FontStyle = FontStyles.Italic;
                        break;
                    default:
                        txtField.FontStyle = FontStyles.Normal;
                        break;
                }
                if (font.Underline)
                    txtField.TextDecorations.Add(TextDecorations.Underline);
                if (font.Strikeout)
                    txtField.TextDecorations.Add(TextDecorations.Strikethrough);

            }
        }

        private void CarryOverMI_Checked(object sender, RoutedEventArgs e)
        {
            txtField.TextWrapping = TextWrapping.WrapWithOverflow;
        }

        private void CarryOverMI_Unchecked(object sender, RoutedEventArgs e)
        {
            txtField.TextWrapping = TextWrapping.NoWrap;
        }

        private static byte Reverse(int v) => (byte)(256 - v - 1);
        private void SelectColorMI_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var color = dialog.Color;
                txtField.Foreground = new SolidColorBrush(new Color() { R = color.R, G = color.G, B = color.B, A = 255});

                var grad = new LinearGradientBrush();
                txtField.Background = grad;
                grad.StartPoint = new Point(0, 0);
                grad.EndPoint = new Point(0, 1);
                grad.GradientStops.Add(new GradientStop(Colors.White, offset: 0));
                grad.GradientStops.Add(new GradientStop(new Color() { A = 255, R = Reverse(color.R), G = Reverse(color.G), B = Reverse(color.B) }, offset: 3));
            }
        }
        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Normal;
            DragMove();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.ShowDialog();
        }
    }
}

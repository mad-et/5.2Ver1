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
using Microsoft.Win32;
using System.IO;
using System.Windows.Ink;

namespace WpfApp5._2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            inkcanvas.DefaultDrawingAttributes.Color = Colors.Black;
            inkcanvas.DefaultDrawingAttributes.Width = 20;
            inkcanvas.DefaultDrawingAttributes.Height = 20;
            inkcanvas.Background = new SolidColorBrush(Colors.White);

            int Type1 = 1; black.Tag = Type1;
            int Type2 = 2; red.Tag = Type2;
            int Type3 = 3; blue.Tag = Type3;
            int Type4 = 4; green.Tag = Type4;
        }
        private void Thin1(object sender, RoutedEventArgs e)
        {
            inkcanvas.DefaultDrawingAttributes.Width = 5;
            inkcanvas.DefaultDrawingAttributes.Height = 5;
        }
        private void Thin2(object sender, RoutedEventArgs e)
        {
            inkcanvas.DefaultDrawingAttributes.Width = 10;
            inkcanvas.DefaultDrawingAttributes.Height = 10;
        }
        private void Thin3(object sender, RoutedEventArgs e)
        {
            inkcanvas.DefaultDrawingAttributes.Width = 15;
            inkcanvas.DefaultDrawingAttributes.Height = 15;
        }
        private void Thin4(object sender, RoutedEventArgs e)
        {
            inkcanvas.DefaultDrawingAttributes.Width = 20;
            inkcanvas.DefaultDrawingAttributes.Height = 20;
        }
        private void Eraser(object sender, RoutedEventArgs e)
        {
            if (eraser.IsChecked == true)
            {
                inkcanvas.Cursor = Cursors.Hand;
                inkcanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                inkcanvas.EraserShape = new EllipseStylusShape(20, 20);
            }
            else
            {
                inkcanvas.EditingMode = InkCanvasEditingMode.Ink;
            }
        }
        private void Select(object sender, RoutedEventArgs e)
        {
            if (select.IsChecked == true)
            {
                inkcanvas.Cursor = Cursors.Hand;
                inkcanvas.EditingMode = InkCanvasEditingMode.Select;
                inkcanvas.EraserShape = new EllipseStylusShape(20, 20);
            }
            else
            {
                inkcanvas.EditingMode = InkCanvasEditingMode.Ink;
            }
        }
        private void Color(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            switch ((int)B.Tag)
            {
                case 1: inkcanvas.DefaultDrawingAttributes.Color = Colors.Black; break;
                case 2: inkcanvas.DefaultDrawingAttributes.Color = Colors.Red; break;
                case 3: inkcanvas.DefaultDrawingAttributes.Color = Colors.Blue; break;
                case 4: inkcanvas.DefaultDrawingAttributes.Color = Colors.Green; break;
            }
        }
        private void Print(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(inkcanvas, "Распечатываем элемент InkCanvas");
            }
        }
        private void Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Inkcanvas Format (*.sandy)|*.sandy|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                var fs = new FileStream(dlg.FileName, FileMode.OpenOrCreate);
                StrokeCollection strokes = new StrokeCollection(fs);
                inkcanvas.Strokes = strokes;
            }
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "InkCanvas Format (*.sandy)|*.sandy|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fs = File.Open(dlg.FileName, FileMode.OpenOrCreate);
                inkcanvas.Strokes.Save(fs);
                fs.Close();
            }
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
    }
}

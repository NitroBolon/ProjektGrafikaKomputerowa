using GraphicEditor.Objects;
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

namespace GraphicEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EditorInstance editor;
        public MainWindow()
        {
            InitializeComponent();
            editor = new EditorInstance(Canva);
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Undo";
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Redo";
        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Przemieść";
        }

        private void Resize_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Zmień rozmiar";
        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Okrąg (ręcznie)";

            editor.AddCircle(new Objects.Point(50,50),50);
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Prostokąt (ręcznie)";

            editor.AddRectangle(new Objects.Point(100, 100), new Objects.Point(200,200), new Objects.Point(100,100));
        }

        private void Line_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Linia (ręcznie)";

            editor.AddLine(new Objects.Point(75,75), new Objects.Point(150,150));
        }

        private void Rectangle_Menu_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Prostokąt (programowo)";
        }

        private void Circle_Menu_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Okrąg (programowo)";
        }

        private void Line_Menu_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Line (programowo)";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}

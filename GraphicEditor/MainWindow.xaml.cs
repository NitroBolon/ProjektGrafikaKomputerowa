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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "-";
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "-";
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
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Prostokąt (ręcznie)";
        }

        private void Line_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Linia (ręcznie)";
        }

        private void Rectangle_Menu_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "-";
        }

        private void Circle_Menu_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "-";
        }

        private void Line_Menu_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "-";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}

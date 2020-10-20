using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy DrawingParameters.xaml
    /// </summary>
    public partial class DrawingParameters : Window
    {
        int maxW, maxH, sx1, sy1, sx2, sy2;

        public DrawingParameters(double canvaWidth, double canvaHeight)
        {
            InitializeComponent();
            dx1b.Content = "<<<"; dx1.Content = "<"; x1.Content = "0"; ux1.Content = ">"; ux1b.Content = ">>>";
            dx2b.Content = "<<<"; dx2.Content = "<"; x2.Content = "0"; ux2.Content = ">"; ux2b.Content = ">>>";
            dy1b.Content = "<<<"; dy1.Content = "<"; y1.Content = "0"; uy1.Content = ">"; uy1b.Content = ">>>";
            dy2b.Content = "<<<"; dy2.Content = "<"; y2.Content = "0"; uy2.Content = ">"; uy2b.Content = ">>>";
            maxW = (int)canvaWidth; maxH = (int)canvaHeight; sx1 = 0; sy1 = 0; sx2 = 0; sy2 = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DrawingParameters.GetWindow(this).IsInitialized)
                if (lista.SelectedIndex == 0)
                {
                    Tips.Content = "Wybierz rodzaj figury by rozpocząć konfigurację";
                    group.IsEnabled = false;
                }
                else if (lista.SelectedIndex == 1)
                {
                    Tips.Content = "Podaj współrzędne punktów ograniczających odcinek A(x1, y1)\nB(x2, y2)";
                    group.IsEnabled = true;
                }
                else if (lista.SelectedIndex == 2)
                {
                    Tips.Content = "Podaj współrzędne punktów środka okręgu oraz przykładowego\npunktu leżącego na okręgu O(x1, y1) R(x2, y2)";
                    group.IsEnabled = true;
                }
                else if (lista.SelectedIndex == 3)
                {
                    Tips.Content = "Podaj współrzędne punktów leżących na jednej z przekątnych\nprostokątu A(x1, y1) C(x2, y2)";
                    group.IsEnabled = true;
                }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["type"] = lista.SelectedIndex;
            Application.Current.Properties["sx1"] = sx1;
            Application.Current.Properties["sy1"] = sy1;
            Application.Current.Properties["sx2"] = sx2;
            Application.Current.Properties["sy2"] = sy2;
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["type"] = null;
            this.DialogResult = true;
        }

        private void DownX1_Click(object sender, RoutedEventArgs e)
        {
            if (sx1 > 0) sx1--;
            x1.Content = sx1.ToString();
        }
        private void UpX1_Click(object sender, RoutedEventArgs e)
        {
            if (sx1 < maxW) sx1++;
            x1.Content = sx1.ToString();
        }
        private void DownY1_Click(object sender, RoutedEventArgs e)
        {
            if (sy1 > 0) sy1--;
            y1.Content = sy1.ToString();
        }
        private void UpY1_Click(object sender, RoutedEventArgs e)
        {
            if (sy1 < maxH) sy1++;
            y1.Content = sy1.ToString();
        }
        private void DownX2_Click(object sender, RoutedEventArgs e)
        {
            if (sx2 > 0) sx2--;
            x2.Content = sx2.ToString();
        }
        private void UpX2_Click(object sender, RoutedEventArgs e)
        {
            if (sx2 < maxW) sx2++;
            x2.Content = sx2.ToString();
        }
        private void DownY2_Click(object sender, RoutedEventArgs e)
        {
            if (sy2 > 0) sy2--;
            y2.Content = sy2.ToString();
        }
        private void UpY2_Click(object sender, RoutedEventArgs e)
        {
            if (sy2 < maxH) sy2++;
            y2.Content = sy2.ToString();
        }


        private void DownX1b_Click(object sender, RoutedEventArgs e)
        {
            if (sx1 > 10) sx1-=10;
            x1.Content = sx1.ToString();
        }
        private void UpX1b_Click(object sender, RoutedEventArgs e)
        {
            if (sx1 < maxW-10) sx1+=10;
            x1.Content = sx1.ToString();
        }
        private void DownY1b_Click(object sender, RoutedEventArgs e)
        {
            if (sy1 > 10) sy1-=10;
            y1.Content = sy1.ToString();
        }
        private void UpY1b_Click(object sender, RoutedEventArgs e)
        {
            if (sy1 < maxH-10) sy1+=10;
            y1.Content = sy1.ToString();
        }
        private void DownX2b_Click(object sender, RoutedEventArgs e)
        {
            if (sx2 > 10) sx2-=10;
            x2.Content = sx2.ToString();
        }
        private void UpX2b_Click(object sender, RoutedEventArgs e)
        {
            if (sx2 < maxW-10) sx2+=10;
            x2.Content = sx2.ToString();
        }
        private void DownY2b_Click(object sender, RoutedEventArgs e)
        {
            if (sy2 > 10) sy2-=10;
            y2.Content = sy2.ToString();
        }
        private void UpY2b_Click(object sender, RoutedEventArgs e)
        {
            if (sy2 < maxH-10) sy2+=10;
            y2.Content = sy2.ToString();
        }
    }
}

using System.Windows;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy MoveObject.xaml
    /// </summary>
    public partial class MoveObject : Window
    {
        int sx=0, sy=0;
        public MoveObject()
        {
            InitializeComponent();
            dxb.Content = "<<<"; dx.Content = "<"; x.Content = "0"; ux.Content = ">"; uxb.Content = ">>>";
            dyb.Content = "<<<"; dy.Content = "<"; y.Content = "0"; uy.Content = ">"; uyb.Content = ">>>";
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["x"] = sx;
            Application.Current.Properties["y"] = sy;
            this.DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["x"] = 0;
            Application.Current.Properties["y"] = 0;
            this.DialogResult = true;
        }

        private void DownX_Click(object sender, RoutedEventArgs e)
        {
            sx--;
            x.Content = sx.ToString();
        }
        private void UpX_Click(object sender, RoutedEventArgs e)
        {
            sx++;
            x.Content = sx.ToString();
        }
        private void DownY_Click(object sender, RoutedEventArgs e)
        {
            sy--;
            y.Content = sy.ToString();
        }
        private void UpY_Click(object sender, RoutedEventArgs e)
        {
            sy++;
            y.Content = sy.ToString();
        }
        
        private void DownXb_Click(object sender, RoutedEventArgs e)
        {
            sx -= 10;
            x.Content = sx.ToString();
        }
        private void UpXb_Click(object sender, RoutedEventArgs e)
        {
            sx += 10;
            x.Content = sx.ToString();
        }
        private void DownYb_Click(object sender, RoutedEventArgs e)
        {
            sy -= 10;
            y.Content = sy.ToString();
        }
        private void UpYb_Click(object sender, RoutedEventArgs e)
        {
            sy += 10;
            y.Content = sy.ToString();
        }
    }
}

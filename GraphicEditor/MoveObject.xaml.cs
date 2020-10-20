using System.Windows;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy MoveObject.xaml
    /// </summary>
    public partial class MoveObject : Window
    {
        int sx=0, sy=0, xsx=0, ysy=0;
        public MoveObject()
        {
            InitializeComponent();
            xdxb.Content = "<<<"; xdx.Content = "<"; xx.Content = "0"; xux.Content = ">"; xuxb.Content = ">>>";
            ydyb.Content = "<<<"; ydy.Content = "<"; yy.Content = "0"; yuy.Content = ">"; yuyb.Content = ">>>";
        }

        private void OK_Click2(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["xx"] = xsx.ToString();
            Application.Current.Properties["yy"] = ysy.ToString();
            this.DialogResult = true;
        }
        private void Cancel_Click2(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["xx"] = (0).ToString();
            Application.Current.Properties["yy"] = (0).ToString();
            this.DialogResult = true;
        }

        private void DownxX_Click(object sender, RoutedEventArgs e)
        {
            xsx--;
            xx.Content = xsx.ToString();
        }
        private void UpxX_Click(object sender, RoutedEventArgs e)
        {
            xsx++;
            xx.Content = xsx.ToString();
        }
        private void DownyY_Click(object sender, RoutedEventArgs e)
        {
            ysy--;
            yy.Content = ysy.ToString();
        }
        private void UpyY_Click(object sender, RoutedEventArgs e)
        {
            ysy++;
            yy.Content = ysy.ToString();
        }

        private void DownxXb_Click(object sender, RoutedEventArgs e)
        {
            xsx -= 10;
            xx.Content = xsx.ToString();
        }
        private void UpxXb_Click(object sender, RoutedEventArgs e)
        {
            xsx += 10;
            xx.Content = xsx.ToString();
        }
        private void DownyYb_Click(object sender, RoutedEventArgs e)
        {
            ysy -= 10;
            yy.Content = ysy.ToString();
        }
        private void UpyYb_Click(object sender, RoutedEventArgs e)
        {
            ysy += 10;
            yy.Content = ysy.ToString();
        }
    }
}

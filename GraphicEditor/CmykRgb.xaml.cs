using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy CmykRgb.xaml
    /// </summary>
    public partial class CmykRgb : Window
    {
        double C = 0.0, M = 0.0, Y = 0.0, K = 0.0, t2;
        byte R = 255, G = 255, B = 255, t1;

        Color color;
        SolidColorBrush brush;

        public CmykRgb()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void rInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (rInput != null && rSlider != null && Byte.TryParse(rInput.Text, out t1))
            {
                if (Convert.ToInt32(rInput.Text) > 255 || Convert.ToInt32(rInput.Text) < 1)
                {
                    rInput.Text = "255";
                    rSlider.Value = 255;
                    R = 255;
                }
                else
                {
                    R = Convert.ToByte(rInput.Text);
                    rSlider.Value = R;
                }
                RgbToCmyk();
                UpdateCmyk();
            }
        }
        private void gInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (gInput != null && gSlider != null && Byte.TryParse(gInput.Text, out t1))
            {
                if (Convert.ToInt32(gInput.Text) > 255 || Convert.ToInt32(gInput.Text) < 1)
                {
                    gInput.Text = "255";
                    gSlider.Value = 255;
                    G = 255;
                }
                else
                {
                    G = Convert.ToByte(gInput.Text);
                    gSlider.Value = G;
                }
                RgbToCmyk();
                UpdateCmyk();
            }
        }
        private void bInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (bInput != null && bSlider != null && Byte.TryParse(bInput.Text, out t1))
            {
                if (Convert.ToInt32(bInput.Text) > 255 || Convert.ToInt32(bInput.Text) < 1)
                {
                    bInput.Text = "255";
                    bSlider.Value = 255;
                    B = 255;
                }
                else
                {
                    B = Convert.ToByte(bInput.Text);
                    bSlider.Value = B;
                }
                RgbToCmyk();
                UpdateCmyk();
            }
        }

        private void cInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                C = Convert.ToDouble(cInput.Text);
                cSlider.Value = (int)(C * 100);

                CmykToRgb();
                UpdateRgb();
            }
            catch { }
        }
        private void mInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                M = Convert.ToDouble(mInput.Text);
                mSlider.Value = (int)(M * 100);

                CmykToRgb();
                UpdateRgb();
            }
            catch { }
        }
        private void yInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                Y = Convert.ToDouble(yInput.Text);
                ySlider.Value = (int)(Y * 100);

                CmykToRgb();
                UpdateRgb();
            }
            catch { }
        }
        private void kInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                Y = Convert.ToDouble(yInput.Text);
                ySlider.Value = (int)(Y * 100);

                CmykToRgb();
                UpdateRgb();
            }
            catch { }
        }


        private void rSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (rInput != null && rSlider != null)
            {
                R = Convert.ToByte(rSlider.Value);
                rInput.Text = Convert.ToInt32(R).ToString();

                RgbToCmyk();
                UpdateCmyk();
            }
        }
        private void gSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (gInput != null && gSlider != null)
            {
                G = Convert.ToByte(gSlider.Value);
                gInput.Text = Convert.ToInt32(G).ToString();

                RgbToCmyk();
                UpdateCmyk();
            }
        }
        private void bSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (bInput != null && bSlider != null)
            {
                B = Convert.ToByte(bSlider.Value);
                bInput.Text = Convert.ToInt32(B).ToString();

                RgbToCmyk();
                UpdateCmyk();
            }
        }

        private void cSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (cInput != null && cSlider != null)
            {
                //C = Convert.ToDouble(cSlider.Value);
                //cInput.Text = C.ToString();

                //CmykToRgb();
                //UpdateRgb();
            }
        }
        private void mSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mInput != null && mSlider != null)
            {
                //M = Convert.ToDouble(mSlider.Value);
                //mInput.Text = M.ToString();

                //CmykToRgb();
                //UpdateRgb();
            }
        }
        private void ySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (yInput != null && ySlider != null)
            {
                //Y = Convert.ToDouble(ySlider.Value);
                //yInput.Text = Y.ToString();

                //CmykToRgb();
                //UpdateRgb();
            }
        }
        private void kSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (kInput != null && kSlider != null)
            {
                //K = Convert.ToDouble(kSlider.Value);
                //kInput.Text = K.ToString();

                //CmykToRgb();
                //UpdateRgb();
            }
        }

        private void UpdateRgb()
        {
            if (rInput != null && gInput != null && bInput != null && rSlider != null && gSlider != null && bSlider != null && colorView != null)
            {
                rInput.Text = R.ToString();
                gInput.Text = G.ToString();
                bInput.Text = B.ToString();
                rSlider.Value = (int)R;
                gSlider.Value = (int)G;
                bSlider.Value = (int)B;
                color = Color.FromArgb((byte)255, R, G, B);
                colorView.Background = brush;
            }
        }

        private void UpdateCmyk()
        {
            if (cInput != null && mInput != null && yInput != null && kInput != null && cSlider != null && mSlider != null && ySlider != null && kSlider != null && colorView != null)
            {
                cInput.Text = C.ToString();
                mInput.Text = M.ToString();
                yInput.Text = Y.ToString();
                kInput.Text = K.ToString();
                cSlider.Value = C;
                mSlider.Value = M;
                ySlider.Value = Y;
                kSlider.Value = K;
                color = Color.FromArgb((byte)255, R, G, B);
                colorView.Background = brush;
            }
        }

        public void CmykToRgb()
        {
            R = Convert.ToByte(255 * (1 - C) * (1 - K));
            G = Convert.ToByte(255 * (1 - M) * (1 - K));
            B = Convert.ToByte(255 * (1 - Y) * (1 - K));

            color = Color.FromArgb((byte)255, R, G, B);
            brush = new SolidColorBrush(color);
        }

        private void RgbToCmyk()
        {
            double dr = (double)R / 255;
            double dg = (double)G / 255;
            double db = (double)B / 255;
            K = 1 - Math.Max(Math.Max(dr, dg), db);
            C = (1 - dr - K) / (1 - K);
            M = (1 - dg - K) / (1 - K);
            Y = (1 - db - K) / (1 - K);

            color = Color.FromArgb((byte)255, (byte)R, (byte)G, (byte)B);
            brush = new SolidColorBrush(color);
        }
    }
}
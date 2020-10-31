using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy CmykRgb.xaml
    /// </summary>
    public partial class CmykRgb : Window
    {
        int C = 0, M = 0, Y = 0, K = 0, R = 255, G = 255, B = 255;

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
            if (rSlider != null && rInput != null)
                if (Convert.ToInt32(rInput.Text) > 255)
                {
                    rInput.Text = "255";
                    rSlider.Value = 255;
                    R = 255;
                }
                else
                {
                    rSlider.Value = Convert.ToInt32(rInput.Text);
                    R = Convert.ToInt32(rInput.Text);
                }
            RgbToCmyk();
        }

        private void gInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (gSlider != null && gInput != null)
                if (Convert.ToInt32(gInput.Text) > 255)
                {
                    gInput.Text = "255";
                    gSlider.Value = 255;
                    G = 255;
                }
                else
                {
                    gSlider.Value = Convert.ToInt32(gInput.Text);
                    G = Convert.ToInt32(gInput.Text);
                }
            RgbToCmyk();
        }

        private void bInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (bSlider != null && bInput != null)
                if (Convert.ToInt32(bInput.Text) > 255)
                {
                    bInput.Text = "255";
                    bSlider.Value = 255;
                    B = 255;
                }
                else
                {
                    bSlider.Value = Convert.ToInt32(bInput.Text);
                    B = Convert.ToInt32(bInput.Text);
                }
            RgbToCmyk();
        }

        private void cInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (cSlider != null && cInput != null)
                if (Convert.ToInt32(cInput.Text) > 100)
                {
                    cInput.Text = "100";
                    cSlider.Value = 100;
                    C = 100;
                }
                else
                {
                    cSlider.Value = Convert.ToInt32(cInput.Text);
                    C = Convert.ToInt32(cInput.Text);
                }
            CmykToRgb();
        }

        private void mInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (mSlider != null && mInput != null)
                if (Convert.ToInt32(mInput.Text) > 100)
                {
                    mInput.Text = "100";
                    mSlider.Value = 100;
                    M = 100;
                }
                else
                {
                    mSlider.Value = Convert.ToInt32(mInput.Text);
                    M = Convert.ToInt32(mInput.Text);
                }
            CmykToRgb();
        }

        private void yInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (ySlider != null && yInput != null)
                if (Convert.ToInt32(yInput.Text) > 100)
                {
                    yInput.Text = "100";
                    ySlider.Value = 100;
                    Y = 100;
                }
                else
                {
                    ySlider.Value = Convert.ToInt32(yInput.Text);
                    Y = Convert.ToInt32(yInput.Text);
                }
            CmykToRgb();
        }

        private void kInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (kSlider != null && kInput != null)
                if (Convert.ToInt32(kInput.Text) > 100)
                {
                    kInput.Text = "100";
                    kSlider.Value = 100;
                    K = 100;
                }
                else
                {
                    kSlider.Value = Convert.ToInt32(kInput.Text);
                    K = Convert.ToInt32(kInput.Text);
                }
            CmykToRgb();
        }


        private void rSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (rSlider != null && rInput != null)
            {
                rInput.Text = rSlider.Value.ToString();
                R = Convert.ToInt32(rInput.Text);
            }

            RgbToCmyk();
        }

        private void gSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (gSlider != null && gInput != null)
            {
                gInput.Text = gSlider.Value.ToString();
                G = Convert.ToInt32(gInput.Text);
            }

            RgbToCmyk();
        }

        private void bSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (bSlider != null && bInput != null)
            {
                bInput.Text = bSlider.Value.ToString();
                B = Convert.ToInt32(bInput.Text);
            }

            RgbToCmyk();
        }

        private void cSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (cSlider != null && cInput != null)
            {
                cInput.Text = cSlider.Value.ToString();
                C = Convert.ToInt32(cInput.Text);
            }

            CmykToRgb();
        }

        private void mSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mSlider != null && mInput != null)
            {
                mInput.Text = mSlider.Value.ToString();
                M = Convert.ToInt32(mInput.Text);
            }

            CmykToRgb();
        }

        private void ySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ySlider != null && yInput != null)
            {
                yInput.Text = ySlider.Value.ToString();
                Y = Convert.ToInt32(yInput.Text);
            }

            CmykToRgb();
        }

        private void kSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (kSlider != null && kInput != null)
            {
                kInput.Text = kSlider.Value.ToString();
                K = Convert.ToInt32(kInput.Text);
            }

            CmykToRgb();
        }


        public void CmykToRgb()
        {
            R = Convert.ToInt32(255 * (1 - ((float)C / 100)) * (1 - ((float)K / 100)));
            G = Convert.ToInt32(255 * (1 - ((float)M / 100)) * (1 - ((float)K / 100)));
            B = Convert.ToInt32(255 * (1 - ((float)Y / 100)) * (1 - ((float)K / 100)));

            Color color = Color.FromArgb((byte)255, (byte)R, (byte)G, (byte)B);
            SolidColorBrush brush = new SolidColorBrush(color);
            if(colorView != null)
                colorView.Background = brush;
        }

        private void RgbToCmyk()
        {
            float rf = R / 255F;
            float gf = G / 255F;
            float bf = B / 255F;

            K = (int)ClampCmyk(1 - Math.Max(Math.Max(rf, gf), bf))*100;
            C = (int)ClampCmyk((1 - rf - ((float)K / 100)) / (1 - ((float)K / 100))) * 100;
            M = (int)ClampCmyk((1 - gf - ((float)K / 100)) / (1 - ((float)K / 100))) * 100;
            Y = (int)ClampCmyk((1 - bf - ((float)K / 100)) / (1 - ((float)K / 100))) * 100;

            Color color = Color.FromArgb((byte)255, (byte)R, (byte)G, (byte)B);
            SolidColorBrush brush = new SolidColorBrush(color);
            if(colorView != null)
                colorView.Background = brush;
        }

        private static float ClampCmyk(float value)
        {
            if (value < 0 || float.IsNaN(value))
            {
                value = 0;
            }

            return value;
        }
    }
}
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
        double C = 0.001, M = 0.001, Y = 0.001, K = 0.001;
        int R, G, B;
        bool editFlag = true;
        Color color;
        SolidColorBrush brush = new SolidColorBrush();

        public CmykRgb()
        {
            InitializeComponent();
        }

        private void rInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (rInput != null && rSlider != null && editFlag)
            {
                R = Int32.TryParse(rInput.Text, out int x) ? x : 1;
                rSlider.Value = R;
                RgbToCmyk();
                UpdateCmyk();
            }
        }
        private void gInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (gInput != null && gSlider != null && editFlag)
            {
                G = Int32.TryParse(gInput.Text, out int x) ? x : 1;
                gSlider.Value = G;
                RgbToCmyk();
                UpdateCmyk();
            }
        }
        private void bInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (bInput != null && bSlider != null && editFlag)
            {
                B = Int32.TryParse(bInput.Text, out int x) ? x : 1;
                bSlider.Value = B;
                RgbToCmyk();
                UpdateCmyk();
            }
        }

        private void cInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (cInput != null && cSlider != null && editFlag)
            {
                C = Math.Round(Convert.ToDouble(cInput.Text), 3);
                cSlider.Value = C;
                CmykToRgb();
                UpdateRgb();
            }
        }
        private void mInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (mInput != null && mSlider != null && editFlag)
            {
                M = Math.Round(Convert.ToDouble(mInput.Text), 3);
                mSlider.Value = M;
                CmykToRgb();
                UpdateRgb();
            }
        }
        private void yInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (yInput != null && ySlider != null && editFlag)
            {
                Y = Math.Round(Convert.ToDouble(yInput.Text), 3);
                ySlider.Value = Y;
                CmykToRgb();
                UpdateRgb();
            }
        }
        private void kInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (kInput != null && kSlider != null && editFlag)
            {
                K = Math.Round(Convert.ToDouble(kInput.Text), 3);
                kSlider.Value = K;
                CmykToRgb();
                UpdateRgb();
            }
        }


        private void rSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (rInput != null && rSlider != null && editFlag)
            {
                rInput.Text = rSlider.Value.ToString();
            }
        }
        private void gSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (gInput != null && gSlider != null && editFlag)
            {
                gInput.Text = gSlider.Value.ToString();
            }
        }
        private void bSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (bInput != null && bSlider != null && editFlag)
            {
                bInput.Text = bSlider.Value.ToString();
            }
        }

        private void cSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (cInput != null && cSlider != null && editFlag)
            {
                cInput.Text = cSlider.Value.ToString();
            }
        }
        private void mSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mInput != null && mSlider != null && editFlag)
            {
                mInput.Text = mSlider.Value.ToString();
            }
        }
        private void ySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (yInput != null && ySlider != null && editFlag)
            {
                yInput.Text = ySlider.Value.ToString();
            }
        }
        private void kSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (kInput != null && kSlider != null && editFlag)
            {
                kInput.Text = kSlider.Value.ToString();
            }
        }

        private void UpdateRgb()
        {
            if (rInput != null && gInput != null && bInput != null && rSlider != null && gSlider != null && bSlider != null && colorView != null)
            {
                try
                {
                    editFlag = false;

                    rInput.Text = R.ToString();
                    gInput.Text = G.ToString();
                    bInput.Text = B.ToString();
                    rSlider.Value = R;
                    gSlider.Value = G;
                    bSlider.Value = B;

                    editFlag = true;

                    color = Color.FromArgb((byte)255, (byte)R, (byte)G, (byte)B);
                    brush.Color = color;
                    colorView.Background = brush;
                }
                catch { }
            }
        }

        private void UpdateCmyk()
        {
            if (cInput != null && mInput != null && yInput != null && kInput != null && cSlider != null && mSlider != null && ySlider != null && kSlider != null && colorView != null)
            {
                try
                {
                    editFlag = false;

                    cInput.Text = C.ToString();
                    mInput.Text = M.ToString();
                    yInput.Text = Y.ToString();
                    kInput.Text = K.ToString();
                    cSlider.Value = C;
                    mSlider.Value = M;
                    ySlider.Value = Y;
                    kSlider.Value = K;

                    editFlag = true;

                    color = Color.FromArgb((byte)255, (byte)R, (byte)G, (byte)B);
                    brush.Color = color;
                    colorView.Background = brush;
                }
                catch { }
            }
        }

        public void CmykToRgb()
        {
            R = (int)(255 * (1 - C) * (1 - K));
            G = (int)(255 * (1 - M) * (1 - K));
            B = (int)(255 * (1 - Y) * (1 - K));
        }

        private void RgbToCmyk()
        {
            double dR = R / 255;
            double dG = G / 255;
            double dB = B / 255;

            K = 1 - Math.Max(Math.Max(dR, dG), dB);
            C = Math.Abs((1 - dR - K) / (1 - K + 0.001));
            M = Math.Abs((1 - dG - K) / (1 - K + 0.001));
            Y = Math.Abs((1 - dB - K) / (1 - K + 0.001));
        }
    }
}
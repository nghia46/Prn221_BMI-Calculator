using System;
using System.Windows;
using System.Windows.Input;

namespace BMI_Calculator
{
    public partial class MainWindow : Window
    {
        private float height;
        private float weight;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtHeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private void txtWeight_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsNumeric(string text)
        {
            return float.TryParse(text, out _);
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (float.TryParse(txtWeight.Text, out weight) && float.TryParse(txtHeight.Text, out height))
            {
                float bmi = CalculateBMI(weight, height);
                txtBmi.Content = bmi;

                // Determine BMI status
                string status = GetBMIStatus(bmi);
                txtStatus.Content = status;
            }
        }

        private float CalculateBMI(float weight, float height)
        {
            return MathF.Round(weight / MathF.Pow(height / 100, 2), 2);
        }

        private string GetBMIStatus(float bmi)
        {
            var bmiStatusRanges = new (float, float, string)[]
            {
                (float.MinValue, 18.5f, "Underweight"),
                (18.5f, 24.9f, "Normal weight"),
                (24.9f, 29.9f, "Overweight"),
                (29.9f, float.MaxValue, "Obese"),
            };
            foreach (var (min, max, status) in bmiStatusRanges)
            {
                if (bmi >= min && bmi < max)
                {
                    return status;
                }
            }
            return "Unknown";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtHeight.Text = "0";
            txtWeight.Text = "0";
            txtBmi.Content = string.Empty;
            txtStatus.Content = string.Empty;
        }
    }
}

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
using System.Windows.Shapes;
using MotelManagement.Models;

namespace MotelManagement
{
    /// <summary>
    /// Interaction logic for OTPWindow.xaml
    /// </summary>
    public partial class OTPWindow : Window
    {
        public OTPWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (OtpTextBox.Text == OTPManager.CurrentOtp)
            {
                OTPManager.IsOtpVerified = true;
                MessageBox.Show("OTP verified successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid OTP. Please try again.");
            }
        }
    }
}

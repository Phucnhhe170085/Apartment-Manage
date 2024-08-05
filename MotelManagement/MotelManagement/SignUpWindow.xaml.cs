using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using MotelManagement.Models;

namespace MotelManagement
{
    public partial class SignUpWindow : Window
    {
        private MotelDbContext _context;

        public SignUpWindow()
        {
            InitializeComponent();
            _context = new MotelDbContext();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string userMail = txtUserMail.Text.Trim();
            string password = txtPassword.Password.Trim();
            string confirmPassword = txtConfirmPassword.Password.Trim();
            string userGender = cmbUserGender.Text.Trim();
            DateTime? userBirth = dpUserBirth.SelectedDate;
            string userAddress = txtUserAddress.Text.Trim();
            string userPhone = txtUserPhone.Text.Trim();

   
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userMail) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(userGender) || !userBirth.HasValue ||
                string.IsNullOrWhiteSpace(userAddress) || string.IsNullOrWhiteSpace(userPhone))
            {
                txtErrorMessage.Text = "Please fill in all fields.";
                return;
            }

      
            if (!userMail.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                txtErrorMessage.Text = "Email must end with @gmail.com.";
                return;
            }

            if (!Regex.IsMatch(userPhone, @"^\d{10}$"))
            {
                txtErrorMessage.Text = "Phone number must contain exactly 10 digits.";
                return;
            }

  
            if (userBirth.Value > DateTime.Today)
            {
                txtErrorMessage.Text = "Date of birth cannot be in the future.";
                return;
            }

            var existingAccount = _context.Accounts.FirstOrDefault(a => a.UserMail == userMail);
            if (existingAccount != null)
            {
                txtErrorMessage.Text = "An account with this email already exists.";
                return;
            }

            // Create Account entity
            var account = new Account
            {
                UserMail = userMail,
                UserPassword = password,
                UserRole = 1 // Hardcoding UserRole to 1 (Renter)
            };

    
            _context.Accounts.Add(account);
            _context.SaveChanges();

            string otp = GenerateOTP();

            // Send OTP email
            SendOtpEmail(userMail, otp);

            OTPManager.CurrentOtp = otp;
            OTPManager.Email = userMail;

            // Open OTP confirmation window
            var otpWindow = new OTPWindow();
            otpWindow.ShowDialog();

            // Optional: Check OTP confirmation status
            if (OTPManager.IsOtpVerified)
            {
                // Create User entity with AccountID
                var user = new User
                {
                    UserId = account.UserId, // Assign AccountID to UserId in User entity
                    UserName = userName,
                    UserGender = userGender,
                    UserBirth = DateOnly.FromDateTime(userBirth.Value),
                    UserAddress = userAddress,
                    UserPhone = userPhone
                };

                // Add User to context and save changes
                _context.Users.Add(user);
                _context.SaveChanges();

                MessageBox.Show("Account created successfully!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }


        private string GenerateOTP(int length = 6)
        {
            var random = new Random();
            var otp = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                otp.Append(random.Next(0, 10).ToString());
            }
            return otp.ToString();
        }

        private void SendOtpEmail(string toEmail, string otp)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("bnvqm1721@gmail.com", "tgqwyawkaytmqvka"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("bnvqm1721@gmail.com", "My Project"),
                Subject = "Your OTP Code",
                Body = $"Your OTP code is {otp}",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            smtpClient.Send(mailMessage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}

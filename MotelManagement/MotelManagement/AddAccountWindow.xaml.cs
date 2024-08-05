using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddAccountWindow.xaml
    /// </summary>
    public partial class AddAccountWindow : Window
    {
        private readonly MotelDbContext _context = new MotelDbContext();
        public AddAccountWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Close();
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

            // Validation: Check for empty fields
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userMail) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(userGender) || !userBirth.HasValue ||
                string.IsNullOrWhiteSpace(userAddress) || string.IsNullOrWhiteSpace(userPhone))
            {
                txtErrorMessage.Text = "Please fill in all fields.";
                return;
            }

            // Validation: Validate userMail format
            if (!userMail.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                txtErrorMessage.Text = "Email must end with @gmail.com.";
                return;
            }

            // Validation: Validate userPhone format
            if (!Regex.IsMatch(userPhone, @"^\d{10}$"))
            {
                txtErrorMessage.Text = "Phone number must contain exactly 10 digits.";
                return;
            }

            // Validation: Ensure birth date is not in the future
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

            // Add Account to context and save changes to get its ID
            _context.Accounts.Add(account);
            _context.SaveChanges();

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
            this.Close();
        }
    }
}

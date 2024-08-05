using Microsoft.EntityFrameworkCore;
using MotelManagement.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace MotelManagement
{
    public partial class AdminEditUser : Window
    {
        private readonly MotelDbContext _context = new MotelDbContext();
        private readonly int _accountId;

        public AdminEditUser(int accountId)
        {
            InitializeComponent();
            _accountId = accountId;
            LoadAccountDetails();
        }

        private void LoadAccountDetails()
        {
            var account = _context.Accounts.Include(a => a.User).FirstOrDefault(a => a.UserId == _accountId);
            if (account != null)
            {
                txtUserName.Text = account.User?.UserName;
                txtUserMail.Text = account.UserMail;
                txtPassword.Password = account.UserPassword;
                txtConfirmPassword.Password = account.UserPassword; // Assuming password and confirm password are same
                cmbUserRole.SelectedItem = cmbUserRole.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Tag.ToString() == account.UserRole.ToString());
                cmbUserGender.SelectedItem = cmbUserGender.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == account.User?.UserGender);
                dpUserBirth.SelectedDate = account.User?.UserBirth.ToDateTime(new TimeOnly(0, 0));

                txtUserAddress.Text = account.User?.UserAddress;
                txtUserPhone.Text = account.User?.UserPhone;
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string userMail = txtUserMail.Text;
            string userPhone = txtUserPhone.Text;
            DateTime? userBirth = dpUserBirth.SelectedDate;

            // Validate user input
            if (!IsValidEmail(userMail))
            {
                txtErrorMessage.Text = "Email must end with @gmail.com.";
                return;
            }
            if (!IsValidPhoneNumber(userPhone))
            {
                txtErrorMessage.Text = "Phone number must contain exactly 10 digits.";
                return;
            }
            if (userBirth.HasValue && userBirth.Value > DateTime.Today)
            {
                txtErrorMessage.Text = "Date of birth cannot be in the future.";
                return;
            }

            try
            {
                var account = _context.Accounts.Include(a => a.User).FirstOrDefault(a => a.UserId == _accountId);
                if (account != null)
                {
                    account.UserMail = userMail;
                    account.UserPassword = txtPassword.Password;
                    if (cmbUserRole.SelectedItem is ComboBoxItem selectedRole)
                    {
                        account.UserRole = int.Parse(selectedRole.Tag.ToString());
                    }
                    if (account.User != null)
                    {
                        account.User.UserName = txtUserName.Text;
                        account.User.UserGender = cmbUserGender.Text;
                        account.User.UserBirth = DateOnly.FromDateTime((DateTime)userBirth);
                        account.User.UserAddress = txtUserAddress.Text;
                        account.User.UserPhone = userPhone;
                    }
                    _context.SaveChanges();
                    MessageBox.Show("Account updated successfully.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the account: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            return email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d{10}$");
        }
    }
}

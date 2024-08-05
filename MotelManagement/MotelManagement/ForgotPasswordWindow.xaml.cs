using System.Linq;
using System.Windows;
using MotelManagement.Models;

namespace MotelManagement
{
    public partial class ForgotPasswordWindow : Window
    {
        private MotelDbContext _context;

        public ForgotPasswordWindow()
        {
            InitializeComponent();
            _context = new MotelDbContext();
        }

        private void btnResetPassword_Click(object sender, RoutedEventArgs e)
        {
            string userMail = txtUserMail.Text.Trim();
            string newPassword = txtNewPassword.Password.Trim();
            string confirmNewPassword = txtConfirmPassword.Password.Trim();

            // Validation: Check for empty fields
            if (string.IsNullOrWhiteSpace(userMail) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmNewPassword))
            {
                txtErrorMessage.Text = "Please fill in all fields.";
                return;
            }

            // Validation: Ensure passwords match
            if (newPassword != confirmNewPassword)
            {
                txtErrorMessage.Text = "Passwords do not match.";
                return;
            }

            var account = _context.Accounts.FirstOrDefault(a => a.UserMail == userMail);
            if (account == null)
            {
                txtErrorMessage.Text = "Account not found for this email.";
                return;
            }

            // Update password
            account.UserPassword = newPassword;
            _context.SaveChanges();

            MessageBox.Show("Password reset successfully!");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}

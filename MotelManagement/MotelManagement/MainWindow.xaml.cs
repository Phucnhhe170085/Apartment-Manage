using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using MotelManagement.Models;

namespace MotelManagement
{
    public partial class MainWindow : Window
    {
        private readonly int _userId;
        private MotelDbContext _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new MotelDbContext();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string userMail = txtUserName.Text;
            string password = txtPassword.Password;

            var account = _context.Accounts
                                 .FirstOrDefault(a => a.UserMail == userMail && a.UserPassword == password);
            account = _context.Accounts.Include(o => o.User).FirstOrDefault(a => a.UserMail == userMail && a.UserPassword == password);
            //var renter = _context.Renters.FirstOrDefault(o => o.UserId == account.UserId);
            //if (renter != null)
            //{
            //    var room = _context.Rooms.Include(r => r.RoomHistories)
            //        .Where(rh => rh.RoomHistories.Any(o => o.RenterId == renter.RenterId))
            //        .ToList();
            //}
                
            if (account != null)
            {
                // Successful login
                switch ((UserRole)account.UserRole)
                {
                    case UserRole.Renter:
                        // Navigate to Renter's dashboard
                        RenterWindow renterWindow = new RenterWindow(account.UserId);
                        renterWindow.Show();
                        this.Close();
                        break;
                    case UserRole.Owner:
                        OwnerWindow ownerWindow = new OwnerWindow(account.UserId);
                        ownerWindow.Show();
                        this.Close();
                        MessageBox.Show("Welcome, Owner!");
                        break;
                    case UserRole.Security:
                        // Navigate to Security's dashboard or show a message
                        MessageBox.Show("Welcome, Security!");
                        break;
                    case UserRole.Admin:
                        // Navigate to Admin's dashboard
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                        this.Close();
                        break;
                    default:
                        txtErrorMessage.Text = "Unknown user role.";
                        break;
                }
            }
            else
            {
                // Failed login
                txtErrorMessage.Text = "Invalid username or password.";
            }
        }


        public enum UserRole
        {
            Renter = 1,
            Owner = 2,
            Security = 3,
            Admin = 4
        }


        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            this.Close();
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.Show();
            this.Close();
        }
    }
}

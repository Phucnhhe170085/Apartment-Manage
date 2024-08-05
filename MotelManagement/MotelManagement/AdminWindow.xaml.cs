using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MotelManagement.Enums;
using MotelManagement.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MotelManagement.MainWindow;

namespace MotelManagement
{
    public partial class AdminWindow : Window
    {
        private readonly MotelDbContext _context = new MotelDbContext();

        public AdminWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            var users = _context.Accounts.Include(a => a.User).ToList();
            listAccount.ItemsSource = users;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listAccount.SelectedItem is Account selectedAccount)
            {
                userMailTextBox.Text = selectedAccount.UserMail;
                userPasswordTextBox.Text = selectedAccount.UserPassword;
                userRoleComboBox.SelectedItem = userRoleComboBox.Items
                    .OfType<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == Role.GetRole(selectedAccount.UserRole).Name);
            }
        }

        private void listAccount_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshAccountList();
        }

        private void RefreshAccountList()
        {
            try
            {
                var accounts = _context.Accounts.ToList();
                listAccount.ItemsSource = accounts;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading accounts: {ex.Message}");
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterAccounts();
        }

        private void RoleFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterAccounts();
        }

        private void FilterAccounts()
        {
            try
            {
                string searchText = searchBox.Text.ToLower();
                int selectedRole = (cbbxRole.SelectedItem is ComboBoxItem selectedRoleItem)
                    ? GetRoleValue(selectedRoleItem.Content.ToString())
                    : 0;

                var filteredAccounts = _context.Accounts
                    .Where(account =>
                        (string.IsNullOrEmpty(searchText) || account.UserMail.ToLower().Contains(searchText)) &&
                        (selectedRole == 0 || account.UserRole == selectedRole))
                    .ToList();

                listAccount.ItemsSource = filteredAccounts;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filtering accounts: {ex.Message}");
            }
        }

        private int GetRoleValue(string roleName)
        {
            return roleName switch
            {
                "Renter" => 1,
                "Owner" => 2,
                "Security" => 3,
                "Admin" => 4,
                _ => 0 // Default or unknown role
            };
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           AddAccountWindow addAccountWindow = new AddAccountWindow();
            addAccountWindow.Show();
            this.Close();
        }

        private void ClearTextBoxes()
        {
            userMailTextBox.Text = string.Empty;
            userPasswordTextBox.Text = string.Empty;
            userRoleComboBox.SelectedItem = null;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listAccount.SelectedItem is Account selectedAccount)
            {
                var editWindow = new AdminEditUser(selectedAccount.UserId);
                editWindow.ShowDialog();
                RefreshAccountList(); // Refresh the account list after editing
            }
            else
            {
                MessageBox.Show("Please select an account to edit.");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listAccount.SelectedItem is Account selectedAccount)
            {
                try
                {
                    // Get the UserId of the selected account
                    int userIdToDelete = selectedAccount.UserId;

                    // Fetch the account and user to delete
                    var accountToDelete = _context.Accounts
                        .Include(a => a.User)  // Make sure to include the related User entity
                        .FirstOrDefault(a => a.UserId == userIdToDelete);

                    if (accountToDelete != null)
                    {
                        // Remove the user first
                        if (accountToDelete.User != null)
                        {
                            _context.Users.Remove(accountToDelete.User);
                        }

                        // Remove the account
                        _context.Accounts.Remove(accountToDelete);

                        // Save changes to the database
                        _context.SaveChanges();

                        // Refresh the DataGrid to reflect changes
                        listAccount.ItemsSource = _context.Accounts.ToList();

                        // Clear the TextBoxes after deletion
                        ClearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show($"Account with UserId {userIdToDelete} not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to delete account: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select an account to delete.");
            }
        }



        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (listAccount.SelectedItem is Account selectedAccount)
            {
                userMailTextBox.Text = selectedAccount.UserMail;
                userPasswordTextBox.Text = selectedAccount.UserPassword;

                userRoleComboBox.Items.Clear();
                foreach (Role role in Role.Roles.Values)
                {
                    if (role != Role.Admin)
                    {
                        ComboBoxItem item = new ComboBoxItem
                        {
                            Content = role.Name,
                            Tag = role.Value,
                        };
                        userRoleComboBox.Items.Add(item);
                    }
                }

                userRoleComboBox.SelectedItem = userRoleComboBox.Items
                    .OfType<ComboBoxItem>()
                    .FirstOrDefault(item => item.Tag.ToString() == selectedAccount.UserRole.ToString());
            }
            else
            {
                MessageBox.Show("Please select a user to load details.");
            }
        }
    }
}

using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using MotelManagement.Models;

namespace MotelManagement
{
    public partial class RenterWindow : Window
    {
        private readonly MotelDbContext _context;
        private readonly int _userId;
        private int _currentPage;
        private int _itemsPerPage;
        private ICollectionView _roomsView;
        private List<Room> _filteredRooms;

        public RenterWindow(int userId)
        {
            InitializeComponent();
            _context = new MotelDbContext();
            _userId = userId;
            _currentPage = 1;
            _itemsPerPage = 10;
            LoadRenterProfile();
            LoadRooms();
            LoadMyRoom();
            LoadRequests();
           
        }

        private void LoadRequests()
        {
            // Fetch requests associated with the current user
            var requests = _context.Requests
                .Where(r => r.UserID == _userId) // Assuming `UserId` is a property in the `Request` model
                .OrderByDescending(r => r.CreateAt)
                .ToList();

            // Bind the list of requests to the DataGrid
            listRequest.ItemsSource = requests;
        }



        private void LoadMyRoom()
        {
            var latestRoomHistory = _context.RoomHistories
                .Where(rh => rh.Status == true && rh.RenterId == _userId)
                .OrderByDescending(rh => rh.CreateAt)
                .FirstOrDefault();

            if (latestRoomHistory != null)
            {
                var room = _context.Rooms.FirstOrDefault(r => r.RoomId == latestRoomHistory.RoomId);
                if (room != null)
                {
                    txtRoomFloor.Text = room.RoomFloor.ToString();
                    txtRoomNumber.Text = room.RoomNumber.ToString();
                    txtRoomSize.Text = room.RoomSize.ToString();
                    txtRoomFee.Text = room.RoomFee.ToString();

                    // Show room details
                    txtRoomFloor.Visibility = Visibility.Visible;
                    txtRoomNumber.Visibility = Visibility.Visible;
                    txtRoomSize.Visibility = Visibility.Visible;
                    txtRoomFee.Visibility = Visibility.Visible;
                }
            }
            else
            {
                // Hide room details and show message
                txtRoomFloor.Visibility = Visibility.Collapsed;
                txtRoomNumber.Visibility = Visibility.Collapsed;
                txtRoomSize.Visibility = Visibility.Collapsed;
                txtRoomFee.Visibility = Visibility.Collapsed;
            }
        }




        private void LoadRenterProfile()
        {
            var renter = _context.Users.FirstOrDefault(u => u.UserId == _userId);
            if (renter != null)
            {
                txtUserName.Text = renter.UserName;
                txtUserGender.Text = renter.UserGender;
                dtpDOB.SelectedDate = renter.UserBirth.ToDateTime(TimeOnly.MinValue);
                txtUserAddress.Text = renter.UserAddress;
                txtUserPhone.Text = renter.UserPhone;
            }
        }

        private void LoadRooms()
        {
            var renter = _context.Users.FirstOrDefault(u => u.UserId == _userId);
            if (renter != null)
            {
                var room = _context.Rooms.Include(r => r.RoomHistories).FirstOrDefault(r => r.RoomHistories.Any(r => r.RenterId == _userId && r.Status == true));
                if (room != null)
                {
                    txtRoomMessage.Text = "You are already accommodated.";
                    dgRooms.Visibility = Visibility.Collapsed;
                    btn_Previous.Visibility = Visibility.Collapsed;
                    btn_Next.Visibility = Visibility.Collapsed;
                    lb_ListRoom.Visibility = Visibility.Collapsed;
                    btnRent.Visibility = Visibility.Collapsed;
                }
                else
                {
                    txtRoomMessage.Text = "";
                    dgRooms.Visibility = Visibility.Visible;
                    btnRent.Visibility = Visibility.Visible;

                    // Filter rooms with RoomStatus = 1
                    _filteredRooms = _context.Rooms.Where(r => r.RoomStatus == 1).ToList();
                    _roomsView = CollectionViewSource.GetDefaultView(_filteredRooms);
                    _roomsView.Filter = RoomsFilter;
                    UpdatePage();
                }
            }
        }

        private bool RoomsFilter(object item)
        {
            var index = _filteredRooms.IndexOf((Room)item);
            return index >= (_currentPage - 1) * _itemsPerPage && index < _currentPage * _itemsPerPage;
        }

        private void UpdatePage()
        {
            _roomsView.Refresh();
            txtPageInfo.Text = $"Page {_currentPage} of {Math.Ceiling((double)_filteredRooms.Count / _itemsPerPage)}";
            dgRooms.ItemsSource = _roomsView;
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdatePage();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < Math.Ceiling((double)_filteredRooms.Count / _itemsPerPage))
            {
                _currentPage++;
                UpdatePage();
            }
        }

        private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            var renter = _context.Users.FirstOrDefault(u => u.UserId == _userId);
            if (renter != null)
            {
                renter.UserName = txtUserName.Text;
                renter.UserGender = txtUserGender.Text;
                renter.UserBirth = DateOnly.FromDateTime((DateTime)dtpDOB.SelectedDate);
                renter.UserAddress = txtUserAddress.Text;
                renter.UserPhone = txtUserPhone.Text;

                _context.SaveChanges();
                MessageBox.Show("Profile updated successfully!");
            }
        }

        private void btnRent_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoom = dgRooms.SelectedItem as Room;
            if (selectedRoom != null)
            {
                // Update RoomStatus to 2
                selectedRoom.RoomStatus = 2;

                // Create new RoomHistory entry
                var roomHistory = new RoomHistory
                {
                    CreateAt = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                    RenterId = _userId,
                    RoomId = selectedRoom.RoomId,
                    CheckIn = null,
                    CheckOut = null,
                    UserId = _userId
                    
                };

                _context.RoomHistories.Add(roomHistory);
                _context.SaveChanges();

                MessageBox.Show("Room rented successfully!");
                LoadRooms();
                LoadMyRoom();
            }
            else
            {
                MessageBox.Show("Please select a room to rent.");
            }
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to cancel the room?", "Confirm Cancellation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var latestRoomHistory = _context.RoomHistories
                    .Where(rh => rh.Status == true && rh.RenterId == _userId)
                    .OrderByDescending(rh => rh.CreateAt)
                    .FirstOrDefault();

                if (latestRoomHistory != null)
                {
                    latestRoomHistory.Status = false;
                    latestRoomHistory.CheckOut = DateOnly.FromDateTime(DateTime.UtcNow.Date);
                    _context.SaveChanges();
                    MessageBox.Show("Room cancellation successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadRooms();
                    LoadMyRoom();
                }
                else
                {
                    MessageBox.Show("No active room to cancel.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void RefreshDataGrid()
        {
            using (var context = new MotelDbContext())
            {
                listRequest.ItemsSource = context.Requests.ToList();
            }
        }


        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Assuming you have a DbContext named `context`
            using (var context = new MotelDbContext())
            {
                // Get the selected request from the DataGrid
                var selectedRequest = listRequest.SelectedItem as Request;
                if (selectedRequest == null)
                {
                    MessageBox.Show("Please select a request to edit.");
                    return;
                }

                // Update properties
                selectedRequest.Title = txtTitle.Text;
                selectedRequest.Desciption = txtDescription.Text;
                selectedRequest.RequestType = (int)cbRequestType.SelectedValue; // Assuming you are using an integer for the type

                // Save changes to the database
                context.Requests.Update(selectedRequest);
                context.SaveChanges();

                MessageBox.Show("Request updated successfully.");
                RefreshDataGrid(); // Method to refresh the DataGrid
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MotelDbContext())
            {
                // Get the selected request from the DataGrid
                var selectedRequest = listRequest.SelectedItem as Request;
                if (selectedRequest == null)
                {
                    MessageBox.Show("Please select a request to delete.");
                    return;
                }

                // Remove the selected request
                context.Requests.Remove(selectedRequest);
                context.SaveChanges();

                MessageBox.Show("Request deleted successfully.");
                RefreshDataGrid(); // Method to refresh the DataGrid
            }
        }


     
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected RequestType ID
            var selectedRequestTypeId = cbRequestType.SelectedValue;

            if (selectedRequestTypeId != null)
            {
                // Convert selected value to integer (or the appropriate type)
                int requestTypeId = (int)selectedRequestTypeId;

                MessageBox.Show($"Selected Request Type ID: {requestTypeId}");

                // Further processing based on selectedRequestTypeId
            }
        }

        private void dgRooms_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }

}



using Microsoft.EntityFrameworkCore;
using MotelManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MotelManagement
{
    public partial class OwnerWindow : Window, INotifyPropertyChanged
    {
        private readonly MotelDbContext _context;
        private readonly int _userId;
        private int _currentPage;
        private int _itemsPerPage;

        private ICollectionView _roomsView;
        private List<Room> _filteredRooms;
        private List<RoomHistory> _roomHistories;

        public OwnerWindow(int userId)
        {
            InitializeComponent();
            _context = new MotelDbContext();
            _userId = userId;
            _currentPage = 1;
            _itemsPerPage = 10;
            LoadOwnerProfile();
            LoadRooms();
            LoadRoomHistories();
            DataContext = this;
        }

        private void LoadRooms()
        {
            // Load rooms based on status values
            _filteredRooms = _context.Rooms
                                     .Where(r => r.RoomStatus == 1 || r.RoomStatus == 2 || r.RoomStatus == 3)
                                     .ToList();

            // Initialize ICollectionView for data binding
            _roomsView = CollectionViewSource.GetDefaultView(_filteredRooms);
            _roomsView.Filter = RoomsFilter;

            // Update the DataGrid view
            RoomPage();
            UpdateRoomPage();
        }
        private bool RoomsFilter1(object item)
        {
            var room = item as Room;
            var index = _filteredRooms.IndexOf(room);
            return room != null && index >= (_currentPage - 1) * _itemsPerPage && index < _currentPage * _itemsPerPage;
        }




        private bool RoomsFilter(object item)
        {
            var index = _filteredRooms.IndexOf((Room)item);
            return index >= (_currentPage - 1) * _itemsPerPage && index < _currentPage * _itemsPerPage;
        }

        private void RoomPage()
        {
            // Refresh the view to update the data in the DataGrid
            _roomsView.Refresh();

            // Update the page information text block
            txtPage.Text = $"Page {_currentPage} of {Math.Ceiling((double)_filteredRooms.Count / _itemsPerPage)}";

            // Set the ItemsSource of the renterList DataGrid to the filtered view
            renterList.ItemsSource = _roomsView;
        }


        private void UpdateRoomPage()
        {
            _roomsView.Refresh();
            txtPage.Text = $"Page {_currentPage} of {Math.Ceiling((double)_filteredRooms.Count / _itemsPerPage)}";
            dgRooms.ItemsSource = _roomsView;
            
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateRoomPage();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < Math.Ceiling((double)_filteredRooms.Count / _itemsPerPage))
            {
                _currentPage++;
                UpdateRoomPage();
            }
        }

        private void LoadOwnerProfile()
        {
            var owner = _context.Users.FirstOrDefault(u => u.UserId == _userId);
            if (owner != null)
            {
                txtUserName.Text = owner.UserName;
                txtUserGender.Text = owner.UserGender;
                dtpDOB.SelectedDate = owner.UserBirth.ToDateTime(TimeOnly.MinValue);
                txtUserAddress.Text = owner.UserAddress;
                txtUserPhone.Text = owner.UserPhone;
            }
        }

        private void btnUpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is Room selectedRoom)
            {
                MessageBox.Show($"Update Room {selectedRoom.RoomNumber}");
                EditRoomWindow editRoomWindow = new EditRoomWindow(selectedRoom);
                editRoomWindow.Show();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            var owner = _context.Users.FirstOrDefault(u => u.UserId == _userId);
            if (owner != null)
            {
                owner.UserName = txtUserName.Text;
                owner.UserGender = txtUserGender.Text;
                owner.UserBirth = DateOnly.FromDateTime((DateTime)dtpDOB.SelectedDate);
                owner.UserAddress = txtUserAddress.Text;
                owner.UserPhone = txtUserPhone.Text;

                _context.SaveChanges();
                MessageBox.Show("Profile updated successfully!");
            }
        }

        private void LoadRoomHistories()
        {
            _roomHistories = _context.RoomHistories.ToList();
            listHistory.ItemsSource = _roomHistories;
        }

        private void CheckIn_Click(object sender, RoutedEventArgs e)
        {
            if (listHistory.SelectedItem is RoomHistory selectedHistory)
            {
                var result = MessageBox.Show("Are you sure you want to check in?", "Confirm Check-In", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (selectedHistory.CheckOut != null)
                    {
                        MessageBox.Show("This room has already been checked out and cannot be checked in again.", "Check-In Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var room = _context.Rooms.FirstOrDefault(r => r.RoomId == selectedHistory.RoomId);
                    if (room != null)
                    {
                        room.RoomStatus = 4; // Set RoomStatus to 4 for "Rent"
                        selectedHistory.CheckIn = DateOnly.FromDateTime(DateTime.UtcNow.Date);
                        selectedHistory.Status = true;
                        _context.SaveChanges();
                        LoadRoomHistories();
                        MessageBox.Show("Check-in completed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (listHistory.SelectedItem is RoomHistory selectedHistory)
            {
                var result = MessageBox.Show("Are you sure you want to check out?", "Confirm Check-Out", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (selectedHistory.CheckOut != null)
                    {
                        MessageBox.Show("This room has already been checked out and cannot be checked in again.", "Check-In Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    var room = _context.Rooms.FirstOrDefault(r => r.RoomId == selectedHistory.RoomId);
                    if (room != null)
                    {
                        room.RoomStatus = 1; // Set RoomStatus to 1 for "On List"
                        selectedHistory.CheckOut = DateOnly.FromDateTime(DateTime.UtcNow.Date);
                        selectedHistory.Status = false;
                        _context.SaveChanges();
                        LoadRoomHistories();
                        MessageBox.Show("Check-out completed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }


        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateRoomPage();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < Math.Ceiling((double)_filteredRooms.Count / _itemsPerPage))
            {
                _currentPage++;
                UpdateRoomPage();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

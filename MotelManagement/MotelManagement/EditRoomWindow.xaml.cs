using MotelManagement.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MotelManagement
{
    public partial class EditRoomWindow : Window
    {
        private Room _room;

        public EditRoomWindow(Room room)
        {
            InitializeComponent();
            _room = room;

            // Initialize fields with room details
            txtRoomNumber.Text = _room.RoomNumber.ToString();
            txtFloor.Text = _room.RoomFloor.ToString();
            txtSize.Text = _room.RoomSize.ToString();
            txtFee.Text = _room.RoomFee.ToString();

            // Initialize ComboBox with the current status
            switch (_room.RoomStatus)
            {
                case 1:
                    cbStatus.SelectedIndex = 0; // On List
                    break;
                case 2:
                    cbStatus.SelectedIndex = 1; // On Process
                    break;
                case 3:
                    cbStatus.SelectedIndex = 2; // On Repair
                    break;
                default:
                    cbStatus.SelectedIndex = -1; // Unknown status
                    break;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Update room details from UI
            _room.RoomNumber = int.Parse(txtRoomNumber.Text);
            _room.RoomFloor = int.Parse(txtFloor.Text);
            _room.RoomSize = int.Parse(txtSize.Text);
            _room.RoomFee = decimal.Parse(txtFee.Text);

            // Get the selected status from the ComboBox
            var selectedStatus = (ComboBoxItem)cbStatus.SelectedItem;
            _room.RoomStatus = int.Parse(selectedStatus.Tag.ToString());

            // Save changes to the database or perform other logic
            // Example: _context.SaveChanges(); // Assuming _context is your DbContext

            MessageBox.Show("Room details saved successfully!");
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

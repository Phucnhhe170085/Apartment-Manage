   M S B A M L   `   `   `  ����J  FMotelManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MotelManagementMotelManagement  5   	   !    MotelManagement.OwnerWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MotelManagement     5   	     � Title$  OwnerWindow��5   	   $	��450��6   $	��800��6*   .��5	      � .+�5
   
   �� .@�5      �� $
��Home��6   .��5      �  � ColumnDefinitions 5      �� 5      $	��86* �6+   �� 5      $
��311* �6+   .+�5      �� $)��#Welcome to Motel Management System!��6    $��24(�6K   $
��Left=�6Y   $	��Top=�6t   $��
162,10,0,0q�6�   $��2��6�   5      5      �� 5      $��Profile��6   .��5      �� .+�5      �� $��Renter Details��6    $��20(�66   $
��Bold"�6D   $	��791��6V   $��28��6b   �� 5      $��20q�6!   $	��286��6-   $	��458��6:   .+�5      �� $��
User Name:��6$   $
��Bold"�66   �� 5      -   5         ��Name$ txtUserName��5   "   $��0,0,0,10q�67   �� 5      $��Gender:��6$   $
��Bold"�63   �� 5      -   5        $ txtUserGender��5   "   $��0,0,0,10q�69   �� 5      $��Date of Birth:��6$   $
��Bold"�6:   (  "System.Windows.Controls.DatePicker  5       -   5          Name$ dtpDOB��5    %   $��0,0,0,10q�65   �� 5!      $��Address:��6$   $
��Bold"�64   �� 5"      -   5        $ txtUserAddress��5"   "   $��0,0,0,10q�6:   �� 5#      $��Phone:��6$   $
��Bold"�62   �� 5$      -   5        $ txtUserPhone��5$   "   $��0,0,0,10q�68   �� 5%      -   5         ��Name$ btnUpdateProfile��5%   !   $��Update��6;   $	��149��6k   5&      5'      5(      �� 5+      $��Update Room��6   .��5-      �� .+�5.      �� -   5         ��Name$ lb_ListRoom��5.       $��Renter Room��65   $��20(�6H   $
��Bold"�6V   $	��791��6h   &   System.Windows.Controls.DataGrid  5/      -   5          Name$ dgRooms��5/      $��10q�60      AutoGenerateColumns$ False��6<      Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Room��61   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *
RoomNumber+6^     52      $	 Floor��61   $
 * 6^    6@   �� *	RoomFloor+6^     53      $
	 Size��61   $
 * 6\    6?   �� *
RoomSize+6\     54      $		 Fee��61   $
 * 6Z    6>   �� *	RoomFee+6Z   4  .System.Windows.Controls.DataGridTemplateColumn  57      $	 Update��65      CellTemplate 58   "   �� 59   &   .R�5:   *   �� -	   5        $��Update��5:   1   6B   5;   '   5=      5A      �� 5C       �� Orientation$ 
Horizontal=�6!   $��Center=�6:   $��10q�6W   $	��120��6c   .+�5D      �� -
   5        $ btn_Previous��5D   !   $��Previous��67   6J   �� 5E      -   5        $ txtPageInfo��5E   $   $
��10,0q�69   �� 5F      -   5        $ btn_Next��5F   !   $
��Next��63   $��51��6Y   5G      �� 5H      -   5        $ txtRoomMessage��5H       $�� ��68   $��20(�6@   $
��Bold"�6N   $��10q�6`   $	��472��6l   5I      5J      �� 5M      $��Renter Room��6   .��5N      �� $��1,0,3,0q�6   $	��404��6.   .+�5O      �� $��	Room List��6    $��20(�61   $
��Bold"�6?   $��10q�6Q     5P      -   5        $ 
renterList��5P      $ False��63   $��10q�6O    5Q        5R      $	 Room ID��61   $	
 100 6]    6B   �� *RoomId+6]     5S      $	 Floor��61   $	
 100 6^    6@   �� *	RoomFloor+6^     5T      $	 Number��61   $	
 100 6`    6A   �� *
RoomNumber+6`     5U      $
	 Size��61   $	
 100 6\    6?   �� *
RoomSize+6\     5V      $		 Fee��61   $	
 100 6Z    6>   �� *	RoomFee+6Z     5W      $	 Status��61   $	
 100 6`    6A   �� *
RoomStatus+6`   5Y      �� 5Z      $ 
Horizontal=�6!   $��Center=�6:   $��10q�6W   $��40��6c   $	��192��6o   .+�5[      �� -   5        $ btnPrevious��5[   !   $��Previous��66   $��38��6c   �� 5\      -   5        $ txtPage��5\   $   $��Page 1 of 1��65   $��Center=�6H   $��10q�6c   �� 5]      -   5        $ btnNext��5]   !   $
��Next��62   $��49��6W   5^      5_      5`      �� 5c      $��Room Register��6   .��5d      �� $��1,0,3,0q�6   $	��404��6.   .+�5e      �� -   5         ��Name$ listHistory��5e      ��64   �� *RoomHistories+ �� SelectedItem 6Z   �� *SelectedRoomHistory+ �� View 5f      �� 5g      .P�5h   "   �� $��	Create At��61    �� Width$	 150��6n    �� DisplayMemberBinding 6D   �� *
CreateAt+6n   �� 5i   "   $��	Renter ID��61   $	 100��6n    6D   �� *
RenterId+6n   �� 5j   "   $��Room ID��61   $	 100��6j    6B   �� *RoomId+6j   �� 5k   "   $��Check-In��61   $	 150��6l    6C   �� *	CheckIn+6l   �� 5l   "   $��	Check-Out��61   $	 150��6n    6D   �� *
CheckOut+6n   �� 5m   "   $��Status��61   $	 150��6i    6A   �� *Status+6i   5n      5p      �� 5q      $ 
Horizontal=�6!   $��Center=�6:   $��10q�6W   $	��222��6c   .+�5r      �� -   5        $��Check-In��5r   !   $	��100��64   $��5q�6@   6K   �� 5s      -   5        $��	Check-Out��5s   !   $	��100��65   $��5q�6A   6L   5t      5u      5v      �� 5w      -   5         ��Name$ LogOut��5w      $��Log Out��6&   5x      5z      5{      5|      
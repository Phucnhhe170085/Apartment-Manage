   M S B A M L   `   `   `  ����J  FMotelManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MotelManagementMotelManagement  5   	        MotelManagement.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MotelManagement     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ Login Window��5	   	   $	��300��6   $	��400��6+    � WindowStartupLocation$ CenterScreen=�5
   	    � 	Resources 5         
LabelStyle �� 
TargetType �� Property �� Value  ButtonStyle��%M  &	        &	 x     �� 5   
   " ��6#   .�5      ��  � ��$ Center=�62   �� 5       � ��$ 
20,10,0,10q�6'   5      �� 5   
   " ��6$   .�5      ��  � ��$	 100��6&   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 	10,0,20,0q�6'   �� 5       � ��$ Right=�64   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    � ColumnDefinitions 5!   
   �� 5"      $��* �6   �� 5#      $��* �6   .+�5&   
   �� $��0��6   $��2��6!   $!��Welcome to Motel Management��65   $��20(�6X   $
��Left=�5'      $��
57,-2,0,42q�6/   �� 5)   
   $��1��6   $��0��6!   $��	Usermail:��61   #��[  6B   �� 5*   
   -   5        	 ��Name$	 txtUserName��5*      $��1��6   $��110,11,70,9q�64   $��2��6I   $��Center=�5+      
 � ToolTip$
 Enter your username��6-   �� 5-   
   $��2��6   $��0��6!   $��	Password:��61   #��[  6B   G� 5.   
   -   5         G�Name$ txtPassword��5.   #   $��2��6   $��110,11,70,9q�68   $��2��6M   $��Center=�5/      $
 Enter your password��61   �� 51   
   -   5         ��Name$ btnLogin��51      $��3��6#   $��1��60   $��Login��6@   #��[ 6P   $��24��52   (   $	��Top=�64   $��	0,10,10,0q�6L   $��2��6_   �� 54   
   -   5        $ btnForgotPassword��54      $��4��6,   $!��Forgot Password? Click here��69   $
��Left=�6_   $
��Bold"�55      
������ 
������ 
���   �$��0q�6i   $��1��56   1   $��
10,46,0,44q�6A   �� 58   
   -   5         ��Name$ txtErrorMessage��58      
���  ��$
��Left=�6>   $��
20,30,0,32q�59      $��15(�6(   $	��370��66   $��2��6B   $��2��6S   �� 5:   
   -   5        $ 	btnSignUp��5:      $��3��6$   $��Sign Up��61   #��[ 6C   $��24��5;      $	��Top=�6   $��
170,10,0,0q�62   $��2��6F   $
��Left=�6W   $��2��6r   6�   5<      5=      
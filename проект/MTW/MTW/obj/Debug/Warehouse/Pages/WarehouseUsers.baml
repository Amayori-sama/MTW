   M S B A M L   `   `   `  ����>  :MTW, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MTW.Warehouse.PagesMTW.Warehouse.Pages  5      (    "MTW.Warehouse.Pages.WarehouseUsers   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:MTW.Warehouse.Pages     5      -   5          N� Title$  Пользователи��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5      $	��53* �6   �� 5      -   5         ��Name$ ColumnChange��5      $	��270 �65    � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      $��60 �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��0��6"   $��3��62    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $
��0,10q�6z   .+�5      �� -   5         ��Name$ 	RecordAdd��5   6      MainBtn#��[  6   $��Добавить��6I   $��5,5,0,5q�5      5      �� 5      -   5        $ 
RecordCopy��5   6   #��[  6   $��Копировать��6J   $��5,5,0,5q�5      5      �� 5      -   5        $ 
RecordEdit��5   6   #��[  6   $��Изменить��6J   $��5,5,0,5q�5      5       �� 5!      -   5        $ RecordDellete��5!   6   #��[  6   $��Удалить��6M   $��5,5,0,5q�5"      5#      �� 5$      $��3��6   $��30��6"   ���#fafafa$��20, 0, 20, 0q�6=   6U   �� 5%      -   5         ��Name$ FilterComboBox��5%      $	��150��6/   $��30��6;   �� 5&      -   5         ��Name$ FilterTextBox��5&      $��5,0,5,0q�6-   $	��170��6>   $��30��6J   6V   5'      &   System.Windows.Controls.DataGrid  5)   
   -	   5          Name$ PageGrid��5)   0   $��2��6   $��0��6    
������ 	   AutoGenerateColumns	 .  
   
IsReadOnly
 .    CanUserAddRows .     RowBackground �#fafafa   HorizontalGridLinesBrush �#060722   VerticalGridLinesBrush �#060722
������ $��0, 0, 10, 0q�5+   ,   $��2��6A      Columns 5,      0  *System.Windows.Controls.DataGridTextColumn  5-         
Foreground �#060722,  &System.Windows.Controls.DataGridColumn   Header$ 
Логин��6:      Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 6h   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6I   ��  �� Path$ login�6h     5.       �#060722$ Пароль��6:   $	 10* 6l    6J   �� $ password�6l   50      �� 52   
   $��2��6   $��2��6"   $��Stretch=�62   .+�54      �� -
   5         ��Name$ RecordLabel��54      $/��)Добавить пользователя��6)   $��16(�6I   $��Medium"�6W   $��5,0,0,0q�6k   ���#fafafa6|   �� 56      $��
Логин��6   $��5,0,0,0q�6$   ���#fafafa65   �� 57      -   5        $ LoginTextBox��57      $��30��58      $��	20,0,20,0q�6   �� 5:      $��Пароль��6   $��5,0,0,0q�6%   ���#fafafa66   �� 5;      -   5        $ PasswordTextBox��5;      $��30��5<      $��	20,0,20,0q�6   �� 5>      $ 
Horizontal=�6   $��Center=�62   $��0,15,0,0q�6O   .+�5?      �� -   5        $ 	AddCommit��5?   :   #��[  6   $#��Добавить запись��6M   $��0, 5, 20, 0q�5@      6+   �� 5A      -   5        $ AddRollback��5A   :   #��[  6   $��Отменить��6O   $��
0, 5, 0, 0q�5B      6*   5C      5D      5E      5F      
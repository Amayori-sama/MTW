   M S B A M L   `   `   `  ����>  :MTW, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MTW.Warehouse.PagesMTW.Warehouse.Pages  5      +    %MTW.Warehouse.Pages.WarehouseCountrys   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:MTW.Warehouse.Pages     5      -   5          N� Title$  Страны��5	      .��5
      �  � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5      $	��53* �6   �� 5      -   5         ��Name$ ColumnChange��5      $	��270 �65    � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      $��60 �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��0��6"   $��3��62    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $
��0,10q�6z   .+�5      �� -   5         ��Name$ 	RecordAdd��5   6      MainBtn#��[  6   $��Добавить��6I   $��5,5,0,5q�5      5      �� 5      -   5        $ 
RecordCopy��5   6   #��[  6   $��Копировать��6J   $��5,5,0,5q�5      5      �� 5      -   5        $ 
RecordEdit��5   6   #��[  6   $��Изменить��6J   $��5,5,0,5q�5      5      �� 5       -   5        $ RecordDellete��5    6   #��[  6   $��Удалить��6M   $��5,5,0,5q�5!      5"      �� 5#      $��3��6   $��30��6"   ���#fafafa$��20, 0, 20, 0q�6=   6U   �� 5$      -   5         ��Name$ FilterComboBox��5$      $	��150��6/   $��30��6;   �� 5%      -   5         ��Name$ FilterTextBox��5%      $��5,0,5,0q�6-   $	��170��6>   $��30��6J   6V   5&      &   System.Windows.Controls.DataGrid  5(   
   -	   5          Name$ PageGrid��5(   0   $��2��6   $��0��6    
������ 	   AutoGenerateColumns	 .  
   
IsReadOnly
 .    CanUserAddRows .     RowBackground �#fafafa   HorizontalGridLinesBrush �#060722   VerticalGridLinesBrush �#060722
������ $��0, 0, 10, 0q�5*   ,   $��2��6A      Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,         
Foreground �#060722,  &System.Windows.Controls.DataGridColumn   Header$ Название��6:      Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 6j   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6L   ��  �� Path$
 name�6j   5.      �� 50   
   $��2��6   $��2��6"   $��Stretch=�62   .+�52      �� -
   5         ��Name$ RecordLabel��52      $#��Добавить страну��6)   $��16(�6C   $��Medium"�6Q   $��5,0,0,0q�6e   ���#fafafa6v   �� 54      $��Название��6   $��5,0,0,0q�6'   ���#fafafa68   �� 55      -   5        $ NameTextBox��55      $��30��56      $��	20,0,20,0q�6   �� 58      $ 
Horizontal=�6   $��Center=�62   $��0,15,0,0q�6O   .+�59      �� -   5        $ 	AddCommit��59   :   #��[  6   $#��Добавить запись��6M   $��0, 5, 20, 0q�5:      6+   �� 5;      -   5        $ AddRollback��5;   :   #��[  6   $��Отменить��6O   $��
0, 5, 0, 0q�5<      6*   5=      5>      5?      5@      
   M S B A M L   `   `   `  ����>  :MTW, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MTW.MainWindowsMTW.MainWindows  5   	   $    MTW.MainWindows.ProductsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MTW.MainWindows     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � Title$% Просмотр товаров��5
   	   $	��720��6"   $
��1280��62   $	��720��6B   $
��1280��6O   .��5      � ��5   
   �� 5       �� ImageSource$ /images/welcome.jpg��6    �� Opacity$	 0.5Q�6;    � ColumnDefinitions 5   
   �� 5      $��0* �6   �� 5      $	��53* �6   �� 5      -   5         ��Name$ ColumnChange��5      $	��270 �65    � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      $��28 �6   �� 5      $��22 �6   �� 5      $��* �6   .+�5   
   �� $��1��6   $��0��6"   $��3��62    �� Orientation$ 
Horizontal=�6F   $��Center=�6_   $��0,0,-0.333,10q�6z   $��2��6�   .+�5      �� -   5         ��Name$ FilterComboBox��5      $	��150��6/   $��30��6;   �� 5      -   5        	 ��Name$	 FilterTextBox��5      $��5,0,5,0q�6-   $	��170��6>   $��30��6J   6V   5      &   System.Windows.Controls.DataGrid  5    
   -   5        
  Name$
 PageGrid��5    0   $��3��6   $��0��6    
������    AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .     RowBackground �#fafafa   HorizontalGridLinesBrush �#060722   VerticalGridLinesBrush �#060722
������ $��0, 0, 10, 0q�5"   ,   $��2��6A      Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$         
Foreground �#060722,  &System.Windows.Controls.DataGridColumn   Header$ Тип��6:      Width5  /System.Windows.Controls.DataGridLengthConverter$	 10* 6k   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6G   ��  �� Path$ 
types.name�6k     5%       �#060722$ Модель��6:   $	 10* 6o    6J   �� $ models.name�6o     5&       �#060722$ Название��6:   $	 10* 6j    6L   �� $
 name�6j     5'       �#060722$ Описание��6:   $	 10* 6q    6L   �� $ description�6q     5(       �#060722$  Производитель��6;   $	 10* 6~    6R   �� $ manufacturers.name�6~     5)       �#060722$ Цена��6:   $	 10* 6f    6H   �� $
 cost�6f     5*       �#060722$ Кол-во,шт.��6:   $	 10* 6m    6N   �� $ count�6m     5+       �#060722$ Вес,кг��6:   $	 10* 6j    6J   �� $ weight�6j     5,       �#060722$ Высота,мм��6:   $	 10* 6m    6M   �� $ height�6m     5-       �#060722$ Ширина,мм��6:   $	 10* 6l    6M   �� $ width�6l     5.       �#060722$ Длина,мм��6:   $	 10* 6l    6L   �� $ length�6l     5/       �#060722$ 
Место��6:   $	 10* 6n    6I   �� $ places.name�6n     50       �#060722$ Фото��6:   $	 10* 6g    6H   �� $ image�6g   52       � 54   
   -   5          �Name$ 	PagePlace��54      55      56      
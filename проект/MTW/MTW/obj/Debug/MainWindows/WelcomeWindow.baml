   M S B A M L   `   `   `  ����>  :MTW, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MTW.MainWindowsMTW.MainWindows  5   	   #    MTW.MainWindows.WelcomeWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MTW.MainWindows     5   	     � 
ResizeMode$  NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	    � Title$ WelcomeWindow��5   	   $	��400��6   $	��700��6,   .��5      � ��5   
   �� 5       �� ImageSource$ /images/welcome.jpg��6    �� Opacity$	 0.5Q�6;   .+�5   
   �� $��Center=�6   $	��Top=�6-   
���� ��$��70(�6Y   $��0,50,0,0q�6g   $%��Добро пожаловать��6y   �� 5   
   -   5         ��Name$ AuthBtn��5   2      MainBtn#��[  6   $"��Редактирование��6C   $��20(�6\   $��Bottom=�5      $��Right=�6,   $	��200��6H   $��80��6T   $��	0,0,80,80q�6`    � ToolTip 5      _� 5      $	��300��6   $	��100��6&   
������ 
������ $	��Top=�6f   $��18(�6�   $��DemiBold"�6�   .��5      �� .+�5      �� $,��&Доступ к авторизации��6$   
���� ��6@   5      5      5      �� 5   
   -   5        $ SeeBtn��5   2   #��[  6   $%��Просмотр товаров��6B   $��20(�6]   $��Bottom=�5      $
��Left=�6,   $	��200��6G   $��80��6S   $��	80,0,0,80q�6_    5      _� 5      $	��300��6   $	��100��6&   
������ 
������ $	��Top=�6f   $��18(�6�   $��DemiBold"�6�   .��5      �� .+�5       �� $-��'Просмотр товаров без ��6$   
���� ��6A   �� 5!      $<��6возможности изменения данных��6$   
���� ��6H   5"      5#      5%      5&      5'      
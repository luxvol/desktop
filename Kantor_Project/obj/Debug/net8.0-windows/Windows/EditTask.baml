   M S B A M L   `   `   `  ����F  BLifePlanner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:LifePlanner.WindowsLifePlanner.Windows  5   	   "    LifePlanner.Windows.EditTask   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:LifePlanner.Windows     5   	     � Title$  EditTask��5   	   $	��300��6   $	��500��6'   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��DateTextBlock��5      $��0��6)   $��16(�66   $��10q�6D   $��Center=�6P   $��Center=�6k   �� 5   
   -   5        $��TaskTextBox��5      $��1��6%   $��10q�62   $��Stretch=�6>   $��Stretch=�6Z    �� AcceptsReturn$
 True��6x   $��30(�6�   �� 5   
   -   5        $
��Save��5      $��2��6    $	��100��6-   $��30��69   $��10q�6E   $��Center=�6R   $��Bottom=�6o   6�   5      5      
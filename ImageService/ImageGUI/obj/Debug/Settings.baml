   M S B A M L   `   `   `  ����C  ?ImageGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ImageGUIImageGUI  5   	       ImageGUI.Settings   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:ImageGUI     5   	   $��15(�5   *   $	��450��5   	   $	��800��6     � 	Resources  5	       	  Title �� Property �� Value��%�   &	        �� 5
   
   .�5      ��  � ��$ 30(�63   �� 5       � ��$
 Bold"�65   �� 5       � ��$ Center=�6>   �� 5       � ��
 ��  �65   5      5      .��5      �� .+�5   
   �� -   5         ��Name$ Setting��5      #��[  6!   .��6@   
Settings6J   � 5   
    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $	��150 �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��0��6%   $
��Bold"�65   Output Directory:6Z   �� 5       $��0��6   $��1��6%   ��65   �� *VM_OutputDir+�� 5!      $��1��6   $��0��6%   $
��Bold"�65   Source Name:6U   �� 5"      $��1��6   $��1��6%   ��65   �� *VM_SourceName+�� 5#      $��2��6   $��0��6%   $
��Bold"�65   	Log Name:6R   �� 5$      $��2��6   $��1��6%   ��65   �� *
VM_LogName+�� 5%      $��3��6   $��0��6%   $
��Bold"�65   Thumbnail Size:6X   �� 5&      $��3��6   $��1��6%   ��65   �� *VM_ThumbnailSize+5'      �� 5(   
   $��0,15,0,0q�6   $��50��6#   $��Stretch=�6/   ��6M   �� *RemoveHandlerCommand+.��6v   Remove6~   �� 5)   
   $
��Auto��6    �� SelectedItem 6    �� *SelectedHandler+ �� Mode$ TwoWay=���6V   �� *VM_Handlers+5*      5+      
   M S B A M L   `   `   `  ����C  ?ImageGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ImageGUI.ViewModelImageGUI.ViewModel  5          ImageGUI.Logs   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:ImageGUI.ViewModel     5        � 	Resources  5	      $  ImageGUI.ViewModel.TypeToColor   	TypeColor 	 Title �� Property �� Value��%�   &	        &	        5
   
   6   �� 5   
   .�5      ��  � ��$ 30(�63   �� 5       � ��$
 Bold"�65   �� 5       � ��$ Center=�6>   �� 5       � ��
 ��  �65   5      5      .��5      �� .+�5   
   �� #��[ 6   .��6/   Logs65   &   System.Windows.Controls.DataGrid  5   
      AutoGenerateColumns .  ��6   �� *	VM_logs+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Type��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   ��  �� Path$
 Type�	   ElementStyle	 5      �� 5      
 �� 
TargetType"
 ��6    .�5      ��  � �� 6;   �� $
 Type� �� 	Converter# [  5      5        5      $ Message��6%   $ * 66    6@   �� $ Message�5       5!      5"      
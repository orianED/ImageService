   M S B A M L   `   `   `  ���� C  ?ImageGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ImageGUIImageGUI      ImageGUI.Settings   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:ImageGUI     $��15(�$	��450��$	��800��.���� .+��� $��30(�$��Center=�$
��Bold"�.��
Settings�   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* � � ColumnDefinitions �� $	��150 ��� $��* �.+��� $��0��$��0��$
��Bold"�Output Directory:�� $��0��$��1������ *VM_OutputDir+�� $��1��$��0��$
��Bold"�Source Name:�� $��1��$��1������ *VM_SourceName+�� $��2��$��0��$
��Bold"�	Log Name:�� $��2��$��1������ *
VM_LogName+�� $��3��$��0��$
��Bold"�Thumbnail Size:�� $��3��$��1������ *VM_ThumbnailSize+�� $��0,15,0,0q�$��50��$��Stretch=����� *RemoveHandlerCommand+.��Remove�� $
��Auto�� �� SelectedItem �� *SelectedHandler+ �� Mode$ TwoWay=����� *VM_Handlers+
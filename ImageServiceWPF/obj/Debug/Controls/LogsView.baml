   M S B A M L   `   `   `  ����J  FImageServiceWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ImageServiceWPF.ControlsImageServiceWPF.Controls  5      '    !ImageServiceWPF.Controls.LogsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:ImageServiceWPF.Controls     5        � 	Resources  5	      )  #ImageServiceWPF.Controls.LogConvert   
LogConvert��%   &	          5
   
   60   5      .��5      �� .+�5   
   �� $��Center=�6   $
��Logs��6-   $��25(�6<   $
��Bold"�6J   
���  ��$��10q�6m   &   System.Windows.Controls.DataGrid  5   
      AutoGenerateColumns .     CanUserAddRows .  "   VerticalScrollBarVisibility$
 Auto=�6l   ��. $
��Auto��6�   $
��Auto��6�   ��6   �� *VM_LogEntries+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Type��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 63      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6R   �� 	 �� Path$	 Status�
   ElementStyle
 5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property � �� �� Value 6;   �� $	 Status� �� 	Converter# [  5      5      5        5      $ Message��6%   $ * 66    .  6S   �� *	Message+5      5      5      5      
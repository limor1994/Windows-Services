   M S B A M L   `   `   `  ����J  FImageServiceWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ImageServiceWPF.ControlsImageServiceWPF.Controls  5   	        ImageServiceWPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   5controls&clr-namespace:ImageServiceWPF.Controls  5   	     � Title$  Image Service��5	   	   $	��450��6   $	��800��6,   .  (System.Windows.Interactivity.Interaction   Triggers 5
      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ 
Disconnect��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *DisconnectCommand+6L   5      .��5      � .+�5   
   �� ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value
 �����63    �� Triggers 5      �� 5      	 �� Binding	 6&   �� *VM_IsConnected+
 �� Value	
 False6I   .a�5      ��  � ��
 �����6;   5      5      5      �� 5   
    �� TabStripPlacement$	 Top=�6   $��Stretch=�6-   .@�5      �� $��Settings��6   $
��Auto��6(   .��5      +   %ImageServiceWPF.Controls.SettingsView  5      �� 5       $
��Logs��6   .��5!      '   !ImageServiceWPF.Controls.LogsView  5"      5#      5$      5%      
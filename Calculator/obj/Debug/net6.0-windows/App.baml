   M S B A M L   `   `   `  ����E  ACalculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Calculator
Calculator  5          Calculator.App   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      *localclr-namespace:Calculator     5        �� 	Resources  5         Calculator.MainWindow �� 
TargetType �� Property �� Value   OperationLabel  ResultLabel  CommonButtonStyle  OrangeButtonStyle �� BasedOn �� 
TargetType A�Name �� Triggers P� Property	 P� Value
 �� 
TargetName N� EnterActions �� 
TargetName �� TargetProperty	 �� To g� Duration  DarkGrayButtonStyle  LightGrayButtonStyle��%]
  '        &	  N     &	 2    &	     &	     7  &	 m    7  &	 �    7  �� 5   
   "  6   .�5	      ��  � �� �#0000006+   5
      �� 5   
   " ��6'   .�5      ��  � ��$ Right=�64   �� 5       � ��$ Bottom=�62   �� 5       � ��$ Arial)�6+   �� 5       � ��$ 10(�6)   �� 5       � ��
 �����6+   5      �� 5   
   " ��6$   .�5      ��  � ��$ Right=�64   �� 5       � ��$ Bottom=�62   �� 5       � ��$ Arial)�6+   �� 5       � ��$ 50(�6)   �� 5       � ��
 �����6+   5      �� 5   
   " ��6*   .�5      ��  � ��$ Arial)�6+   �� 5        � ��$ Center=�6;   �� 5!       � ��$ Center=�69   �� 5"       � ��$ 5q�6'   �� 5#       � ��
 �����6+   �� 5$       � ��$ 26(�6)   5%      �� 5'   
   " ��6*   8   .�5(      ��  � �� 5)      �� 5*      " ��6&   .R�5+      � .+�5,      A� $ ButtonBackground��6&   ���#FFA5006@   �� 5-      $��Center=�6/   $��Center=�6L   5.       5/      P� 50       � ��$
	 True��6;   . �51   "   �� $
 ButtonBackground��6)    � �� �#FFFFFF6W    52   "   �� 53   &   .��54   *   �� .��55   .   �� $ ButtonBackground��6=   $* $(Shape.Fill).(SolidColorBrush.Color)�56   5    #FFA50057   5   $ 0:0:0.2D�6B   58   +   59   '   5;      5=      5?      5@      �� 5B   
   " ��6,   8   .�5C      ��  � �� 5D      �� 5E      " ��6&   .R�5F      � .+�5G      A� $ ButtonBackground��6&   ���#3F3F3F6@   �� 5H      $��Center=�6/   $��Center=�6L   5I       5J      P� 5K       � ��$
	 True��6;   . �5L   "   �� $
 ButtonBackground��6)    � �� �#D3D3D36W    5M   "   �� 5N   &   .��5O   *   �� .��5P   .   �� $ ButtonBackground��6=   $* $(Shape.Fill).(SolidColorBrush.Color)�5Q   5    #3F3F3F5R   5   $ 0:0:0.2D�6B   5S   +   5T   '   5V      5X      5Z      5[      �� 5]   
   " ��6-   8   .�5^      ��  � �� 5_      �� 5`      " ��6&   .R�5a      � .+�5b      A� $ ButtonBackground��6&   ���#D3D3D36@   �� 5c      $��Center=�6/   $��Center=�6L   5d       5e      P� 5f       � ��$
	 True��6;   . �5g   "   �� $
 ButtonBackground��6)    � �� �#FFFFFF6W    5h   "   �� 5i   &   .��5j   *   �� .��5k   .   �� $ ButtonBackground��6=   $* $(Shape.Fill).(SolidColorBrush.Color)�5l   5    #D3D3D35m   5   $ 0:0:0.2D�6B   5n   +   5o   '   5q      5s      5u      5v      5z      5{      
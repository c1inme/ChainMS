   M S B A M L   `   `   `  ����U  QSystem.Windows, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aG4clr-namespace:System.Windows;assembly=System.WindowsSystem.Windows  5      M IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089#   Fluent.ObjectToImageConverter   StringToImageConvert  RibbonButtonControlTemplate  �� 
TargetType  Fluent.Button ��Name  TransparentBrush ��Name �� Orientation ��Name J� SnapsToDevicePixels �� RelativeSource �� 	Converter  Fluent.TwoLineLabel  Name	   Header
   Text  TwoLineLabelStyle �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value ! ButtonPressedOuterBorderBrush % !ButtonPressedOuterBackgroundBrush  Fluent.GrayscaleEffect ]� 
Conditions �� Property �� Value # ButtonHoverOuterBackgroundBrush  ButtonHoverOuterBorderBrush  RibbonButtonStyle �� 
TargetType 	 DefaultFontSize 
 DefaultFontFamily  DefaultFontBrush  ControlStyleEmptyFocus �� Triggers  Fluent.RibbonControl �� BasedOn ( $IronicallyNamedChromelessButtonStyle  TransparentWhiteBrush �Name J� Opacity�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      <vsm4clr-namespace:System.Windows;assembly=System.Windows  5      Fluentclr-namespace:Fluent  5      8d2http://schemas.microsoft.com/expression/blend/2008  6'   Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  6d   %�  &	        &	      7   7   7   &	 m    7  '  \    7  &	 ~       5      6"   �� 5      "   68   .R�5      �� $ border��6   #���  6   #���  6M   $
��Auto��6~   $
��Left=�6�   $��Stretch=�6�   ._�5      � $
��Auto��6   .+�5	      �� $ 
stackPanel��6   $
��Auto��6+   $ Vertical=�68   .+�5
      �� $ 	iconImage��6   $��Center=�62   $��32��6O   $��Center=�6[   $��32��6�   $��3,2,3,1q�6   . ��6v   �� *	LargeIcon+ �� *TemplatedParent+8   6    5      $ controlLabel��6"   #
 z	 68   #���  6X   $��Stretch=�6�   $��Center=�6�   $��2,-1,2,0q�6�   5      5      5       5      P� 5       �@ Size$ Small=�6    . �5   
   �� $ 
stackPanel��6(    �@��Orientation$ 
Horizontal=�6@   �� 5   
   $ controlLabel��6'    � y�$ 	Collapsed=�6A   �� 5   
   $ controlLabel��6(    �@ HasTwoLines .  6B   �� 5   
   $ 	iconImage��6"    � ��$ 16��69   �� 5   
   $ 	iconImage��6#    � ��$ 16��6:   �� 5   
   $ 	iconImage��6#    � ��$ 1,0,2,0q�6:   �� 5   
   $ 	iconImage��6$    6;   �� *Icon+ �� *TemplatedParent+8   � ���� 5   
   $ controlLabel��6#    � ��$ 2,-1,2,1q�6=   5   	   P� 5       �@ Size$ Middle=�6    . �5   
   �� $ 
stackPanel��6(    �@��Orientation$ 
Horizontal=�6@   �� 5   
   $ 
stackPanel��6"    � ��$
 Auto��6:   �� 5   
   $ 	iconImage��6.    � ��$ Center=�6E   �� 5   
   $ controlLabel��6(    �@ HasTwoLines .  6B   �� 5   
   $ controlLabel��6#    � ��$ 2,-1,2,1q�6=   �� 5    
   $ border��6.    � ��$ Stretch=�6B   �� 5!   
   $ 	iconImage��6"    � ��$ 16��69   �� 5"   
   $ 	iconImage��6#    � ��$ 16��6:   �� 5#   
   $ 	iconImage��6#    � ��$ 1,0,2,0q�6:   �� 5$   
   $ 	iconImage��6$    6;   �� *Icon+ �� *TemplatedParent+8   � ���� 5%   
   $ controlLabel��6.    � ��$ Center=�6H   5&   	   P� 5'       � �� . . �5(   
   �� $ border��6(   # �  6<    � ���� 5)   
   $ border��6'   # �  6;    � ��5*   	   P� 5+       � {� .  . �5,   
   �� $ 	iconImage��6$    �@J�Opacity$	 0.5Q�6;   �� 5-   
   $ controlLabel��6$    �@J�Opacity$	 0.5Q�6>   �� 5.   
   $ 	iconImage��6#    �@J�Effect 5/        50      52      53   	   ]� 54       55   
   �� 56       �@J�IsMouseOver . 6-   �� 57       � �� .  6+   �� 58       � {� . 6+   ./�5:   
   �� $ border��6'   # �  6;    � ���� 5;   
   $ border��6(   # �  6<    � ��5<   	   ]� 5=       5>   
   �� 5?       �@J�IsKeyboardFocusWithin . 67   �� 5@       � �� .  6+   ./�5B   
   �� $ border��6'   # �  6;    � ���� 5C   
   $ border��6(   # �  6<    � ��5D   	   5F      �� 5G      "  6$   .�5H      ��  � ��8   6!   �� 5I       � ��$ 66��6   �� 5J       � ��$
 Auto��6   �� 5K       � ��# � 	 6!   �� 5L       � ��# � 
 6#   �� 5M       � ��# �  6#   �� 5N       �@�FocusVisualStyle# �  6)   �� 5O       �@J�SnapsToDevicePixels .  6,    5P      P� 5Q       �@ Size$ Large=�65   . �5R   
   ��  � ��$ 66��6#   5S   	   P� 5T       �@ Size$ Middle=�65   . �5U   
   ��  � ��$ 22��6#   �� 5V   
    � ��$
 Left=�60   5W   	   P� 5X       �@ Size$ Small=�65   . �5Y   
   ��  � ��$ 22��6#   �� 5Z   
    � ��$ 22��6"   �� 5[   
    � ��$
 Left=�60   5\   	   5^      �� 5`      "  6
   8   6.   �� 5b      " ��67   .�5c   
   ��  � ��# �  6'   �� 5d   
    � ��# � ��6'   �� 5e   
    � ��$ Center=�67   �� 5f   
    � ��$ Center=�65   �� 5g   
    � ��$ 1q�6$   �� 5h   
    � �� 5i      �� 5j      "  ��6"   .R�5k      � $
 grid��6   #��z��6)   .+�5l      �� $ contentPresenter��6+   #��z��5m   +   #��z��5n   +   #��z��5o   +   # z 5p   +   ��. $
 0.75Q�5r   +   5s       5t      P� 5u       �@J�IsMouseOver . . �5v      �� $ contentPresenter��6%    �@J�Opacity$ 1Q�6V   �� 5w      $
 grid��6%   # �  6M    � ��5x      P� 5y       �@J�IsMouseOver .  . �5z      �� $ contentPresenter��6%    �@J�Opacity$ .5Q�6V   5{      P� 5|       � �� . . �5}      �� $
 grid��6%   # �  6M    � ��5~      P� 5       � {� .  . �5�      ��  � �� �#ADADAD6;   5�      5�      5�      5�      5�      
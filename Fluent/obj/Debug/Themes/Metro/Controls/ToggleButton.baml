   M S B A M L   `   `   `  ����M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089#    Fluent.ObjectToImageConverter   StringToImageConvert  RibbonToggleButtonStyle  �� 
TargetType   Fluent.ToggleButton �� Property % !RibbonToggleButtonControlTemplate �� Value  DefaultFontSize  DefaultFontFamily  DefaultFontBrush  ControlStyleEmptyFocus �� Triggers   Fluent.RibbonControl P� Property P� Value  ButtonCheckedBrush �� Opacity �� Color �� RelativeSource	 �� AncestorType   Fluent.MetroWindow
 �� 
TargetType ��Name  TransparentBrush ��Name �� Orientation ��Name J� SnapsToDevicePixels �� 	Converter   Fluent.TwoLineLabel  Name   Header   Text 	 TwoLineLabelStyle �� Triggers �� 
TargetName �� Path !
 ButtonPressedOuterBorderBrush % !ButtonPressedOuterBackgroundBrush   Fluent.GrayscaleEffect ]� 
Conditions �� Property �� Value # ButtonHoverOuterBackgroundBrush  ButtonHoverOuterBorderBrush�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      Fluentclr-namespace:Fluent  5      8d2http://schemas.microsoft.com/expression/blend/2008  6'   Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  6d   %/  &	        &	      &	 �    &	 �    7   7   7      5      6"   �� 5      "   6*   .�5      ��  � ��# �  6!   �� 5       � ��$ 66��6   �� 5       � ��$
 Auto��6   �� 5	       � ��# �  6!   �� 5
       � ��# �  6#   �� 5       � ��# �  6#   �� 5       �@�FocusVisualStyle# �  6)    5      P� 5       �@ Size$ Large=�65   . �5   
   ��  � ��$ 66��6#   5   	   P� 5       �@ Size$ Middle=�65   . �5   
   ��  � ��$ 22��6#   5   	   P� 5       �@ Size$ Small=�65   . �5   
   ��  � ��$ 22��6#   �� 5   
    � ��$ 22��6"   5   	   5      �� 5      $	 0.2Q�6�    6/   �� *RibbonThemeColor.Color+ �� *FindAncestor+"	  6�   �� 5      "
  6>   .R�5      �� $ border��6   $��1q�6   #���  61   #���  6a   $
��Auto��6�   $
��Left=�6�   $��Stretch=�6�   ._�5      � $
��Auto��6	   .+�5      �� $ 
stackPanel��6   $
��Auto��6+   $ Vertical=�68   .+�5      �� $ 	iconImage��6   $��Center=�62   $��32��6O   $��Center=�6[   $��32��6�   $��2q�6   . ��6v   �� *	LargeIcon+ �� *TemplatedParent+8   6    5       $ controlLabel��6"   # z 68   #��� 	 6X   $��Stretch=�6�   $��Stretch=�6�   $��2,-3,2,1q�6�   5!      5"      5#       5$      P� 5%       �@ Size$ Small=�65   . �5&   
   �� $ 
stackPanel��6(    �@��Orientation$ 
Horizontal=�6@   �� 5'   
   $ controlLabel��6'    � y�$ 	Collapsed=�6A   �� 5(   
   $ controlLabel��6(    �@ HasTwoLines .  6B   �� 5)   
   $ 	iconImage��6"    � ��$ 16��69   �� 5*   
   $ 	iconImage��6#    � ��$ 16��6:   �� 5+   
   $ 	iconImage��6#    � ��$ 1,0,2,0q�6:   �� 5,   
   $ 	iconImage��6$    6;   �� $
 Icon� �� *TemplatedParent+8   � ���� 5-   
   $ controlLabel��6#    � ��$ 2,-2,2,2q�6=   5.   	   P� 5/       �@ Size$ Middle=�65   . �50   
   �� $ 
stackPanel��6(    �@��Orientation$ 
Horizontal=�6@   �� 51   
   $ 
stackPanel��6"    � ��$
 Auto��6:   �� 52   
   $ 	iconImage��6.    � ��$ Center=�6E   �� 53   
   $ controlLabel��6(    �@ HasTwoLines .  6B   �� 54   
   $ controlLabel��6#    � ��$ 2,2,2,2q�6=   �� 55   
   $ border��6.    � ��$ Stretch=�6B   �� 56   
   $ 	iconImage��6"    � ��$ 16��69   �� 57   
   $ 	iconImage��6#    � ��$ 16��6:   �� 58   
   $ 	iconImage��6#    � ��$ 1,0,2,0q�6:   �� 59   
   $ controlLabel��6.    � ��$ Center=�6H   �� 5:   
   $ 	iconImage��6$    6;   �� $
 Icon� �� *TemplatedParent+8   � ��5;   	   P� 5<       � �� . . �5=   
   �� $ border��6(   # � 
 6<    � ���� 5>   
   $ border��6'   # �  6;    � ��5?   	   P� 5@       �@d�	IsChecked$
 TrueY�6%   . �5A   
   �� $ border��6(   # �  6<    � ���� 5B   
   $ border��6'   # �  6;    � ��5C   	   P� 5D       � {� .  . �5E   
   �� $ 	iconImage��6$    �@J�Opacity$	 0.5Q�6;   �� 5F   
   $ controlLabel��6$    �@J�Opacity$	 0.5Q�6>   �� 5G   
   $ 	iconImage��6#    �@J�Effect 5H        5I      5K      5L   	   ]� 5M       5N   
   �� 5O       �@J�IsMouseOver . 6-   �� 5P       � �� .  6+   ./�5R   
   �� $ border��6'   # �  6;    � ���� 5S   
   $ border��6(   # �  6<    � ��5T   	   ]� 5U       5V   
   �� 5W       �@J�IsKeyboardFocusWithin . 67   �� 5X       � �� .  6+   ./�5Z   
   �� $ border��6'   # �  6;    � ���� 5[   
   $ border��6(   # �  6<    � ��5\   	   ]� 5]       5^   
   �� 5_       �@J�IsMouseOver . 6-   �� 5`       � �� .  6+   �� 5a       �@d�	IsChecked$
 TrueY�6+   ./�5c   
   �� $ border��6'   # �  6;    � ���� 5d   
   $ border��6(   # �  6<    � ��5e   	   ]� 5f       5g   
   �� 5h       �@J�IsKeyboardFocusWithin . 67   �� 5i       � �� .  6+   �� 5j       �@d�	IsChecked$
 TrueY�6+   ./�5l   
   �� $ border��6'   # �  6;    � ���� 5m   
   $ border��6(   # � 
 6<    � ��5n   	   5p      5q      
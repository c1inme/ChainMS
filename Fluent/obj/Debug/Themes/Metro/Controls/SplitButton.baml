   M S B A M L   `   `   `  ����M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source#    Fluent.ObjectToImageConverter   StringToImageConvert  RibbonSplitButtonStyle �� 
TargetType   Fluent.SplitButton �� Property $  RibbonSplitButtonControlTemplate �� Value  DefaultFontSize  DefaultFontFamily  DefaultFontBrush  ControlStyleEmptyFocus �� Triggers   Fluent.DropDownButton P� Property P� Value  MenuSeparatorStyle �� BasedOn	 �� 
TargetType  TransparentBrush
 �� Orientation   Fluent.ToggleButton  Name   Command   CommandParameter   CommandTarget 	 InnerButtonStyle   IsButtonEnabled   Header   Header � ToolTip   IsDefinitive   IsDefinitive'    CanAddButtonToQuickAccessToolBar!   CanAddToQuickAccessToolBar   	LargeIcon �� RelativeSource �� 	Converter   Icon d� 	IsChecked �� Mode   Fluent.TwoLineLabel  Name   Text 
 TwoLineLabelStyle   HasTriangle    HasGlyph! �Name"   IsDropDownOpen# � AllowsTransparency$ J� SnapsToDevicePixels% � RowDefinitions& � ColumnDefinitions' ��Name( � ActualWidth ! ContextMenuBarBackgroundBrush  ContextMenuBarBorderBrush) �� CornerRadius* �� Stretch+ ��Name  MenuScrollViewer,   MaxDropDownHeight   Fluent.MenuPanel-  Name. K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation/   DirectionalNavigation # ContextMenuBarResizeBorderBrush & "ContextMenuBarResizeBackgoundBrush0 n�Name1 � Cursor & "ResizeVerticalThumbControlTemplate " ResizeBothThumbControlTemplate2 �� Triggers3 �� 
TargetName ! ButtonPressedOuterBorderBrush ! ButtonPressedInnerBorderBrush % !ButtonPressedInnerBackgroundBrush4 ]� 
Conditions5 �� 
SourceName6 �� Property7 �� Value  ButtonHoverInnerBorderBrush  ButtonHoverOuterBorderBrush  InnerButtonControlTemplate8 ��Name9 ��Name: ��Name % !ButtonPressedOuterBackgroundBrush # ButtonHoverOuterBackgroundBrush   Fluent.GrayscaleEffect�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      Fluentclr-namespace:Fluent  5      8d2http://schemas.microsoft.com/expression/blend/2008  5      Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5        5      �� 5   
   $[ Upack://application:,,,/Fluent;component\Themes\Office2010\Controls\MenuSeparator.xaml?�6   %�*  &	        &	      '�� �    7  &	 �    7   7   &		 k    &	 J       5
      6$   �� 5      "  6+   .�5   
   ��  � ��# �  6%   �� 5   
    � ��$ 66��6#   �� 5   
    � ��$
 Auto��6"   �� 5   
    � ��# �  6%   �� 5   
    � ��# �  6'   �� 5   
    � ��# �  6'   �� 5   
    �@�FocusVisualStyle# �  6-   �� 5   
    �@J�SnapsToDevicePixels . 60    5   
   P� 5       �@ Size$ Large=�6&   . �5      ��  � ��$ 66��6+   5      P� 5       �@ Size$ Middle=�6&   . �5      ��  � ��$ 22��6+   5      P� 5       �@ Size$ Small=�6&   . �5      ��  � ��$ 22��6+   �� 5       � ��$ 34��6*   5      5       �� 5!      8   " ��67   �� 5"      "	  6?   .R�5#   
   � .+�5$      �� $��PART_ButtonBorder��6)   $��1q�6   #���  6B   #���  6s   ._�5%      �� $��
stackPanel��61   $
 Vertical=�6   .+�5&        $ PART_Button��6$   #��z 5'   .   #��z 5(   .   #��z 5)   .   $��0,0,0,0q�5*      $
��Auto��5+   .   $
��Auto��5,   .   #��� 	 5-   .   #{�z 50   .   # z 51   .   # z 52   .   # z 54   .   # z 55   .    5.   .   �� *	LargeIcon+ �� *TemplatedParent+8    5/   .   �� *Icon+ �� *TemplatedParent+8   53   .   �� *	IsChecked+$ TwoWay=� �� *TemplatedParent+55   .   �� 56      $��
downBorder��6(   $��0,1,0,0q�6   #���  6:   #���  6k   $��0,0,0,0q�6�   ._�57      � .+�58   	     $ controlLabel��6   # z 63   #��� 
 6S   $��Center=�59   2   $��Center=�6O   $��2,0,2,0q�6j   #  z 5:   2   5>   	   5?      5@      5A      � 5B      $! 
PART_Popup��6   $��Bottom=�6   #��z" 62   # . |�.  $��Slide=�5C   @   $��0,0,-4,0q�5D      $	��Top=�6&   $��2��6>   .��5E      � $��DropDown��6   $ . $��0q�5G      $	��Top=�5H      % 5I      �� 5J      $��* �6(   �� 5K      $��10 �6(   & 5M      �� 5N      $��* �6+   �� 5O      $��10 �6+   .+�5Q      �� $
��Left=�6    $	��Top=�5R       $��4��5S       $��4��5T       $��1��5U       $��1��5V       $��0��5W       $��	-1,-1,0,0q�5X       5Z      �� 5[      $��0��6    $
��Left=�5\       $��-1,3,0,0q�5]       $	��Top=�5^       $��4��5_       $��4��5`       $��1��5a       $��0��5b       5d      �� 5e      $��0��6    $
��Left=�5f       $��3,-1,0,0q�5g       $	��Top=�5h       $��4��5i       $��4��5j       $��0��5k       $��1��5l       5n      �� 5o      $��0��6    $��Stretch=�5p       $��7,-1,1,0q�5q       $	��Top=�5r       $
��Auto��5s       $��4��5t       $��0��5u       $��1��5v       5x      �� 5y      $��0��6    $
��Left=�5z       $��-1,7,0,1q�5{       $��Stretch=�5|       $��4��5}       $
��Auto��5~       $��1��5       $��0��5�       5�      �� 5�      $' PART_DropDownBorder��6   #��z( 5�      $ . #���  5�      #���  5�      $��1q�5�      $) 1��5�      $��1��5�      $��Stretch=�5�      $	��Top=�5�      ._�5�      � $
��Auto��6   $
��Auto��5�      $��Stretch=�5�      $	��Top=�5�      .+�5�      �� $	��Top=�6%   $
��Auto��5�   %   $
��Auto��5�   %   #���  5�   %   $��1q�5�   %   � 5�      $��1q�6#   $��Stretch=�5�   #   $	��Top=�5�   #   % 5�   "   �� 5�   &   $��* �64   �� 5�   &   $
��Auto �64   .+�5�   "   �� $
* Fill=�5�   ,   ���	#FFE2E4E7$
��Left=�5�   ,   $��24,0,0,0q�5�   ,   $��1��5�   ,   $
��Auto��5�   ,   $��2��5�   ,   $y�Visible=�5�   ,   $��Stretch=�5�   ,   ��5�   ,   Z� 5�   ,   �� 5�   "   $+ PART_ScrollViewer��6/   #���  5�   /   $��1q�5�   /   #��z, 5�   /   $ . $
��Auto=�5�   /   $��Hidden=�5�   /   .��5�   &     $- PART_MenuPanel��67   . . $/ Cycle=�5�   7   5�   #   �� 5�   "   $' resizeBorder��6)   $��Stretch=�5�   )   $	��Top=�5�   )   $��11��5�   )   #���  5�   )   $��0,1,0,0q�5�   )   $��1��5�   )   $) 0,0,1,1��5�   )   #���  5�   )   ._�5�   &   � .+�5�   *   n� $0 PART_ResizeVerticalThumb��60   $��Stretch=�5�   0   $��	0,0,0,-90q�5�   0   $
��Auto��5�   0   $��10��5�   0   $	��Top=�5�   0   $1 SizeNS��5�   0   #���  5�   0   n� 5�   *   $0 PART_ResizeBothThumb��60   $��Right=�5�   0   $��	0,0,0,-90q�5�   0   $��10��5�   0   $��10��5�   0   $	��Top=�5�   0   $1 SizeNWSE��5�   0   #���  5�   0   5�   '   5�   #   5�      5�      5�      5�      5�      5�      2 5�   
   P� 5�       �@ 
ResizeMode$
 None=�6,   . �5�      �� $3 resizeBorder��6/    � y�$ 	Collapsed=�6I   5�      P� 5�       �@ 
ResizeMode$
 Both=�6,   . �5�      �� $3 PART_ResizeVerticalThumb��5�       � y�$ 	Collapsed=�5�      �� 5�      $3 PART_ResizeBothThumb��5�       � y�$ Visible=�5�      �� 5�      $3 resizeBorder��5�       � y�$ Visible=�5�      5�      P� 5�       �@ 
ResizeMode$ Vertical=�5�      . �5�      �� $3 PART_ResizeBothThumb��5�       � y�$ 	Collapsed=�5�      �� 5�      $3 PART_ResizeVerticalThumb��5�       � y�$ Visible=�5�      �� 5�      $3 resizeBorder��5�       � y�$ Visible=�5�      5�      P� 5�       �@ 	IsChecked$
 TrueX�6+   . �5�      �� $3 PART_ButtonBorder��60   # �  6O    � ���� 5�      $3 PART_ButtonBorder��6/   # �  6N    � ��5�      P� 5�       �@ Size$ Small=�6&   . �5�      �� $3 PART_Button��6)    �@ Size$ Small=�6B   �� 5�      $3 
stackPanel��6$    �@��Orientation$ 
Horizontal=�6<   �� 5�      $3 controlLabel��6    � ��$ 10��68   �� 5�      $3 controlLabel��6    � ��$ 	0,-19,0,0q�69   �� 5�      $3 controlLabel��6    �@ Text$  ��67   �� 5�      $3 
downBorder��6    � ��$ 0q�67   5�      P� 5�       �@ Size$ Middle=�6   . �5�      �� $3 PART_Button��6)    �@ Size$ Middle=�6B   �� 5�      $3 
stackPanel��6$    �@��Orientation$ 
Horizontal=�6<   �� 5�      $3 controlLabel��6    � ��$ 10��68   �� 5      $3 controlLabel��6    � ��$ 	0,-19,0,0q�69   �� 5     $3 controlLabel��6    �@ Text$  ��67   �� 5     $3 
downBorder��6    � ��$ 0q�67   5     P� 5      �@ IsButtonEnabled .  . �5     �� $3 PART_Button��5      � {� .  5	     5
     P� 5      �@ IsDropDownOpen . . �5     �� $3 PART_ButtonBorder��5     # �  5      � ���� 5     $3 PART_ButtonBorder��5     # �  5      � ��5     ]� 5     4 5     �� 5     $5 PART_Button��5     6 �@J�IsMouseOver7 . �� 5     6 �@ IsDropDownOpen7 .  5      ./�5     �� $3 PART_ButtonBorder��5     # �  5      � ��5     ]� 5      4 5!     �� 5"     $5 
downBorder��5$     6 �@J�IsMouseOver7 . �� 5%     6 �@ IsDropDownOpen7 .  5&      ./�5(     �� $3 PART_ButtonBorder��5)     # �  5*      � ���� 5+     $3 
downBorder��5,     # �  5-      � ���� 5.     $3 
downBorder��5/     # �  50      � ��51     53     �� 56     "  57     .�58  
   ��  � ��# �  59     �� 5:  
    � ��$ Stretch=�5;     �� 5<  
    � ��$ Stretch=�5=     �� 5>  
    �@�FocusVisualStyle# �  5?     5@     �� 5B     "	  5C     .R�5D  
   �� $' border��6   $��1q�5E     #���  5F     #���  5G     $
��Auto��5H     $��Stretch=�5I     $��Stretch=�5J     $ .  ._�5L     � $
��Auto��6   .+�5M     �� $8 	rectangle��6    $��0��5N      $y�	Collapsed=�5O      5P     �� 5Q     $9 
stackPanel��6!   $
��Auto��5R  !   $
 Vertical=�5S  !   $��Stretch=�5U  !   $ . .+�5W     �� $: 	iconImage��6+   $��Center=�5X  +   $��32��5Y  +   $��Center=�5Z  +   #��z 5[  +   $��32��5\  +   $ . 5]  +     5^     $ controlLabel��6.   # z 5_  .   #��� 
 5`  .   $��Stretch=�5a  .   $��Center=�5b  .   $��2,-1,2,2q�5c  .   $y�	Collapsed=�5d  .   5e     5f     5g     2 5h  
   P� 5i      � �� . . �5k     �� $3 border��5l     # �  5m      � ���� 5n     $3 border��5o     # �  5p      � ��5q     ]� 5r     4 5s     �� 5t     6 �@J�IsMouseOver7 . 5u      �� 5v     6 � ��7 .  5w      �� 5x     6 � {�7 . 5y      ./�5{     �� $3 border��5|     # �  5}      � ���� 5~     $3 border��5     # �  5�      � ��5�     ]� 5�     4 5�     �� 5�     6 �@J�IsKeyboardFocusWithin7 . 5�      �� 5�     6 � ��7 .  5�      �� 5�     6 � {�7 . 5�      ./�5�     �� $3 border��5�     # �  5�      � ���� 5�     $3 border��5�     # �  5�      � ��5�     P� 5�      � {� .  . �5�     �� $3 	iconImage��5�      �@J�Opacity$	 0.5Q�5�     �� 5�     $3 controlLabel��5�      �@J�Opacity$	 0.5Q�5�     �� 5�     $3 	iconImage��5�      �@J�Effect 5�       5�     5�     5�     P� 5�      �@ Size$ Small=�5�     . �5�     �� $3 
stackPanel��5�      �@��Orientation$ 
Horizontal=�5�     �� 5�     $3 controlLabel��5�      � y�$ 	Collapsed=�5�     �� 5�     $3 controlLabel��5�      �@ HasTwoLines .  5�     �� 5�     $3 	iconImage��5�      � ��$ 16��5�     �� 5�     $3 	iconImage��5�      � ��$ 16��5�     �� 5�     $3 	iconImage��5�      � ��$ 1,0,2,0q�5�     �� 5�     $3 	iconImage��5�      5�     �� *Icon+ �� *TemplatedParent+ � ���� 5�     $3 controlLabel��5�      � ��$ 2,-1,2,1q�5�     5�     P� 5�      �@ Size$ Middle=�5�     . �5�     �� $3 
stackPanel��5�      �@��Orientation$ 
Horizontal=�5�     �� 5�     $3 
stackPanel��5�      � ��$
 Auto��5�     �� 5�     $3 	iconImage��5�      � ��$ Center=�5�     �� 5�     $3 controlLabel��5�      �@ HasTwoLines .  5�     �� 5�     $3 controlLabel��5�      � ��$ 	2,-1,-1,1q�5�     �� 5�     $3 border��5�      � ��$ Stretch=�5�     �� 5�     $3 	iconImage��5�      � ��$ 16��5�     �� 5�     $3 	iconImage��5�      � ��$ 16��5�     �� 5�     $3 	iconImage��5�      � ��$ 1,0,2,0q�5�     �� 5�     $3 	iconImage��5�      5�     �� *Icon+ �� *TemplatedParent+ � ���� 5�     $3 controlLabel��5�      � y�$ Visible=�5�     �� 5�     $3 controlLabel��5�      � ��$ Center=�5�     5�     5�     5�     
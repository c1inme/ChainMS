   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source#    Fluent.ObjectToImageConverter   StringToImageConvert  RibbonDropDownButtonStyle �� 
TargetType   Fluent.DropDownButton �� Property ' #RibbonDropDownButtonControlTemplate �� Value  DefaultFontSize  DefaultFontFamily  DefaultFontBrush  ControlStyleEmptyFocus �� Triggers P� Property P� Value �� 
TargetType	 � 	Resources  MenuSeparatorStyle
 �� BasedOn ��Name  TransparentBrush ��Name �� Orientation ��Name J� SnapsToDevicePixels �� RelativeSource �� 	Converter   Fluent.TwoLineLabel  Name   Header   Text 	 TwoLineLabelStyle   HasTriangle   HasGlyph �Name   IsDropDownOpen � AllowsTransparency � RowDefinitions � ColumnDefinitions 
� RadiusX 
� Center 
� GradientOrigin 
� RadiusY  � Color! � Offset" �� EndPoint# �� 
StartPoint$ � MappingMode% � ActualWidth !
 ContextMenuBarBackgroundBrush  ContextMenuBarBorderBrush& �� CornerRadius' �� Stretch( ��Name  MenuScrollViewer)   MaxDropDownHeight   Fluent.MenuPanel*  Name+ K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation,   DirectionalNavigation # ContextMenuBarResizeBorderBrush & "ContextMenuBarResizeBackgoundBrush- n�Name. � Cursor & "ResizeVerticalThumbControlTemplate " ResizeBothThumbControlTemplate/ �� Triggers0 �� 
TargetName ! ButtonPressedOuterBorderBrush % !ButtonPressedOuterBackgroundBrush   Fluent.GrayscaleEffect1 ]� 
Conditions2 �� Property3 �� Value # ButtonHoverOuterBackgroundBrush  ButtonHoverOuterBorderBrush�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     Fluentclr-namespace:Fluent  8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006    �� $[ Upack://application:,,,/Fluent;component\Themes\Office2010\Controls\MenuSeparator.xaml?�%�  &	        &	      &	 �    7  7   7   7      �� "  .���  � ��# �  ��  � ��$ 66����  � ��$
 Auto����  � ��# �  ��  � ��# �  ��  � ��# �  ��  �@�FocusVisualStyle# �  ��  �@J�SnapsToDevicePixels .   P�  �@ Size$ Large=�. ���  � ��$ 66��P�  �@ Size$ Middle=�. ���  � ��$ 22��P�  �@ Size$ Small=�. ���  � ��$ 22����  � ��$ 31���� "  	 ���� 8
   '��       " ��.R�� .+��� $ PART_ButtonBorder��$��1q�#���  #���  $
��Auto��$
��Left=�$��Stretch=�._�� $
��Auto��.+��� $ 
stackPanel��$
��Auto��$ Vertical=�.+��� $ 	iconImage��$��Center=�$��32��$��Center=�$��32��$	��3,2q� . ���� *	LargeIcon+ �� *TemplatedParent+8    $ controlLabel��# z #��� 	 $��Center=�$��Center=�$��2,-2,2,2q�# z � $ 
PART_Popup��$��Bottom=�#��z  . $��Slide=�$��0,0,-4,0q�$	��Top=�$��2��.��� $��DropDown�� . $��0q�$	��Top=� �� $��* ��� $��10 � �� $��* ��� $��10 �.+��� $
��Left=�$	��Top=�$��4��$��4��$��1��$��1��$��0��$��	-1,-1,0,0q���
� $ 0.984Q�$ 0.017,0.002&�$ 0.017,0.002&�$ 1Q�.��� $  	#3F000000��$! 0Q�� $  	#00000000��$! 1Q��� $��0��$
��Left=�$��-1,3,0,0q�$	��Top=�$��4��$��4��$��1��$��0����
� $ 
0.032,1.01&�$ 
0.032,1.01&�$ 0.984Q�$ 1Q�.��� $  	#3F000000��$! 0Q�� $  	#00000000��$! 1Q��� $��0��$
��Left=�$��3,-1,0,0q�$	��Top=�$��4��$��4��$��0��$��1����
� $ 0.996,0.027&�$ 0.996,0.027&�$ 0.984Q�$ 1Q�.��� $  	#3F000000��$! 0Q�� $  	#00000000��$! 1Q��� $��0��$��Stretch=�$��7,-1,1,0q�$	��Top=�$
��Auto��$��4��$��0��$��1������ $" 0.5,1&�$# 0.5,0&�$$ RelativeToBoundingBox=�.��� $  	#3F000000��$! 0Q�� $  	#00000000��$! 1Q��� $��0��$
��Left=�$��-1,7,0,1q�$��Stretch=�$��4��$
��Auto��$��1��$��0������ $" 1,0.5&�$# 0,0.5&�$$ RelativeToBoundingBox=�.��� $  	#3F000000��$! 0Q�� $  	#00000000��$! 1Q��� $ PART_DropDownBorder��#��z%  . #��� 
 #���  $��1q�$& 1��$��1��$��Stretch=�$	��Top=�._�� $
��Auto��$
��Auto��$��Stretch=�$	��Top=�.+��� $	��Top=�$
��Auto��$
��Auto��#��� 
 $��1q�$& 1��� $��1q�$��Stretch=�$	��Top=� �� $��* ��� $
��Auto �.+��� $
' Fill=����	#FFE2E4E7$
��Left=�$��24,0,0,0q�$��1��$
��Auto��$��2��$y�Visible=�$��Stretch=���Z� �� $( PART_ScrollViewer��#���  ��.  $��1q�#��z)  . $
��Auto=�$��Hidden=�.��  $* PART_MenuPanel��+ . $, Local=��� $ resizeBorder��$��Stretch=�$	��Top=�$��11��#���  $��0,1,0,0q�$��1��$& 0,0,1,1��#���  ._�� .+�n� $- PART_ResizeVerticalThumb��$��Stretch=�$��	0,0,0,-90q�$
��Auto��$��10��$	��Top=�$. SizeNS��#���  n� $- PART_ResizeBothThumb��$��Right=�$��	0,0,0,-90q�$��10��$��10��$	��Top=�$. SizeNWSE��#���  / P�  �@ 
ResizeMode$
 None=�. ��� $0 resizeBorder�� � y�$ 	Collapsed=�P�  �@ 
ResizeMode$
 Both=�. ��� $0 PART_ResizeVerticalThumb�� � y�$ 	Collapsed=��� $0 PART_ResizeBothThumb�� � y�$ Visible=��� $0 resizeBorder�� � y�$ Visible=�P�  �@ 
ResizeMode$ Vertical=�. ��� $0 PART_ResizeBothThumb�� � y�$ 	Collapsed=��� $0 PART_ResizeVerticalThumb�� � y�$ Visible=��� $0 resizeBorder�� � y�$ Visible=�P�  �@ Size$ Small=�. ��� $0 
stackPanel�� �@��Orientation$ 
Horizontal=��� $0 	iconImage�� �� *Icon+ �� *TemplatedParent+8   � ���� $0 controlLabel�� �@ HasTwoLines .  �� $0 controlLabel�� �@ Text$  ���� $0 	iconImage�� � ��$ 16���� $0 	iconImage�� � ��$ 16���� $0 	iconImage�� � ��$ 1,0,0,0q��� $0 controlLabel�� � ��$ 	-2,-1,2,1q��� $0 controlLabel�� � ��$ 10���� $0 controlLabel�� � ��$ Center=�P�  �@ Size$ Middle=�. ��� $0 
stackPanel�� �@��Orientation$ 
Horizontal=��� $0 	iconImage�� �� *Icon+ �� *TemplatedParent+8   � ���� $0 
stackPanel�� � ��$
 Auto���� $0 	iconImage�� � ��$ Center=��� $0 controlLabel�� �@ HasTwoLines .  �� $0 controlLabel�� � ��$ 2,-1,2,1q��� $0 PART_ButtonBorder�� � ��$ Stretch=��� $0 	iconImage�� � ��$ 16���� $0 	iconImage�� � ��$ 16���� $0 	iconImage�� � ��$ 1,0,2,0q��� $0 controlLabel�� � ��$ Center=�P�  �@ IsDropDownOpen . . ��� $0 PART_ButtonBorder��# �   � ���� $0 PART_ButtonBorder��# �   � ��P�  � {� .  . ��� $0 	iconImage�� �@J�Opacity$	 0.5Q��� $0 controlLabel�� �@J�Opacity$	 0.5Q��� $0 	iconImage�� �@J�Effect   ]� 1 �� 2 �@J�IsMouseOver3 . �� 2 �@ IsDropDownOpen3 .  ./��� $0 PART_ButtonBorder��# �   � ���� $0 PART_ButtonBorder��# �   � ��]� 1 �� 2 �@J�	IsFocused3 . �� 2 �@ IsDropDownOpen3 .  ./��� $0 PART_ButtonBorder��# �   � ���� $0 PART_ButtonBorder��# �   � ��
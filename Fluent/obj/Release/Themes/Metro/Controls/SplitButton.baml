   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source#    Fluent.ObjectToImageConverter   StringToImageConvert  RibbonSplitButtonStyle �� 
TargetType   Fluent.SplitButton �� Property $  RibbonSplitButtonControlTemplate �� Value  DefaultFontSize  DefaultFontFamily  DefaultFontBrush  ControlStyleEmptyFocus �� Triggers   Fluent.DropDownButton P� Property P� Value  MenuSeparatorStyle �� BasedOn	 �� 
TargetType  TransparentBrush
 �� Orientation   Fluent.ToggleButton  Name   Command   CommandParameter   CommandTarget 	 InnerButtonStyle   IsButtonEnabled   Header   Header � ToolTip   IsDefinitive   IsDefinitive'    CanAddButtonToQuickAccessToolBar!   CanAddToQuickAccessToolBar   	LargeIcon �� RelativeSource �� 	Converter   Icon d� 	IsChecked �� Mode   Fluent.TwoLineLabel  Name   Text 
 TwoLineLabelStyle   HasTriangle    HasGlyph! �Name"   IsDropDownOpen# � AllowsTransparency$ J� SnapsToDevicePixels% � RowDefinitions& � ColumnDefinitions' ��Name( � ActualWidth ! ContextMenuBarBackgroundBrush  ContextMenuBarBorderBrush) �� CornerRadius* �� Stretch+ ��Name  MenuScrollViewer,   MaxDropDownHeight   Fluent.MenuPanel-  Name. K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation/   DirectionalNavigation # ContextMenuBarResizeBorderBrush & "ContextMenuBarResizeBackgoundBrush0 n�Name1 � Cursor & "ResizeVerticalThumbControlTemplate " ResizeBothThumbControlTemplate2 �� Triggers3 �� 
TargetName ! ButtonPressedOuterBorderBrush ! ButtonPressedInnerBorderBrush % !ButtonPressedInnerBackgroundBrush4 ]� 
Conditions5 �� 
SourceName6 �� Property7 �� Value  ButtonHoverInnerBorderBrush  ButtonHoverOuterBorderBrush  InnerButtonControlTemplate8 ��Name9 ��Name: ��Name % !ButtonPressedOuterBackgroundBrush # ButtonHoverOuterBackgroundBrush   Fluent.GrayscaleEffect�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     Fluentclr-namespace:Fluent  8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006    �� $[ Upack://application:,,,/Fluent;component\Themes\Office2010\Controls\MenuSeparator.xaml?�%~  &	        &	      '�� �    7  &	 �    7   7   &		 �    &	 W       �� "  .���  � ��# �  ��  � ��$ 66����  � ��$
 Auto����  � ��# �  ��  � ��# �  ��  � ��# �  ��  �@�FocusVisualStyle# �  ��  �@J�SnapsToDevicePixels .  P�  �@ Size$ Large=�. ���  � ��$ 66��P�  �@ Size$ Middle=�. ���  � ��$ 22��P�  �@ Size$ Small=�. ���  � ��$ 22����  � ��$ 34���� 8   " ���� "	  .R�� .+��� $��PART_ButtonBorder��$��1q�#���  #���  ._��� $��
stackPanel��$
 Vertical=�.+�  $ PART_Button��#��z #��z #��z $��0,0,0,0q�$
��Auto��$
��Auto��#��� 	 #{�z # z # z # z # z  �� *	LargeIcon+ �� *TemplatedParent+8    �� *Icon+ �� *TemplatedParent+8   �� *	IsChecked+$ TwoWay=� �� *TemplatedParent+�� $��
downBorder��$��0,1,0,0q�#���  #���  $��0,0,0,0q�._�� .+�  $ controlLabel��# z #��� 
 $��Center=�$��Center=�$��2,0,2,0q�#  z � $! 
PART_Popup��$��Bottom=�#��z" # . |�.  $��Slide=�$��0,0,-4,0q�$	��Top=�$��2��.��� $��DropDown��$ . $��0q�$	��Top=�% �� $��* ��� $��10 �& �� $��* ��� $��10 �.+��� $
��Left=�$	��Top=�$��4��$��4��$��1��$��1��$��0��$��	-1,-1,0,0q��� $��0��$
��Left=�$��-1,3,0,0q�$	��Top=�$��4��$��4��$��1��$��0���� $��0��$
��Left=�$��3,-1,0,0q�$	��Top=�$��4��$��4��$��0��$��1���� $��0��$��Stretch=�$��7,-1,1,0q�$	��Top=�$
��Auto��$��4��$��0��$��1���� $��0��$
��Left=�$��-1,7,0,1q�$��Stretch=�$��4��$
��Auto��$��1��$��0���� $' PART_DropDownBorder��#��z( $ . #���  #���  $��1q�$) 1��$��1��$��Stretch=�$	��Top=�._�� $
��Auto��$
��Auto��$��Stretch=�$	��Top=�.+��� $	��Top=�$
��Auto��$
��Auto��#���  $��1q�� $��1q�$��Stretch=�$	��Top=�% �� $��* ��� $
��Auto �.+��� $
* Fill=����	#FFE2E4E7$
��Left=�$��24,0,0,0q�$��1��$
��Auto��$��2��$y�Visible=�$��Stretch=���Z� �� $+ PART_ScrollViewer��#���  $��1q�#��z, $ . $
��Auto=�$��Hidden=�.��  $- PART_MenuPanel��. . $/ Cycle=��� $' resizeBorder��$��Stretch=�$	��Top=�$��11��#���  $��0,1,0,0q�$��1��$) 0,0,1,1��#���  ._�� .+�n� $0 PART_ResizeVerticalThumb��$��Stretch=�$��	0,0,0,-90q�$
��Auto��$��10��$	��Top=�$1 SizeNS��#���  n� $0 PART_ResizeBothThumb��$��Right=�$��	0,0,0,-90q�$��10��$��10��$	��Top=�$1 SizeNWSE��#���  2 P�  �@ 
ResizeMode$
 None=�. ��� $3 resizeBorder�� � y�$ 	Collapsed=�P�  �@ 
ResizeMode$
 Both=�. ��� $3 PART_ResizeVerticalThumb�� � y�$ 	Collapsed=��� $3 PART_ResizeBothThumb�� � y�$ Visible=��� $3 resizeBorder�� � y�$ Visible=�P�  �@ 
ResizeMode$ Vertical=�. ��� $3 PART_ResizeBothThumb�� � y�$ 	Collapsed=��� $3 PART_ResizeVerticalThumb�� � y�$ Visible=��� $3 resizeBorder�� � y�$ Visible=�P�  �@ 	IsChecked$
 TrueX�. ��� $3 PART_ButtonBorder��# �   � ���� $3 PART_ButtonBorder��# �   � ��P�  �@ Size$ Small=�. ��� $3 PART_Button�� �@ Size$ Small=��� $3 
stackPanel�� �@��Orientation$ 
Horizontal=��� $3 controlLabel�� � ��$ 10���� $3 controlLabel�� � ��$ 	0,-19,0,0q��� $3 controlLabel�� �@ Text$  ���� $3 
downBorder�� � ��$ 0q�P�  �@ Size$ Middle=�. ��� $3 PART_Button�� �@ Size$ Middle=��� $3 
stackPanel�� �@��Orientation$ 
Horizontal=��� $3 controlLabel�� � ��$ 10���� $3 controlLabel�� � ��$ 	0,-19,0,0q��� $3 controlLabel�� �@ Text$  ���� $3 
downBorder�� � ��$ 0q�P�  �@ IsButtonEnabled .  . ��� $3 PART_Button�� � {� .  P�  �@ IsDropDownOpen . . ��� $3 PART_ButtonBorder��# �   � ���� $3 PART_ButtonBorder��# �   � ��]� 4 �� $5 PART_Button��6 �@J�IsMouseOver7 . �� 6 �@ IsDropDownOpen7 .  ./��� $3 PART_ButtonBorder��# �   � ��]� 4 �� $5 
downBorder��6 �@J�IsMouseOver7 . �� 6 �@ IsDropDownOpen7 .  ./��� $3 PART_ButtonBorder��# �   � ���� $3 
downBorder��# �   � ���� $3 
downBorder��# �   � ���� "  .���  � ��# �  ��  � ��$ Stretch=���  � ��$ Stretch=���  �@�FocusVisualStyle# �  �� "	  .R��� $' border��$��1q�#���  #���  $
��Auto��$��Stretch=�$��Stretch=�$ .  ._�� $
��Auto��.+��� $8 	rectangle��$��0��$y�	Collapsed=��� $9 
stackPanel��$
��Auto��$
 Vertical=�$��Stretch=�$ . .+��� $: 	iconImage��$��Center=�$��32��$��Center=�#��z $��32��$ .   $ controlLabel��# z #��� 
 $��Stretch=�$��Center=�$��2,-1,2,2q�$y�	Collapsed=�2 P�  � �� . . ��� $3 border��# �   � ���� $3 border��# �   � ��]� 4 �� 6 �@J�IsMouseOver7 . �� 6 � ��7 .  �� 6 � {�7 . ./��� $3 border��# �   � ���� $3 border��# �   � ��]� 4 �� 6 �@J�IsKeyboardFocusWithin7 . �� 6 � ��7 .  �� 6 � {�7 . ./��� $3 border��# �   � ���� $3 border��# �   � ��P�  � {� .  . ��� $3 	iconImage�� �@J�Opacity$	 0.5Q��� $3 controlLabel�� �@J�Opacity$	 0.5Q��� $3 	iconImage�� �@J�Effect   P�  �@ Size$ Small=�. ��� $3 
stackPanel�� �@��Orientation$ 
Horizontal=��� $3 controlLabel�� � y�$ 	Collapsed=��� $3 controlLabel�� �@ HasTwoLines .  �� $3 	iconImage�� � ��$ 16���� $3 	iconImage�� � ��$ 16���� $3 	iconImage�� � ��$ 1,0,2,0q��� $3 	iconImage�� �� *Icon+ �� *TemplatedParent+ � ���� $3 controlLabel�� � ��$ 2,-1,2,1q�P�  �@ Size$ Middle=�. ��� $3 
stackPanel�� �@��Orientation$ 
Horizontal=��� $3 
stackPanel�� � ��$
 Auto���� $3 	iconImage�� � ��$ Center=��� $3 controlLabel�� �@ HasTwoLines .  �� $3 controlLabel�� � ��$ 	2,-1,-1,1q��� $3 border�� � ��$ Stretch=��� $3 	iconImage�� � ��$ 16���� $3 	iconImage�� � ��$ 16���� $3 	iconImage�� � ��$ 1,0,2,0q��� $3 	iconImage�� �� *Icon+ �� *TemplatedParent+ � ���� $3 controlLabel�� � y�$ Visible=��� $3 controlLabel�� � ��$ Center=�
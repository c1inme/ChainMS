   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089#    Fluent.ObjectToImageConverter   StringToImageConvert  RibbonToggleButtonStyle  �� 
TargetType   Fluent.ToggleButton �� Property % !RibbonToggleButtonControlTemplate �� Value  DefaultFontSize  DefaultFontFamily  DefaultFontBrush  ControlStyleEmptyFocus �� Triggers   Fluent.RibbonControl P� Property P� Value  ButtonCheckedBrush �� Opacity �� Color �� RelativeSource	 �� AncestorType   Fluent.MetroWindow
 �� 
TargetType ��Name  TransparentBrush ��Name �� Orientation ��Name J� SnapsToDevicePixels �� 	Converter   Fluent.TwoLineLabel  Name   Header   Text 	 TwoLineLabelStyle �� Triggers �� 
TargetName �� Path !
 ButtonPressedOuterBorderBrush % !ButtonPressedOuterBackgroundBrush   Fluent.GrayscaleEffect ]� 
Conditions �� Property �� Value # ButtonHoverOuterBackgroundBrush  ButtonHoverOuterBorderBrush�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     Fluentclr-namespace:Fluent  8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  %(  &	        &	      &	 u    &	 �    7   7   7      �� "   .���  � ��# �  ��  � ��$ 66����  � ��$
 Auto����  � ��# �  ��  � ��# �  ��  � ��# �  ��  �@�FocusVisualStyle# �   P�  �@ Size$ Large=�. ���  � ��$ 66��P�  �@ Size$ Middle=�. ���  � ��$ 22��P�  �@ Size$ Small=�. ���  � ��$ 22����  � ��$ 22���� $	 0.2Q� �� *RibbonThemeColor.Color+ �� *FindAncestor+"	  �� "
  .R��� $ border��$��1q�#���  #���  $
��Auto��$
��Left=�$��Stretch=�._�� $
��Auto��.+��� $ 
stackPanel��$
��Auto��$ Vertical=�.+��� $ 	iconImage��$��Center=�$��32��$��Center=�$��32��$��2q� . ���� *	LargeIcon+ �� *TemplatedParent+8     $ controlLabel��# z #��� 	 $��Stretch=�$��Stretch=�$��2,-3,2,1q� P�  �@ Size$ Small=�. ��� $ 
stackPanel�� �@��Orientation$ 
Horizontal=��� $ controlLabel�� � y�$ 	Collapsed=��� $ controlLabel�� �@ HasTwoLines .  �� $ 	iconImage�� � ��$ 16���� $ 	iconImage�� � ��$ 16���� $ 	iconImage�� � ��$ 1,0,2,0q��� $ 	iconImage�� �� $
 Icon� �� *TemplatedParent+8   � ���� $ controlLabel�� � ��$ 2,-2,2,2q�P�  �@ Size$ Middle=�. ��� $ 
stackPanel�� �@��Orientation$ 
Horizontal=��� $ 
stackPanel�� � ��$
 Auto���� $ 	iconImage�� � ��$ Center=��� $ controlLabel�� �@ HasTwoLines .  �� $ controlLabel�� � ��$ 2,2,2,2q��� $ border�� � ��$ Stretch=��� $ 	iconImage�� � ��$ 16���� $ 	iconImage�� � ��$ 16���� $ 	iconImage�� � ��$ 1,0,2,0q��� $ controlLabel�� � ��$ Center=��� $ 	iconImage�� �� $
 Icon� �� *TemplatedParent+8   � ��P�  � �� . . ��� $ border��# � 
  � ���� $ border��# �   � ��P�  �@d�	IsChecked$
 TrueY�. ��� $ border��# �   � ���� $ border��# �   � ��P�  � {� .  . ��� $ 	iconImage�� �@J�Opacity$	 0.5Q��� $ controlLabel�� �@J�Opacity$	 0.5Q��� $ 	iconImage�� �@J�Effect   ]�  ��  �@J�IsMouseOver . ��  � �� .  ./��� $ border��# �   � ���� $ border��# �   � ��]�  ��  �@J�IsKeyboardFocusWithin . ��  � �� .  ./��� $ border��# �   � ���� $ border��# �   � ��]�  ��  �@J�IsMouseOver . ��  � �� .  ��  �@d�	IsChecked$
 TrueY�./��� $ border��# �   � ���� $ border��# �   � ��]�  ��  �@J�IsKeyboardFocusWithin . ��  � �� .  ��  �@d�	IsChecked$
 TrueY�./��� $ border��# �   � ���� $ border��# � 
  � ��
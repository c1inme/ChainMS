   M S B A M L   `   `   `  ���� U  QSystem.Windows, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aG4clr-namespace:System.Windows;assembly=System.WindowsSystem.Windows  M IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089#   Fluent.ObjectToImageConverter   StringToImageConvert  RibbonButtonControlTemplate  �� 
TargetType  Fluent.Button ��Name  TransparentBrush ��Name �� Orientation ��Name J� SnapsToDevicePixels �� RelativeSource �� 	Converter  Fluent.TwoLineLabel  Name	   Header
   Text  TwoLineLabelStyle �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value ! ButtonPressedOuterBorderBrush % !ButtonPressedOuterBackgroundBrush  Fluent.GrayscaleEffect ]� 
Conditions �� Property �� Value # ButtonHoverOuterBackgroundBrush  ButtonHoverOuterBorderBrush  RibbonButtonStyle �� 
TargetType 	 DefaultFontSize 
 DefaultFontFamily  DefaultFontBrush  ControlStyleEmptyFocus �� Triggers  Fluent.RibbonControl �� BasedOn ( $IronicallyNamedChromelessButtonStyle  TransparentWhiteBrush �Name J� Opacity�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     <vsm4clr-namespace:System.Windows;assembly=System.Windows  Fluentclr-namespace:Fluent  8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  %�  &	        &	      7   7   7   &	 �    7  '  M	    7  &	 \	       �� "   .R��� $ border��#���  #���  $
��Auto��$
��Left=�$��Stretch=�._�� $
��Auto��.+��� $ 
stackPanel��$
��Auto��$ Vertical=�.+��� $ 	iconImage��$��Center=�$��32��$��Center=�$��32��$��3,2,3,1q� . ���� *	LargeIcon+ �� *TemplatedParent+8     $ controlLabel��#
 z	 #���  $��Stretch=�$��Center=�$��2,-1,2,0q� P�  �@ Size$ Small=�. ��� $ 
stackPanel�� �@��Orientation$ 
Horizontal=��� $ controlLabel�� � y�$ 	Collapsed=��� $ controlLabel�� �@ HasTwoLines .  �� $ 	iconImage�� � ��$ 16���� $ 	iconImage�� � ��$ 16���� $ 	iconImage�� � ��$ 1,0,2,0q��� $ 	iconImage�� �� *Icon+ �� *TemplatedParent+8   � ���� $ controlLabel�� � ��$ 2,-1,2,1q�P�  �@ Size$ Middle=�. ��� $ 
stackPanel�� �@��Orientation$ 
Horizontal=��� $ 
stackPanel�� � ��$
 Auto���� $ 	iconImage�� � ��$ Center=��� $ controlLabel�� �@ HasTwoLines .  �� $ controlLabel�� � ��$ 2,-1,2,1q��� $ border�� � ��$ Stretch=��� $ 	iconImage�� � ��$ 16���� $ 	iconImage�� � ��$ 16���� $ 	iconImage�� � ��$ 1,0,2,0q��� $ 	iconImage�� �� *Icon+ �� *TemplatedParent+8   � ���� $ controlLabel�� � ��$ Center=�P�  � �� . . ��� $ border��# �   � ���� $ border��# �   � ��P�  � {� .  . ��� $ 	iconImage�� �@J�Opacity$	 0.5Q��� $ controlLabel�� �@J�Opacity$	 0.5Q��� $ 	iconImage�� �@J�Effect   ]�  ��  �@J�IsMouseOver . ��  � �� .  ��  � {� . ./��� $ border��# �   � ���� $ border��# �   � ��]�  ��  �@J�IsKeyboardFocusWithin . ��  � �� .  ./��� $ border��# �   � ���� $ border��# �   � ���� "  .���  � ��8   ��  � ��$ 66����  � ��$
 Auto����  � ��# � 	 ��  � ��# � 
 ��  � ��# �  ��  �@�FocusVisualStyle# �  ��  �@J�SnapsToDevicePixels .   P�  �@ Size$ Large=�. ���  � ��$ 66��P�  �@ Size$ Middle=�. ���  � ��$ 22����  � ��$
 Left=�P�  �@ Size$ Small=�. ���  � ��$ 22����  � ��$ 22����  � ��$
 Left=��� "  8   �� " ��.���  � ��# �  ��  � ��# � ����  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� "  ��.R�� $
 grid��#��z��.+��� $ contentPresenter��#��z��#��z��#��z��# z ��. $
 0.75Q� P�  �@J�IsMouseOver . . ��� $ contentPresenter�� �@J�Opacity$ 1Q��� $
 grid��# �   � ��P�  �@J�IsMouseOver .  . ��� $ contentPresenter�� �@J�Opacity$ .5Q�P�  � �� . . ��� $
 grid��# �   � ��P�  � {� .  . ���  � �� �#ADADAD
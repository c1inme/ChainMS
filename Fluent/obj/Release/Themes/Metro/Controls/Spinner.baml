   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089#    Fluent.ObjectToImageConverter   StringToImageConvert   SpinnerButtonControlTemplate  �� 
TargetType ��Name  TransparentBrush ��Name �� Triggers P� Property P� Value �� 
TargetName ! ButtonPressedOuterBorderBrush �� Value �� Property % !ButtonPressedOuterBackgroundBrush	 ]� 
Conditions
 �� Property �� Value # ButtonHoverOuterBackgroundBrush  ButtonHoverOuterBorderBrush  SpinnerButtonStyle �� 
TargetType  DefaultFontBrush 	 ControlStyleEmptyFocus 
 SpinnerStyle   Fluent.Spinner  DefaultFontFamily  DefaultFontSize � ColumnDefinitions ��Name �� Orientation �� RelativeSource �� 	Converter ��Name   Fluent.RibbonControl   Header  TextBoxBorderBrush  TextBoxBackgroundBrush   
InputWidth � RowDefinitions ��Name F�Name �� Stretch  TextBoxHoverBorderBrush ��Name J� RenderTransformOrigin �� EdgeMode " TextBoxDisabledBackgroundBrush  TextBoxDisabledBorderBrush   Fluent.GrayscaleEffect  TextBoxHoverBackgroundBrush�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  Fluentclr-namespace:Fluent  %E  &	        &	      &	 r    7  &	
 �    7   7  7     �� "  ��.R��� $ border��$��1q�#���  #���  $
��Auto��$��Stretch=�$��Stretch=�._��� $ contentPresenter��$��Center=�$��Center=� P�  �@J�IsMouseOver .  P�  � �� . . ��� $ border��# �   � ���� $ border��# �   � ��P�  � {� .  . ��� $ contentPresenter�� �@J�Opacity$	 0.5Q�]� 	 �� 
 �@J�IsMouseOver . �� 
 � �� .  ./��� $ border��# �   � ���� $ border��# �   � ��]� 	 �� 
 �@J�IsKeyboardFocusWithin . �� 
 � �� .  ./��� $ border��# �   � ���� $ border��# �   � ���� " ��.���  � ��8   ��  � ��# �  ��  �@�FocusVisualStyle# � 	 �� "  .���  �@�FocusVisualStyle# � 	 ��  � ��# �  ��  � ��# �  ��  � ��# �  ��  � |� .  ��  � �� �� "   .R�� $
��Auto��$��22�����	#00000000 �� $
��Auto ��� $��* �.+��� $ 
stackPanel��$ 
Horizontal=�$��1��$
��Left=�$��0,0,3,0q�.+��� $ 	iconImage��$��16��$��16��$��Center=�$��4,0,0,0q����� *Icon+ �� *TemplatedParent+8   �� $ 	textBlock��#��z $
��Wrap=�$��Center=�$��5,0,4,0q��� $ border��$��22��$��1q�#���  #���  $��1��$��1��#��z $��Right=�._�� $��1��$��1�� �� $��* ��� $��17 � �� $��* ��� $��* �.+��� $ PART_TextBox��$��0q�$��2��$��0��#���  #��z��$��2,0,2,0q�$��0q�F� $
 path��$y�	Collapsed=�$
��Left=�$��	0,-1,0,-1q�$��1�����	#FF000000$
 Fill=�#���  $��1��$��1��$��2������        �    @B 	F� $ path1��$
��Left=�$��0,0,-1,0q�$
��Auto�����	#FF000000$
 Fill=�#���  $��1������        �@B     	$y�	Collapsed=�$��Bottom=�$��1��$��1��$��1���� $ PART_ButtonUp��$��1��$��0��8�� $ 0,0.5&�$��Stretch=�.��F� $��5��$��3�����	#FF000000$
 Fill=����	#FF000000"����        �@B ����р�     	$��Center=�$��0��$��Center=�$��1,2,0,1q�$ Aliased=��� $ PART_ButtonDown��$��1��$��1��8�� .��F� $��5��$��3�����	#FF000000$
 Fill=����	#FF000000"����        �@B @B р�     	$��0��$��Center=�$��1,-1,0,0q�$��Center=�$ Aliased=� P�  Z�  �@ Icon. ��� $ 	iconImage�� � y�$ 	Collapsed=�P�  �@ 
InputWidth$	 NaNQ�. ��� $ border�� � ��$ Stretch=�P�  � {� .  . ��� $ PART_TextBox�� � {� .  �� $ border��# �   � ���� $ border��# �   � ���� $ PART_TextBox��# �   � ���� $ 	iconImage�� �@J�Opacity$	 0.5Q��� $ 	textBlock�� �@J�Opacity$	 0.5Q��� $ 	iconImage�� �@J�Effect   P�  �@ Size$ Middle=�. ��� $ 	textBlock�� � y�$ 	Collapsed=�P�  �@ Size$ Small=�. ��� $ 
stackPanel�� � y�$ 	Collapsed=�P�  �@J�IsMouseOver . . ��� $
 path�� � y�$ Visible=��� $ path1�� � y�$ Visible=��� $ border��# �   � ���� $ border��# �   � ���� $ PART_TextBox��# �   � ��
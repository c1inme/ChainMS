   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 #  BackstageTabItemControlTemplate  �� 
TargetType    Fluent.BackstageTabItem ��Name �� Color �� Opacity ��Name    Header � 	Resources �� 
TargetType �� Property	 �� Value
 �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName P� Property P� Value  BackstageTabItemStyle  DefaultFontFamily  BackstageFontBrush  ControlStyleEmptyFocus �� RelativeSource �� AncestorType    Fluent.BackstageTabControl�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     Fluentclr-namespace:Fluent  %'  &	        &	 2    7   �� "    .R�� ���	#00000000.+��� $ hoverBorder��$��0,0,0,0q�$y�	Collapsed=����� $ Black��$	 0.2Q��� $ selectedBorder��$��0q�$y�	Collapsed=����� $ White��$	 0.2Q�._�� $
��Auto��$��0q�$��Stretch=��� $ PART_Header��$
��20,0q�$
��Left=�$��Center=�#��z  ���� '��       " ��.���  � ��$	 NoWrap=���  � ��$	 CharacterEllipsis=�
 ]�  ��  �@J�IsMouseOver . ��  �@��
IsSelected .  ./��� $ hoverBorder�� � y�$	 Visible=��� $ selectedBorder�� � y�$	 	Collapsed=�P�  � {� .  . ��� $ hoverBorder�� � y�$	 Hidden=��� $ PART_Header�� �@J�Opacity$		 0.5Q�P�  �@��
IsSelected . . ��� $ selectedBorder�� � y�$	 Visible=��� $ PART_Header�� � ��
	 ������� "   .���  � ��#	 �  ��  � ��#	 �  ��  � ��8	   ��  �@�FocusVisualStyle#	 �  ��  � ��$	 37����  � ��	 �� *
Background+ �� *FindAncestor+"  ��  � ��$		 0,1q�
   M S B A M L   `   `   `  ����M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 #  BackstageTabItemControlTemplate  �� 
TargetType    Fluent.BackstageTabItem ��Name �� Color �� Opacity ��Name    Header � 	Resources �� 
TargetType �� Property	 �� Value
 �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName P� Property P� Value  BackstageTabItemStyle  DefaultFontFamily  BackstageFontBrush  ControlStyleEmptyFocus �� RelativeSource �� AncestorType    Fluent.BackstageTabControl�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      Fluentclr-namespace:Fluent  5      %  &	        &	 �    7   �� 5      "    5      .R�5   
   � ���	#00000000.+�5      �� $ hoverBorder��6   $��0,0,0,0q�5      $y�	Collapsed=�5	      ��5
      �� 5      $ Black��6    $	 0.2Q�6.   5      �� 5      $ selectedBorder��6   $��0q�5      $y�	Collapsed=�5      ��5      �� 5      $ White��6    $	 0.2Q�6.   ._�5      � $
��Auto��6   $��0q�5      $��Stretch=�5      5      5      �� 5      $ PART_Header��6   $
��20,0q�5      $
��Left=�5      $��Center=�5      #��z 5       5      ���� 5      '��       " ��6   .�5       ��  � ��$	 NoWrap=�5!   !   �� 5"       � ��$	 CharacterEllipsis=�5#   !   5$      5%      5&      5'      
 5(   
   ]� 5)       5*      �� 5+       �@J�IsMouseOver . 5,       �� 5-       �@��
IsSelected .  5.       ./�50      �� $ hoverBorder��6    � y�$	 Visible=�52      �� 53      $ selectedBorder��6    � y�$	 	Collapsed=�55      56      P� 57       � {� .  . �59      �� $ hoverBorder��6    � y�$	 Hidden=�5;      �� 5<      $ PART_Header��6    �@J�Opacity$		 0.5Q�5>      5?      P� 5@       �@��
IsSelected . . �5B      �� $ selectedBorder��6    � y�$	 Visible=�5D      �� 5E      $ PART_Header��6    � ��
	 �����5G      5H      5J      �� 5M      "   5N      .�5O   
   ��  � ��#	 �  5P      �� 5Q   
    � ��#	 �  5R      �� 5S   
    � ��8	   5T      �� 5U   
    �@�FocusVisualStyle#	 �  5V      �� 5W   
    � ��$	 37��5X      �� 5Y   
    � ��	 5Z      �� *
Background+ �� *FindAncestor+"  �� 5]   
    � ��$		 0,1q�5^      5_      5`      
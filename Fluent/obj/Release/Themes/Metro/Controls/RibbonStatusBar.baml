   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089    Fluent.StatusBar  �� 
TargetType �� Property �� Value �� 
TargetType �� RelativeSource �� AncestorType   Fluent.MetroWindow   Fluent.StatusBarPanel  Name K� IsItemsHost �� Triggers	 P� Property
 P� Value �� 
TargetName   RibbonStatusBarItemStyle   Fluent.StatusBarItem �� Triggers   Fluent.StatusBarMenuItem 1 -RibbonStatusBarContextMenuItemControlTemplate  DefaultFontFamily  DefaultFontSize  DefaultFontBrush �� Mode � ColumnDefinitions ��Name # ButtonHoverOuterBackgroundBrush  ButtonHoverOuterBorderBrush �� CornerRadius # ButtonHoverInnerBackgroundBrush  ButtonHoverInnerBorderBrush ��Name ��Name #	 MenuItemCheckBoxBackgroundBrush 
 MenuItemCheckBoxBorderBrush �� RadiusX �� RadiusY ��Name J� SnapsToDevicePixels m� Icon F�Name �� Stretch �� EndPoint �� 
StartPoint � Color � Offset ]� 
Conditions �� Value  �� Property! �� BasedOn�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     !Controlsclr-namespace:Fluent  %�	  '         &	  q    '  �    &	 �    '  ]	    7   �� "    .���  �@J�SnapsToDevicePixels . ��  � ��$ 23����  � ��$ 0,0,0,-1q���  � �� �� "   .R��� $
��Auto��$	��0,1q����� *RibbonThemeColor+ �� *FindAncestor+"  ._�  $ itemsPresenter�� . $
��Auto��$��0q� P� 	 �@  IsWindowMaximized
 .  . ��� $ itemsPresenter�� � ��$ 0,0,16,0q��� "   .���  � ��$ 3,0,3,0q���  � �� �� "  .R��� #��z�� P� 	 � {�
 .  . ���  � �� �	#FF888888�� "   .���  � ��# �  ��  � ��# �  ��  � ��# �  ��  � ��# �  ��  �@m�IsCheckable . ��  �@m�	IsChecked �� *StatusBarItem.IsChecked+ �� *Self+$ TwoWay=��� "  .R�� $
��Auto��$��22�����	#00000000 �� $��25 ��� $��* ��� $
��Auto �.+��� $ border��$��Stretch=�$��0q�$
��Auto��$y�	Collapsed=�#���  #���  $��1q�$ 2��$��0��$��3��._��� #���  #���  $��1q�$ 1���� $ textBlock_Copy��$
��Auto��$
��Auto��$��9,3,30,3q�$��Center=�$��1��$
��None=�$y�Hidden=�$
��Left=����� *StatusBarItem.Title+ �� *TemplatedParent+�� $ 	textBlock��$
��Auto��$
��Auto��$��9,3,3,3q�$��Center=�$��1��$��CharacterEllipsis=����� *StatusBarItem.Title+ �� *TemplatedParent+�� $ 	rectangle��#��� 	 #��� 
 $��1��$��Center=�$��Center=�$��20��$��20��$ 1��$ 1��$y�	Collapsed=��� $ image��$��Center=�$��Center=�$<��6/Fluent;Component/Themes/Office2010/Images/Checked.png��$
��None=�$y�	Collapsed=� . �� $ 	iconImage��$��Center=�$��3,2,2,2q�$��Center=�$
��Auto��#��z  . �� $
��Auto��$
��Auto��$��9,3,3,3q�$��Center=�$��3��$��CharacterEllipsis=����� *StatusBarItem.Value+ �� *TemplatedParent+F� $
 path��$
 Fill=�$��Right=�$��1,0,4,0q�$��4��"����        �@B @B �    �� 	$��6��$��3����Z� ���� $ 0.945,0.872&�$ 0.055,0.128&�.��� $ Black��$ 1Q�� $ 	#FF828282�� P� 	 �@m�	IsChecked
 . . ��� $ 	rectangle�� � y�$ Visible=�]�  ��  Z�   �@m�Icon��   �@m�	IsChecked . ./��� $ image�� � y�$ Visible=�P� 	 �@��HasItems
 .  . ��� $
 path�� � y�$ 	Collapsed=�]�  ��   �@m�IsHighlighted . ��   � {� . ./��� $ border�� � y�$ Visible=��� 8!   "   
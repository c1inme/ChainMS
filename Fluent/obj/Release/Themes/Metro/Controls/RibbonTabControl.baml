   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   boolToVisibilityConverter $  MenuScrollingVisibilityConverter / +RibbonTabControlToggleButtonControlTemplate  �� 
TargetType    Fluent.ToggleButton ��Name  TransparentBrush � ToolTip   Fluent.ScreenTip  Name   Title   Fluent.Ribbon   Localization �� Source �� Path �� Mode	   Text
 ��Name �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value ! ButtonPressedOuterBorderBrush % !ButtonPressedOuterBackgroundBrush % !ButtonPressedInnerBackgroundBrush ! ButtonPressedInnerBorderBrush ]� 
Conditions �� Property �� Value  ButtonHoverInnerBorderBrush #	 ButtonHoverInnerBackgroundBrush #
 ButtonHoverOuterBackgroundBrush  ButtonHoverOuterBorderBrush % !RibbonTabControlToggleButtonStyle �� 
TargetType  DefaultFontSize  DefaultFontFamily  DefaultFontBrush  ControlStyleEmptyFocus  RibbonTabControlStyle   Fluent.RibbonTabControl # RibbonTabControlControlTemplate � 	Resources  Key ! ScrollViewerButtonBorderBrush % !ScrollViewerButtonBackgroundBrush ��Name 
� Center 
� GradientOrigin 
� RadiusY � Color � Offset  ScrollButtonControlTemplate �Name J� IsHitTestVisible ��Name �� CanContentScroll  �� LineLeftCommand  ScrollButtonStyle! �� RelativeSource" �� FallbackValue# `� 	Converter$ `� ConverterParameter% �� Stretch& �� LineRightCommand' � RowDefinitions( ��Name)   ContentGapHeight  RibbonBackgoundBrush* J� OpacityMask+ �� EndPoint, �� 
StartPoint- ��Name. ��Name/ J� Opacity0 � ColumnDefinitions   Fluent.RibbonScrollViewer1  Name  RibbonTabScrollViewer    Fluent.RibbonTabsContainer2  Name3 K� IsItemsHost4 +� IsFocusScope5 ��Name6 �� Orientation7   Name8    Size9 d� 	IsChecked: �� 	Converter; �Name< � 	StaysOpen= � AllowsTransparency> � VerticalOffset?   IsDropDownOpen@ � HorizontalOffsetA � PlacementTargetB �� ElementName�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     Fluentclr-namespace:Fluent  %�   &	        &	      &	 
     &	     7  &	 �    7  &	 K    7  7  7  7  7  7   �� l� �� "    .R��� $ border��$��1q�#���  #���  $��22��$
��Left=�$��Stretch=�$��22��   $ 	screenTip�� �� # Z $" MinimizeButtonScreenTipTitle�$ OneWay=�	 �� # Z $! MinimizeButtonScreenTipText�$ OneWay=�._��� $ border1��$
��Auto��#���  $��1q�#���  ._�� .+��� $
 	iconImage��$��Center=�$
��Auto��$
��None=�$��Center=�$C��=/Fluent;Component/Themes/Office2010/Images/RibbonCollapse.png��$
��Auto��$��2q��� $
 iconImageChecked��$y�	Collapsed=�$��Center=�$
��Auto��$
��None=�$��Center=�$A��;/Fluent;Component/Themes/Office2010/Images/RibbonExpand.png��$
��Auto��$��2q� P�  �@d�	IsChecked$
 TrueY�. ��� $ 	iconImage�� � y�$ 	Collapsed=��� $ iconImageChecked�� � y�$ Visible=��� $ 	screenTip�� �� # Z $  ExpandButtonScreenTipTitle�$ OneWay=� �@ Title�� $ 	screenTip�� �� # Z $ ExpandButtonScreenTipText�$ OneWay=� �@ TextP�  � �� . . ��� $ border��# �   � ���� $ border��# �   � ���� $ border1��# �   � ���� $ border1��# �   � ��]�  ��  �@J�IsMouseOver . ��  � �� .  ./��� $ border1��# �   � ���� $ border1��# � 	  � ���� $ border��# � 
  � ���� $ border��# �   � ��]�  ��  �@J�IsKeyboardFocusWithin . ��  � �� .  ./��� $ border1��# �   � ���� $ border1��# � 	  � ���� $ border��# � 
  � ���� $ border��# �   � ���� "   .���  � ��8   ��  � ��$ 22����  � ��$ 22����  � ��# �  ��  � ��# �  ��  � ��# �  ��  �@�FocusVisualStyle# �  �� "  .���  � ��$	 Top=���  �@+�IsFocusScope .  ��  � ��# �  ��  � ��# �  ��  � ��# �  ��  � ��8   �� "    ���� ScrollButtonControlTemplate "  ��.R��� $ border��$��1q�#���  $
��Auto��$��Stretch=�$��Stretch=�#���  ._��� $ border1��$
��Auto��#���  $��1q�#���  ._�� $
��Auto��.+��� $ 	rectangle��$��0��$y�	Collapsed=���
� $ 0.503,1&�$ 0.503,1&�$ 0.202Q�.��� $ 	#7FFFFFFF��$ 0Q�� $ 1Q��� $��Center=�$��Center=� P�  �@J�IsMouseOver .  P�  � �� . . ��� $ border��# �   � ���� $ border��# �   � ���� $ border1��# �   � ���� $ border1��# �   � ��P�  � {� .  ]�  ��  �@J�IsMouseOver . ��  � �� .  ./��� $ border1��# �   � ���� $ border1��# � 	  � ���� $ border��# � 
  � ���� $ border��# �   � ���� $ 	rectangle�� � y�$ Visible=�]�  ��  �@J�IsKeyboardFocusWithin . ��  � �� .  ./��� $ border1��# �   � ���� $ border1��# � 	  � ���� $ border��# � 
  � ���� $ border��# �   � ���� ScrollButtonStyle " ��.���  � ��8   ��  � ��# �  ��  �@�FocusVisualStyle# �  �� '��       " ��.���  �@+�IsFocusScope .  ��  � �� �� "  ��.R�� $
 Grid��#��z�� . .+��� $! PART_ScrollContentPresenter��#��z��#��z��#��z��# z�� . �� #��Z  |�.  $
��Left=�$��13��$
��Auto��$��Stretch=�8�� ���� ! �� *TemplatedParent+y�`� $" Visibility.Collapsed��8#  $$ 0��.1��� $+ %ComputedHorizontalScrollBarVisibility�! �� *TemplatedParent+�� $ HorizontalOffset�! �� *TemplatedParent+�� $ ExtentWidth�! �� *TemplatedParent+�� $ ViewportWidth�! �� *TemplatedParent+.��F� 
���   �$
% Fill=�$��0��$��Center=�$��0q�$��Center=�$��3��$��5��"����        �@B @B �@B ����	��Z� �� #��Z& |�.  $��Right=�$��13��$
��Auto��$��Stretch=�8�� ���� ! �� *TemplatedParent+y�`� $" Visibility.Collapsed��8#  $	$ 100��.1��� $+ %ComputedHorizontalScrollBarVisibility�! �� *TemplatedParent+�� $ HorizontalOffset�! �� *TemplatedParent+�� $ ExtentWidth�! �� *TemplatedParent+�� $ ViewportWidth�! �� *TemplatedParent+.��F� 
���   �$
% Fill=�$��0��$��Center=�$��0q�$��Center=�$��3��$��5��"����@B     �    @B �    ����	��Z� ��  � �� . ��  � ��$
 Auto=���  � ��$ Disabled=�.R�� $ ContentGrid��$
��Auto��$	��Top=�' �� $��24 ��� $( rowDefinition��$��86 ��� $( rowDefinition1��#��z) .+��� $ border��$��Stretch=�$��0,-1,0,0q�$
��Auto��$	��0,1q�$��1��#���  ���� $ SelectedItem.BorderBrush�! �� *TemplatedParent+�� $��0,-1,0,0q�$��1��$��50��
���   �$��1��$
��Left=�$	��Top=�$��1��* �� $+ 1,0.5&�$, 0,0.5&�.��� $ 	#66000000��$ 0Q�� $ Transparent��$ 1Q��� $��0,-1,0,0q�$��1��$��50��
���   �$��1��$��Right=�$	��Top=�$��1��* �� $+ 0,0.5&�$, 1,0.5&�.��� $ 	#66000000��$ 0Q�� $ Transparent��$ 1Q��� $��Stretch=�$��1,0,1,1q�$��1��$��1��._��� $- mainContentControl��$��0,1,0,0q�|�.  .���� $. contentPresenter��$��SelectedContent��$��Stretch=�$��0q��� $ shadowBorder��$��0,-1,0,0q�$��Stretch=�$
��Auto��$��2��$��1��$��0,1,0,0q����� $ SelectedItem.BorderBrush�! �� *TemplatedParent+���� $ SelectedItem.BorderBrush�! �� *TemplatedParent+* �� $+ 0.5,1&�$, 0.5,0&�.��� $ Black��$ 0.003Q�� $ 	#00000000��$ 0.991Q�� $ 	#7F0A0A0A��$
 0.16Q�� $ 	#FF040404��$
 0.15Q��� $ shadowBorder_Copy��$��0,-1,0,0q�$��Stretch=�$
��Auto��$��2��$��1��$��0,1,0,0q�
���   �
���   �$	/ 0.3Q�* �� $+ 0.5,1&�$, 0.5,0&�.��� $ Black��$ 0.003Q�� $ 	#00000000��$ 0.991Q�� $ 	#7F0A0A0A��$
 0.16Q�� $ 	#FF040404��$
 0.15Q�� $ TabsGrid��$��Stretch=�$��0q�$
��Auto��$��1��0 �� $��* ��� $
��Auto �.+�]� ��. .+��� $	��Top=�$
��Menu��$
��Left=�  $1 scrollViewer��$��Right=�$
��Left=�$��0,-7,1,0q�$
��Auto��#���  $
��Auto=�$��Disabled=� . .��  $2 PART_TabsContainer��3 . $
��Auto��$��Stretch=�4 .  $��0q��� $5 toolbarPlaceholder��$
��Left=�$��1��$6 
Horizontal=�$��0,0,0,1q�.+�� $��22��$��22��.+��� $
 image��$>��8/Fluent;Component/Themes/Office2010/Images/RibbonPin.png��$
��None=�$y�	Collapsed=�   $7 PART_MinimizeButton��$��0q�$��22��$��22��$8 Small=�#���  9 �� $ IsMinimized�$ TwoWay=�! �� *TemplatedParent+y��� *
HasItems+8:  �� $5 PART_ToolbarPanel��$6 
Horizontal=�4 .  � $; 
PART_Popup��< . = . $��Slide=�$> -1��#��z? $@ 0��$��Custom=�A �� $ SelectedTabItem�! �� *TemplatedParent+$ OneWay=�.��� $
��Auto��$
��Auto��$
��0,20q�' �� $��86 ��� $��5 �.+��� $
��Auto��#���  $	��0,1q����� $ SelectedItem.BorderBrush�! �� *TemplatedParent+._��� $- contentControl��$��0q��� $��0,-1,0,0q�$��Bottom=�$
��Auto��$��1��$��1��$��0,1,0,0q�$��6������ $ SelectedItem.BorderBrush�! �� *TemplatedParent+���� $ SelectedItem.BorderBrush�! �� *TemplatedParent+* �� $+ 0.5,1&�$, 0.5,0&�.��� $ Black��$ 0.003Q�� $ 	#00000000��$ 0.991Q�� $ 	#7F0A0A0A��$
 0.16Q�� $ 	#FF040404��$
 0.15Q��� $��0q�$��Bottom=�$
��Auto��$��1��$��1��$��0,1,0,0q�$��6��
���   �
���   �$	/ 0.3Q�* �� $+ 0.5,1&�$, 0.5,0&�.��� $ Black��$ 0.003Q�� $ 	#00000000��$ 0.991Q�� $ 	#7F0A0A0A��$
 0.16Q�� $ 	#FF040404��$
 0.15Q� P�  �@ IsMinimized . . ��� $ rowDefinition�� � ��$ 0 ��� $ rowDefinition1�� � ��$ 0 ��� $ mainContentControl�� Z�  � ���� $ contentControl�� �� $B contentPresenter��$ OneWay=� � ���� $ border�� � y�$ 	Collapsed=��� $ shadowBorder�� � y�$ 	Collapsed=��� $ image�� � y�$ 	Collapsed=�]�  ��  �@ IsDropDownOpen . ��  �@ IsMinimized . ./��� $ image�� � y�$ Visible=��� $ PART_MinimizeButton�� �@J�Opacity$ 0Q�
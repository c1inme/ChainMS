   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source#    Fluent.ObjectToImageConverter   StringToImageConvert  RibbonComboButtonStyle �� 
TargetType   Fluent.ToggleButton �� Property $  RibbonComboButtonControlTemplate �� Value  DefaultFontSize  DefaultFontFamily  DefaultFontBrush  ControlStyleEmptyFocus �� 
TargetType ��Name �� CornerRadius  ButtonBorderBrush ��Name	 F�Name
 �� StrokeStartLineCap �� Stretch �� StrokeEndLineCap �� StrokeMiterLimit �� StrokeLineJoin �� StrokeDashCap J� IsHitTestVisible �� Triggers P� Property P� Value �� 
TargetName ! ButtonPressedOuterBorderBrush %	 !ButtonPressedOuterBackgroundBrush !
 ButtonPressedInnerBorderBrush ]� 
Conditions �� Property �� Value # ButtonHoverOuterBackgroundBrush  ButtonHoverOuterBorderBrush(  "System.Windows.Controls.GroupStyle  ComboBoxGroupStyle   ContainerStyle � ColumnDefinitions ��Name  GroupIconConverter `� 	Converter �� Path �� ElementName P� 
SourceName   Fluent.ComboBox  ComboboxControlTemplate � 	Resources  ScrollBarWhite  �� BasedOn  ScrollViewerStyle! ��Name" �� Orientation# ��Name$ �� RelativeSource% �� 	Converter& ��Name'   Header  TextBoxBorderBrush  TextBoxBackgroundBrush(   
InputWidth  TextBoxHoverBorderBrush)  Name*   IsDefinitive+ �� 	ClickMode!,   CanAddToQuickAccessToolBar- d� 	IsChecked. �� Mode/ �� SelectionBoxItem0 �� SelectionBoxItemTemplate1 ��Name2 �� TextWrapping3 �� 
IsReadOnly4 �� 
IsReadOnly5 �Name6 �� IsDropDownOpen7 � AllowsTransparency8 � PlacementTarget9 � 	Resources: J� SnapsToDevicePixels; � RowDefinitions< 
� RadiusX= 
� Center> 
� GradientOrigin? 
� RadiusY@ � ColorA � OffsetB �� EndPointC �� 
StartPointD � MappingMode ! ContextMenuBarBackgroundBrush  ContextMenuBarBorderBrush-  'System.Windows.Input.KeyboardNavigationE   DirectionalNavigationF ��NameG �NameH �� MaxDropDownHeightI ��Name  GalleryBorderBrush   Fluent.GalleryPanelJ  NameK   GroupByL   GroupByM K� IsItemsHostN   
ItemHeightO   
ItemHeightP   OrientationQ   ItemContainerGeneratorR J� RenderTransformOrigin # ContextMenuBarResizeBorderBrush & "ContextMenuBarResizeBackgoundBrushS n�NameT � Cursor & "ResizeVerticalThumbControlTemplate " ResizeBothThumbControlTemplateU �� ScaleXV �� ScaleY " TextBoxDisabledBackgroundBrush  TextBoxDisabledBorderBrush   Fluent.GrayscaleEffect  TextBoxHoverBackgroundBrushW X� PositiveInfinityX �� Triggers  EditableComboBoxItemStyle    NonEditableComboBoxItemStyle�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     Fluentclr-namespace:Fluent  8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  1System&clr-namespace:System;assembly=mscorlib    �� $\ Vpack://application:,,,/Fluent;component\Themes\Office2010\Controls\ScrollBarWhite.xaml?��� $U Opack://application:,,,/Fluent;component\Themes\Office2010\Controls\TextBox.xaml?��� $` Zpack://application:,,,/Fluent;component\Themes\Office2010\Controls\RibbonScrollViewer.xaml?�%A!  &	        &	      &	 �     &	     7  &	 �    7  7  7   7  '      7     �� "  .���  � ��# �  ��  � ��$ 66����  � ��$
 Auto����  � ��# �  ��  � ��# �  ��  � ��# �  ��  �@�FocusVisualStyle# �  �� "  .R��� $ border��$��1q�$ 0��
�������#���  $
��Auto��$��Stretch=�$��Stretch=�._�� $
��Auto��$��Right=�.+��� $ 	rectangle��$��0��$y�	Collapsed=�
�������F� $
	 path��$��Center=�$��Center=�"����        р�     �@B @B 	#���  $��3,0,3,0q�$
 Round=�$
 Fill=�$ Round=�#���  $��1��$ 10Q�$ Round=�$��5��$��4��$ Round=� .   P�  � �� . . ��� $ border��# �   � ���� $ border��# � 	  � ��P�  �@d�	IsChecked$
 TrueY�. ��� $ border��# �   � ���� $ border��# � 
  � ��P�  � {� .  . ��� $
 path�� �@J�Opacity$	 0.5Q�]�  ��  �@J�IsMouseOver . ��  � �� .  ��  �@d�	IsChecked$ FalseY�./��� $ 	rectangle��# �   � ���� $ border��# �   � ���� $ 	rectangle�� � y�$ Visible=�   �� " ��.���  � �� �� " ��.R��  �� $
��Auto ��� $��* �.+�� $��21�����	#FFF0F2F5$��2��$	��Top=�.+��� $	��7,0q�$��Center=�$
��Left=�$
��Bold"�$��NoWrap=�$��CharacterEllipsis=����� *Name+�� $ 	iconImage��$��0��$
��None=�$��4,21,4,0q�$	��Top=�$��Center=���`� 8   .1��� $ Items��� $ 
layoutRoot��F� $	 menuLine��$
 Fill=����	#FFE2E4E7$��Right=�$��1��$
��Auto������        �    @B 	$y�Visible=�$��0,21,0,0q���Z� �� $��1��$��0,21,0,0q� P� $ 	iconImage�� Z�  � ��. ��� $ menuLine�� � y�$ 	Collapsed=��� $ 	iconImage�� � y�$ 	Collapsed=��� "   �� �� '��       " ��8    �� '��       " ��8   .R��� $
��Auto�����	#00000000$��22��._�� $
��Auto��$
��Auto�����	#00000000 �� $
��Auto ��� .+��� $! 
stackPanel��$" 
Horizontal=�$��1��$��Stretch=�$��0,0,4,0q�.+��� $# 	iconImage��$��16��$��16��$��Center=�$��3,0,0,0q����� *Icon+$ �� *TemplatedParent+8%  �� $& 	textBlock��#��z' $
��Wrap=�$��Center=�$��5,0,0,0q��� $ PART_ContentBorder��$
��Auto��$��1,1,1,1q�#���  #���  $��1��$��1��#��z( $��Right=�._��  �� $��* ��� $��13 �.+��� $ border1��$��1��$��1��$��1,0,0,0q�#���  $y�	Collapsed=�#���    $) ToggleButton��* .  |�.  $+ Press=�$
��Auto��$��Stretch=�$��2��$��
0,-1,-1,-1q�#���  , .  - �� *IsDropDownOpen+$. TwoWay=�$ �� *TemplatedParent+�� $# PART_ContentSite��$��3q� .  #��z/ #��z0 #��z��$	��Top=�$
��Left=��� $ PART_SelectedImage��$��0q�$
��None=� .  $	��Top=�$
��Left=�$y�	Collapsed=�$
��Auto��$
��Auto���� $1 PART_EditableTextBox��|�. $��Stretch=�$��Stretch=�$2 NoWrap=�$��0,0,0,0q�#4 z3 #���  $y�Hidden=�� $5 
PART_Popup��$��Bottom=�#��z6 7 . |�.  $��Slide=�$��0,0,-4,0q�$	��Top=�$��2��8 �� $ PART_ContentBorder��9 �� �� '��       " ��8   .��� $��DropDown��: . $��0q�$	��Top=�; �� $��* ��� $��4 � �� $��* ��� $��4 �.+��� $
��Left=�$	��Top=�$��4��$��4��$��1��$��1��$��0��$��	-1,-1,0,0q���
� $< 0.984Q�$= 0.017,0.002&�$> 0.017,0.002&�$? 1Q�.��� $@ 	#3F000000��$A 0Q�� $@ 	#00000000��$A 1Q��� $��0��$
��Left=�$��-1,3,0,0q�$	��Top=�$��4��$��4��$��1��$��0����
� $= 
0.032,1.01&�$> 
0.032,1.01&�$< 0.984Q�$? 1Q�.��� $@ 	#3F000000��$A 0Q�� $@ 	#00000000��$A 1Q��� $��0��$
��Left=�$��3,-1,0,0q�$	��Top=�$��4��$��4��$��0��$��1����
� $= 0.996,0.027&�$> 0.996,0.027&�$< 0.984Q�$? 1Q�.��� $@ 	#3F000000��$A 0Q�� $@ 	#00000000��$A 1Q��� $��0��$��Stretch=�$��7,-1,1,0q�$	��Top=�$
��Auto��$��4��$��0��$��1������ $B 0.5,1&�$C 0.5,0&�$D RelativeToBoundingBox=�.��� $@ 	#3F000000��$A 0Q�� $@ 	#00000000��$A 1Q��� $��0��$
��Left=�$��-1,7,0,1q�$��Stretch=�$��4��$
��Auto��$��1��$��0������ $B 1,0.5&�$C 0,0.5&�$D RelativeToBoundingBox=�.��� $@ 	#3F000000��$A 0Q�� $@ 	#00000000��$A 1Q��� $ PART_DropDownBorder��$��0q�: . #���  #���  $��1q�$ 1��$��1��$��Stretch=�$	��Top=����� *ActualWidth+$ PART_ContentBorder��._�� $
��Auto��$
��Auto��$��Stretch=�$	��Top=�$E Cycle=�.+��� $��Stretch=�$
��Auto��$
��Auto��#���  $��1q�$ 1��� $��1q�$��Stretch=�$	��Top=�; �� $
��Auto ��� $��* ��� $
��Auto �.+��� $
 Fill=����	#FFE2E4E7$
��Left=�$��24,0,0,0q�$��1��$
��Auto��$��3��$y�Visible=�$��Stretch=���Z� �� $F upResizeContent��� $G PART_MenuPanel��$��1��#��zH $	��Top=�; �� $��* ��� $
��Auto �.+��� $I PART_ScrollViewer��|�.  #���  $��1q�: . $
E None=�$��Stretch=�$��0,0,0,1q�.��  $J PART_GalleryPanel��
�������#L zK M . $
E None=�#O zN $P Vertical=�Q �� *ItemContainerGenerator+$ �� *TemplatedParent+�� $# menuPresenter��$
��Menu��$��1��$
E None=�$	��Top=��� $F downResizeContent��$��2��.���� $ resizeBorder��$R 0.5, 0.5&�$��Stretch=�$	��Top=�$��11��#���  $��0,1,0,0q�$ 0,0,1,1��#���  ._�� .+�n� $S PART_ResizeVerticalThumb��$��Stretch=�$��	0,0,0,-90q�$
��Auto��$��10��$	��Top=�$T SizeNS��#���  n� $S PART_ResizeBothThumb��$��Right=�$��	0,0,0,-90q�$��10��$��10��$	��Top=�$T SizeNWSE��#���   P�  �@ ShowPopupOnTop . . ��� $ downResizeContent�� Z�  � ���� $ upResizeContent�� �� $ resizeBorder�� � ���� $ resizeBorder�� �@��CornerRadius$ 1,1,0,0���� $ PART_ResizeBothThumb�� �@�Cursor$ SizeNESW���� $ PART_ResizeBothThumb�� �@�LayoutTransform �� $U 1Q�$V -1Q��� $ PART_ResizeVerticalThumb�� �@�LayoutTransform �� $U 1Q�$V -1Q�P�  Z�  �@ Menu. ��� $ PART_ScrollViewer�� � ��$ 0q�P�  �@ 
ResizeMode$
 None=�. ��� $ resizeBorder�� � y�$ 	Collapsed=�P�  �@ 
ResizeMode$
 Both=�. ��� $ PART_ResizeVerticalThumb�� � y�$ 	Collapsed=��� $ PART_ResizeBothThumb�� � y�$ Visible=��� $ resizeBorder�� � y�$ Visible=�P�  �@ 
ResizeMode$ Vertical=�. ��� $ PART_ResizeBothThumb�� � y�$ 	Collapsed=��� $ PART_ResizeVerticalThumb�� � y�$ Visible=��� $ resizeBorder�� � y�$ Visible=�P�  Z�  �@ Icon. ��� $ 	iconImage�� � y�$ 	Collapsed=�P�  �@ 
InputWidth$	 NaNQ�. ��� $ PART_ContentBorder�� � ��$ Stretch=�P�  �@ Size$ Middle=�. ��� $ 	textBlock�� � y�$ 	Collapsed=�P�  �@ Size$ Small=�. ��� $ 
stackPanel�� � y�$ 	Collapsed=�P�  � {� .  . ��� $ PART_EditableTextBox�� � {� .  �� $ PART_ContentBorder��# �   � ���� $ PART_ContentBorder��# �   � ���� $ border1��# �   � ���� $ PART_EditableTextBox��# �   � ���� $ 	iconImage�� �@J�Opacity$	 0.5Q��� $ 	textBlock�� �@J�Opacity$	 0.5Q��� $ 	iconImage�� �@J�Effect   P�  �@J�IsMouseOver . . ��� $ border1�� � y�$ Visible=��� $ PART_ContentBorder��# �   � ���� $ PART_EditableTextBox��# �   � ���� $ border1��# �   � ���� $ PART_ContentBorder��# �   � ��P�  �@J�IsKeyboardFocusWithin . . ��� $ border1�� � y�$ Visible=��� $ PART_ContentBorder��# �   � ���� $ PART_EditableTextBox��# �   � ���� $ border1��# �   � ���� $ PART_ContentBorder��# �   � ��P�  �@��HasItems .  . ��� $ PART_DropDownBorder�� � ��$ 95��P�  �@��
IsGrouping . . ���  � �� .  P�  �@��
IsEditable . . ���  � �� .  �� $ PART_EditableTextBox�� � y�$ Visible=��� $ PART_ContentSite�� � y�$ Hidden=��� "  .���  � ��8   ��  � ��$ Visible=���  � ��$ Disabled=���  �@��MaxDropDownHeight# ZW ��  �@�FocusVisualStyle# �  ��  �@��
IsEditable . X ]�  ��  � �� Z� ��  �@��
IsEditable . ./���  � ��# �  ]�  ��  � �� Z� ��  �@��
IsEditable .  ./���  � ��# �   
   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source#    Fluent.ObjectToImageConverter   StringToImageConvert �� 
TargetType  GroupBoxSeparator �� Property �� Value �� 
TargetType   GroupBoxSeparatorBorderBrush $  GroupBoxSeparatorBackgroundBrush  RibbonGroupBoxStyle   Fluent.RibbonGroupBox ! RibbonGroupBoxControlTemplate  DefaultFontFamily  DefaultFontSize  DefaultFontBrush 	 ControlStyleEmptyFocus �� Triggers P� Property P� Value /
 +RibbonGroupBoxDropDownButtonControlTemplate " RibbonGroupBoxQuickAccessStyle	 ��Name
 �� CornerRadius  TransparentBrush ��Name 
� Center 
� GradientOrigin 
� RadiusY � Color � Offset ��Name �� Orientation ��Name J� SnapsToDevicePixels �� RelativeSource �� 	Converter   Fluent.TwoLineLabel  Name   Header   Text  TwoLineLabelStyle �Name   IsDropDownOpen � AllowsTransparency � ColumnDefinitions � RowDefinitions �� EndPoint  �� 
StartPoint! � MappingMode" 
� RadiusX  RibbonBackgoundBrush  RibbonTopBorderBrush# �Name$ �� ElementName  GroupBoxFontBrush% �� TextAlignment   Fluent.Button&  Name ' #DialogLauncherButtonControlTemplate'   LauncherIcon(   Icon)   LauncherText*   Header+ � ToolTip, �Name- K� IsItemsHost. � Orientation/ +� IsFocusScope0 �� Triggers1 �� 
TargetName ! ButtonPressedOuterBorderBrush % !ButtonPressedOuterBackgroundBrush % !ButtonPressedInnerBackgroundBrush ! ButtonPressedInnerBorderBrush   Fluent.GrayscaleEffect2 ]� 
Conditions3 �� Property4 �� Value  ButtonHoverInnerBorderBrush # ButtonHoverInnerBackgroundBrush # ButtonHoverOuterBackgroundBrush  ButtonHoverOuterBorderBrush5 � 	Resources  Key6 g� 	BeginTime7 �� 
TargetName8 �� TargetProperty9 O� KeyTime: O� Value; �� BasedOn  ScrollBarWhite  ScrollViewerStyle  GroupHoverBrush< J� Opacity  GroupHighlightBrush= ��Name>   HasGlyph?   HasTwoLines@ � 	StaysOpenA � PlacementTargetB �� Mode  GroupSeparatorBorderBrush !  GroupSeparatorBackgroundBrushC ��NameD N� EnterActionsE ��Name ! OnMouseEnter1F N� ExitActions " OnMouseLeave1G J� RenderTransformOriginH �� ScaleX�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     Fluentclr-namespace:Fluent  8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006    �� $\ Vpack://application:,,,/Fluent;component\Themes\Office2010\Controls\ScrollBarWhite.xaml?��� $` Zpack://application:,,,/Fluent;component\Themes\Office2010\Controls\RibbonScrollViewer.xaml?�%&'  &	        &	      &	 �     &	 a    &	
 K    7   7   &	 S    7  7  7  7   7 ! 7 " &	 �#       �� " ��.���  � �� �� " ��.R��� $��3��$��55��$��Center=�$��Center=�$��2,5,2,10q�$	��1,0q�#���  #���  �� "  .���  � ��# �  ��  � ��$
 Auto����  � ��$ 85����  � ��# �  ��  � ��# �  ��  � ��# �  ��  �@�FocusVisualStyle# � 	 ��  �@+�IsFocusScope .   P�  �@ State$ QuickAccess=�. ���  � ��# � 
 ��  � ��$ 22����  � ��$ 22����  � |� .  ��  � ��# �  ��  � ��# �  ��  � ��# �  ��  �@�FocusVisualStyle# � 	 ��  �@J�SnapsToDevicePixels .  �� "  .���  � ��# � 
 ��  � ��$ 22����  � ��$ 22����  � |� .  ��  � ��# �  ��  � ��# �  ��  � ��# �  ��  �@�FocusVisualStyle# � 	 ��  �@J�SnapsToDevicePixels .  �� "  .R�� $��22��.+��� $	 PART_ButtonBorder��$��1q�$
 2��#���  #���  $
��Auto��$
��Left=�$��Stretch=�._��� $	 border1��$
��Auto��#���  $��1q�$
 2��#���  ._�� $
��Auto��.+��� $ 	rectangle��$��0��$y�	Collapsed=���
� $ 0.503,1&�$ 0.503,1&�$ 0.202Q�.��� $ 	#7FFFFFFF��$ 0Q�� $ 1Q��� $ 
stackPanel��$
��Auto��$ Vertical=�.+��� $ 	iconImage��$��Center=�$��16��$��Center=�$��16��$��1,1,0,0q� . ���� *Icon+ �� *TemplatedParent+8     $ controlLabel��$y�	Collapsed=�# z #���  $��Center=�$��Center=�$��2,-2,2,2q�� $ 
PART_Popup��$��Bottom=�#��z  . |�.  $��Slide=�$��0,0,-4,0q�$	��Top=�$��2��.��� $
��Auto��$
��Auto��$
��0,20q� �� $��* ��� $��5 � �� $��* ��� $��5 �.+��� $��0��$��Stretch=�$��	11,-1,1,0q�$��Stretch=�$
��Auto��$��0��$��1��$��1����Z� ���� $ 0.5,1&�$  0.5,0&�$! RelativeToBoundingBox=�.��� $ 	#7F000000��$ 0Q�� $ 1Q��� $��0��$��Stretch=�$��	-1,11,0,1q�$��Stretch=�$
��Auto��$��1��$��0��$��1����Z� ���� $ 1.002,0&�$! RelativeToBoundingBox=�.��� $ 	#7F000000��$ 0Q�� $ 1Q��� $��0��$��Stretch=�$��	-1,-1,0,0q�$��Stretch=�$
��Auto��$��1��$��1��$��1����Z� ��
� $ -0.003,0.011&�$ -0.003,0.011&�$" 1.006Q�$ 1Q�.��� $ 	#7F000000��$ 0.002Q�� $ 1Q��� $��0��$��Stretch=�$��-1,5,0,0q�$	��Top=�$��6��$��1��$��0��$��1����Z� ��
� $ -0.01,1.004&�$ -0.01,1.004&�$" 1.006Q�$ 1Q�.��� $ 	#7F000000��$ 0.002Q�� $ 1Q��� $��0��$
��Left=�$��5,-1,0,0q�$��Stretch=�$
��Auto��$��0��$��1��$��1��$��6����Z� ��
� $ 1.003,-0.006&�$ 1.003,-0.006&�$" 1.006Q�$ 1Q�.��� $ 	#7F000000��$ 0.002Q�� $ 1Q��� $	 border��$
��Auto��$��1��#���  #���  $��1q�$
 1��._�� $# PART_ParentPanel��$��0,1,0,2q�$��2��$��85�� �� $��* ��� $��17 �.+�� $# PART_DownGrid��$��0,-2,2,2q�$��1��$��Bottom=�$��17������ *ActualWidth+$$ PART_UpPanel�� �� $��* ��� $
��Auto �.+��� $��Stretch=�$��Center=�#��z $��NoWrap=�$��1��#���  $	��2,0q�$��CharacterEllipsis=�$% Center=�  $& PART_DialogLauncherButton��$��0,0,1,1q�$��Stretch=�$��15��$��14��$��1��|�.  ��.  #���  $��Bottom=�#( z' #* z) ���� *LauncherCommand+ �� *TemplatedParent+���� *LauncherCommandTarget+ �� *TemplatedParent+���� *LauncherCommandParameter+ �� *TemplatedParent++ �� *LauncherToolTip+ �� *TemplatedParent+{��� *IsLauncherEnabled+ �� *TemplatedParent+� $, PART_UpPanel��$
��Left=�$��Stretch=�$
��Auto��$
��Auto��- . $��2,0,4,0q�$. Vertical=�/ .  0 P�  �@ State$ 	Collapsed=�. ��� $1 
stackPanel�� �@��Orientation$ 
Horizontal=��� $1 	iconImage�� �� *Icon+ �� *TemplatedParent+8   � ���� $1 controlLabel�� �@ HasTwoLines .  �� $1 controlLabel�� �@ Text$  ���� $1 	iconImage�� � ��$ 16���� $1 	iconImage�� � ��$ 16���� $1 	iconImage�� � ��$ 1,0,0,0q��� $1 controlLabel�� � ��$ 	-2,-1,2,1q��� $1 controlLabel�� � ��$ 10���� $1 controlLabel�� � ��$ Center=�P�  �@ IsDropDownOpen . . ��� $1 PART_ButtonBorder��# �   � ���� $1 PART_ButtonBorder��# �   � ���� $1 border1��# �   � ���� $1 border1��# �   � ���� $1 	rectangle�� � y�$ Visible=�P�  � {� .  . ��� $1 	iconImage�� �@J�Opacity$	 0.5Q��� $1 controlLabel�� �@J�Opacity$	 0.5Q��� $1 	iconImage�� �@J�Effect   ]� 2 �� 3 �@J�IsMouseOver4 . �� 3 �@ IsDropDownOpen4 .  ./��� $1 border1��# �   � ���� $1 border1��# �   � ���� $1 PART_ButtonBorder��# �   � ���� $1 PART_ButtonBorder��# �   � ���� $1 	rectangle�� � y�$ Visible=�]� 2 �� 3 �@J�	IsFocused4 . �� 3 �@ IsDropDownOpen4 .  ./��� $1 border1��# �   � ���� $1 border1��# �   � ���� $1 PART_ButtonBorder��# �   � ���� $1 PART_ButtonBorder��# �   � ���� $1 	rectangle�� � y�$ Visible=��� "  5 �� �� OnMouseEnter1 .��U� $6 00:00:00X�$7 	hoverGrid��$8 (UIElement.Opacity)�.\��� $9 00:00:01��$: 1Q��� OnMouseLeave1 .��U� $6 00:00:00X�$7 	hoverGrid��$8 (UIElement.Opacity)�.\��� $9 00:00:01��$: 0Q��� '��       " ��8;   �� '��       " ��8;  �� '��       " ��8;  .R�� $# grid2��$��Stretch=�.+�� ���	#00000000.+�� $# 	hoverGrid��#���  $��Stretch=�$��0,0,1,1q�$��Stretch=�$
��Auto��$
��Auto��}�. $< 0Q�$��2��.+��� $��0��$��Stretch=�$��Stretch=�$
��Auto��#���  ��Z� �� $= border1��|�.  .��� $# PART_ParentPanel��$��0,1,0,2q�$��2��$��85�� �� $��* ��� $��17 �.+�� $# PART_DownGrid��$��0,-2,2,2q�$��1��$��Bottom=�$��17������ *ActualWidth+$$ PART_UpPanel�� �� $��* ��� $
��Auto �.+��� $��Stretch=�$��Center=�#��z $��NoWrap=�$��1��#���  $	��2,0q�$��CharacterEllipsis=�$% Center=�  $& PART_DialogLauncherButton��$��0,0,1,1q�$��Stretch=�$��15��$��14��$��1��|�.  ��.  #���  $��Bottom=�#( z' #* z) ���� *LauncherCommand+ �� *TemplatedParent+���� *LauncherCommandTarget+ �� *TemplatedParent+���� *LauncherCommandParameter+ �� *TemplatedParent++ �� *LauncherToolTip+ �� *TemplatedParent+{��� *IsLauncherEnabled+ �� *TemplatedParent+� $, PART_UpPanel��$
��Left=�$��Stretch=�$
��Auto��$
��Auto��- . $��2,0,4,0q�$. Vertical=�/ .  � $# grid1��$��0q�$
��Auto��$
��Auto�����	#00000000$y�	Collapsed=�$��2��.+�  $��Center=�$	��Top=�# z $��5,38,5,0q�> . ? . #���  �� $��Center=�$	��Top=�$��31��$��31�����	#FFD0D4D9$��1q�$��5,3,8,0q�$
 3��._��� $ 	iconImage��$��16��$��16�� . ���� *Icon+ �� *TemplatedParent+8  � $ 
PART_Popup��$
��Auto��$
��Auto�� . @ . #��z A �� $$ grid1��$B OneWay=�.��� $
��Auto��$
��Auto��$
��0,20q� �� $��* ��� $��5 � �� $��* ��� $��5 �.+��� $��0��$��Stretch=�$��	11,-1,1,0q�$��Stretch=�$
��Auto��$��0��$��1��$��1����Z� ���� $ 0.5,1&�$  0.5,0&�$! RelativeToBoundingBox=�.��� $ 	#7F000000��$ 0Q�� $ 1Q��� $��0��$��Stretch=�$��	-1,11,0,1q�$��Stretch=�$
��Auto��$��1��$��0��$��1����Z� ���� $ 1.002,0&�$! RelativeToBoundingBox=�.��� $ 	#7F000000��$ 0Q�� $ 1Q��� $��0��$��Stretch=�$��	-1,-1,0,0q�$��Stretch=�$
��Auto��$��1��$��1��$��1����Z� ��
� $ -0.003,0.011&�$ -0.003,0.011&�$" 1.006Q�$ 1Q�.��� $ 	#7F000000��$ 0.002Q�� $ 1Q��� $��0��$��Stretch=�$��-1,5,0,0q�$	��Top=�$��6��$��1��$��0��$��1����Z� ��
� $ -0.01,1.004&�$ -0.01,1.004&�$" 1.006Q�$ 1Q�.��� $ 	#7F000000��$ 0.002Q�� $ 1Q��� $��0��$
��Left=�$��5,-1,0,0q�$��Stretch=�$
��Auto��$��0��$��1��$��1��$��6����Z� ��
� $ 1.003,-0.006&�$ 1.003,-0.006&�$" 1.006Q�$ 1Q�.��� $ 	#7F000000��$ 0.002Q�� $ 1Q��� $	 border��$
��Auto��$��1��#���  #���  $��1q�$
 1��._��� $= popupContent���� $	 	separator��#���  #���   $��Right=�$��Stretch=�$��3��$
��Auto��$��2��$	��1,0q��� $C PART_SnappedImage��$y�	Collapsed=�0 P�  �@ State$ 	Collapsed=�. ��� $1 grid1�� � y�$ Visible=��� $1 border1�� � y�$ 	Collapsed=��� $1 grid2�� � ��$
 Left=��� $1 border1�� Z�  � ���� $1 popupContent�� �� $$ PART_ParentPanel�� � ��P�  �@J�IsMouseOver . P�  �@ IsDropDownOpen . D �� $#E OnMouseEnter1_BeginStoryboard��8�� F �� $#E OnMouseLeave1_BeginStoryboard��8�� P�  �@ IsLauncherVisible .  . ��� $1 PART_DialogLauncherButton�� � y�$ 	Collapsed=��� "  .R��� $	 border��$��1q�$
 1,0,0,0��#���  #���  ._��� $	 border1��$
��Auto��$
��Auto��$��1q�$
 1,0,0,0��#���  #���  ._��� $C image��$
��Auto��$��Center=�$��Center=�$Y��Spack://application:,,,/Fluent;Component\Themes\Office2010\Images\DialogLauncher.png�� . $
��None=�$G 0.5,0.5&�z�V� .~��� �� �� T� 0 P�  � ��$ RightToLeft=�. ��� $1 image�� � z� V� .~��� $H -1Q��� �� T� P�  � �� . . ��� $1 border��# �   � ���� $1 border��# �   � ���� $1 border1��# �   � ���� $1 border1��# �   � ��]� 2 �� 3 �@J�IsMouseOver4 . �� 3 � ��4 .  ./��� $1 border��# �   � ���� $1 border1��# �   � ���� $1 border1��# �   � ���� $1 border��# �   � ��
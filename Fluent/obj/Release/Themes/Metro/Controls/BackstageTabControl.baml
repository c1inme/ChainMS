   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  1clr-namespace:Fluent.InternalFluent.Internal  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source#    Fluent.ObjectToImageConverter   StringToImageConvert " BackstageButtonControlTemplate �� 
TargetType   Fluent.Button ��Name �� CornerRadius �� Orientation ��Name J� SnapsToDevicePixels �� RelativeSource	 �� 	Converter
 ��Name   Header �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName �� Property �� Value P� Property P� Value   Fluent.GrayscaleEffect  BackstageButtonStyle �� 
TargetType  ControlStyleEmptyFocus �� AncestorType    Fluent.BackstageTabControl �� FallbackValue  BackstageFontBrush   Fluent.SeparatorTabItem " BackstageSeparatorTabItemStyle �� StrokeStartLineCap �� Stretch �� StrokeEndLineCap  RibbonTopBorderBrush �� StrokeMiterLimit �� StrokeLineJoin �� StrokeDashArray  BackstageControlTemplate � 	Resources �� BasedOn  ScrollBarWhite   Fluent.DropDownButton  	 DropDownButtonBackstageStyle   Fluent.ComboBox 
 ComboBoxBackstageStyle   Fluent.Spinner  SpinnerBackstageStyle  Key  �� TargetProperty! �� 
TargetName<  6System.Windows.Media.Animation.EasingThicknessKeyFrame" p� KeyTime# p� Value$   EasingFunction.	  (System.Windows.Media.Animation.CubicEase7
  1System.Windows.Media.Animation.EasingFunctionBase% 
  
EasingMode9  3System.Windows.Media.Animation.EasingDoubleKeyFrame& O� KeyTime' O� Value( �Name  BackstageBackgroundBrush) � ColumnDefinitions* � 	Resources  ButtonBackstageStyle   Fluent.MetroWindow+ � UseLayoutRounding, J� RenderTransformOrigin- F�Name
. �� Top/ �� Left0 A�Name.  (System.Windows.Interactivity.Interaction1   Triggers/  )System.Windows.Interactivity.EventTrigger2   	EventName3  -System.Windows.Interactivity.EventTriggerBase3   
SourceName.  (System.Windows.Interactivity.TriggerBase4   Actions#   Fluent.Internal.InvokeCommand   Fluent.RibbonCommands5   OpenBackstage6   Command7 �� Value8 �� Binding   Fluent.Backstage9 N� EnterActions: ��Name  OnIsOpenTrueStoryboard; N� ExitActions< �� BeginStoryboardName= P� 
SourceName  BackstageStyle  DefaultFontBrush  DefaultFontFamily  DefaultFontSize ! BackstagePanelBackgroundBrush�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     Fluentclr-namespace:Fluent  *Internalclr-namespace:Fluent.Internal  bInteractivityPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity    �� $\ Vpack://application:,,,/Fluent;component\Themes\Office2010\Controls\ScrollBarWhite.xaml?��� $_ Ypack://application:,,,/Fluent;component\Themes\Office2010\Controls\BackstageControls.xaml?�%  &	        &	      7   &	     7  &	 �    &	 {    7  7  7 	 7 
 7  7  7  7  &	 �    7     �� "  .R�� ���	#00000000.+��� $ border��$��0q�$ 1��$y�	Collapsed=�#��z����Z� ._��� $��0q�$ 1��$��1q����	#19000000���	#BFFFFFFF._��� ���	#7FFFFFFF$��1q�$ 1�����	#33FFFFFF�� $ 
Horizontal=�$��	16,0,15,0q�$
��Left=�$��Center=�.+��� $ 	iconImage��$
��Left=�$��16��$��Center=�$��16�� . ���� *Icon+ �� *TemplatedParent+8	   �� $
 controlLabel��#��z $��NoWrap=�$��Center=�$
��Left=�$��5,0,0,0q�$��CharacterEllipsis=� ]�  ��  �@J�IsMouseOver . ��  � {� . ./��� $ border�� � y�$ Visible=�P�  � {� .  . ��� $ 	iconImage�� �@J�Opacity$	 0.5Q��� $ controlLabel�� �@J�Opacity$	 0.5Q��� $ 	iconImage�� �@J�Effect   �� "  .���  �@�FocusVisualStyle# �  ��  � ��$ 26����  � ��$	 5,1q���  � ��8   ��  � �� �� *
Background+ �� *FindAncestor+"  $	 Red����  � ��# �  �� "  .���  � �� �� "  .R�� $��0,0,10,0q�.+��� $
 header��$	��5,5q�$
��Wrap=�#��z��$��Center=�$��14(�$
��Bold"����	#FF5E5E5E$y�Visible=�F� ����        �@B     	$��1��$��0q�$
 Flat=�$
 Fill=�$
 Flat=�#���  $��1��$ 10Q�$ Miter=�$��Bottom=�$	 2 2R���Z�  P�  � �� .  . ��� $ header�� � y�$ 	Collapsed=��� "   �� �� '        "  8   �� '��       " ��8  �� '        "  8  �� '        "  8  �� '        "  8  �� '        "  8  �� OnIsOpenTrueStoryboard .��r� $  (FrameworkElement.Margin)�$
! grid��.	�  $" 0��$# 	-30,0,0,0q�  $" 0:0:0.2��$# 0q�$ 	  $% 	EaseInOut=�U� $  (UIElement.Opacity)�$
! grid��.\�  $& 0��$' 0Q�  $& 0:0:0.2��$' 1Q�U� $V  P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�$! 
backbutton��.\�  $& 0:0:0.1��$	' -23Q�  $& 0:0:0.2��$' 0Q�.R�� $
( grid��$��0q�#���  ) �� $	��107��$
��Auto ��� $��* �.+�� $
��Auto��$��1��* �� �� '        "  8  .+��� $ PART_SelectedContentHost��$��Stretch=�$��Stretch=�$
��Auto��$��1��$��SelectedContent��� $��0q�$��Stretch=����� *RibbonThemeColor+ �� *FindAncestor+"  .+�� $( 
backbutton��$��32��$��32��+ .  $��	15,10,0,0q�$	��Top=�$
��Left=�$, 0.5,0.5&�z�V� .~��� �� �� T� .+�F� $- 	arrowpath��$��16��$��12��$. 10��$	/ 7.5��$ Uniform=�
�������a���� Q%��O� Q%����p\-�������@��� �8@����ا  6n� �8��� ������� �p\-��O� Q%��OA� $0 backellipse��
�������$��32��$/ 0��$��32��$��2��F� K����        �     H� H� H��     H� H� H� H� H�� H�    	
������ $��Stretch=�$��0q�$
 None=�$
��Auto��1   $2 MouseLeftButtonDown��$3 
backbutton��.4   #6 Z5 �� $��0,70,0,0q�$
��Auto�� �� $
7 True��8 �� *IsOpen+ �� "  9 �� $: IsOpenBeginStoryboard��8�� ; �� $< IsOpenBeginStoryboard��P� $= 
backbutton�� �@J�IsMouseOver . . ��� $ 	arrowpath�� �@J�Opacity$	 0.5Q��� $ backellipse�� �@J�Opacity$	 0.5Q��� "  .���  � ��# �  ��  � ��# �  ��  � ��# �  ��  � ��8   ��  �@�FocusVisualStyle# �  ��  � ��$ 	0,-24,0,0q���  � �� �� *
Background+ �� *FindAncestor+"  $	 Red����  �@ ItemsPanelBackground# �  
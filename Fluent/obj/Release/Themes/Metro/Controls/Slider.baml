   M S B A M L   `   `   `  ���� M  IFluent, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546fclr-namespace:FluentFluent  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   SliderButtonStyle  �� 
TargetType �� Property �� Value �� 
TargetType  SliderThumbStyle  SliderThumbControlTemplate �� Triggers P� Property P� Value �� 
TargetName �� Opacity	 �� Color
 �� RelativeSource �� AncestorType    Fluent.MetroWindow ]� 
Conditions �� Property �� Value  SliderRepeatButtonStyle % !SliderRepeatButtonControlTemplate  ZoomSliderStyle  ZoomSliderControlTemplate � ColumnDefinitions  SliderShadowBrush  SliderLightenBrush ��Name ]�Name ]� DecreaseRepeatButton ��Name ]� Thumb n�Name ]� IncreaseRepeatButton �� Stretch P� 
SourceName�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     8d2http://schemas.microsoft.com/expression/blend/2008  Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  Fluentclr-namespace:Fluent  %�
  &	        &	 �     7  &	 R    &	     &	 {    &	     7  &	 �    7   7  7   �� "  ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� 
������ �� "  n�.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$ 14����  � ��$ 14����  � ��8   �� " n�.R�� $��2.5,0.5q�.+��� $
��path��
�������$��3��$��11��._��� $��hoverborder�� P�  �@n�
IsDragging . . ��� $ hoverborder�� � �� �� $	 0.5Q�	 �� *RibbonThemeColor.Color+
 �� *FindAncestor+"   ]�  ��  �@J�IsMouseOver . ��  �@n�
IsDragging .  ./��� $ hoverborder�� � �� �� $	 0.5Q�	 �� *RibbonThemeColor.Color+
 �� *FindAncestor+"   �� "  ��.���  � ��$ 16����  � ��$ 16����  � ��
 �������  � ��# �  �� " ��.R�� .+��� $��0q�$��14��$��14��$��Center=�$��Center=� P�  � �� . P�  �@J�IsMouseOver . �� "  ��.���  � ��8   ��  � ��$	 133����  � ��$ 16����  �@�Maximum$	 500Q���  �@�Minimum$ 10Q���  �@�SmallChange$ 1Q���  �@�LargeChange$ 10Q���  �@�Value$	 100Q��� " ��.R�� $��16��$	��133�� �� $��16 ��� $��* ��� $��16 �.+��� $��1��$��0,-1,0,0q�#���  $��Center=�$��1��$��1���� $��1��$��0,1,0,0q�#���  $��Center=�$��1��$��1���� $��1��$��0,1,0,1q�#���  $��Center=�$��7��$��1��$��1��$��Center=��� $ 	rectangle��$��1��$��0q�$��1��$��0�����	#00000000��Z� ]� $ 
PART_Track��$��0��$��16��$��1��$��1�� �� $ 
leftButton��8��  $��Slider.DecreaseLarge�� n� $ thumb��8��  �� $ rightButton��8�� $��Slider.IncreaseLarge���� $ repeatButton��#���  $��Slider.DecreaseLarge��.��� $��0q�.+�F� ����        �@B     	$��Center=�$��0q�$
 Fill=�$��10��$��Center=�$��2��$��2��
���������Z� �� $ repeatButton_Copy��#���  $��2��$��Slider.IncreaseLarge��.��� $��0q�.+�F� ����        �    @B 	$��Center=�$��0q�$
 Fill=�$��2��$��Center=�$��10��$��2��
�������F� ����        �@B     	$��Center=�$��0q�$
 Fill=�$��10��$��Center=�$��2��$��2��
������� P� $ 
leftButton�� �@J�IsMouseOver . . ��� $ thumb�� � ��
 �  ��P� $ rightButton�� �@J�IsMouseOver . . ��� $ thumb�� � ��
 �  ��P� $ thumb�� �@J�IsMouseOver . . ��� $ thumb�� � ��
 �  ��